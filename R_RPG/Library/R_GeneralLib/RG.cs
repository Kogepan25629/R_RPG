//Version 1.3.4

using System;

namespace R_GeneralLib
{
    public static class RG
    {
        // 定数
        public const byte INFO = 0;
        public const byte WARM = 1;
        public const byte ERROR = 2;
        public const byte NONE = 3;

        //変数
        private static byte OutputLogLevel = INFO;

        // コンソール出力のレベル変更(ERROR, WARM, NORMAL, NONE)
        public static void ChangeOutputLogLevel(byte outputLogLevel)
        {
            if (outputLogLevel == INFO || outputLogLevel == WARM || outputLogLevel == ERROR || outputLogLevel == NONE) {
                OutputLogLevel = outputLogLevel;
            }
        }

        // コンソール出力関数
        public static void OutputLog(string str, byte outputLogLevel = INFO, bool enableTime = true)
        {
            if (outputLogLevel >= OutputLogLevel) {
                //時間表示
                if (enableTime == true) {
                    DateTime dt = DateTime.Now;

                    // 各桁が1桁の場合先頭に0をつける
                    string hour = dt.Hour.ToString();
                    string minute = dt.Minute.ToString();
                    string second = dt.Second.ToString();
                    if (hour.Length == 1) {
                        hour = "0" + hour;
                    }else if(minute.Length == 1) {
                        minute = "0" + minute;
                    }
                    else if(second.Length == 1) {
                        second = "0" + second;
                    }

                    Console.Write("[" + hour + ":" + minute + ":" + second + "]");
                }

                // modeに応じて文字列の色を変更
                if (outputLogLevel == WARM) {
                    Console.ForegroundColor = ConsoleColor.Yellow;  // WARMは黄色
                }
                else if (outputLogLevel == ERROR) {
                    Console.ForegroundColor = ConsoleColor.Red;     // ERRORは赤色
                }

                // 時間の後に空白
                if (enableTime == true) {
                    Console.Write(" ");
                }

                Console.WriteLine(str); // 文字列を指定された色で出力
                Console.ResetColor();   // 色をリセット
            }
        }

        // 文字列を指定した任意の型で返す
        public static Types ConvertTypes<Types>(string str)
        {
            bool failedParse = false;
            if (typeof(Types) == typeof(string)) {  // string
                return (Types)(object)str;
            }
            else if (typeof(Types) == typeof(int)) {  // int
                int intNum;
                if (int.TryParse(str, out intNum)) {
                    return (Types)(object)intNum;
                }
                else {
                    failedParse = true;
                }
            }
            else if (typeof(Types) == typeof(double)) {  // double
                double doubleNum;
                if (double.TryParse(str, out doubleNum)) {
                    return (Types)(object)doubleNum;
                }
                else {
                    failedParse = true;
                }
            }
            else if (typeof(Types) == typeof(float)) {  // float
                float floatNum;
                if (float.TryParse(str, out floatNum)) {
                    return (Types)(object)floatNum;
                }
                else {
                    failedParse = true;
                }
            }
            else if (typeof(Types) == typeof(bool)) {  // bool
                bool boolNum;
                if (bool.TryParse(str, out boolNum)) {
                    return (Types)(object)boolNum;
                }
                else {
                    failedParse = true;
                }
            }

            // 正常に型変換されなかった場合
            if (failedParse == true) {
                RG.OutputLog("[R_GeneralLib.RG][関数:GetValue] 型変換に失敗" + "[値:" + str + "; 型:" + typeof(Types).ToString() + "]", RG.ERROR);
            }
            else {
                RG.OutputLog("[R_GeneralLib.RG][関数:GetValue] 型[" + typeof(Types).ToString() + "]に対応していません", RG.ERROR);
            }
            return default;
        }
    }
}
