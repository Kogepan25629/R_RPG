using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using R_UILib;

namespace R_RPG.Game_Scene
{
    class C_GameEscMenu
    {
        // このクラスの初期化ハンドル
        private static bool EnableInit = true;

        // ボタンのインスタンス
        private static RUI_Button Button1;
        private static RUI_Button Button2;
        private static RUI_Button Button3;
        private static RUI_Button Button4;

        // このクラスの初期化
        private static void CGameEscMenuInit()
        {
            int ButtonWidth1 = (int)(GeD.Window_Width * 0.2);
            int ButtonHeight1 = (int)(GeD.Window_Heigt * 0.1);
            int DrawSpaceX = (int)(GeD.Window_Width * 0.05);
            int DrawSpaceY = (int)(GeD.Window_Heigt * 0.05);
            int ButtonPositionX = GeD.Window_Width / 2 - DrawSpaceX / 2 - ButtonWidth1;
            int ButtonPositionY = (int)(GeD.Window_Heigt * 0.2 + ButtonHeight1);

            Button1 = new RUI_Button();
            Button2 = new RUI_Button();
            Button3 = new RUI_Button();
            Button4 = new RUI_Button();

            Button1.SetPoint(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1);
            Button1.Mode = 3;
            Button1.SetString("設定", FoD.TestFont);
            Button1.GrHandle = Graphic_Data.GraphicData["Black"];

            ButtonPositionX = ButtonPositionX + DrawSpaceX + ButtonWidth1;
            Button2.SetPoint(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1);
            Button2.Mode = 3;
            Button2.SetString("実績", FoD.TestFont);
            Button2.GrHandle = Graphic_Data.GraphicData["Black"];

            ButtonPositionX = ButtonPositionX - DrawSpaceX - ButtonWidth1;
            ButtonPositionY += ButtonHeight1 + DrawSpaceY;
            ButtonWidth1 += ButtonWidth1 + DrawSpaceX;
            Button3.SetPoint(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1);
            Button3.Mode = 3;
            Button3.SetString("ゲームに戻る", FoD.TestFont);
            Button3.GrHandle = Graphic_Data.GraphicData["Black"];
        }
        
        static public byte CGameEscMenu()
        {
            // CGameEscMenuを開いたときに実行
            // CGameMenuのボタンを初期化
            if (EnableInit == true) {
                CGameEscMenuInit();
                EnableInit = false;
            }

            //Draw_Esc_Menu
            int ButtonResult = Draw_Esc_Menu();

            //Escを押した時
            if((Key_State.KeyState[DX.KEY_INPUT_ESCAPE] == 1 && Key_State.KeyStateOld[DX.KEY_INPUT_ESCAPE] == 0) || ButtonResult == 1)
            {
                DX.SetDrawBright(255, 255, 255);    // 通常の明るさにする(明るさを戻す)
                Game_Main.GameControlHandle = "GameMain";    // 操作をGameMainに戻す
                EnableInit = true;
                return 0;
            }
            return 0;// 一時的
        }

        //Draw_Esc_Menu
        private static byte Draw_Esc_Menu()
        {
            try
            {
                DX.SetDrawBright(255, 255, 255);    // 明るさを戻しEscMenuを描画

                // ボタンの描画
                Button1.Show();
                Button2.Show();
                Button3.Show();

                //ボタンのクリック判定
                if (Button1.LeftUpDetection() == true)
                {
                    return 2;
                }
                if (Button2.LeftUpDetection() == true)
                {
                    return 3;
                }
                if (Button3.LeftUpDetection() == true)
                {
                    return 1;
                }
                return 0;
            }
            finally
            {
                DX.SetDrawBright(100, 100, 100);    // 少し暗くして
            }
        }

    }
}
