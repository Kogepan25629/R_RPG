using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_RPG
{
    class FoD
    {
        public static int TestFont;
        public static void Load_Font()
        {
            TestFont = DX.CreateFontToHandle(null, -1, -1);
        }
    }
}
