using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Blocks
{
    class PageGame : UIRoot
    {

        UIButton btnPlay;
        UILabel lblHello;
        UISprite imgStone;
        UIButton btnNext;

        int level, fase, boardHeigth, boardWidth, numPieces, dim;
        public static int gridWidth = 18, gridHeigth = 30;
        Piece[] pieces;
        int[][] grid;

        public PageGame(int level, int fase)
        {
            this.level = level;
            this.fase = fase;

            readConfig();

            grid = new int[gridHeigth][];
            for (int i = 0; i < gridHeigth; i++)
            {
                grid[i] = new int[gridWidth];
                for (int j = 0; j < gridWidth; j++)
                    grid[i][j] = -1;
            }

            initPieces();
            

            Add(imgStone = new UISprite(Tex.stone),
                btnPlay = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed));

            btnPlay.SetWidth(0.052f).SetHeight(0.052f).SetY(0.5f).SetX(0.5f);
        }


        public override void Update(int dt)
        {
            base.Update(dt);
        }

        public void readConfig(){
            
            int[][][] matrizes;
            //LER PECAS E TAMANHO

            for (int i = 0; i < numPieces; i++)
                pieces[i] = new Piece(matrizes[i]);
        }

        public void initPieces() {
            for (int i = 0; i < numPieces; i++) {
                bool fitted = false;
                for (int x0 = 0; x0 < gridWidth && !fitted; x0++) {
                    for (int y0 = 0; y0 < gridHeigth && !fitted; y0++) {
                        if (fit(i, x0, y0)) {
                            put(i, x0, y0);
                            fitted = true;
                        }
                    }
                } 
            }
            
        }

        public bool fit(int id, int x0, int y0) {
            bool fitted = true;
            for (int x = 0; x < Piece.dim  && fitted; x++)
            {
                for (int y = 0; y < Piece.dim && fitted; y++)
                {
                    if (pieces[id].block[x][y] == 1) {
                        if (x > gridWidth - 1 || y > gridHeigth - 1){
                            fitted = false;
                        }
                        else if (grid[x0 + x][y0 + y] != -1){
                            fitted = false;
                        }
                     }

                }
            }
            return fitted;
        }

        public void put(int id, int x0, int y0) {
            pieces[id].x0 = x0;
            pieces[id].y0 = y0;
            for (int x = x0; x < Piece.dim; x++)
            {
                for (int y = y0; y < Piece.dim; y++)
                {
                    grid[x][y] = id;
                }
            }
        }

    }
}
