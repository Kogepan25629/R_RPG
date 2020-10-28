using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_RPG.Scene.S_Game
{
    public static class Tile_Data
    {
        //配列
        public static int[] TextureData = new int[1024];
        public static bool[] Tile_Collision = new bool[1024];

        private static void Tile_Data_Base(int TileId, int GrHandle, bool TileCollision)
        {
            TextureData[TileId] = GrHandle;
            Tile_Collision[TileId] = TileCollision;
        }
        static public void TileData()
        {
            int tileid;
            int grhandle;
            bool tilecollision;

            //タイルデータ

            {//0 Transparent 透明 あたり判定なし
                tileid = 0;
                tilecollision = false;
                grhandle = DX.LoadGraph("Texture\\Transparent.png");

                Tile_Data_Base(tileid, grhandle, tilecollision);
            }
            {//1 Transparent 透明
                tileid = 1;
                tilecollision = true;
                grhandle = DX.LoadGraph("Texture\\Transparent.png");

                Tile_Data_Base(tileid, grhandle, tilecollision);
            }
            {//2 emerald_block
                tileid = 2;
                tilecollision = false;
                grhandle = DX.LoadGraph("Texture\\emerald_block.png");

                Tile_Data_Base(tileid, grhandle, tilecollision);
            }
            {//3 cobblestone
                tileid = 3;
                tilecollision = false;
                grhandle = DX.LoadGraph("Texture\\cobblestone.png");

                Tile_Data_Base(tileid, grhandle, tilecollision);
            }
            {//4 iron_bars
                tileid = 4;
                tilecollision = true;
                grhandle = DX.LoadGraph("Texture\\iron_bars.png");

                Tile_Data_Base(tileid, grhandle, tilecollision);
            }
            {//5 diamond_block
                tileid = 5;
                tilecollision = false;
                grhandle = DX.LoadGraph("Texture\\diamond_block.png");

                Tile_Data_Base(tileid, grhandle, tilecollision);
            }
            
        }
    }
}
