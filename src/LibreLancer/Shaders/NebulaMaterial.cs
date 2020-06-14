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
    
    public class NebulaMaterial
    {
        private static byte[] vertex_bytes = new byte[256] {
27, 4, 2, 32, 140, 195, 56, 134, 60, 240, 16, 69, 25, 158, 67, 105, 91, 33, 97, 100, 175, 65, 235, 202, 187, 37, 205, 157, 95, 104,
68, 125, 183, 3, 124, 121, 10, 22, 224, 96, 192, 227, 180, 58, 198, 164, 130, 45, 220, 212, 199, 98, 128, 75, 212, 243, 170, 155, 34, 120,
235, 87, 19, 210, 88, 68, 6, 99, 176, 66, 76, 220, 101, 241, 137, 239, 229, 249, 190, 252, 149, 215, 253, 112, 190, 60, 41, 135, 249, 129,
25, 245, 98, 212, 150, 177, 64, 109, 229, 165, 213, 42, 0, 111, 21, 67, 115, 206, 140, 171, 220, 154, 45, 230, 217, 166, 101, 51, 206, 207,
143, 235, 245, 243, 186, 32, 208, 82, 200, 102, 74, 43, 54, 219, 64, 147, 170, 228, 99, 231, 131, 24, 73, 146, 155, 90, 100, 186, 132, 157,
107, 142, 2, 214, 186, 231, 190, 54, 138, 180, 114, 217, 46, 63, 101, 12, 174, 2, 124, 211, 180, 138, 229, 178, 99, 138, 136, 255, 55, 229,
85, 207, 229, 132, 202, 19, 16, 197, 6, 50, 34, 69, 159, 184, 233, 161, 47, 196, 147, 213, 235, 239, 76, 82, 179, 241, 180, 183, 130, 232,
202, 19, 108, 178, 129, 100, 107, 226, 113, 11, 81, 98, 132, 208, 139, 174, 183, 146, 109, 32, 73, 253, 9, 27, 11, 227, 184, 226, 188, 94,
43, 84, 59, 51, 126, 233, 243, 141, 248, 219, 99, 131, 201, 143, 40, 27
};
        private static byte[] fragment_bytes = new byte[169] {
27, 16, 1, 0, 140, 196, 56, 134, 242, 16, 101, 240, 38, 234, 145, 169, 178, 52, 22, 172, 43, 223, 150, 52, 241, 164, 38, 191, 208, 141,
125, 200, 108, 39, 58, 196, 41, 255, 109, 163, 221, 100, 22, 118, 221, 133, 73, 34, 105, 128, 145, 6, 214, 93, 122, 170, 79, 220, 52, 26,
21, 59, 158, 57, 50, 94, 80, 14, 147, 170, 83, 116, 199, 19, 235, 36, 47, 134, 196, 17, 249, 181, 232, 51, 94, 171, 133, 141, 203, 243,
49, 121, 244, 89, 92, 46, 188, 244, 44, 29, 176, 196, 132, 200, 252, 226, 98, 205, 179, 205, 228, 21, 119, 209, 241, 194, 141, 46, 207, 7,
14, 54, 5, 116, 239, 18, 187, 124, 81, 228, 133, 28, 193, 204, 118, 142, 252, 97, 94, 60, 162, 179, 192, 79, 40, 15, 137, 254, 36, 176,
87, 4, 16, 213, 246, 39, 175, 196, 158, 85, 213, 113, 150, 36, 200, 163, 226, 193, 2
};
        static ShaderVariables[] variants;
        private static bool iscompiled = false;
        private static int GetIndex(ShaderFeatures features)
        {
            ShaderFeatures masked = (features & ((ShaderFeatures)(8)));
            if ((masked == ((ShaderFeatures)(8))))
            {
                return 1;
            }
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
            ShaderVariables.Log("Compiling NebulaMaterial");
            string vertsrc;
            string fragsrc;
            vertsrc = ShCompHelper.FromArray(vertex_bytes);
            fragsrc = ShCompHelper.FromArray(fragment_bytes);
            variants = new ShaderVariables[2];
            variants[0] = ShaderVariables.Compile(vertsrc, fragsrc, "");
            variants[1] = ShaderVariables.Compile(vertsrc, fragsrc, "\n#define VERTEX_DIFFUSE\n#line 1\n");
        }
    }
}