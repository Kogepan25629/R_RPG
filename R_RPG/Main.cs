﻿using System;
using DxLibDLL;

namespace R_RPG
{
    //変数
    class Program
    {
        //[STAThread]

        /**/
        static void Main()
        {
            //Dxlibの初期化
            Dxlib_Init.Dxlib_Initialization();

            //Menu画面
            Menu_Scene.Menu_Main.Menu();

            //Dxlib終了処理
            DX.DxLib_End();

            //DxLibMain.Main_Test();
        }
        /*/
        static void Main()
        {
            Class2.class2();
        }
        /**/
    }
}

