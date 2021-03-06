//Version 1.1.0

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

        
    }
}
