using System;
using R_UILib;

namespace R_RPG.Init
{
    class Post_Init
    {
        public static void PostInit()
        {
            //テクスチャ読み込み
            Game_Scene.Tile_Data.TileData();
            Graphic_Data.Graphic_Data_Load();

            //フォント読み込み
            FoD.Load_Font();

            //R_UILibの初期化
            RUI.R_UILibInit();

            if (Setting.Setting_EnableSystemLog == true) {
                R_GeneralLib.RG.OutputLog("Post Initialized");
            }
        }
    }
}
