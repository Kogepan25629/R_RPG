using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using R_UILib;

namespace R_RPG
{
    class Class2
    {
        //ウィンドウサイズを格納する変数
        static public int Window_Width, Window_Heigt;
        static public void class2()
        {
            //Dxlib　前処理
            {
                //垂直同期
                DX.SetWaitVSyncFlag(DX.TRUE);

                {//ウィンドウモード

                    DX.SetWindowStyleMode(9);

                    Window_Width = 854; Window_Heigt = 480;
                    DX.SetGraphMode(Window_Width, Window_Heigt, 32);
                    DX.ChangeWindowMode(DX.TRUE);

                    // window のサイズ変更の可不可
                    DX.SetWindowSizeChangeEnableFlag(DX.FALSE);
                }
            }

            //DxLib初期化
            if (DX.DxLib_Init() == -1)
            {
                return;
            }

            //描写を裏画面に指定
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            DX.SetMouseDispFlag(DX.TRUE);

            int FontHandle = DX.CreateFontToHandle(null, -1, -1);

            int grhandle = DX.LoadGraph("Texture\\diamond_block.png");

            RUI_ButtonImageString Button1 = new RUI_ButtonImageString();

            while (DX.ScreenFlip() == 0 && DX.ProcessMessage() == 0 && DX.ClearDrawScreen() == 0)
            {
                RUI.UptadeMouseState();

                if (Button1.Show(0, 0, /*200, 100, */"おはよう", FontHandle, DX.GetColor(255,255,255), grhandle) == true)
                {
                    Console.WriteLine("Clicked");
                }
            }
        }

    }
}
