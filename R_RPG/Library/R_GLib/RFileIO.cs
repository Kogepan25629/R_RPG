using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace R_GeneralLib
{
    class RFileIO
    {
        //// 変数
        //private static bool OutputSystemLog;
        private static string FilePath;
        private static Encoding Encoding;
        private static char SplitChar = '=';
        private static Dictionary<string, string> DictionaryData;

        // Flag
        private static bool FinishedInit = true;   // 未使用のためtrue
        private static bool ExistFileReference = false;

        // 未使用
        public static int Init(bool outputSystemLog)
        {
            //OutputSystemLog = outputSystemLog;
            
            // Log
            RG.OutputLog("[R_GeneralLib:RFileData] Initialized");
            return 0;
        }

        // 読み書き
        // エラー発生時値を戻す
        // 参照するファイルを設定
        public static int ChangeFileReference(string filePath, string encoding)
        {
            // エンコードの文字コードを指定
            try {
                Encoding = Encoding.GetEncoding(encoding);
            }
            catch (ArgumentException) {
                RG.OutputLog("[R_GeneralLib:RFileData][関数:Init] encordingの値[" + encoding + "]が正しくありません", RG.ERROR);
                Encoding = Encoding.GetEncoding("UTF-8");
                return -1;
            }
            // FilePathの変更
            FilePath = filePath;
            // Log
            RG.OutputLog("[R_GeneralLib:RFileData] 参照ファイルを " + FilePath + " に変更[" + encoding + "]");

            SplitChar = '=';
            return 0;
        }

        public static int ChangeSplitString(char splitChar)
        {
            SplitChar = splitChar;
            return 0;
        }

        // PRIVATE
        // 初期化やファイル参照のチェック
        private static bool CheckExistFileReference(string FunctionName)
        {
            if (FinishedInit == true) {
                if (ExistFileReference == true) {
                    return true;
                }
                else {
                    RG.OutputLog("[R_GeneralLib:RFileData][関数:" + FunctionName + "] [関数:ChangeFileReference]でファイルが参照されていません", RG.ERROR);
                    return false;
                }
            }
            else {
                RG.OutputLog("[R_GeneralLib:RFileData][関数:" + FunctionName + "] [関数:Init]で初期化されていません", RG.ERROR);
                return false;
            }
        }

        // ファイル生成
        public static int CreateFile()
        {
            if (CheckExistFileReference("CreateFile") == true) {
                if (File.Exists(FilePath) == false) {
                    FileStream fileOptionsCreate = File.Create(FilePath);
                    fileOptionsCreate.Close();
                    RG.OutputLog("[R_GeneralLib:RFileData][関数:CreateFile] " + FilePath + " が生成されました", RG.INFO);
                    return 0;
                }
                else {
                    RG.OutputLog("[R_GeneralLib:RFileData][関数:CreateFile] " + FilePath + " はすでに存在しています", RG.WARM);
                    return 0;
                }
            }
            else {
                return -1;
            }
        }

        // キーが存在するかどうか確認
        public static int CheckKey(string key)
        {
            string tmp;
            try {
                tmp = DictionaryData[key];
                return 0;
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                return -1;
            }
        }

        // キーに対する値を設定
        public static int SetValue(string key, string value)
        {
            try {
                DictionaryData[key] = value;
                return 0;
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                RG.OutputLog("[R_GeneralLib: RFileData][関数:SetValue] keyが存在しません", RG.ERROR);
                return -1;
            }
        }

        // キーに対する値を取得
        public static string GetValue(string key)
        {
            try {
                return DictionaryData[key];
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                RG.OutputLog("[R_GeneralLib: RFileData][関数:GetValue] keyが存在しません", RG.ERROR);
                return "";
            }
        }

        // キーに対する値を任意の型で取得
        public static Types GetValue<Types>(string Key)
        {
            bool failedParse = false;
            try {
                if (typeof(Types) == typeof(string)) {  // string
                    return (Types)(object)DictionaryData[Key].ToString();
                }
                else if (typeof(Types) == typeof(int)) {  // int
                    int intNum;
                    if (int.TryParse(DictionaryData[Key].ToString(), out intNum)) {
                        return (Types)(object)intNum;
                    }
                    else {
                        failedParse = true;
                    }
                }
                else if (typeof(Types) == typeof(double)) {  // double
                    double doubleNum;
                    if (double.TryParse(DictionaryData[Key].ToString(), out doubleNum)) {
                        return (Types)(object)doubleNum;
                    }
                    else {
                        failedParse = true;
                    }
                }
                else if (typeof(Types) == typeof(float)) {  // float
                    float floatNum;
                    if (float.TryParse(DictionaryData[Key].ToString(), out floatNum)) {
                        return (Types)(object)floatNum;
                    }
                    else {
                        failedParse = true;
                    }
                }
                else if (typeof(Types) == typeof(bool)) {  // bool
                    bool boolNum;
                    if (bool.TryParse(DictionaryData[Key].ToString(), out boolNum)) {
                        return (Types)(object)boolNum;
                    }
                    else {
                        failedParse = true;
                    }
                }

                // 正常に型変換されなかった場合
                if (failedParse == true) {
                    RG.OutputLog("[R_GeneralLib: RFileData][関数:GetValue] 型変換に失敗" + "[" + "Key:" + Key + "; Value:" + Key + "; 型:" + typeof(Types).ToString() + "]", RG.ERROR);
                }
                else {
                    RG.OutputLog("[R_GeneralLib: RFileData][関数:GetValue] 型[" + typeof(Types).ToString() + "]に対応していません", RG.ERROR);
                }
                return default;
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                RG.OutputLog("[R_GeneralLib: RFileData][関数:GetValue] Keyが存在しません" + "型:" + typeof(Types).ToString(), RG.ERROR);
                return default;
            }
        }

        // キーに対する値をまとめてDictionaryで設定
        public static int SetAllValue(Dictionary<string, string> dictionaryData)
        {
            DictionaryData = dictionaryData;
            return 0;
        }
        public static int SetAllValue(Dictionary<string, object> dictionaryData)
        {
            Dictionary<string, string> dictionaryDataTmp = new Dictionary<string, string>();

            foreach (KeyValuePair<string, object> item in dictionaryData) {
                dictionaryDataTmp[item.Key] = item.Value.ToString();
            }

            DictionaryData = dictionaryDataTmp;
            return 0;
        }

        // キーに対する値をまとめてDictionaryで取得
        public static Dictionary<string, string> GetAllValue()
        {
            return DictionaryData;
        }

        // ファイルに書き込み
        public static int LoadData()
        {
            if (CheckExistFileReference("LoadData") == true) {
                // ファイルが存在しなければ規定値で生成
                if (File.Exists(FilePath) == false) {
                    CreateFile();
                }

                // ファイルを開く
                FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader file = new StreamReader(fs, Encoding);

                // 読み込み
                while (file.Peek() != -1) {
                    // 1行ずつ読み込み
                    string str = file.ReadLine();

                    // SplitChar でくぎる
                    char[] charSeparator = { SplitChar };
                    string[] arr = str.Split(charSeparator, StringSplitOptions.RemoveEmptyEntries);

                    // 要素数が2のとき(Key, Value)読み込んだ値を対応するOptionsValueに格納
                    if (arr.Length == 2) {
                        //Console.WriteLine(arr[0] + ":" + arr[1]);

                        bool existkey = false;
                        // 対応するKeyを探す
                        foreach (KeyValuePair<string, string> item in DictionaryData) {
                            if (item.Key == arr[0]) {
                                existkey = true;
                            }
                        }
                        if (existkey == true) {
                            SetValue(arr[0], arr[1]);
                        }
                        else {
                            DictionaryData.Add(arr[0], arr[1]);
                        }
                    }
                }

                // ファイルを閉じる
                file.Close();
                fs.Close();
                RG.OutputLog("[R_GeneralLib: RFileData][関数:LoadData] " + FilePath + " から読み込みました", RG.ERROR);
                return 0;
            }
            else {
                return -1;
            }
        }

        // ファイルから読み込み
        public static int SaveData()
        {
            if (CheckExistFileReference("SaveData") == true) {
                if (File.Exists(FilePath) == false) {
                    CreateFile();
                }

                try {
                    // ファイルを開く
                    FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    StreamWriter fileOptions = new StreamWriter(fs, Encoding);

                    // テキストを書き込む
                    foreach (KeyValuePair<string, string> item in DictionaryData) {
                        fileOptions.WriteLine(item.Key + SplitChar + item.Value);
                    }

                    // ファイルを閉じる
                    fileOptions.Close();
                    fs.Close();
                    RG.OutputLog("[R_GeneralLib: RFileData][関数:SaveData] " + FilePath + " への書き込みが正常に行われました", RG.ERROR);
                    return 0;
                }
                catch (System.IO.IOException) {
                    RG.OutputLog("[R_GeneralLib: RFileData][関数:SaveData] " + FilePath + " ファイルにアクセスできません。別のプロセスが使用中です", RG.ERROR);
                    RG.OutputLog("[R_GeneralLib: RFileData][関数:SaveData] " + FilePath + " への書き込み(オプションの適応)がスルーされました。", RG.ERROR);
                    return -1;
                }
            }
            else {
                return -1;
            }
        }
    }
}
