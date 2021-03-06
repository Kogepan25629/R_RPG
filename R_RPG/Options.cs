using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_GeneralLib;

namespace R_RPG
{
    /*
    class Options
    {
        
        //// PUBLIC
        // オプションを読み込み各データを設定する
        public static void LoadOptions()
        {
            // option.txt からオプションを読み込み
            if (Setting.Setting_EnableLoadOptionsFile == true) {
                LoadOptionsFile();    // option.txt から読み込み
                SaveOptionsFile();    // 読み込んだ値をoption.txtに書き込み 余計な文字列が消される
                bool aho = GetOptionsValue<int>("UIScale");
            }
            
            foreach (KeyValuePair<string, object[]> item in OptionsValue) {
                Console.WriteLine(item.Key + "=" + GetOptionsValue<string>(item.Key));
            }
            
        }


        //// PRIVATE
        // オプションの規定値
        private static readonly Dictionary<string, object[]> OptionsDefaultValue = new Dictionary<string, object[]>()
        {
            {"FullScreen",   new object[2]{false, typeof(bool)} },
            {"WindowWidth",  new object[2]{854,   typeof(int)} },
            {"WindowHeight", new object[2]{480,   typeof(int)} },
            {"UIScale",      new object[2]{0,     typeof(int)} }
        };
        // ファイルから読み込んだ値を格納する
        private static Dictionary<string, object[]> OptionsValue = new Dictionary<string, object[]>(OptionsDefaultValue);

        private static int SetOptionsValue(string str, object value)
        {
            OptionsValue[str][0] = value;
            return 0;
        }

        // OptionsValueのKeyに対応する値を取得
        private static Types GetOptionsValue<Types>(string str)
        {
            try {
                if (typeof(Types) != typeof(string) && typeof(Types) != (Type)OptionsValue[str][1]) {
                    RG.OutputLog("[メソッド:GetOptionsValue] キャストする型が一致していません", true, RG.WARM);
                }

                if (typeof(Types) == typeof(string)) {
                    return (Types)(object)OptionsValue[str][0].ToString();
                }
                else if (typeof(Types) == typeof(int)) {
                    int intNum;
                    if (int.TryParse(OptionsValue[str][0].ToString(), out intNum)) {
                        return (Types)(object)intNum;
                    }
                    else {
                        RG.OutputLog("[メソッド:GetOptionsValue] 型変換(ToInt)に失敗", true, RG.ERROR);
                        return default;
                    }
                }
                else if (typeof(Types) == typeof(double)) {
                    double doubleNum;
                    if (double.TryParse(OptionsValue[str][0].ToString(), out doubleNum)) {
                        return (Types)(object)doubleNum;
                    }
                    else {
                        RG.OutputLog("[メソッド:GetOptionsValue] 型変換(ToDouble)に失敗", true, RG.ERROR);
                        return default;
                    }
                }
                else if (typeof(Types) == typeof(float)) {
                    float floatNum;
                    if (float.TryParse(OptionsValue[str][0].ToString(), out floatNum)) {
                        return (Types)(object)floatNum;
                    }
                    else {
                        RG.OutputLog("[メソッド:GetOptionsValue] 型変換(ToFloat)に失敗", true, RG.ERROR);
                        return default;
                    }
                }
                else if (typeof(Types) == typeof(bool)) {
                    bool boolNum;
                    if (bool.TryParse(OptionsValue[str][0].ToString(), out boolNum)) {
                        return (Types)(object)boolNum;
                    }
                    else {
                        RG.OutputLog("[メソッド:GetOptionsValue] 型変換(ToBool)に失敗", true, RG.ERROR);
                        return default;
                    }
                }

                RG.OutputLog("[メソッド:GetOptionsValue]非対応の型に変換" + typeof(Types), true, RG.ERROR);
                return default;
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                RG.OutputLog("[メソッド:GetOptionsValue] OptionsValueに対して対応するKeyが存在しません", true, RG.ERROR);
                return default;
            }
        }

        // options.txt からオプションを読み込み
        private static void LoadOptionsFile()
        {
            //bool enableChangeValue = false;

            // ファイルが存在しなければ規定値で生成
            if (File.Exists("options.txt") == false) {
                CreateOptionsFile();
            }

            // 文字コードを指定
            Encoding enc = Encoding.GetEncoding("UTF-8");

            // ファイルを開く
            FileStream fs = new FileStream(@"./options.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader fileOptions = new StreamReader(fs, enc);

            // 読み込み
            while (fileOptions.Peek() != -1) {
                // 1行ずつ読み込み
                string str = fileOptions.ReadLine();

                // = でくぎる
                char[] charSeparator = { '=' };
                string[] arr = str.Split(charSeparator, StringSplitOptions.RemoveEmptyEntries);

                // 要素数が2のとき(Key, Value)読み込んだ値を対応するOptionsValueに格納
                if (arr.Length == 2) {
                    //Console.WriteLine(arr[0] + ":" + arr[1]);

                    bool enableChangeValue = false;    
                    // 対応するKeyを探す
                    foreach (KeyValuePair<string, object[]> item in OptionsValue) {
                        if (item.Key == arr[0]) {
                            enableChangeValue = true;
                        }
                    }
                    if (enableChangeValue == true) {
                        SetOptionsValue(arr[0], arr[1]);
                    }
                }
            }

            // ファイルを閉じる
            fileOptions.Close();
            fs.Close();
        }

        private static void SaveOptionsFile()
        {
            if (File.Exists("options.txt") == false) {
                CreateOptionsFile();
            }
            // 文字コードを指定
            Encoding enc = Encoding.GetEncoding("UTF-8");

            try {
                // ファイルを開く
                FileStream fs = new FileStream(@"./options.txt", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter fileOptions = new StreamWriter(fs, enc);

                // テキストを書き込む
                foreach (KeyValuePair<string, object[]> item in OptionsValue) {
                    fileOptions.WriteLine(item.Key + "=" + item.Value[0]);
                }

                // ファイルを閉じる
                fileOptions.Close();
                fs.Close();
            }
            catch (System.IO.IOException) {
                RG.OutputLog("option.txt ファイルにアクセスできません。別のプロセスが使用中です", true, RG.ERROR);
                RG.OutputLog("option.txt への書き込み(オプションの適応)がスルーされました。", true, RG.ERROR);
                return;
            }
        }

        // options.txt ファイルを生成し規定値を書き込む
        private static void CreateOptionsFile()
        {
            // (ファイルが存在しなければ)生成
            if (File.Exists("options.txt") == false) {
                FileStream fileOptionsCreate = File.Create("./options.txt");
                fileOptionsCreate.Close();
            }

            // 文字コードを指定
            Encoding enc = Encoding.GetEncoding("UTF-8");

            // ファイルを開く
            FileStream fs = new FileStream(@"./options.txt", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter fileOptions = new StreamWriter(fs, enc);

            // テキストを書き込む
            foreach (KeyValuePair<string, object[]> item in OptionsDefaultValue) {
                fileOptions.WriteLine(item.Key + "=" + item.Value[0]);
            }

            // ファイルを閉じる
            fileOptions.Close();
            fs.Close();

            RG.OutputLog("Created options.txt");
        }
    }
    */
}
