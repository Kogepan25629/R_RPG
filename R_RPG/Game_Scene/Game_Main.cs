using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

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
            Tile_Data.TileData();

            //Player_Data読み込み
            Player_Data_Load.PDL();                 //プレイヤーのデータを読み込む(Player_Dataに)
            Player_Data PD = new Player_Data();     //Player_Dataのインスタンス化

            //コントロール
            GameControlHandle = "GameMain";

            ///////////////////////////////////////////
            //メインループ
            while (DX.ProcessMessage() == 0)
            {
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

                //操作
                if (GameControlHandle == "GameMain")
                {
                    CharacterControl.Player_Control(PD, ElapsedTime1F);
                }
                else if (GameControlHandle == "GameEscMenu")
                {
                    Game_Draw.Draw_Esc_Menu();
                }

                //描画
                Game_Draw.GameDrawMain(PD,FPS);
            }
        }
    }
}
