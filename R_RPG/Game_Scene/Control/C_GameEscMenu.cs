using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using R_UILib;

namespace R_RPG.Game_Scene
{
    class C_GameEscMenu
    {
        static RUI_ButtonImageString Button1 = new RUI_ButtonImageString();
        static RUI_ButtonImageString Button2 = new RUI_ButtonImageString();
        static RUI_ButtonImageString Button3 = new RUI_ButtonImageString();
        static RUI_ButtonImageString Button4 = new RUI_ButtonImageString();
        static public byte CGameEscMenu()
        {
            //Draw_Esc_Menu
            int Handle = Draw_Esc_Menu();

            //Escを押した時
            if((Key_State.KeyState[DX.KEY_INPUT_ESCAPE] == 1 && Key_State.KeyStateOld[DX.KEY_INPUT_ESCAPE] == 0) || Handle == 1)
            {
                DX.SetDrawBright(255, 255, 255);    // 通常の明るさにする
                Game_Main.GameControlHandle = "GameMain";    // 操作をGameMainに戻す
                return 0;
            }
            return 0;// 一時的
        }

        //Draw_Esc_Menu
        static private byte Draw_Esc_Menu()
        {
            byte Return = 0;
            DX.SetDrawBright(255, 255, 255);    // 明るさを戻しEscMenuを描画

            // ボタンサイズの計算
            //int ButtonSizeX1, ButtonSizeY1, ButtonSizeX2, ButtonSizeY2, DrawSpaceX;
            int ButtonWidth1    = (int)(GeD.Window_Width * 0.2);
            int ButtonHeight1   = (int)(GeD.Window_Heigt * 0.1);
            int DrawSpaceX      = (int)(GeD.Window_Width * 0.1);
            int DrawSpaceY      = (int)(GeD.Window_Heigt * 0.05);
            int ButtonPositionX = GeD.Window_Width / 2 - DrawSpaceX / 2 - ButtonWidth1;
            int ButtonPositionY = (int)(GeD.Window_Heigt * 0.2 + ButtonHeight1);
            // ボタンの描画
            if(Button1.Show(ButtonPositionX, ButtonPositionY, ButtonPositionX+ButtonWidth1, ButtonPositionY+ButtonHeight1, "アホ", FoD.TestFont, DX.GetColor(255, 255, 255), Graphic_Data.GraphicData["Black"]) == true)
            {
                Return = 2;
            }
            ButtonPositionX = ButtonPositionX + DrawSpaceX + ButtonWidth1; 
            if(Button1.Show(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1, "クズ", FoD.TestFont, DX.GetColor(255, 255, 255), Graphic_Data.GraphicData["Black"]))
            {
                Return = 3;
            }
            ButtonPositionX = ButtonPositionX - DrawSpaceX - ButtonWidth1;
            ButtonPositionY += DrawSpaceY; 
            if(Button1.Show(ButtonPositionX, ButtonPositionY, ButtonPositionX + ButtonWidth1, ButtonPositionY + ButtonHeight1, "クズ", FoD.TestFont, DX.GetColor(255, 255, 255), Graphic_Data.GraphicData["Black"]))
            {
                Return = 1;
            }

            DX.SetDrawBright(100, 100, 100);    // 少し暗くして
            return Return;
        }
    }
}
