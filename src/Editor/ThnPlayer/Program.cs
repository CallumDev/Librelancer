// MIT License - Copyright (c) Callum McGing
// This file is subject to the terms and conditions defined in
// LICENSE, which is part of this source code package

using System;
using LibreLancer;

namespace ThnPlayer
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            MainWindow mw = null;
            AppHandler.Run(() =>
            {
                mw = new MainWindow();
                mw.Run();
            }, () => mw.Crashed());
        }
    }
}