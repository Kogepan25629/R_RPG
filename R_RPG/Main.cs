using System;
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
            // Dxlibの初期化
            Init.Dxlib_Init.Dxlib_Initialization();

            // Pre初期化
            Init.Pre_Init.PreInit();


            // Post初期化
            Init.Post_Init.PostInit();

            // Menu画面
            Menu_Scene.Menu_Main.Menu();
                //Console.ReadLine();

            // Dxlib終了処理
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

