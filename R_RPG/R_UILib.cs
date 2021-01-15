using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_UILib
{
    class RUI_MouseData
    {
        //マウス座標
        protected static int MousePointX;
        protected static int MousePointY;
        //左クリックした瞬間のマウス座標取得
        protected static int MouseClickDownLeftPointX;
        protected static int MouseClickDownLeftPointY;
        //左クリックを離した瞬間のマウス座標取得
        protected static int MouseClickUpLeftPointX;
        protected static int MouseClickUpLeftPointY;
        //右クリックした瞬間のマウス座標
        protected static int MouseClickDownRightPointX;
        protected static int MouseClickDownRightPointY;
        //右クリックを離した瞬間のマウス座標
        protected static int MouseClickUpRightPointX;
        protected static int MouseClickUpRightPointY;
        //クリックしている間
        protected static bool MouseClickLeft;
        protected static bool MouseClickRight;
        //Oldクリックしている間
        protected static bool OldMouseClickLeft;
        protected static bool OldMouseClickRight;
        //クリックした瞬間
        protected static bool MouseClickDownLeft;
        protected static bool MouseClickDownRight;
        //クリックを離した瞬間
        protected static bool MouseClickUpLeft;
        protected static bool MouseClickUpRight;

        protected static bool ClickUpLeftDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickUpLeftPointX >= x1 && MouseClickUpLeftPointX <= x2 && MouseClickUpLeftPointY >= y1 && MouseClickUpLeftPointY <= y2 && MouseClickDownLeftPointX >= x1 && MouseClickDownLeftPointX <= x2 && MouseClickDownLeftPointY >= y1 && MouseClickDownLeftPointY <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class RUI : RUI_MouseData
    {
        /*ppp
        private static bool ClickUpLeftDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickUpLeftPointX >= x1 && MouseClickUpLeftPointX <= x2 && MouseClickUpLeftPointY >= y1 && MouseClickUpLeftPointY <= y2 && MouseClickDownLeftPointX >= x1 && MouseClickDownLeftPointX <= x2 && MouseClickDownLeftPointY >= y1 && MouseClickDownLeftPointY <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        public static void UptadeMouseState()
        {
            OldMouseClickLeft  = MouseClickLeft;
            OldMouseClickRight = MouseClickRight;
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
            if (OldMouseClickLeft == false && MouseClickLeft == true)
            {
                MouseClickDownLeft = true;
            }
            else
            {
                MouseClickDownLeft = false;
            }
            if (OldMouseClickRight == false && MouseClickRight == true)
            {
                MouseClickDownRight = true;
            }
            else
            {
                MouseClickDownRight = false;
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
            //マウス座標取得
            DX.GetMousePoint(out MousePointX, out MousePointY);
            //左クリックした瞬間のマウス座標取得
            if (MouseClickDownLeft == true) {
                DX.GetMousePoint(out MouseClickDownLeftPointX, out MouseClickDownLeftPointY);
            }
            //左クリックを離した瞬間のマウス座標取得
            if (MouseClickUpLeft == true) {
                DX.GetMousePoint(out MouseClickUpLeftPointX, out MouseClickUpLeftPointY);
            }
            //右クリックした瞬間のマウス座標取得
            if (MouseClickDownRight == true)
            {
                DX.GetMousePoint(out MouseClickDownRightPointX, out MouseClickDownRightPointY);
            }
            //右クリックを離した瞬間のマウス座標取得
            if (MouseClickUpRight == true)
            {
                DX.GetMousePoint(out MouseClickUpRightPointX, out MouseClickUpRightPointY);
            }
        }

        public static bool UI_Image(int x1, int y1, int x2, int y2, int grhandle)
        {
            DX.DrawExtendGraph(x1, y2, x2, y2, grhandle, DX.TRUE);
            if (MouseClickUpLeft == true && ClickUpLeftDetection(x1, y1, x2, y2) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UI_StringImage(int x1, int y1, int x2, int y2, string str, int fonthandle, uint color)
        {
            DX.DrawString(x1+(((x2 - x1) - DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle)) / 2)/*中央揃え*/, y1+(((y2-y1) - DX.GetFontSizeToHandle(fonthandle)) / 2)/*縦中央揃え*/, str, color);
            if (MouseClickUpLeft == true && ClickUpLeftDetection(x1, y1, x2, y2) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UI_StringImage(int x1, int y1, int x2, int y2, string str, int fonthandle, uint color, int grhandle)
        {
            DX.DrawExtendGraph(x1, y1, x2, y2, grhandle, DX.TRUE);
            DX.DrawString(x1 + (((x2 - x1) - DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle)) / 2)/*中央揃え*/, y1 + (((y2 - y1) - DX.GetFontSizeToHandle(fonthandle)) / 2)/*縦中央揃え*/, str, color);
            if (MouseClickUpLeft == true && ClickUpLeftDetection(x1, y1, x2, y2) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UI_String(int x1, int y1, string str, int fonthandle, uint color)
        {
            int x2 = x1 + DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle);
            int y2 = y1 + DX.GetFontSizeToHandle(fonthandle);
            DX.DrawString(x1, y1, str, color);

            if (MouseClickUpLeft == true && ClickUpLeftDetection(x1, y1, x2, y2) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UI_String(int x1, int y1, string str, int fonthandle, uint color, int grhandle)
        {
            int x2 = x1 + DX.GetDrawStringWidthToHandle(str, str.Length, fonthandle);
            int y2 = y1 + DX.GetFontSizeToHandle(fonthandle);
            DX.DrawExtendGraph(x1, y1, x2, y2, grhandle, DX.TRUE);
            DX.DrawString(x1, y1, str, color);
            if (MouseClickUpLeft == true && ClickUpLeftDetection(x1, y1, x2, y2) == true)
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

    RUI_Button
}
