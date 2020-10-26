using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_RPG
{
    //変数
    class Program
    {
        [STAThread]
        static void Main()
        {
            Dxlib_Ins.Dxlib_Initialization();   //Dxlibの初期化
            Drawing.Draw_Main();                //Main
            //DxLibMain.Main_Test();
        }
    }
}
