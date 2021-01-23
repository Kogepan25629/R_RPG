using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using R_UILib;

namespace R_RPG.Game_Scene
{
    static class Game_Main
    {
        //
        static public string GameControlHandle;
        static public void GameMain()
        {
            ////変数////
            //時間関係
            int FPS, FPSCounter;
            long NowTime, LastTime, FPSCheckTime;//現在の時間, 前回の時間, FPS計算用時間
            double ElapsedTime1F; //1フレーム経過にかかった時間(秒)

            //FPS計測関係の初期化
            {
                LastTime = FPSCheckTime = DX.GetNowHiPerformanceCount();
                FPS = 0;
                FPSCounter = 0;
            }

            //テクスチャ読み込み
            Tile_Data.TileData();   //
            Graphic_Data.Graphic_Data_Load();

            //Player_Data読み込み
            Player_Data_Load.PDL();                 //プレイヤーのデータを読み込む(Player_Dataに)
            Player_Data PD = new Player_Data();     //Player_Dataのインスタンス化

            //フォント読み込み
            FoD.Load_Font();

            //コントロール
            GameControlHandle = "GameMain";

            ///////////////////////////////////////////
            //メインループ
            while (DX.ScreenFlip() == 0 && DX.ProcessMessage() == 0 && DX.ClearDrawScreen() == 0)
            {
                // ローカルウィンドウサイズの取得
                if (Setting.Setting_PossibleChangeWindowSize == true && Setting.Setting_ChangeWindowMode == true) {
                    DX.GetWindowSize(out GeD.Window_Width, out GeD.Window_Heigt);
                }

                // RUI マウス情報取得
                RUI.UptadeMouseState();

                //時間関係
                NowTime = DX.GetNowHiPerformanceCount();    //現在の時間を取得
                ElapsedTime1F = (NowTime - LastTime) / 1000000.0;   //1フレーム経過にかかった時間(秒)
                LastTime = NowTime;

                //FPS計測
                FPSCounter++;
                if (NowTime - FPSCheckTime > 1000000)
                {
                    FPS = FPSCounter;
                    FPSCounter = 0;
                    FPSCheckTime = NowTime;
                }

                // キーボード押下状態の読み込み
                Array.Copy(Key_State.KeyState, Key_State.KeyStateOld, 256);
                DX.GetHitKeyStateAll(Key_State.KeyState);

                // Handle
                if (GameControlHandle == "GameMain")
                {
                    //描画
                    Game_Draw_Main.GameDrawMain(PD, FPS, true);
                    //操作
                    CharacterControl.Player_Control(PD, ElapsedTime1F);
                }
                else if (GameControlHandle == "GameEscMenu")
                {
                    //描画
                    Game_Draw_Main.GameDrawMain(PD, FPS, false);
                    //操作
                    if (C_GameEscMenu.CGameEscMenu() == 1)
                    {
                        return;
                    }
                }


            }
            if (DX.ProcessMessage() == -1)
            {
                // 主にウィンドウが閉じられたとき実行される
                // データのセーブ等を実行

                //Console.WriteLine("WindowExit");
                //Console.ReadLine();

                return;
            }
        }
    }
}
