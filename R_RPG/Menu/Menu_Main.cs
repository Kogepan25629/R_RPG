using System;
using R_UILib;
using DxLibDLL;

namespace R_RPG.Menu_Scene
{
    static class Menu_Main
    {
        // このクラスの初期化ハンドル
        private static bool EnableInit = true;

        // ボタンのインスタンス
        private static RUI_Button Button1;
        private static RUI_Button Button2;
        private static RUI_Button Button3;
        private static RUI_Button Button4;

        // ボタンの初期化
        private static void MenuInit()
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
            Button3.SetString("ゲームを開始", FoD.TestFont);
            Button3.GrHandle = Graphic_Data.GraphicData["Black"];
            //Button4
            ButtonPositionY += ButtonHeight1 + DrawSpaceY;
            Button4.SetPoint(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1);
            Button4.Mode = 3;
            Button4.SetString("ゲームを終了", FoD.TestFont);
            Button4.GrHandle = Graphic_Data.GraphicData["Black"];

            if (Setting.Setting_EnableSystemLog == true) {
                Console.WriteLine("Menu Initialized");
            }
        }
        static public void Menu()
        {
            while (DX.ScreenFlip() == 0 && DX.ProcessMessage() == 0 && DX.ClearDrawScreen() == 0) {
                // ローカルウィンドウサイズの取得
                if (Setting.Setting_PossibleChangeWindowSize == true && Setting.Setting_ChangeWindowMode == true) {
                    DX.GetWindowSize(out GeD.Window_Width, out GeD.Window_Heigt);
                }

                // R_UILibのマウスのアップデート
                RUI.UptadeMouseState();

                // CGameEscMenuを開いたときに実行
                // CGameMenuのボタンを初期化
                if (EnableInit == true) {
                    MenuInit();
                    EnableInit = false;
                }

                // Draw_Menu
                int ButtonResult = MenuUI();
                if (ButtonResult == 3) {
                    if (Game_Scene.Game_Main.GameMain() == 1) {
                        return;
                    }
                }
                else if (ButtonResult == 4) {
                    return;
                }
            }
        }

        static byte MenuUI()
        {
            // ボタンの描画
            Button1.Show();
            Button2.Show();
            Button3.Show();
            Button4.Show();

            // ボタンのクリック判定
            if (Button1.LeftUpDetection() == true) {
                return 1;
            }
            if (Button2.LeftUpDetection() == true) {
                return 2;
            }
            if (Button3.LeftUpDetection() == true) {
                return 3;
            }
            if (Button4.LeftUpDetection() == true) {
                return 4;
            }
            return 0;
        }
    }
}
