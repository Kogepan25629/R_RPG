using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace R_RPG.Scene
{
    static class Scene_Control
    {
        //シーンフラグ
        static public bool S_Menu;
        static public bool S_Main;

        //シーン
        static public void Scene()
        {
            if (S_Menu == true)
            {
                R_RPG.Scene.S_Menu.Main_S_Menu.S_Menu();
            }
            if (S_Main == true)
            {
                R_RPG.Scene.S_Main.Main_S_Main.S_Main();
            }
        }
    }
}
