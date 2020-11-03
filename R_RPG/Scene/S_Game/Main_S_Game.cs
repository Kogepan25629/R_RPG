using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_RPG.Scene.S_Game
{
    static class Main_S_Game
    {
        static public void MainSGame()
        {
            ////変数////
            //時間関係
            int FPS, FPSCounter;
            long NowTime, Time, FPSCheckTime;
            double DeltaTime;

            //FPS計測関係の初期化
            {
                Time = FPSCheckTime = DX.GetNowHiPerformanceCount();
                FPS = 0;
                FPSCounter = 0;
            }

            //テクスチャ読み込み
            Tile_Data.TileData();

            //Player_Data読み込み
            Player_Data_Load.PDL();                 //プレイヤーのデータを読み込む(Player_Dataに)
            Player_Data PD = new Player_Data();     //Player_Dataのインスタンス化

            ///////////////////////////////////////////
            //メインループ
            while (DX.ProcessMessage() == 0)
            {
                //時間関係
                NowTime = DX.GetNowHiPerformanceCount();
                DeltaTime = (NowTime - Time) / 1000000.0;
                Time = NowTime;

                //FPS計測
                FPSCounter++;
                if (NowTime - FPSCheckTime > 1000000)
                {
                    FPS = FPSCounter;
                    FPSCounter = 0;
                    FPSCheckTime = NowTime;
                }

                //プレイヤー操作
                CharacterControl.Player_Control(PD, DeltaTime);

                //描画
                Draw_S_Game.DrawSGame(PD,FPS);
            }
        }
    }
}
