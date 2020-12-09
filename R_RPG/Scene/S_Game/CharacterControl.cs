using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_RPG.Scene.S_Game
{
    class CharacterControl
    {
        static public bool Collision = false;
        static bool disable_collision = false;

        static public void Player_Control(Player_Data PD, double ElapsedTime1F)
        {
            if(Scene_Control.S_Control_N == 1)
            {
                //変数宣言
                double Player_X, Player_Y, Player_Speed, Player_Now_Speed;
                double tmp_Player_X, tmp_Player_Y;
                int Player_Dimension;
                bool Collision1 = false;
                bool Collision2 = false;
                bool Collision3 = false;

                // キーボード押下状態を保存するバッファの宣言
                byte[] keyStateBuf = new byte[256];

                //Player_Dataの内容を代入
                tmp_Player_X = Player_X = PD.Player_X; //tmp = 移動前座標
                tmp_Player_Y = Player_Y = PD.Player_Y;
                Player_Dimension = PD.Player_Dimension;
                Player_Speed = PD.Player_Speed;

                //Map ID 判別読み込み
                /*
                byte[,] Map_0 = Map_Data.Map_0_Load(PD.Player_Dimension);   //レイヤー0
                byte[,] Map_1 = Map_Data.Map_1_Load(PD.Player_Dimension);   //レイヤー1
                */

                // キーボード押下状態の読み込み
                DX.GetHitKeyStateAll(keyStateBuf);

                //操作 Math.Floor()
                {
                    //左シフト
                    switch (keyStateBuf[DX.KEY_INPUT_LSHIFT])
                    {
                        case 1:
                            Player_Now_Speed = Player_Speed * 3;
                            break;
                        default:
                            Player_Now_Speed = Player_Speed;
                            break;
                    }
                    //W
                    switch (keyStateBuf[DX.KEY_INPUT_W])
                    {
                        case 1:
                            Player_Y -= Player_Now_Speed * ElapsedTime1F;
                            break;
                        default:
                            break;
                    }
                    //S
                    switch (keyStateBuf[DX.KEY_INPUT_S])
                    {
                        case 1:
                            Player_Y += Player_Now_Speed * ElapsedTime1F;
                            break;
                        default:
                            break;
                    }
                    //A
                    switch (keyStateBuf[DX.KEY_INPUT_A])
                    {
                        case 1:
                            Player_X -= Player_Now_Speed * ElapsedTime1F;
                            break;
                        default:
                            break;
                    }
                    //D
                    switch (keyStateBuf[DX.KEY_INPUT_D])
                    {
                        case 1:
                            Player_X += Player_Now_Speed * ElapsedTime1F;
                            break;
                        default:
                            break;
                    }

                    //マウスホイール
                    Player_Dimension += DX.GetMouseWheelRotVol();
                    if (Player_Dimension < 0)
                    {
                        Player_Dimension = 0;
                    }
                    else if (Player_Dimension > 1)
                    {
                        Player_Dimension = 1;
                    }
                }

                //値修正
                {
                    //Xがマップ左端に衝突した場合
                    if (Player_X <= 0)
                    {
                        Player_X = 0;
                    }
                    //Xがマップ右端に衝突した場合
                    if (Player_X >= (Map_Data.map_data[Player_Dimension][0][0].GetLength(0) - 1))
                    {
                        Player_X = (Map_Data.map_data[Player_Dimension][0][0].GetLength(0) - 1);
                    }
                    //Yがマップ上端に衝突した場合
                    if (Player_Y < 0)
                    {
                        Player_Y = 0;
                    }
                    //Yがマップ下端に衝突した場合
                    if (Player_Y >= (Map_Data.map_data[Player_Dimension][0].GetLength(0) - 1))
                    {
                        Player_Y = (Map_Data.map_data[Player_Dimension][0].GetLength(0) - 1);
                    }


                    //X(tmp)がマップ右端に衝突した場合
                    if (tmp_Player_X >= (Map_Data.map_data[Player_Dimension][0][0].GetLength(0) - 1))
                    {
                        tmp_Player_X = (Map_Data.map_data[Player_Dimension][0][0].GetLength(0) - 1);
                    }
                    //Y(tmp)がマップ下端に衝突した場合
                    if (tmp_Player_Y >= (Map_Data.map_data[Player_Dimension][0].GetLength(0) - 1))
                    {
                        tmp_Player_Y = (Map_Data.map_data[Player_Dimension][0].GetLength(0) - 1);
                    }
                }


                ////当たり判定の処理
                {
                    //衝突したかどうか
                    //Xだけで判定 y == tmp_Player_Y
                    if (Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)tmp_Player_Y][(int)Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(tmp_Player_Y + 0.99999999999999)][(int)Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)tmp_Player_Y][(int)(Player_X + 0.99999999999999)]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(tmp_Player_Y + 0.99999999999999)][(int)(Player_X + 0.99999999999999)]] == true)
                    {
                        Collision1 = true;
                    }else if (Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)tmp_Player_Y][(int)Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(tmp_Player_Y + 0.99999999999999)][(int)Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)tmp_Player_Y][(int)(Player_X + 0.99999999999999)]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(tmp_Player_Y + 0.99999999999999)][(int)(Player_X + 0.99999999999999)]] == true)
                    {
                        Collision1 = true;
                    }

                    //Yだけで判定 x == tmp_Player_X
                    if (Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)Player_Y][(int)tmp_Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(Player_Y + 0.99999999999999)][(int)tmp_Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)Player_Y][(int)(tmp_Player_X + 0.99999999999999)]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(Player_Y + 0.99999999999999)][(int)(tmp_Player_X + 0.99999999999999)]] == true)
                    {
                        Collision2 = true;
                    }
                    else if (Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)Player_Y][(int)tmp_Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(Player_Y + 0.99999999999999)][(int)tmp_Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)Player_Y][(int)(tmp_Player_X + 0.99999999999999)]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(Player_Y + 0.99999999999999)][(int)(tmp_Player_X + 0.99999999999999)]] == true)
                    {
                        Collision2 = true;
                    }

                    //X, Yで判定
                    if ((Collision1 == false && Collision2 == false) && (Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)Player_Y][(int)Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(Player_Y + 0.99999999999999)][(int)Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)Player_Y][(int)(Player_X + 0.99999999999999)]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(Player_Y + 0.99999999999999)][(int)(Player_X + 0.99999999999999)]] == true))
                    {
                        Collision3 = true;
                    }
                    else if (((Collision1 == false && Collision2 == false) || (Collision1 == false && Collision2 == false)) && (Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)Player_Y][(int)Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(Player_Y + 0.99999999999999)][(int)Player_X]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)Player_Y][(int)(Player_X + 0.99999999999999)]] == true || Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(Player_Y + 0.99999999999999)][(int)(Player_X + 0.99999999999999)]] == true))
                    {
                        Collision3 = true;
                    }

                    //上3つのどれかでtrueになったとき
                    Collision = false;
                    if(Collision1 == true || Collision2 == true || Collision3 == true)
                    {
                        Collision = true;
                    }


                    if ((Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)Player_Y][(int)Player_X]] == true && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(Player_Y + 0.99999999999999)][(int)Player_X]] == true && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)Player_Y][(int)(Player_X + 0.99999999999999)]] == true && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(Player_Y + 0.99999999999999)][(int)(Player_X + 0.99999999999999)]] == true) || (
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)Player_Y][(int)Player_X]] == true && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(Player_Y + 0.99999999999999)][(int)Player_X]] == true && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)Player_Y][(int)(Player_X + 0.99999999999999)]] == true && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(Player_Y + 0.99999999999999)][(int)(Player_X + 0.99999999999999)]] == true))
                    {
                        disable_collision = true;
                    }
                    if((disable_collision == true) && ((
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)Player_Y][(int)Player_X]] == false && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(Player_Y + 0.99999999999999)][(int)Player_X]] == false && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)Player_Y][(int)(Player_X + 0.99999999999999)]] == false && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][0][(int)(Player_Y + 0.99999999999999)][(int)(Player_X + 0.99999999999999)]] == false) || (
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)Player_Y][(int)Player_X]] == false && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(Player_Y + 0.99999999999999)][(int)Player_X]] == false && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)Player_Y][(int)(Player_X + 0.99999999999999)]] == false && 
                        Tile_Data.Tile_Collision[Map_Data.map_data[Player_Dimension][1][(int)(Player_Y + 0.99999999999999)][(int)(Player_X + 0.99999999999999)]] == false)))
                    {
                        disable_collision = false;
                    }


                    //衝突した場合
                    if (Collision == true && /*頂点4点すべて衝突判定の場合は当たり判定無効*/ disable_collision == false) {
                        //X
                        Console.WriteLine("disable_collison:"+disable_collision.ToString());
                        if (Collision1 == true || Collision3 == true) {
                            if (tmp_Player_X > Player_X/* && (int)tmp_Player_X != (int)Player_X*/)//Xが小さくなった場合
                            {
                                Player_X = (int)Player_X + 1;
                            }
                            else if (tmp_Player_X < Player_X/* && (int)(tmp_Player_X - 0.000000000000001) != (int)(Player_X - 0.000000000000001)*/)//Xが大きくなった場合
                            {
                                Player_X = (int)Player_X;
                            }
                        }
                        //Y
                        if (Collision2 == true || Collision3 == true) {
                            if (tmp_Player_Y > Player_Y/* && (int)tmp_Player_Y != (int)Player_Y*/)//Yが小さくなった場合
                            {
                                Player_Y = (int)Player_Y + 1;
                            }
                            else if (tmp_Player_Y < Player_Y/* && (int)(tmp_Player_Y - 0.000000000000001) != (int)(Player_Y) - 0.000000000000001*/)//Yが大きくなった場合
                            {
                                Player_Y = (int)Player_Y;
                            }
                        }
                    }
                }

                //変更された座標等を代入
                PD.Player_X = Player_X;
                PD.Player_Y = Player_Y;
                PD.Player_Dimension = Player_Dimension;
                PD.Player_Now_Speed = Player_Now_Speed;
            }
        }
    }
}
