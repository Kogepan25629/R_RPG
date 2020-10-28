using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_RPG.Scene.S_Game
{

    public class Player_Data
    {
        //Player Data
        private static double player_X;
        private static double player_Y;
        private static double player_Speed;
        private static int player_Dimension;
        private static double player_Now_Speed;
        public double Player_X
        {
            set { player_X = value; }
            get { return player_X; }
        }

        public double Player_Y
        {
            set { player_Y = value; }
            get { return player_Y; }
        }

        public int Player_Dimension
        {
            set { player_Dimension = value; }
            get { return player_Dimension; }
        }

        public double Player_Speed
        {
            set { player_Speed = value; }
            get { return player_Speed; }
        }
        public double Player_Now_Speed
        {
            set { player_Now_Speed = value; }
            get { return player_Now_Speed; }
        }

    }
    public class Player_Data_Load
    {
        public static void PDL()
        {
            Player_Data PDL_PD = new Player_Data();
            PDL_PD.Player_X = 2;
            PDL_PD.Player_Y = 2;
            PDL_PD.Player_Dimension = 0;
            PDL_PD.Player_Speed = 2.0;
        }
    }
    
}
