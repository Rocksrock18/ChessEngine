using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    public class Zobrist
    {
        private const int white_pawn = 11;
        private const int white_knight = 12;
        private const int white_bishop = 13;
        private const int white_rook = 14;
        private const int white_queen = 15;
        private const int white_king = 16;
        private const int black_pawn = 21;
        private const int black_knight = 22;
        private const int black_bishop = 23;
        private const int black_rook = 24;
        private const int black_queen = 25;
        private const int black_king = 26;

        //seeding gives consistent results
        private const int default_seed = 1070372;

        private ulong[,] table = new ulong[64,13];

        public Zobrist()
        {
            Random rand = new Random(default_seed);
            table = new ulong[64, 13];
            for(int i = 0; i < 64; i++)
            {
                for(int j = 0; j < 13; j++)
                {
                    table[i, j] = (ulong)rand.Next() * (ulong)rand.Next();
                }
            }
        }
        //generates hash from scratch
        public ulong Hash(BoardData boardData)
        {
            ulong key = 0ul;
            int[] board = boardData.Board;
            for (int i = 21; i <= 98; i++)
            {
                if(i%10 == 9)
                {
                    i += 2;
                }
                key = key ^ table[ConvertSquareForHash(i), ConvertPieceForHash(board[i])];
            }
            return key;
        }

        //generates hash from old board + changes to the board
        public ulong UpdateHash(ulong old_key, int newSquare, int oldSquare, int newSquarePiece, int oldSquarePiece)
        {
            ulong key = old_key;
            key = key ^ table[ConvertSquareForHash(newSquare), ConvertPieceForHash(newSquarePiece)];
            key = key ^ table[ConvertSquareForHash(newSquare), ConvertPieceForHash(oldSquarePiece)];
            key = key ^ table[ConvertSquareForHash(oldSquare), ConvertPieceForHash(oldSquarePiece)];
            key = key ^ table[ConvertSquareForHash(oldSquare), 0];
            return key;
        }

        public ulong ReverseUpdateHash(ulong old_key, int newSquare, int oldSquare, int newSquarePiece, int oldSquarePiece)
        {
            ulong key = old_key;
            key = key ^ table[ConvertSquareForHash(newSquare), 0];
            key = key ^ table[ConvertSquareForHash(newSquare), ConvertPieceForHash(oldSquarePiece)];
            key = key ^ table[ConvertSquareForHash(oldSquare), ConvertPieceForHash(oldSquarePiece)];
            key = key ^ table[ConvertSquareForHash(oldSquare), ConvertPieceForHash(newSquarePiece)];
            return key;
        }

        private int ConvertPieceForHash(int piece)
        {
            if(piece == 0)
            {
                return 0;
            }
            else if(piece > 20)
            {
                return piece - 14;
            }
            return piece - 10;
        }
        private int ConvertSquareForHash(int square)
        {
            return ((square / 10 - 2) * 8) + ((square % 10) - 1);
        }
    }
}
