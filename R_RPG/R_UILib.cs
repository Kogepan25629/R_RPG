using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_UILib
{
    class R_UI
    {
        private static int  MousePointX;
        private static int  MousePointY;
        private static int  MouseClickUpPointX;
        private static int  MouseClickUpPointY;
        private static int  MouseClickDownPointX;
        private static int  MouseClickDownPointY;
        //クリックしている間
        private static bool MouseClickLeft;
        private static bool MouseClickRight;
        //Oldクリックしている間
        private static bool OldMouseClickLeft;
        private static bool OldMouseClickRight;
        //クリックした瞬間
        private static bool MouseClickDownLeft;
        private static bool MouseClickDownRight;
        //クリックを離した瞬間
        private static bool MouseClickUpLeft;
        private static bool MouseClickUpRight;

        //GetMouseState
        public static void UptadeMouseState()
        {
            OldMouseClickLeft  = MouseClickLeft;
            OldMouseClickRight = MouseClickRight;
            DX.GetMousePoint(out MousePointX, out MousePointY);
            //クリックしているかどうか判定
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_LEFT) != 0)
            {
                MouseClickLeft = true;
            }
            else
            {
                MouseClickLeft = false;
            }
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_RIGHT) != 0)
            {
                MouseClickRight = true;
            }
            else
            {
                MouseClickRight = false;
            }
            //クリックした瞬間を判定
            if (OldMouseClickLeft == true && MouseClickLeft == false)
            {
                MouseClickUpLeft = true;
            }
            else
            {
                MouseClickUpLeft = false;
            }
            if (OldMouseClickRight == true && MouseClickRight == false)
            {
                MouseClickUpRight = true;
            }
            else
            {
                MouseClickUpRight = false;
            }
            //クリックを離した瞬間を判定
            if (OldMouseClickLeft == true && MouseClickLeft == false)
            {
                MouseClickUpLeft = true;
            }
            else
            {
                MouseClickUpLeft = false;
            }
            if (OldMouseClickRight == true && MouseClickRight == false)
            {
                MouseClickUpRight = true;
            }
            else
            {
                MouseClickUpRight = false;
            }
        }

        private static bool ClickDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickUpPointX >= x1 && MouseClickUpPointX <= x2 && MouseClickUpPointY >= y1 && MouseClickUpPointY <= y2 && MouseClickDownPointX >= x1 && MouseClickDownPointX <= x2 && MouseClickDownPointY >= y1 && MouseClickDownPointY <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UI_Image(int x1, int y1, int x2, int y2, int grhandle)
        {
            DX.DrawExtendGraph(x1, y2, x2, y2, grhandle, DX.TRUE);
            if (MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2 && MouseClickLeft == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UI_String(int x1, int y1, int x2, int y2, string str, int fonthandle, uint color)
        {
            DX.DrawString(x1+(((x2 - x1) - DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle)) / 2)/*中央揃え*/, y1+(((y2-y1) - DX.GetFontSizeToHandle(fonthandle)) / 2)/*縦中央揃え*/, str, color);
            if (MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2 && MouseClickLeft == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UI_String(int x1, int y1, int x2, int y2, string str, int fonthandle, uint color, int grhandle)
        {
            DX.DrawExtendGraph(x1, y1, x2, y2, grhandle, DX.TRUE);
            DX.DrawString(x1 + (((x2 - x1) - DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle)) / 2)/*中央揃え*/, y1 + (((y2 - y1) - DX.GetFontSizeToHandle(fonthandle)) / 2)/*縦中央揃え*/, str, color);
            if (MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2 && MouseClickLeft == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //x1 y1 x2 y2 grhandle 
        // 1クリックで数回判定される
        // 長押し用も必要
    }
}
