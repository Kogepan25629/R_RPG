using System;
using DxLibDLL;

using static R_RPG.Setting;

namespace R_RPG.Game_Scene
{
    class Game_Draw_Main
    {
        // カラー
        static uint Color_White = DX.GetColor(255, 255, 255);

        // メイン描画処理ライン
        static public void GameDrawMain(Player_Data PD,int FPS, bool Enable_Draw_String)
        {
            // 画像描画
            {
                // マップ描画
                Draw_Map(PD.Player_X, PD.Player_Y, PD.Player_Dimension, Map_Data.map_data);

                // プレイヤー描画
                {
                    DX.DrawExtendGraph(GeD.Window_Width / 2 - (Setting_TileSize / 2), GeD.Window_Heigt / 2 - (Setting_TileSize / 2), GeD.Window_Width / 2 + Setting_TileSize - (Setting_TileSize / 2), GeD.Window_Heigt / 2 + Setting_TileSize - (Setting_TileSize / 2), Tile_Data.TileGraphicData[3], DX.TRUE);
                }
            }

            // 文字列描画(座標,FPS値等)
            if(Enable_Draw_String == true){
                Draw_String(PD, FPS);
            }

        }



        // Draw_Map
        static void Draw_Map(double Player_X, double Player_Y, int Player_Dimension, int[][][][] map_data)
        {
            // ローカル関数の宣言
            int INT_Player_X, INT_Player_Y;
            int Tile_Number_X, Tile_Number_Y;
            int Player_X_Map, Player_Y_Map;
            int PlayerX_O, PlayerY_O;
            double PlayerX_O_B, PlayerY_O_B;
            int Player_X_Map_D, Player_Y_Map_D;

            // 整数のプレイヤー座標　プレイヤーのいるマス
            INT_Player_X = (int)Player_X;
            INT_Player_Y = (int)Player_Y;

            // 描画するTile数
            Tile_Number_X = GeD.Window_Width / Setting_TileSize + 1;
            Tile_Number_Y = GeD.Window_Heigt / Setting_TileSize + 1;
            /*
            if (Tile_Number_X % 2 == 1)
            {
                Tile_Number_X++;
            }
            if (Tile_Number_Y % 2 == 1)
            {
                Tile_Number_Y++;
            }
            */


            // Tile位置
            Player_X_Map = INT_Player_X - (Tile_Number_X / 2);
            Player_Y_Map = INT_Player_Y - (Tile_Number_Y / 2);

            // 1マス単位でのずれを計算
            if (Player_X - INT_Player_X > 0 && Player_X - INT_Player_X >= 1 / Setting_TileSize){
                PlayerX_O_B = (Player_X - INT_Player_X) / (1.0 / Setting_TileSize);
            }
            else{
                PlayerX_O_B = 0.0;
            }
            if (Player_Y - INT_Player_Y > 0){
                PlayerY_O_B = (Player_Y - INT_Player_Y) / (1.0 / Setting_TileSize);
            }
            else{
                PlayerY_O_B = 0.0;
            }
            // 何ドットずれるか
            PlayerX_O = (int)PlayerX_O_B;
            PlayerY_O = (int)PlayerY_O_B;

            // 2レイヤー分の描画
            for (int i=0;i < 2;i++) {
                Player_X_Map_D = Player_X_Map;
                Player_Y_Map_D = Player_Y_Map;
                for (int y = 0; y <= Tile_Number_Y; y++) {
                    if (Player_Y_Map_D >= 0 && Player_Y_Map_D < map_data[Player_Dimension][i].GetLength(0)) {
                        Player_X_Map_D = Player_X_Map;
                        int DrawY = ((GeD.Window_Heigt / 2 - (Setting_TileSize / 2)) - (Setting_TileSize * (Tile_Number_Y / 2))) + (Setting_TileSize * y - PlayerY_O);
                        for (int x = 0; x <= Tile_Number_X; x++) {
                            int DrawX = ((GeD.Window_Width / 2 - (Setting_TileSize / 2)) - (Setting_TileSize * (Tile_Number_X / 2))) + (Setting_TileSize * x - PlayerX_O);
                            if (Player_X_Map_D >= 0 && Player_X_Map_D < map_data[Player_Dimension][i][0].GetLength(0)) {
                                DX.DrawExtendGraph(DrawX, DrawY, DrawX + Setting_TileSize, DrawY + Setting_TileSize, Tile_Data.TileGraphicData[map_data[Player_Dimension][i][Player_Y_Map_D][Player_X_Map_D]], DX.TRUE/*0=false 1=true*/);
                            }
                            Player_X_Map_D += 1;
                        }
                    }
                    Player_Y_Map_D += 1;
                }
            }
        }

        //Draw_String
        static void Draw_String(Player_Data PD, int FPS)
        {
            // 文字列の描画
            DX.ChangeFontType(DX.DX_FONTTYPE_ANTIALIASING_4X4);
            DX.DrawString(0, 2, "X座標 : " + PD.Player_X.ToString(), Color_White);
            DX.DrawString(0, 20, "Y座標 : " + PD.Player_Y.ToString(), Color_White);
            DX.DrawString(0, 38, "ディメンション : " + PD.Player_Dimension.ToString(), Color_White);
            DX.DrawString(0, 56, "SPEED : " + PD.Player_Now_Speed.ToString(), Color_White);
            DX.DrawString(0, 74, "FPS : " + FPS.ToString(), Color_White);
            DX.DrawString(0, 92, "衝突 : " + CharacterControl.Collision.ToString(), Color_White);

            //Console.WriteLine("X座標 : " + PD.Player_X.ToString());
            //Console.WriteLine("Y座標 : " + PD.Player_Y.ToString());
            //Console.WriteLine("衝突 : " + CharacterControl.Collision.ToString());

            //Console.WriteLine((int)(25+0.99999999999999));
        }
    }
}
