﻿/* The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 * 
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
 * License for the specific language governing rights and limitations
 * under the License.
 * 
 * 
 * The Initial Developer of the Original Code is Callum McGing (mailto:callum.mcging@gmail.com).
 * Portions created by the Initial Developer are Copyright (C) 2013-2016
 * the Initial Developer. All Rights Reserved.
 */
using System;
using System.Runtime.InteropServices;
using System.Reflection;

namespace LibreLancer
{
    static partial class SSEMath
    {
        public static bool IsAccelerated = false;
        public static void Load()
        {
            //Eliminate 32-bit
            if (IntPtr.Size != 8)
            {
                FLLog.Info("SSE", "SSE Math Disabled: Reason - Not x86-64");
                return;
            }
            //Eliminate ARM
            PortableExecutableKinds peKind;
            ImageFileMachine machine;
            typeof(object).Module.GetPEKind(out peKind, out machine);
            if (machine != ImageFileMachine.AMD64 && machine != ImageFileMachine.I386)
            {
                FLLog.Info("SSE", "SSE Math Disabled: Reason - Not x86-64");
                return;
            }
            var mytype = typeof(SSEMath);
            foreach (var field in mytype.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                AsmMethodAttribute a = null;
                foreach (var attr in field.GetCustomAttributes())
                {
                    if (attr is AsmMethodAttribute)
                        a = (AsmMethodAttribute)attr;
                }
                if (a != null)
                {
                    Delegate func = null;
                    if (IsUnix)
                        func = GetFunction(field.FieldType, a.UnixName);
                    else
                        func = GetFunction(field.FieldType, a.WindowsName);
                    field.SetValue(null, func);
                }
            }
            FLLog.Info("SSE", "SSE Math Enabled");
            IsAccelerated = true;
        }

        static Delegate GetFunction(Type t, string name)
        {
            var mytype = typeof(SSEMath);
            var field = mytype.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (field == null)
                throw new Exception("Implementation missing: " + name);
            else
            {
                var bytes = (byte[])field.GetValue(null);
                if (IsUnix)
                    return GetFunctionUnix(bytes, t);
                else
                    return GetFunctionWindows(bytes, t);
            }
        }
        [DllImport("libc", SetLastError = true)]
        static extern IntPtr mmap(IntPtr addr, IntPtr length, int prot, int flags, int fd, int offset);

        const int MAP_SHARED = 0x01;
        const int MAP_ANONYMOUS = 0x1000;
        const int PROT_READ = 0x1;
        const int PROT_WRITE = 0x2;
        const int PROT_EXEC = 0x4;
        static Delegate GetFunctionUnix(byte[] code, Type type)
        {
            IntPtr func = mmap(IntPtr.Zero, (IntPtr)code.Length, PROT_READ | PROT_WRITE | PROT_EXEC, MAP_SHARED | MAP_ANONYMOUS, -1, 0);
            Marshal.Copy(code, 0, func, code.Length);
            var del = (Delegate)(object)Marshal.GetDelegateForFunctionPointer(func, type);
            return del;
        }
        [DllImport("kernel32.dll")]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_EXECUTE_READWRITE = 0x40;
        static Delegate GetFunctionWindows(byte[] code, Type type)
        {
            IntPtr func = VirtualAlloc(IntPtr.Zero, (IntPtr)code.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
            Marshal.Copy(code, 0, func, code.Length);
            var del = (Delegate)(object)Marshal.GetDelegateForFunctionPointer(func, type);
            return del;
        }
        static bool IsUnix
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Unix;
            }
        }
    }
}