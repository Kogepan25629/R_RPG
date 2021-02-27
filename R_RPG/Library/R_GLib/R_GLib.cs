using System;
using System.Collections.Generic;

namespace R_GLib
{
    public static class RG
    {
        // 定数
        public const byte NORMAL = 0;
        public const byte WARM = 1;
        public const byte ERROR = 2;

        // コンソール出力関数
        public static void OutputLog(string str, bool enableTime = true, byte mode = NORMAL)
        {
            //時間表示
            if (enableTime == true) {
                DateTime dt = DateTime.Now;
                Console.Write("[" + dt.Hour + ":" + dt.Minute + ":" + dt.Second + "]");
            }

            // modeに応じて文字列の色を変更
            if (mode == 1) {
                Console.ForegroundColor = ConsoleColor.Yellow;  // WARMは黄色
            }
            else if (mode == 2) {
                Console.ForegroundColor = ConsoleColor.Red;     // ERRORは赤色
            }

            // 時間の後に空白
            if (enableTime == true) {
                Console.Write(" ");
            }

            Console.WriteLine(str); // 文字列を指定された色で出力
            Console.ResetColor();   // 色をリセット
        }

        /// <summary>
        /// 値を取得、keyがなければデフォルト値を設定し、デフォルト値を取得
        /// keyがなければ null を返す
        /// </summary>
        public static TV GetOrDefault<TK, TV>(this Dictionary<TK, TV> dic, TK key, TV defaultValue = default(TV))
        {
            return dic.TryGetValue(key, out var result) ? result : defaultValue;
        }
    }
}
