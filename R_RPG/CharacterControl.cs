using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_RPG
{
    class CharacterControl
    {

        static public void Player_Control()
        {
            {
                double Player_X, Player_Y, Player_Speed, Player_Now_Speed;
                int Player_Dimension;
                // キーボード押下状態を保存するバッファの宣言
                byte[] keyStateBuf = new byte[256];
                //X,Y
                Player_Data PD = new Player_Data();

                //Player_Dataの内容を代入
                Player_X = PD.Player_X;
                Player_Y = PD.Player_Y;
                Player_Dimension = PD.Player_Dimension;
                Player_Speed = PD.Player_Speed;

                //Map ID 判別読み込み
                byte[,] Map_Main = Map_Data.Map_0_Load(PD.Player_Dimension);

                // キーボード押下状態の読み込み
                DX.GetHitKeyStateAll(keyStateBuf);

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
                        Player_Y -= Player_Now_Speed * Drawing.DeltaTime;
                        if (Player_Y <= 0)
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
                        Player_Y += Player_Now_Speed * Drawing.DeltaTime;
                        if(Player_Y >= (Map_Main.GetLength(0) - 1))
                        {
                            Player_Y = (Map_Main.GetLength(0) - 1);
                        }
                        break;
                    default:
                        break;
                }
                //A
                switch (keyStateBuf[DX.KEY_INPUT_A])
                {
                    case 1:
                        Player_X -= Player_Now_Speed * Drawing.DeltaTime;
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
                        Player_X += Player_Now_Speed * Drawing.DeltaTime;
                        if (Player_X >= (Map_Main.GetLength(1) - 1))
                        {
                            Player_X = (Map_Main.GetLength(1) - 1);
                        }
                        break;
                    default:
                        break;
                }

                //マウスホイール
                Player_Dimension += DX.GetMouseWheelRotVol();
                if(Player_Dimension < 0)
                {
                    Player_Dimension = 0;
                }else if (Player_Dimension > 1)
                {
                    Player_Dimension = 1;
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
