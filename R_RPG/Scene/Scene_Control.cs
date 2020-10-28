using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace R_RPG.Scene
{
    static class Scene_Control
    {
        //コントロール番号
        static public int S_Control_N = 0;

        //シーンフラグ
        static public bool S_Menu = false;
        static public bool S_Game = false;

        //シーン
        static public void Scene()
        {
            if (S_Menu == true)
            {
                R_RPG.Scene.S_Menu.Main_S_Menu.S_Menu();
            }
            if (S_Game == true)
            {
                R_RPG.Scene.S_Game.Main_S_Game.MainSGame();
            }
        }
    }
}
