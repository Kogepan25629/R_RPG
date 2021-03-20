using DxLibDLL;

// Version 0.5.1

namespace R_UILib
{
    static class RUI_Data
    {
        public static int GrHandleBlack = DX.MakeGraph(1, 1);
        public static int GrHandleWhite = DX.MakeGraph(1, 1);
        public static void _RUI_DataInit()
        {
            DX.FillGraph(GrHandleBlack, 0, 0, 0, 255);
            DX.FillGraph(GrHandleWhite, 255, 255, 255, 255);
        }
    }
    abstract class RUI_MouseData
    {
        //マウス座標
        protected static int MousePointX { get; private set; }
        protected static int MousePointY { get; private set; }
        //左クリックした瞬間のマウス座標
        protected static int MouseClickDownLeftPointX { get; private set; }
        protected static int MouseClickDownLeftPointY { get; private set; }
        //左クリックを離した瞬間のマウス座標
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
        /*
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
        */
    }

    // Buttonの基底クラス
    abstract class RUI_ButtonBase : RUI_MouseData
    {
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;

        // コンストラクタ
        public RUI_ButtonBase()
        {
            X1 = -1;
            X2 = -1;
            Y1 = -1;
            Y2 = -1;
        }

        //座標の設定
        public void SetPoint(int x1, int y1)
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

        //左クリックの判定
        protected bool MouseClickLeftDetection()
        {
            if (MouseClickLeft == true && MousePointX >= X1 && MousePointX <= X2 && MousePointY >= Y1 && MousePointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックの判定
        protected bool MouseClickRightDetection()
        {
            if (MouseClickRight == true && MousePointX >= X1 && MousePointX <= X2 && MousePointY >= Y1 && MousePointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックアップの判定
        protected bool MouseClickUpLeftDetection()
        {
            if (MouseClickUpLeft == true && MouseClickUpLeftPointX >= X1 && MouseClickUpLeftPointX <= X2 && MouseClickUpLeftPointY >= Y1 && MouseClickUpLeftPointY <= Y2 && MouseClickDownLeftPointX >= X1 && MouseClickDownLeftPointX <= X2 && MouseClickDownLeftPointY >= Y1 && MouseClickDownLeftPointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //右クリックアップの判定
        protected bool MouseClickUpRightDetection()
        {
            if (MouseClickUpRight == true && MouseClickUpRightPointX >= X1 && MouseClickUpRightPointX <= X2 && MouseClickUpRightPointY >= Y1 && MouseClickUpRightPointY <= Y2 && MouseClickDownRightPointX >= X1 && MouseClickDownRightPointX <= X2 && MouseClickDownRightPointY >= Y1 && MouseClickDownRightPointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //左クリックダウンの判定
        protected bool MouseClickDownLeftDetection()
        {
            if (MouseClickDownLeft == true && MouseClickDownLeftPointX >= X1 && MouseClickDownLeftPointX <= X2 && MouseClickDownLeftPointY >= Y1 && MouseClickDownLeftPointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
        //右クリックダウンの判定
        protected bool MouseClickDownRightDetection()
        {
            if (MouseClickDownRight == true && MouseClickDownRightPointX >= X1 && MouseClickDownRightPointX <= X2 && MouseClickDownRightPointY >= Y1 && MouseClickDownRightPointY <= Y2) {
                return true;
            }
            else {
                return false;
            }
        }
    }

    // RUIクラス
    class RUI : RUI_MouseData
    {
        //定数
        public const int RIGHT = 0;
        public const int LEFT = 1;
        public const int HOLD = 0;
        public const int UP = 1;
        public const int DOWN = 2;
        // マウス情報の更新
        // 毎フレーム呼び出しする必要がある
        public static new void UptadeMouseState()
        {
            RUI_MouseData.UptadeMouseState();
        }

        //RUI_Dataの初期化
        public static void R_UILibInit()
        {
            RUI_Data._RUI_DataInit();
        }
    }

    // RUI_Buttonクラス
    class RUI_Button : RUI_ButtonBase
    {
        public int GrHandle;
        public int GrHandle2;
        public int FontHandle;
        public uint StrColor;
        private bool MatchSizeToString;
        private bool OnCursorHighlight;
        private bool OnCursorChangeImage;
        private string Str;
        private int StringWidth;
        private int StringHeight;

        //コンストラクタ
        public RUI_Button(bool matchSizeToString = false, bool onCursorHighlight = false, bool onCursorChangeImage = false)
        {
            GrHandle = -1;
            GrHandle2 = -1;
            FontHandle = -1;
            StrColor = DX.GetColor(255, 255, 255);
            MatchSizeToString = matchSizeToString;
            OnCursorHighlight = onCursorHighlight;
            OnCursorChangeImage = onCursorChangeImage;
            Str = "";
            StringWidth = -1;
            StringHeight = -1;
        }

        //// ボタンテキスト(Str)の変更
        // PRIVATEメゾッド
        private int _SetString(string str)
        {
            if (FontHandle != -1) {
                Str = str;
                StringWidth = DX.GetDrawStringWidthToHandle(Str, Str.Length, FontHandle);
                StringHeight = DX.GetFontSizeToHandle(FontHandle);
                return 0;
            }
            else {
                return -1;
            }
        }
        // 外部参照
        public int SetString(string str, int fontHandle)
        {
            FontHandle = fontHandle;
            return _SetString(str);
        }
        public int SetString(string str)
        {
            return _SetString(str);
        }

        // PRIVATE
        private int _ChangeSizeToString()
        {
            if (MatchSizeToString == true) {
                if (Str == "" || StringWidth == -1 || StringHeight == -1) {
                    return -1;
                }
                X2 = X1 + StringWidth;
                Y2 = Y1 + StringHeight;
            }
            return 0;
        }

        //ボタン描画
        public int Show()
        {
            // ボタンサイズを文字列に合わせる(モードによる)
            if (_ChangeSizeToString() == -1) {
                return -1;
            }
            // 値が設定されていない場合は終了
            if (X1 == -1 || Y1 == -1 || X2 == -1 || Y2 == -1) {
                return -1;
            }

            // 画像の描画
            if (GrHandle != -1) {
                if (OnCursorChangeImage == true) {
                    if (GrHandle2 != -1) {
                        if (MousePointX >= X1 && MousePointX <= X2 && MousePointY >= Y1 && MousePointY <= Y2) {
                            DX.DrawExtendGraph(X1, Y1, X2, Y2, GrHandle2, DX.TRUE);
                        }
                        else {
                            DX.DrawExtendGraph(X1, Y1, X2, Y2, GrHandle, DX.TRUE);
                        }
                    }
                    else {
                        return -1;
                    }
                }
                else if (OnCursorHighlight == true) {
                    if (MousePointX >= X1 && MousePointX <= X2 && MousePointY >= Y1 && MousePointY <= Y2) {
                        DX.DrawExtendGraph(X1, Y1, X2, Y2, RUI_Data.GrHandleWhite, DX.TRUE);
                    }
                    else {
                        DX.DrawExtendGraph(X1, Y1, X2, Y2, RUI_Data.GrHandleBlack, DX.TRUE);
                    }
                    DX.DrawExtendGraph(X1 + 1, Y1 + 1, X2 - 1, Y2 - 1, GrHandle, DX.TRUE);
                }
                else {
                    DX.DrawExtendGraph(X1, Y1, X2, Y2, GrHandle, DX.TRUE);
                }
            }

            // 文字列の描画
            if (Str != "") {
                if (MatchSizeToString) {
                    DX.DrawString(X1, Y1, Str, StrColor);
                }
                else {
                    DX.DrawString(X1 + (((X2 - X1) - StringWidth) / 2), Y1 + (((Y2 - Y1) - StringHeight) / 2), Str, StrColor);
                }
            }

            return 0;
        }

        //// マウスクリックの判定

        public bool DetectMouseClick(int LeftRight, int UpDown)
        {
            // ボタンサイズを文字列に合わせる(モードにより)
            if (_ChangeSizeToString() == -1) {
                return false;
            }
            switch (LeftRight) {
                case RUI.RIGHT:
                    switch (UpDown) {
                        case RUI.HOLD: return MouseClickRightDetection();
                        case RUI.UP  : return MouseClickUpRightDetection();
                        case RUI.DOWN: return MouseClickDownRightDetection();
                        default: return false;
                    }
                case RUI.LEFT:
                    switch (UpDown) {
                        case RUI.HOLD: return MouseClickLeftDetection();
                        case RUI.UP  : return MouseClickUpLeftDetection();
                        case RUI.DOWN: return MouseClickDownLeftDetection();
                        default: return false;
                    }
                default:
                    return false;
            }
        }
    }
}

