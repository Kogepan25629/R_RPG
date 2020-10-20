using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace R_RPG
{
    class DxLibMain
    {
        
        [STAThread]
        static public void Main_Test()
        {
            //変数宣言
            int TileSize = 32;                //テクスチャの解像度幅
            int Window_WIDTH = TileSize * 26;//横解像度832  TileSize32
            int Window_HEIGHT = TileSize * 16;//縦解像度512  TileSize32
            uint Cr;
            // window modeにする
            DX.ChangeWindowMode(DX.TRUE);
            // window　のサイズを決める
            DX.SetGraphMode(Window_WIDTH, Window_HEIGHT, 32);
            //DxLib初期化
            if (DX.DxLib_Init() == -1)
            {
                return;
            }
            //描写を裏画面に指定
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            //マップ配列
            byte[,] DrawMap =
            {
                { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
            };
            //テクスチャ配列
            int[] Texture = new int[3];
            Texture[0] = DX.LoadGraph("Texture\\diamond_block.png");
            Texture[1] = DX.LoadGraph("Texture\\emerald_block.png");
            Texture[2] = DX.LoadGraph("Texture\\cobblestone.png");
            //デバッグ表示
            {
                Console.WriteLine($"解像度　{Window_WIDTH} * {Window_HEIGHT}\n");
                Console.WriteLine("テクスチャ配列 グラフックバンドル");
                for (int r1 = 0; r1 < Texture.Length; r1++)
                    Console.WriteLine($"{r1}:{Texture[r1]}");
            }
            {
                // 白色の値を取得
                Cr = DX.GetColor(0, 0, 0);
            }
            //描写
            {
                int xt = 0;
                while (true)
                {
                    if (DX.ProcessMessage() != 0)
                    { // メッセージ処理
                        break;//ウィンドウの×ボタンが押されたらループを抜ける
                    }
                    DX.ClearDrawScreen(); // 画面を消す
                                          //テクスチャ描画
                    {
                        for (int y = 0; y <= 15; y++)
                        {
                            for (int x = 0; x <= 25; x++)
                            {
                                DX.DrawExtendGraph(x * TileSize, y * TileSize, x * TileSize + TileSize, y * TileSize + TileSize, Texture[DrawMap[y, x]], DX.FALSE);
                            }
                        }
                        //int test = DX.LoadGraph("Texture\\saruwaka.jpeg");
                        //DX.DrawExtendGraph(352, 192, 480, 320, test, DX.TRUE);
                    }
                    DX.DrawGraph(xt, 100, Texture[2], DX.TRUE); //画像の描画
                    DX.DrawString(2, Window_HEIGHT - 16, "Hello C World!", Cr);// 文字列の描画
                    xt = xt + 1; // xを2増やす
                    DX.ScreenFlip(); //裏画面を表画面に反映
                }
            }
            //メモリ開放
            DX.InitGraph();
            DX.WaitKey();
            DX.DxLib_End();
        }
    }
}
