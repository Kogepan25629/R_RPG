using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_RPG
{
    class Setting
    {
        //Windowモード選択
        public static readonly bool Setting_ChangeWindowMode = true;    //true:WindowMode   false:FullScreen  Def:treu

        // ウィンドウのサイズの変更
        public static readonly bool Setting_PossibleChangeWindowSize = false;
        //TileSize(表示ドット数)
        public static readonly int Setting_TileSize = 32;

        //WindowSize
        public static readonly int Setting_Window_Width = 854;   //854,800,832,1024,1366,1600,1920,2560
        public static readonly int Setting_Window_Heigt = 480;   //480,600,512,768 ,768 ,900 ,1080,1440
    }
}
