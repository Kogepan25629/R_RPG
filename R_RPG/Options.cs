using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_GeneralLib;

namespace R_RPG
{
    class Options
    {
        //
        private static RFileIO Option = new RFileIO(@"./options.txt", "UTF-8");

        // オプションの規定値
        private static readonly Dictionary<string, object[]> OptionsDefaultValue = new Dictionary<string, object[]>()
        {
            {"FullScreen",   new object[2]{false, typeof(bool)} },
            {"WindowWidth",  new object[2]{854,   typeof(int)} },
            {"WindowHeight", new object[2]{480,   typeof(int)} },
            {"UIScale",      new object[2]{0,     typeof(int)} }
        };
        // ファイルから読み込んだ値を格納するDictionary
        private static Dictionary<string, string> OptionsValue;
        
        // Optionsの初期化(データの初期化ではない)
        public static void InitOptions()
        {
            //
            SetDefaultValue();

        }

        public static void LoadOptions()
        {
            Option.LoadFile();
            // 規定値の存在するデータをファイルから取得
            foreach (KeyValuePair<string, object[]> item in OptionsDefaultValue) {
                if (Option.CheckKey(item.Key)) {
                    OptionsValue[item.Key] = Option.GetValue(item.Key);
                }
                else {
                    OptionsValue[item.Key] = OptionsDefaultValue[item.Key][0].ToString();
                }
            }

            // デバッグ用
            foreach (KeyValuePair<string, string> itemm in OptionsValue) {
                Console.WriteLine(itemm.Key + "=" + itemm.Value);
            }

            SaveOptions();
        }

        public static void SaveOptions()
        {
            Option.SetAllValue(OptionsValue);
            Option.SaveFile();
        }

        // OptionsDefaultValueの値をOptionに設定
        public static void SetDefaultValue()
        {
            OptionsValue = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object[]> item in OptionsDefaultValue) {
                OptionsValue.Add(item.Key, item.Value[0].ToString());
            }
            Option.SetAllValue(OptionsValue);
        }
    }

}
