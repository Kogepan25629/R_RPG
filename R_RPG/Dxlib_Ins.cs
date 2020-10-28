using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
//クラス
using static R_RPG.Setting;

namespace R_RPG
{
    static class Dxlib_Ins
    {
        //ウィンドウサイズを格納する変数
        static public int Window_Width, Window_Heigt;
        static public void Dxlib_Initialization()
        {
            //Dxlib　前処理
            {
                //垂直同期
                DX.SetWaitVSyncFlag(DX.TRUE);

                // Window or FullScreen
                if (Setting_ChangeWindowMode == false)
                {//フルスクリーン

                    int tekitou;
                    DX.GetDefaultState(out Window_Width, out Window_Heigt, out tekitou, out tekitou, out tekitou, out tekitou, out tekitou, out tekitou, out tekitou, out tekitou);
                    DX.SetGraphMode(Window_Width, Window_Heigt, 32);
                    DX.SetFullScreenResolutionMode(DX.DX_FSRESOLUTIONMODE_DESKTOP);
                    DX.ChangeWindowMode(DX.FALSE);
                    //Window_Width = 1920; Window_Heigt = 1080;
                }
                else
                {//ウィンドウモード

                    DX.SetWindowStyleMode(9);

                    DX.SetGraphMode(Setting_Window_Width, Setting_Window_Heigt, 32);
                    DX.ChangeWindowMode(DX.TRUE);
                    Window_Width = Setting_Window_Width; Window_Heigt = Setting_Window_Heigt;

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
        }
    }
}
