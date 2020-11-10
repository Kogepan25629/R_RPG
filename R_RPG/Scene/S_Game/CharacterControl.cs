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

        static public void Player_Control(Player_Data PD, double ElapsedTime1F)
        {
            if(Scene_Control.S_Control_N == 1)
            {
                //変数宣言
                double Player_X, Player_Y, Player_Speed, Player_Now_Speed;
                double tmp_Player_X, tmp_Player_Y;
                int Player_Dimension;

                // キーボード押下状態を保存するバッファの宣言
                byte[] keyStateBuf = new byte[256];

                //Player_Dataの内容を代入
                tmp_Player_X = Player_X = PD.Player_X;
                tmp_Player_Y = Player_Y = PD.Player_Y;
                Player_Dimension = PD.Player_Dimension;
                Player_Speed = PD.Player_Speed;

                //Map ID 判別読み込み
                byte[,] Map_0 = Map_Data.Map_0_Load(PD.Player_Dimension);   //レイヤー0
                byte[,] Map_1 = Map_Data.Map_1_Load(PD.Player_Dimension);   //レイヤー1

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
                            if (Player_Y < 0)
                            {
                                Player_Y = 0;
                            }

                            break;
                        default:
                            break;
                    }
                    //S
                    switch (keyStateBuf[DX.KEY_INPUT_S])
                    {
                        case 1:
                            Player_Y += Player_Now_Speed * ElapsedTime1F;
                            if (Player_Y >= (Map_0.GetLength(0) - 1))
                            {
                                Player_Y = (Map_0.GetLength(0) - 1);
                            }
                            break;
                        default:
                            break;
                    }
                    //A
                    switch (keyStateBuf[DX.KEY_INPUT_A])
                    {
                        case 1:
                            Player_X -= Player_Now_Speed * ElapsedTime1F;
                            if (Player_X <= 0)
                            {
                                Player_X = 0;
                            }
                            break;
                        default:
                            break;
                    }
                    //D
                    switch (keyStateBuf[DX.KEY_INPUT_D])
                    {
                        case 1:
                            Player_X += Player_Now_Speed * ElapsedTime1F;
                            if (Player_X >= (Map_0.GetLength(1) - 1))
                            {
                                Player_X = (Map_0.GetLength(1) - 1);
                            }
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

                ////当たり判定の処理
                //移動距離が1をこえた場合移動距離を1にする
                {
                    //X
                    if((tmp_Player_X > Player_X) && (tmp_Player_X - Player_X > 1))//Xが小さくなった場合
                    {
                        Player_X -= 1.0;
                    }
                    else if((tmp_Player_X < Player_X) && (Player_X - tmp_Player_X > 1))//Xが大きくなった場合
                    {
                        Player_X += 1.0;
                    }
                    //Y
                    if ((tmp_Player_Y > Player_Y) && (tmp_Player_Y - Player_Y > 1))//Yが小さくなった場合
                    {
                        Player_Y -= 1.0;
                    }
                    else if ((tmp_Player_Y < Player_Y) && (Player_Y - tmp_Player_Y > 1))//Yが大きくなった場合
                    {
                        Player_Y += 1.0;
                    }
                }
                //当たり判定
                {
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
