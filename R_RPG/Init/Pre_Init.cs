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
            Options.LoadOptions();

            if (Setting.Setting_EnableSystemLog == true) {
                R_GLib.RG.OutputLog("Pre Initialized");
            }
        }
    }
}
