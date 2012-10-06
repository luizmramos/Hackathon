using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class Piece
    {
        public int[][] block;
        public static int dim = 5;
        public int x0, y0;

        public Piece(int[][] leitura) {
        
            int[][] block = new int[dim][];
            
            for (int i = 0; i < dim; i++){
                block[i] = new int[dim];
                for (int j = 0; j < dim; j++)
                    block[i][j] = leitura[i][j];

            }

        }

    }
}
