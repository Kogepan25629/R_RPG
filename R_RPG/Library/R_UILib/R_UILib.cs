using DxLibDLL;
using System;

// Version 0.3.3

namespace R_UILib
{
    static class RUI_Data
    {
        public static int GrHandleBlack = DX.MakeGraph(1, 1);
        public static int GrHandleWhite = DX.MakeGraph(1, 1);
        public static void _RUI_Data() {
            DX.FillGraph(GrHandleBlack, 0, 0, 0, 255);
            DX.FillGraph(GrHandleWhite, 255, 255, 255, 255);
        }
    }
    public abstract class RUI_MouseData
    {
        //マウス座標
        protected static int MousePointX { get; private set; }
        protected static int MousePointY { get; private set; }
        //左クリックした瞬間のマウス座標取得
        protected static int MouseClickDownLeftPointX { get; private set; }
        protected static int MouseClickDownLeftPointY { get; private set; }
        //左クリックを離した瞬間のマウス座標取得
        protected static int MouseClickUpLeftPointX { get; private set; }
        protected static int MouseClickUpLeftPointY { get; private set; }
        //右クリックした瞬間のマウス座標
        protected static int MouseClickDownRightPointX { get; private set; }
        protected static int MouseClickDownRightPointY { get; private set; }
        //右クリックを離した瞬間のマウス座標
        protected static int MouseClickUpRightPointX { get; private set; }
        protected static int MouseClickUpRightPointY { get; private set; }
        //クリックしている間
        protected static bool MouseClickLeft { get; private set; }
        protected static bool MouseClickRight { get; private set; }
        //Oldクリックしている間
        protected static bool OldMouseClickLeft { get; private set; }
        protected static bool OldMouseClickRight { get; private set; }
        //クリックした瞬間
        protected static bool MouseClickDownLeft { get; private set; }
        protected static bool MouseClickDownRight { get; private set; }
        //クリックを離した瞬間
        protected static bool MouseClickUpLeft { get; private set; }
        protected static bool MouseClickUpRight { get; private set; }

        protected static void UptadeMouseState()
        {
            OldMouseClickLeft = MouseClickLeft;
            OldMouseClickRight = MouseClickRight;
            //クリックしているかどうか判定
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_LEFT) != 0) {
                MouseClickLeft = true;
            }
            else {
                MouseClickLeft = false;
            }
            if ((DX.GetMouseInput() & DX.MOUSE_INPUT_RIGHT) != 0) {
                MouseClickRight = true;
            }
            else {
                MouseClickRight = false;
            }
            //クリックした瞬間を判定
            if (OldMouseClickLeft == false && MouseClickLeft == true) {
                MouseClickDownLeft = true;
            }
            else {
                MouseClickDownLeft = false;
            }
            if (OldMouseClickRight == false && MouseClickRight == true) {
                MouseClickDownRight = true;
            }
            else {
                MouseClickDownRight = false;
            }
            //クリックを離した瞬間を判定
            if (OldMouseClickLeft == true && MouseClickLeft == false) {
                MouseClickUpLeft = true;
            }
            else {
                MouseClickUpLeft = false;
            }
            if (OldMouseClickRight == true && MouseClickRight == false) {
                MouseClickUpRight = true;
            }
            else {
                MouseClickUpRight = false;
            }
            //マウス座標取得
            {
                {
                    DX.GetMousePoint(out int mousePointX, out int mousePointY);
                    MousePointX = mousePointX;
                    MousePointY = mousePointY;
                }
            }
            //左クリックした瞬間のマウス座標取得
            if (MouseClickDownLeft == true) {
                {
                    DX.GetMousePoint(out int mouseClickDownLeftPointX, out int mouseClickDownLeftPointY);
                    MouseClickDownLeftPointX = mouseClickDownLeftPointX;
                    MouseClickDownLeftPointY = mouseClickDownLeftPointY;
                }
            }
            //左クリックを離した瞬間のマウス座標取得
            if (MouseClickUpLeft == true) {
                {
                    DX.GetMousePoint(out int mouseClickUpLeftPointX, out int mouseClickUpLeftPointY);
                    MouseClickUpLeftPointX = mouseClickUpLeftPointX;
                    MouseClickUpLeftPointY = mouseClickUpLeftPointY;
                }
            }
            //右クリックした瞬間のマウス座標取得
            if (MouseClickDownRight == true) {
                {
                    DX.GetMousePoint(out int mouseClickDownRightPointX, out int mouseClickDownRightPointY);
                    MouseClickDownRightPointX = mouseClickDownRightPointX;
                    MouseClickDownRightPointY = mouseClickDownRightPointY;
                }
            }
            //右クリックを離した瞬間のマウス座標取得
            if (MouseClickUpRight == true) {
                {
                    DX.GetMousePoint(out int mouseClickUpRightPointX, out int mouseClickUpRightPointY);
                    MouseClickUpRightPointX = mouseClickUpRightPointX;
                    MouseClickUpRightPointY = mouseClickUpRightPointY;
                }
            }
        }

        //左クリックの判定
        protected static bool MouseClickLeftDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickLeft == true && MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックの判定
        protected static bool MouseClickRightDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickRight == true && MousePointX >= x1 && MousePointX <= x2 && MousePointY >= y1 && MousePointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックアップの判定
        protected static bool MouseClickUpLeftDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickUpLeft == true && MouseClickUpLeftPointX >= x1 && MouseClickUpLeftPointX <= x2 && MouseClickUpLeftPointY >= y1 && MouseClickUpLeftPointY <= y2 && MouseClickDownLeftPointX >= x1 && MouseClickDownLeftPointX <= x2 && MouseClickDownLeftPointY >= y1 && MouseClickDownLeftPointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //右クリックアップの判定
        protected static bool MouseClickUpRightDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickUpRight == true && MouseClickUpRightPointX >= x1 && MouseClickUpRightPointX <= x2 && MouseClickUpRightPointY >= y1 && MouseClickUpRightPointY <= y2 && MouseClickDownRightPointX >= x1 && MouseClickDownRightPointX <= x2 && MouseClickDownRightPointY >= y1 && MouseClickDownRightPointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックダウンの判定
        protected static bool MouseClickDownLeftDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickDownLeft == true && MouseClickDownLeftPointX >= x1 && MouseClickDownLeftPointX <= x2 && MouseClickDownLeftPointY >= y1 && MouseClickDownLeftPointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //右クリックダウンの判定
        protected static bool MouseClickDownRightDetection(int x1, int y1, int x2, int y2)
        {
            if (MouseClickDownRight == true && MouseClickDownRightPointX >= x1 && MouseClickDownRightPointX <= x2 && MouseClickDownRightPointY >= y1 && MouseClickDownRightPointY <= y2) {
                return true;
            }
            else {
                return false;
            }
        }

    }

    // RUIクラス
    abstract class RUI : RUI_MouseData
    {
        // マウス情報の更新
        // 毎フレーム呼び出しする必要がある
        public static new void UptadeMouseState()
        {
            RUI_MouseData.UptadeMouseState();
        }

        //RUI_Dataの初期化
        public static void R_UILibInit()
        {
            RUI_Data._RUI_Data();
        }
    }

    // RUI_Buttonクラス
    class RUI_Button : RUI_MouseData
    {
        public  int Mode;
        public  int X1;
        public  int X2;
        public  int Y1;
        public  int Y2;
        public  int GrHandle;
        public  int GrHandle2;
        public  int FontHandle;
        public  uint StrColor;
        private string Str;
        private int StringWidth;
        private int StringHeight;
        //コンストラクタ
        public RUI_Button()
        {
            Mode = 0;
            X1 = -1;
            X2 = -1;
            Y1 = -1;
            Y2 = -1;
            GrHandle = -1;
            GrHandle2 = -1;
            FontHandle = -1;
            StrColor = DX.GetColor(255, 255, 255);
            Str = "";
            StringWidth = -1;
            StringHeight = -1;
        }

        // ボタンテキスト(Str)の変更
        public int SetString(string str, int fonthandle)
        {
            Str = str;
            FontHandle = fonthandle;
            StringWidth = DX.GetDrawStringWidthToHandle(Str, Str.Length, FontHandle);
            StringHeight = DX.GetFontSizeToHandle(FontHandle);

            if (Mode == 1 || Mode == 4 || Mode == 5) {
                if (X1 != -1 && Y1 != -1) {
                    X2 = X1 + StringWidth;
                    Y2 = Y1 + StringHeight;
                    return 0;
                }
                else {
                    return -1;
                }
            }
            return 0;
        }
        public int SetString(string str)
        {
            Str = str;
            if (Mode == 1 || Mode == 4 || Mode == 5) {
                if (FontHandle != -1 && X1 != -1 && Y1 != -1) {
                    StringWidth = DX.GetDrawStringWidthToHandle(Str, Str.Length, FontHandle);
                    StringHeight = DX.GetFontSizeToHandle(FontHandle);
                    X2 = X1 + StringWidth;
                    Y2 = Y1 + StringHeight;

                    return 0;
                }
                else {
                    return -1;
                }
            }
            else {
                return 0;
            }
        }

        //座標の設定
        public void  SetPoint(int x1, int y1)
        {
            X1 = x1;
            Y1 = y1;
        }
        public void SetPoint(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        //ボタン描画
        public int Show()
        {
            if (GrHandle != -1 && X1 != -1 && Y1 != -1 && X2 != -1 && Y2 != -1) {
                if (Mode == 0 || Mode == 1) {
                    //画像の描画
                    DX.DrawExtendGraph(X1, Y1, X2, Y2, GrHandle, DX.TRUE);
                    //文字列の描画
                    if (Str == "") {
                    }
                    else if (Mode == 1) {
                        DX.DrawString(X1, Y1, Str, StrColor);
                    }
                    else {
                        DX.DrawString(X1 + (((X2 - X1) - StringWidth) / 2), Y1 + (((Y2 - Y1) - StringHeight) / 2), Str, StrColor);
                    }
                    return 0;
                }else if ((Mode == 2 || Mode == 4) && GrHandle2 != -1) {
                    //画像の描画
                    if (MousePointX >= X1 && MousePointX <= X2 && MousePointY >= Y1 && MousePointY <= Y2) {
                        DX.DrawExtendGraph(X1, Y1, X2, Y2, GrHandle2, DX.TRUE);
                    }
                    else {
                        DX.DrawExtendGraph(X1, Y1, X2, Y2, GrHandle, DX.TRUE);
                    }
                    //文字列の描画
                    if (Str == "") {

                    }
                    else if(Mode == 4){
                        DX.DrawString(X1, Y1, Str, StrColor);
                    }
                    else {
                        DX.DrawString(X1 + (((X2 - X1) - StringWidth) / 2), Y1 + (((Y2 - Y1) - StringHeight) / 2), Str, StrColor);
                    }
                    return 0;
                }else if (Mode == 3 || Mode == 5) {
                    //画像の描画
                    if (MousePointX >= X1 && MousePointX <= X2 && MousePointY >= Y1 && MousePointY <= Y2) {
                        DX.DrawExtendGraph(X1, Y1, X2, Y2, RUI_Data.GrHandleWhite, DX.TRUE);
                    }
                    else {
                        DX.DrawExtendGraph(X1, Y1, X2, Y2, RUI_Data.GrHandleBlack, DX.TRUE);
                    }
                    DX.DrawExtendGraph(X1 + 1, Y1 + 1, X2 - 1, Y2 - 1, GrHandle, DX.TRUE);
                    //文字列の描画
                    if (Str == "") {

                    }
                    else if(Mode == 5){
                        DX.DrawString(X1, Y1, Str, StrColor);
                    }
                    else {
                        //Console.WriteLine(StringWidth);
                        DX.DrawString(X1 + (((X2 - X1) - StringWidth) / 2), Y1 + (((Y2 - Y1) - StringHeight) / 2), Str, StrColor);
                    }
                    return 0;
                }
                else {
                    return -1;
                }
            }
            else {
                return -1;
            }
        }

        //左クリックの判定
        public  bool LeftDetection()
        {
            return MouseClickLeftDetection(X1, Y1, X2, Y2);
        }
        //右クリックの判定
        public  bool RightDetection()
        {
            return MouseClickLeftDetection(X1, Y1, X2, Y2);
        }
        //左クリックアップの判定
        public  bool LeftUpDetection()
        {
            return MouseClickUpLeftDetection(X1, Y1, X2, Y2);
        }
        //右クリックアップの判定
        public  bool RightUpDetection()
        {
            return MouseClickUpRightDetection(X1, Y1, X2, Y2);
        }
        //左クリックダウンの判定
        public  bool LeftDownDetection()
        {
            return MouseClickDownLeftDetection(X1, Y1, X2, Y2);
        }
        //右クリックダウンの判定
        public  bool RightDownDetection()
        {
            return MouseClickDownRightDetection(X1, Y1, X2, Y2);
        }
    }
}

