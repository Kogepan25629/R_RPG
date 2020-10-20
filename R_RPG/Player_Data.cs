using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_RPG
{

    public class Player_Data
    {
        //Player Data
        private static double player_X;
        private static double player_Y;
        private static double player_Speed;
        private static int player_Dimension;
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

    }
    public class Player_Data_Load
    {
        public static void PDL()
        {
            Player_Data _PD = new Player_Data();
            _PD.Player_X = 2;
            _PD.Player_Y = 2;
            _PD.Player_Dimension = 0;
            _PD.Player_Speed = 2.0;
        }
    }
    
}
