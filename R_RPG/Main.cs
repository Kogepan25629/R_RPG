using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

using R_RPG.Scene;

namespace R_RPG
{
    //変数
    class Program
    {
        [STAThread]
        static void Main()
        {
            Dxlib_Ins.Dxlib_Initialization();   //Dxlibの初期化

            //Gameシーン
            Scene_Control.S_Game = true;
            Scene_Control.S_Control_N = 1;
            Scene_Control.Scene();

            //DxLibMain.Main_Test();
        }
    }
}
