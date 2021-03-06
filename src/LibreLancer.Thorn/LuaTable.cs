﻿// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace LibreLancer.Thorn
{
	public class LuaTable
	{
		public int Capacity;
		bool isArray = false;
		object[] arrayStorage;
		Dictionary<string,object> mapStorage;
        private int arrayCount = 0;

        bool SetAny() => (arrayStorage != null || mapStorage != null);
		public LuaTable (int capacity)
		{
			Capacity = capacity;
		}
		public LuaTable(object[] array)
		{
			Capacity = array.Length;
			SetArray (0, array);
		}

		public void SetArray(int offset, object[] stuff)
		{
			if (offset == 1 && stuff.Length == 36) //TODO: What is this
				throw new Exception ();
			isArray = true;
			if (arrayStorage == null)
				arrayStorage = new object[offset + stuff.Length];
			if (arrayStorage.Length < offset + stuff.Length)
				Array.Resize (ref arrayStorage, offset + stuff.Length);
			for (int i = 0; i < stuff.Length; i++) {
				arrayStorage [i + offset] = stuff [i];
			}
        }

        public int Count
        {
            get
            {
                if (isArray) return arrayStorage.Length;
                return mapStorage.Count;
            }
        }

		public void SetMap(Dictionary<string,object> stuff)
		{
			isArray = false;
			mapStorage = stuff;
		}

		public Vector3 ToVector3()
		{
			if (!isArray)
				throw new InvalidCastException ();
			if (arrayStorage.Length != 3)
				throw new InvalidCastException ();
			return new Vector3 ((float)this [0], (float)this [1], (float)this [2]);
		}

		public static explicit operator Vector3(LuaTable lt)
		{
			return lt.ToVector3 ();
		}

		public object this[object indexer] {
			get {
                if(!SetAny()) throw new InvalidOperationException();
				if (isArray) {
					return arrayStorage [(int)indexer];
				} else {
					return mapStorage [(string)indexer];
				}
			}
            set
            {
                if(!SetAny()) throw new InvalidOperationException();
                if (isArray)
                    arrayStorage[(int)indexer] = value;
                else
                    mapStorage[(string)indexer] = value;
            }
        }
        public bool ContainsKey(string key) => mapStorage.ContainsKey(key);
		public bool TryGetValue(string key, out object value)
		{
            if (!SetAny())
            {
                value = null;
                return false;
            }
			return mapStorage.TryGetValue(key, out value);
		}
		public bool TryGetVector3(string key, out Vector3 val)
		{
			object o;
			val = Vector3.Zero;
			if (mapStorage.TryGetValue(key, out o))
			{
				if (o is LuaTable)
				{
					val = ((LuaTable)o).ToVector3();
					return true;
				}
				if (o is Vector3)
				{
					val = (Vector3)o;
					return true;
				}
				return false;
			}
			return false;
		}
        public bool TryGetVector3(int idx, out Vector3 val)
        {
            if (arrayStorage == null)
                return TryGetVector3(idx.ToString(), out val);
            if (arrayStorage[idx] is LuaTable l)
            {
                val = l.ToVector3();
                return true;
            }
            if (arrayStorage[idx] is Vector3 v)
            {
                val = v;
                return true;
            }
            val = Vector3.Zero;
            return false;
        }
        public static Dictionary<string, string> EnumReverse;
        static string FNice(float f) => f.ToString("0.##########################");
        static string Rev(string s)
        {
            string tmp;
            if (EnumReverse.TryGetValue(s, out tmp)) return tmp;
            return s;
        }
        string ToStr(object o, string tabs)
		{
            if (o is string)
                return "\"" + o.ToString() + "\"";
            else if (o is LuaTable)
                return ((LuaTable)o).ToStringTab(tabs);
            else if (o is float)
                return FNice((float)o);
            else if (o is bool)
            {
                if ((bool)o) return "Y";
                else return "N";
            }
            else if (o is Vector3)
            {
                var v = (Vector3)o;
                return string.Format("{{ {0}, {1}, {2} }}", FNice(v.X), FNice(v.Y), FNice(v.Z));
            }
            else if(o.GetType().IsEnum)
            {
                var t = o.GetType();
                var full = Convert.ToUInt32(o);

                foreach (var v in Enum.GetValues(t))
                {
                    if (full == Convert.ToUInt32(v)) return Rev(o.ToString());
                }
                var sb = new StringBuilder();
                int count = 0;
                foreach(var fl in Enum.GetValues(t)) {
                    var a = Convert.ToUInt32(fl);
                    if (a == 0) continue;
                    if((full & a) == a)
                    {
                        if (count == 0) sb.Append(Rev(fl.ToString()));
                        else sb.Append(" + ").Append(Rev(fl.ToString()));
                        count++;
                    }
                }
                return sb.ToString();
            }
            return o.ToString ();
		}
		public override string ToString ()
		{
            if (Capacity == 0) return "{}";
            return ToStringTab("");
		}
        internal string ToStringTab(string tabs)
        {
            if (Capacity == 0) return tabs + "{}";
            var builder = new StringBuilder();
            if (isArray)
            {
                builder.Append("{");
                for (int i = 0; i < arrayStorage.Length; i++)
                {
                    var item = arrayStorage[i];
                    if (item == null)
                        break;
                    builder.Append(ToStr(item, tabs));
                    builder.Append(",");
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append("}");
            }
            else
            {
                builder.AppendLine("{");
                foreach (var k in mapStorage.Keys)
                {
                    builder.Append(tabs).Append("    ").Append(k.ToString()).Append(" = ").Append(ToStr(mapStorage[k],tabs + "    ")).AppendLine(",");
                }
                builder.Remove(builder.Length - 2, 2);
                builder.AppendLine();
                builder.Append(tabs).AppendLine("}").Append(tabs);
            }
            return builder.ToString();
        }
    }
}

