using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_RPG
{
    class Pre_Init
    {
        public static void PreInit()
        {
            if (Setting.Setting_EnableSystemLog == true) {
                Console.WriteLine("Pre Initialized");
            }
        }
    }
}
