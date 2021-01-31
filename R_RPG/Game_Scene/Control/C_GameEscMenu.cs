using System;
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

        // ボタンの初期化
        private static void CGameEscMenuInit()
        {
            // 変数の宣言と値の計算
            int ButtonWidth1 = (int)(GeD.Window_Width * 0.2);
            int ButtonHeight1 = (int)(GeD.Window_Heigt * 0.1);
            int DrawSpaceX = (int)(GeD.Window_Width * 0.05);
            int DrawSpaceY = (int)(GeD.Window_Heigt * 0.05);
            int ButtonPositionX = GeD.Window_Width / 2 - DrawSpaceX / 2 - ButtonWidth1;
            int ButtonPositionY = (int)(GeD.Window_Heigt * 0.2 + ButtonHeight1);
            // ボタンのインスタンスを生成
            Button1 = new RUI_Button();
            Button2 = new RUI_Button();
            Button3 = new RUI_Button();
            Button4 = new RUI_Button();
            // Button1
            Button1.SetPoint(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1);
            Button1.Mode = 3;
            Button1.SetString("設定", FoD.TestFont);
            Button1.GrHandle = Graphic_Data.GraphicData["Black"];
            // Button2
            ButtonPositionX = ButtonPositionX + DrawSpaceX + ButtonWidth1;
            Button2.SetPoint(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1);
            Button2.Mode = 3;
            Button2.SetString("実績", FoD.TestFont);
            Button2.GrHandle = Graphic_Data.GraphicData["Black"];
            // Button3
            ButtonPositionX = ButtonPositionX - DrawSpaceX - ButtonWidth1;
            ButtonPositionY += ButtonHeight1 + DrawSpaceY;
            ButtonWidth1 += ButtonWidth1 + DrawSpaceX;
            Button3.SetPoint(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1);
            Button3.Mode = 3;
            Button3.SetString("ゲームに戻る", FoD.TestFont);
            Button3.GrHandle = Graphic_Data.GraphicData["Black"];
            //Button4
            ButtonPositionY += ButtonHeight1 + DrawSpaceY;
            Button4.SetPoint(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1);
            Button4.Mode = 3;
            Button4.SetString("ゲームを終了", FoD.TestFont);
            Button4.GrHandle = Graphic_Data.GraphicData["Black"];
        }
        
        static public byte CGameEscMenu()
        {
            // CGameEscMenuを開いたときに実行
            // CGameMenuのボタンを初期化
            if (EnableInit == true) {
                CGameEscMenuInit();
                EnableInit = false;
            }

            // Draw_Esc_Menu
            int ButtonResult = Draw_Esc_Menu();

            // Escを押した時
            if((GeD.KeyState[DX.KEY_INPUT_ESCAPE] == 1 && GeD.KeyStateOld[DX.KEY_INPUT_ESCAPE] == 0) || ButtonResult == 2)
            {
                DX.SetDrawBright(255, 255, 255);    // 通常の明るさにする(明るさを戻す)
                Game_Main.GameControlHandle = "GameMain";    // 操作をGameMainに戻す
                EnableInit = true;
                return 0;
            }
            if (ButtonResult == 1) {
                return 1;
            }
            return 0;// 一時的
        }

        // Draw_Esc_Menu
        private static byte Draw_Esc_Menu()
        {
            try
            {
                DX.SetDrawBright(255, 255, 255);    // 明るさを戻しEscMenuを描画

                // ボタンの描画
                Button1.Show();
                Button2.Show();
                Button3.Show();
                Button4.Show();

                // ボタンのクリック判定
                if (Button1.LeftUpDetection() == true){
                    return 4;
                }
                if (Button2.LeftUpDetection() == true){
                    return 3;
                }
                if (Button3.LeftUpDetection() == true){
                    return 2;
                }
                if (Button4.LeftUpDetection() == true) {
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
