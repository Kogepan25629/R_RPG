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
        private static bool MouseInputLeft;
        private static bool MouseInputRight;

        //GetMouseState
        public static void GetMouseState()
        {
            DX.GetMousePoint(out MousePointX, out MousePointY);
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_LEFT) != 0)
            {
                MouseInputLeft = true;
            }
            else
            {
                MouseInputLeft = false;
            }
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_RIGHT) != 0)
            {
                MouseInputRight = true;
            }
            else
            {
                MouseInputRight = false;
            }
        }

        public static bool UI_Image(int x1, int y1, int x2, int y2, int grhandle)
        {
            DX.DrawExtendGraph(x1, y2, x2, y2, grhandle, DX.TRUE);
            if (MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2 && MouseInputLeft == true)
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
            if (MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2 && MouseInputLeft == true)
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
            if (MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2 && MouseInputLeft == true)
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
