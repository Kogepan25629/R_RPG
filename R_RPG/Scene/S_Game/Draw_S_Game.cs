using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

using static R_RPG.Dxlib_Ins;//Window_Width,Window_Width
using static R_RPG.Setting;

namespace R_RPG.Scene.S_Game
{
    class Draw_S_Game
    {
        //カラー
        static uint Color_White = DX.GetColor(255, 255, 255);

        //メイン描画処理ライン
        static public void DrawSGame(Player_Data PD,int FPS)
        {
            //Map ID 判別読み込み
            byte[,] Map_0 = Map_Data.Map_0_Load(PD.Player_Dimension);   //レイヤー0
            byte[,] Map_1 = Map_Data.Map_1_Load(PD.Player_Dimension);   //レイヤー1

            // 画面を消す
            DX.ClearDrawScreen();

            //画像描画
            {
                //マップ描画
                Draw_Map(PD.Player_X, PD.Player_Y, Map_0, Map_1);

                //プレイヤー描画
                {
                    DX.DrawExtendGraph(Window_Width / 2 - (Setting_TileSize / 2), Window_Heigt / 2 - (Setting_TileSize / 2), Window_Width / 2 + Setting_TileSize - (Setting_TileSize / 2), Window_Heigt / 2 + Setting_TileSize - (Setting_TileSize / 2), Tile_Data.TextureData[3], DX.TRUE);
                }
            }

            //文字列描画(座標,FPS値等)
            {
                Draw_String(PD, FPS);
            }

            //裏画面を表画面と交換
            DX.ScreenFlip();
        }



        //Draw_Map
        static void Draw_Map(double Player_X, double Player_Y, byte[,] Map_0, byte[,] Map_1)
        {
            //ローカル関数の宣言
            int INT_Player_X, INT_Player_Y;
            int Tile_Number_X, Tile_Number_Y;
            int Player_X_Map, Player_Y_Map;
            int PlayerX_O, PlayerY_O;
            double PlayerX_O_B, PlayerY_O_B;
            int Player_X_Map_D, Player_Y_Map_D;

            //
            INT_Player_X = (int)Player_X;
            INT_Player_Y = (int)Player_Y;

            //描画するTile数
            Tile_Number_X = Window_Width / Setting_TileSize + 1;
            Tile_Number_Y = Window_Heigt / Setting_TileSize + 1;
            if (Tile_Number_X % 2 == 0)
            {
                Tile_Number_X++;
            }
            if (Tile_Number_Y % 2 == 0)
            {
                Tile_Number_Y++;
            }


            //Tile位置
            Player_X_Map = INT_Player_X / 1 - (Tile_Number_X / 2);
            Player_Y_Map = INT_Player_Y / 1 - (Tile_Number_Y / 2);

            if (Player_X - INT_Player_X > 0 && Player_X - INT_Player_X >= 1 / Setting_TileSize)
            {
                PlayerX_O_B = (Player_X - INT_Player_X) / (1.0 / Setting_TileSize);
            }
            else
            {
                PlayerX_O_B = 0.0;
            }
            if (Player_Y - INT_Player_Y > 0)
            {
                PlayerY_O_B = (Player_Y - INT_Player_Y) / (1.0 / Setting_TileSize);
            }
            else
            {
                PlayerY_O_B = 0.0;
            }
            PlayerX_O = (int)PlayerX_O_B;
            PlayerY_O = (int)PlayerY_O_B;



            //Map_0  レイヤー0
            Player_X_Map_D = Player_X_Map;
            Player_Y_Map_D = Player_Y_Map;
            for (int y = 0; y <= Tile_Number_Y; y++)
            {
                if (Player_Y_Map_D >= 0 && Player_Y_Map_D < Map_0.GetLength(0))
                {
                    Player_X_Map_D = Player_X_Map;
                    int DrawY = ((Window_Heigt / 2 - (Setting_TileSize / 2)) - (Setting_TileSize * (Tile_Number_Y / 2))) + (Setting_TileSize * y - PlayerY_O);
                    for (int x = 0; x <= Tile_Number_X; x++)
                    {
                        int DrawX = ((Window_Width / 2 - (Setting_TileSize / 2)) - (Setting_TileSize * (Tile_Number_X / 2))) + (Setting_TileSize * x - PlayerX_O);
                        if (Player_X_Map_D >= 0 && Player_X_Map_D < Map_0.GetLength(1))
                        {
                            DX.DrawExtendGraph(DrawX, DrawY, DrawX + Setting_TileSize, DrawY + Setting_TileSize, Tile_Data.TextureData[Map_0[Player_Y_Map_D, Player_X_Map_D]], DX.FALSE);
                        }
                        Player_X_Map_D += 1;
                    }
                }
                Player_Y_Map_D += 1;
            }

            //Map_1  レイヤー1
            Player_X_Map_D = Player_X_Map;
            Player_Y_Map_D = Player_Y_Map;
            for (int y = 0; y <= Tile_Number_Y; y++)
            {
                if (Player_Y_Map_D >= 0 && Player_Y_Map_D < Map_1.GetLength(0))
                {
                    Player_X_Map_D = Player_X_Map;
                    int DrawY = ((Window_Heigt / 2 - (Setting_TileSize / 2)) - (Setting_TileSize * (Tile_Number_Y / 2))) + (Setting_TileSize * y - PlayerY_O);
                    for (int x = 0; x <= Tile_Number_X; x++)
                    {
                        int DrawX = ((Window_Width / 2 - (Setting_TileSize / 2)) - (Setting_TileSize * (Tile_Number_X / 2))) + (Setting_TileSize * x - PlayerX_O);
                        if (Player_X_Map_D >= 0 && Player_X_Map_D < Map_1.GetLength(1))
                        {
                            DX.DrawExtendGraph(DrawX, DrawY, DrawX + Setting_TileSize, DrawY + Setting_TileSize, Tile_Data.TextureData[Map_1[Player_Y_Map_D, Player_X_Map_D]], DX.TRUE);
                        }
                        Player_X_Map_D += 1;
                    }
                }
                Player_Y_Map_D += 1;
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

            Console.WriteLine("X座標 : " + PD.Player_X.ToString());
            Console.WriteLine("Y座標 : " + PD.Player_Y.ToString());
            Console.WriteLine("衝突 : " + CharacterControl.Collision.ToString());
        }

    }
}
