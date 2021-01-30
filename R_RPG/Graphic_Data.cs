using System;
using System.Collections.Generic;
using DxLibDLL;

namespace R_RPG
{
    class Graphic_Data
    {
        //連想配列
        static public  Dictionary<string, int> GraphicData = new Dictionary<string, int>();

        static public void Graphic_Data_Load()
        {
            //黒単色 1*1
            GraphicData.Add("Black", DX.MakeGraph(1, 1));           //空のグラフィックデータを作成
            DX.FillGraph(GraphicData["Black"], 0, 0, 0, 255); //空のデータに色をつける
        }


    }
}
