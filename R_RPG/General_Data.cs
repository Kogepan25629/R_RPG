using System;

namespace R_RPG
{
    static class GeD // GeneralData
    {
        //// 定数
        //ボタンサイズ
        public static readonly int BUTTON1_SIZE1X;
        public static readonly int BUTTON1_SIZE1Y;
        public static readonly int BUTTON1_SIZE2X;
        public static readonly int BUTTON1_SIZE2Y;
        public static readonly int BUTTON1_SIZE3X;
        public static readonly int BUTTON1_SIZE3Y;
        public static readonly int BUTTON1_SIZE4X;
        public static readonly int BUTTON1_SIZE4Y;


        //// 変数
        public static int Window_Width, Window_Heigt;   // ウィンドウサイズ

        //Dxlibで取得したキーの状態
        static public byte[] KeyState = new byte[256];
        static public byte[] KeyStateOld = new byte[256];

        static public string GameDataPath;
    }
}
