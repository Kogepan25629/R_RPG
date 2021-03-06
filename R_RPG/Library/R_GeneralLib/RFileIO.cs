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
        private string FilePath;
        private Encoding Encoding;
        private char SplitChar = '=';
        private Dictionary<string, string> DictionaryData;

        // コンストラクタ
        public RFileIO(string filePath, string encoding, char splitChar = '=')
        {
            // エンコードの文字コードを指定
            try {
                Encoding = Encoding.GetEncoding(encoding);
            }
            catch (ArgumentException) {
                RG.OutputLog("[R_GeneralLib.RFileIO][コンストラクタ] encordingの値[" + encoding + "]は正しくありません。UTF-8を使用します。", RG.ERROR);
                Encoding = Encoding.GetEncoding("UTF-8");
            }
            // FilePathの変更
            FilePath = filePath;
            // ファイルで
            SplitChar = splitChar;
        }

        // CreateFile
        public int CreateFile()
        {
            if (File.Exists(FilePath) == false) {
                FileStream fileOptionsCreate = File.Create(FilePath);
                fileOptionsCreate.Close();
                RG.OutputLog("[R_GeneralLib.RFileIO][関数:CreateFile] " + FilePath + " が生成されました", RG.INFO);
                return 0;
            }
            else {
                RG.OutputLog("[R_GeneralLib.RFileIO][関数:CreateFile] " + FilePath + " はすでに存在しています", RG.WARM);
                return 0;
            }
        }

        // LoadFile
        public int LoadFile()
        {
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

                // 要素数が2のとき(Key, Value)読み込んだ値を対応するDictionaryDataに格納
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
            RG.OutputLog("[R_GeneralLib.RFileIO][関数:LoadFile] " + FilePath + " から読み込みました", RG.INFO);
            return 0;

        }

        // SaveFile
        public int SaveFile()
        {
            if (File.Exists(FilePath) == false) {
                CreateFile();
            }

            try {
                // ファイルを開く
                StreamWriter fileOptions = new StreamWriter(FilePath, false, Encoding);

                // テキストを書き込む
                foreach (KeyValuePair<string, string> item in DictionaryData) {
                    fileOptions.WriteLine(item.Key + SplitChar + item.Value);
                }

                // ファイルを閉じる
                fileOptions.Close();
                RG.OutputLog("[R_GeneralLib.RFileIO][関数:SaveFile] " + FilePath + " への書き込みが正常に行われました", RG.INFO);
                return 0;
            }
            catch (System.IO.IOException) {
                RG.OutputLog("[R_GeneralLib.RFileIO][関数:SaveFile] " + FilePath + " ファイルにアクセスできません。別のプロセスが使用中です", RG.ERROR);
                RG.OutputLog("[R_GeneralLib.RFileIO][関数:SaveFile] " + FilePath + " への書き込みがスルーされました。", RG.ERROR);
                return -1;
            }
        }

        // キーが存在するかどうか確認
        public bool CheckKey(string key)
        {
            string tmp;
            try {
                tmp = DictionaryData[key];
                return true;
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                return false;
            }
        }

        // データを追加
        public void AddValue(string Key, string Value)
        {
            DictionaryData.Add(Key, Value);
        }

        // キーに対する値を設定
        public int SetValue(string key, string value)
        {
            try {
                DictionaryData[key] = value;
                return 0;
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                RG.OutputLog("[R_GeneralLib.RFileData][関数:SetValue] key[" + key + "]は存在しません", RG.ERROR);
                return -1;
            }
        }

        // キーに対する値を取得
        public string GetValue(string key)
        {
            try {
                return DictionaryData[key];
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                RG.OutputLog("[R_GeneralLib.RFileData][関数:GetValue] key[" + key + "]は存在しません", RG.ERROR);
                return default;
            }
        }

        // キーに対する値を任意の型で取得
        public Types GetValue<Types>(string key)
        {
            try {
                return RG.ConvertTypes<Types>(DictionaryData[key].ToString());
            }
            catch (System.Collections.Generic.KeyNotFoundException) {
                RG.OutputLog("[R_GeneralLib.RFileData][関数:GetValue] key[" + key + "]は存在しません" + "型:" + typeof(Types).ToString(), RG.ERROR);
                return default;
            }
        }

        // キーに対する値をまとめてDictionaryで設定
        public void SetAllValue(Dictionary<string, string> dictionaryData)
        {
            DictionaryData = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in dictionaryData) {
                DictionaryData[item.Key] = item.Value;
            }
        }
        public void SetAllValue(Dictionary<string, object> dictionaryData)
        {
            DictionaryData = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> item in dictionaryData) {
                DictionaryData[item.Key] = item.Value.ToString();
            }
        }

        // キーに対する値をまとめてDictionaryで取得
        public Dictionary<string, string> GetAllValue()
        {
            Dictionary<string, string> dictionaryData = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in DictionaryData) {
                dictionaryData[item.Key] = item.Value;
            }
            return dictionaryData;
        }
    }
}
