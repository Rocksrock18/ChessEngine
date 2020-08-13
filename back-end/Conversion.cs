using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    public class Conversion
    {
        public enum Squares
        {
            a1 = 21,
            b1 = 22,
            c1 = 23,
            d1 = 24,
            e1 = 25,
            f1 = 26,
            g1 = 27,
            h1 = 28,
            a2 = 31,
            b2 = 32,
            c2 = 33,
            d2 = 34,
            e2 = 35,
            f2 = 36,
            g2 = 37,
            h2 = 38,
            a3 = 41,
            b3 = 42,
            c3 = 43,
            d3 = 44,
            e3 = 45,
            f3 = 46,
            g3 = 47,
            h3 = 48,
            a4 = 51,
            b4 = 52,
            c4 = 53,
            d4 = 54,
            e4 = 55,
            f4 = 56,
            g4 = 57,
            h4 = 58,
            a5 = 61,
            b5 = 62,
            c5 = 63,
            d5 = 64,
            e5 = 65,
            f5 = 66,
            g5 = 67,
            h5 = 68,
            a6 = 71,
            b6 = 72,
            c6 = 73,
            d6 = 74,
            e6 = 75,
            f6 = 76,
            g6 = 77,
            h6 = 78,
            a7 = 81,
            b7 = 82,
            c7 = 83,
            d7 = 84,
            e7 = 85,
            f7 = 86,
            g7 = 87,
            h7 = 88,
            a8 = 91,
            b8 = 92,
            c8 = 93,
            d8 = 94,
            e8 = 95,
            f8 = 96,
            g8 = 97,
            h8 = 98
        };
        public enum Pieces
        {
            WHITE_PAWN = 11,
            WHITE_KNIGHT = 12,
            WHITE_BISHOP = 13,
            WHITE_ROOK = 14,
            WHITE_QUEEN = 15,
            WHITE_KING = 16,
            BLACK_PAWN = 21,
            BLACK_KNIGHT = 22,
            BLACK_BISHOP = 23,
            BLACK_ROOK = 24,
            BLACK_QUEEN = 25,
            BLACK_KING = 26
        };

        public enum BoardRep
        {
            P = 11,
            N = 12,
            B = 13,
            R = 14,
            Q = 15,
            K = 16,
            p = 21,
            n = 22,
            b = 23,
            r = 24,
            q = 25,
            k = 26
        };

        public int FileToNum(string file)
        {
            switch(file)
            {
                case "a":
                    return 1;
                case "b":
                    return 2;
                case "c":
                    return 3;
                case "d":
                    return 4;
                case "e":
                    return 5;
                case "f":
                    return 6;
                case "g":
                    return 7;
                case "h":
                    return 8;
            }
            return 0;
        }

        public int RowToNum(string row)
        {
            switch (row)
            {
                case "1":
                    return 2;
                case "2":
                    return 3;
                case "3":
                    return 4;
                case "4":
                    return 5;
                case "5":
                    return 6;
                case "6":
                    return 7;
                case "7":
                    return 8;
                case "8":
                    return 9;
            }
            return 0;
        }

        public int SquareToNum(string square)
        {
            string file = square.Substring(0, 1);
            string row = square.Substring(1);
            return RowToNum(row) * 10 + FileToNum(file);
        }

        public int MoveToNum(string move, BoardData board)
        {
            //enpassant move
            if(move.Length > 4)
            {
                return (SquareToNum(move.Substring(0, 2)) * 100 + SquareToNum(move.Substring(2, 2))) * 100;
            }
            if(move.Equals("e1g1"))
            {
                if(board.PieceLocations[4] == 25)
                {
                    return -10;
                }
            }
            else if (move.Equals("e1c1"))
            {
                if (board.PieceLocations[4] == 25)
                {
                    return -11;
                }
            }
            else if (move.Equals("e8g8"))
            {
                if (board.PieceLocations[28] == 95)
                {
                    return -20;
                }
            }
            else if (move.Equals("e8c8"))
            {
                if (board.PieceLocations[28] == 95)
                {
                    return -21;
                }
            }
            string before = move.Substring(0, 2);
            string after = move.Substring(2);
            return SquareToNum(before) * 100 + SquareToNum(after);
        }

        public string NumToMove(int move)
        {
            if(move > 100000)
            {
                move /= 100;
            }
            if(move == -10 || move == -20)
            {
                return "O-O";
            }
            else if(move == -11 || move == -21)
            {
                return "O-O-O";
            }
            else if(move == 0)
            {
                return "";
            }
            return "" + NumToLetter(move / 100 % 10) + (move / 1000 - 1) + NumToLetter(move % 10) + (move / 10 % 10 - 1);
        }

        private string NumToLetter(int num)
        {
            switch(num)
            {
                case 1:
                    return "a";
                case 2:
                    return "b";
                case 3:
                    return "c";
                case 4:
                    return "d";
                case 5:
                    return "e";
                case 6:
                    return "f";
                case 7:
                    return "g";
                case 8:
                    return "h";
            }
            return " ";
        }
    }
}
