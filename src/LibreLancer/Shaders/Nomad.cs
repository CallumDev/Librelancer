﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibreLancer.Shaders
{
    using System;
    
    public class Nomad
    {
        private static byte[] vertex_bytes = new byte[286] {
27, 141, 2, 0, 196, 194, 57, 117, 223, 168, 27, 39, 5, 118, 25, 145, 34, 112, 220, 242, 136, 7, 206, 169, 107, 225, 129, 104, 183, 101,
224, 128, 162, 57, 127, 163, 211, 1, 207, 112, 219, 150, 176, 201, 130, 96, 61, 230, 210, 211, 52, 22, 236, 147, 156, 27, 198, 71, 44, 58,
147, 89, 103, 181, 158, 246, 27, 237, 106, 82, 78, 233, 7, 88, 20, 157, 240, 242, 246, 14, 94, 222, 237, 225, 125, 214, 26, 187, 162, 147,
198, 103, 47, 204, 38, 206, 242, 241, 188, 121, 214, 145, 210, 249, 249, 40, 4, 67, 54, 124, 127, 62, 223, 14, 12, 61, 29, 12, 120, 96,
161, 80, 74, 218, 56, 110, 31, 238, 199, 221, 173, 120, 87, 84, 50, 47, 195, 196, 204, 84, 202, 93, 145, 22, 15, 50, 96, 127, 192, 56,
253, 73, 122, 121, 123, 23, 151, 243, 29, 21, 48, 121, 27, 178, 45, 129, 51, 164, 228, 30, 27, 250, 87, 21, 150, 110, 114, 65, 82, 144,
174, 127, 158, 136, 21, 60, 87, 16, 246, 113, 84, 212, 19, 0, 120, 32, 29, 150, 144, 50, 50, 79, 22, 42, 47, 41, 159, 56, 79, 86,
66, 152, 140, 49, 193, 60, 80, 193, 182, 7, 222, 221, 99, 11, 155, 198, 128, 84, 110, 67, 146, 74, 58, 64, 5, 185, 137, 162, 159, 199,
200, 113, 24, 84, 223, 16, 105, 65, 67, 53, 27, 11, 211, 44, 80, 129, 166, 198, 89, 24, 21, 23, 146, 68, 30, 131, 78, 114, 233, 72,
160, 54, 55, 23, 44, 253, 115, 191, 133, 134, 78, 233, 251, 77, 20, 9
};
        private static byte[] fragment_bytes = new byte[244] {
27, 211, 1, 32, 196, 36, 221, 102, 178, 76, 135, 255, 87, 124, 67, 76, 214, 226, 146, 162, 19, 215, 212, 111, 126, 196, 87, 140, 42, 203,
94, 219, 223, 192, 70, 19, 156, 248, 172, 212, 27, 47, 69, 36, 188, 191, 241, 22, 149, 26, 221, 197, 7, 168, 14, 11, 181, 78, 165, 59,
246, 71, 245, 66, 185, 50, 164, 40, 234, 111, 216, 180, 191, 224, 181, 94, 58, 184, 6, 236, 243, 250, 179, 188, 94, 31, 171, 148, 227, 201,
69, 87, 128, 105, 74, 215, 128, 171, 226, 1, 152, 216, 188, 20, 81, 203, 186, 30, 149, 151, 54, 98, 133, 61, 231, 195, 135, 83, 70, 153,
71, 211, 251, 126, 236, 124, 4, 211, 236, 178, 223, 109, 162, 55, 130, 230, 229, 157, 144, 244, 39, 2, 128, 205, 233, 58, 231, 239, 241, 97,
6, 98, 117, 101, 193, 196, 171, 253, 111, 45, 166, 82, 203, 151, 93, 41, 161, 194, 54, 44, 13, 55, 225, 76, 41, 2, 0, 222, 44, 79,
243, 243, 77, 164, 86, 179, 12, 75, 91, 154, 164, 40, 180, 139, 246, 194, 200, 128, 218, 42, 120, 172, 69, 124, 138, 198, 28, 143, 50, 36,
128, 33, 102, 55, 48, 222, 179, 76, 53, 82, 104, 27, 47, 52, 30, 22, 183, 198, 99, 187, 128, 138, 11, 235, 174, 6, 115, 60, 135, 82,
148, 249, 205, 2
};
        static ShaderVariables[] variants;
        private static bool iscompiled = false;
        private static int GetIndex(ShaderFeatures features)
        {
            ShaderFeatures masked = (features & ((ShaderFeatures)(0)));
            return 0;
        }
        public static ShaderVariables Get(ShaderFeatures features)
        {
            return variants[GetIndex(features)];
        }
        public static ShaderVariables Get()
        {
            return variants[0];
        }
        public static void Compile()
        {
            if (iscompiled)
            {
                return;
            }
            iscompiled = true;
            ShaderVariables.Log("Compiling Nomad");
            string vertsrc;
            string fragsrc;
            vertsrc = ShCompHelper.FromArray(vertex_bytes);
            fragsrc = ShCompHelper.FromArray(fragment_bytes);
            variants = new ShaderVariables[1];
            variants[0] = ShaderVariables.Compile(vertsrc, fragsrc, "");
        }
    }
}
