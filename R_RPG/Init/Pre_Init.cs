using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_RPG.Init
{
    class Pre_Init
    {
        public static void PreInit()
        {
            // Log
            R_GeneralLib.RG.OutputLog("Pre Initializing...");

            // オプションの初期化
            Options.InitOptions();
            // オプションの読み込み
            Options.LoadOptions();

            // Log
            R_GeneralLib.RG.OutputLog("Pre Initialized");
        }
    }
}
