using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_RPG.Game_Scene
{
    class C_GameEscMenu
    {
        static public byte CGameEscMenu()
        {
            //Draw_Esc_Menu
            Draw_Esc_Menu();

            //Escを押した時
            if(Key_State.KeyState[DX.KEY_INPUT_ESCAPE] == 1 && Key_State.KeyStateOld[DX.KEY_INPUT_ESCAPE] == 0)
            {
                DX.SetDrawBright(255, 255, 255);    // 通常の明るさにする
                Game_Main.GameControlHandle = "GameMain";    // 操作をGameMainに戻す
                return 0;
            }
            return 0;// 一時的
        }

        //Draw_Esc_Menu
        static private void Draw_Esc_Menu()
        {
            DX.SetDrawBright(100, 100, 100);    // 少し暗くして
        }
    }
}
