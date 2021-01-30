using System;
using DxLibDLL;
//クラス
using static R_RPG.Setting;

namespace R_RPG
{
    static class Dxlib_Init
    {
        //ウィンドウサイズを格納する変数

        static public void Dxlib_Initialization()
        {
            //Dxlib　前処理
            {
                int tekitou;
                int ColorBitDepth;

                DX.GetDefaultState(out GeD.Window_Width, out GeD.Window_Heigt, out ColorBitDepth, out tekitou, out tekitou, out tekitou, out tekitou, out tekitou, out tekitou, out tekitou);
                DX.SetGraphMode(GeD.Window_Width, GeD.Window_Heigt, ColorBitDepth);

                //垂直同期
                DX.SetWaitVSyncFlag(DX.TRUE);

                // Window or FullScreen
                if (Setting_ChangeWindowMode == false)
                {//フルスクリーン

                    DX.SetFullScreenResolutionMode(DX.DX_FSRESOLUTIONMODE_DESKTOP);
                    DX.ChangeWindowMode(DX.FALSE);
                    //Window_Width = 1920; Window_Heigt = 1080;
                }
                else
                {//ウィンドウモード

                    DX.SetWindowStyleMode(9);
                    GeD.Window_Width = Setting_Window_Width;
                    GeD.Window_Heigt = Setting_Window_Heigt;
                    //ウィンドウサイズの変更の可不可
                    if (Setting_PossibleChangeWindowSize == true)
                    {//変更可
                        DX.SetWindowSize(GeD.Window_Width, GeD.Window_Heigt);

                        // window のサイズ変更の可不可
                        DX.SetWindowSizeChangeEnableFlag(DX.TRUE, DX.FALSE);
                    }
                    else
                    {//変更不可
                        DX.SetGraphMode(GeD.Window_Width, GeD.Window_Heigt, ColorBitDepth);
                        DX.SetWindowSizeChangeEnableFlag(DX.FALSE);
                    }
                    DX.ChangeWindowMode(DX.TRUE);
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
        }
    }
}
