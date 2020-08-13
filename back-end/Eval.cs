using System;
using System.Collections.Generic;
using System.Text;
using static ChessEngine.Conversion.Pieces;

namespace ChessEngine
{
    public class Eval
    {
        private static int[] PAWN = new int[]
        {
            0,  0,   0,   0,   0,   0,  0,  0 ,
            5, 10,  10, -20, -20,  10, 10,  5 ,
            5, -5, -10,  0,   0,  -10, -5,  5 ,
            0,  0,   0,  20,  20,   0,  0,  0 ,
            5,  5,  10,  25,  25,  10,  5,  5 ,
            10, 10,  20,  30,  30,  20, 10, 10 ,
            50, 50,  50,  50,  50,  50, 50, 50 ,
            0,  0,   0,   0,   0,   0,  0,  0
        };
        private static int[] BISHOP = new int[]
        {
            -20, -10, -10, -10, -10, -10, -10, -20 ,
            -10,   5,   0,   0,   0,   0,   5, -10 ,
            -10,  10,  10,  10,  10,  10,  10, -10 ,
            -10,   0,  10,  10,  10,  10,   0, -10 ,
            -10,   5,   5,  10,  10,   5,   5, -10 ,
            -10,   0,   5,  10,  10,   5,   0, -10 ,
            -10,   0,   0,   0,   0,   0,   0, -10 ,
            -20, -10, -10, -10, -10, -10, -10, -20
        };
        private static int[] KNIGHT = new int[]
        {
            -50, -30, -30, -30, -30, -30, -30, -50 ,
            -40, -20,   0,   5,   5,   0, -20, -40 ,
            -30,   5,  10,  15,  15,  10,   5, -30 ,
            -30,   0,  15,  20,  20,  15,   0, -30 ,
            -30,   5,  15,  20,  20,  15,   5, -30 ,
            -30,   0,  10,  15,  15,  10,   0, -30 ,
            -40, -20,   0,   0,   0,   0, -20, -40 ,
            -50, -40, -30, -30, -30, -30, -40, -50
        };
        private static int[] ROOK = new int[]
        {
             0,  0,  0,  5,  5,  0,  0,  0 ,
            -5,  0,  0,  0,  0,  0,  0, -5 ,
            -5,  0,  0,  0,  0,  0,  0, -5 ,
            -5,  0,  0,  0,  0,  0,  0, -5 ,
            -5,  0,  0,  0,  0,  0,  0, -5 ,
            -5,  0,  0,  0,  0,  0,  0, -5 ,
            5,  10, 10, 10, 10, 10, 10,  5 ,
            0,   0,  0,  0,  0,  0,  0,  0
        };
        private static int[] QUEEN = new int[]
        {
            -20, -10, -10, -5, -5, -10, -10, -20 ,
            -10,   0,   5,  0,  0,   0,   0, -10 ,
            -10,   5,   5,  5,  5,   5,   0, -10 ,
             0,   0,   5,  5,  5,   5,   0,  -5 ,
            -5,   0,   5,  5,  5,   5,   0,  -5 ,
            -10,   0,   5,  5,  5,   5,   0, -10 ,
            -10,   0,   0,  0,  0,   0,   0, -10 ,
            -20, -10, -10, -5, -5, -10, -10, -20
        };
        private static int[] KING = new int[]
        {
             20,  50,  30,   0,   0,  10,  50,  20 ,
             20,  20,   0,   0,   0,   0,  20,  20 ,
             -10, -20, -20, -20, -20, -20, -20, -10 ,
             -10, -20, -20, -20, -20, -20, -20, -10 ,
             -20, -30, -30, -40, -40, -30, -30, -20 ,
             -30, -40, -40, -50, -50, -40, -40, -30 ,
             -30, -40, -40, -50, -50, -40, -40, -30 ,
             -30, -40, -40, -50, -50, -40, -40, -30 ,
             -30, -40, -40, -50, -50, -40, -40, -30
        };
        private static int[] KING_END = new int[]
        {
            -20, -10, -10, -5, -5, -10, -10, -20 ,
            -10,   0,   0,  0,  0,   0,   0, -10 ,
            -10,   0,   5,  5,  5,   5,   0, -10 ,
             0,   0,   5,  5,  5,   5,   0,  -5 ,
            -5,   0,   5,  5,  5,   5,   0,  -5 ,
            -10,   0,   5,  5,  5,   5,   0, -10 ,
            -10,   0,   0,  0,  0,   0,   0, -10 ,
            -20, -10, -10, -5, -5, -10, -10, -20
        };

        public static HashSet<int> black_holes = new HashSet<int>();
        public static HashSet<int> white_holes = new HashSet<int>();

        public static int Evaluate(BoardData boardData)
        {
            int score = 0;
            int[] board = boardData.Board;
            ClearHoles();
            Holes(board);
            score += PieceValue(boardData, board);
            score += Space(boardData.PieceLocations);
            return score;
        }

        private static void ClearHoles()
        {
            white_holes = new HashSet<int>();
            black_holes = new HashSet<int>();
        }
        public static int Space(int[] pieces)
        {
            return (int)(WhiteSpace(pieces) * 3.5) - (int)(BlackSpace(pieces) * 3.5);
        }

        private static int WhiteSpace(int[] pieces)
        {
            int score = 0;
            for(int i = 8; i < 16; i++)
            {
                int piece = pieces[i];
                if(piece != 0)
                {
                    score += piece / 10 - 3;
                }
            }
            return score;
        }

        private static int BlackSpace(int[] pieces)
        {
            int score = 0;
            for (int i = 16; i < 24; i++)
            {
                int piece = pieces[i];
                if(piece != 0)
                {
                    score += 8 - piece / 10;
                }
            }
            return score;
        }

        public static int PieceValue(BoardData boardData, int[] board)
        {
            int score = 0;
            int index = 0;
            for (int i = 21; i <= 98; i++)
            {
                if (i % 10 == 9)
                {
                    i += 2;
                }
                int piece = board[i];
                if (piece != 0)
                {
                    switch(piece)
                    {
                        case (int)WHITE_PAWN:
                            score += 100;
                            score += PAWN[index];
                            score += PawnWeakness(board, true, i);
                            break;
                        case (int)BLACK_PAWN:
                            score -= 100;
                            score -= PAWN[(63 - index) / 8 * 8 + index % 8];
                            score -= PawnWeakness(board, false, i);
                            break;
                        case (int)WHITE_KNIGHT:
                            score += 300;
                            score += KNIGHT[index];
                            score += KnightBonus(i, true, board);
                            break;
                        case (int)BLACK_KNIGHT:
                            score -= 300;
                            score -= KNIGHT[(63 - index) / 8 * 8 + index % 8];
                            score -= KnightBonus(i, false, board);
                            break;
                        case (int)WHITE_BISHOP:
                            score += 330;
                            score += BISHOP[index];
                            score += BishopBonus(i, true, boardData.PieceLocations, board);
                            break;
                        case (int)BLACK_BISHOP:
                            score -= 330;
                            score -= BISHOP[(63 - index) / 8 * 8 + index % 8];
                            score -= BishopBonus(i, false, boardData.PieceLocations, board);
                            break;
                        case (int)WHITE_ROOK:
                            score += 500;
                            score += ROOK[index];
                            score += RookBonus(i, true, board);
                            break;
                        case (int)BLACK_ROOK:
                            score -= 500;
                            score -= ROOK[(63 - index) / 8 * 8 + index % 8];
                            score -= RookBonus(i, false, board);
                            break;
                        case (int)WHITE_QUEEN:
                            score += 900;
                            score += QUEEN[index];
                            break;
                        case (int)BLACK_QUEEN:
                            score -= 900;
                            score -= QUEEN[(63 - index) / 8 * 8 + index % 8];
                            break;
                        case (int)WHITE_KING:
                            if (boardData.endGame)
                            {
                                score += KING_END[index];
                            }
                            else
                            {
                                score += KING[index];
                                score += KingSafety(board, i, true);
                            }
                            break;
                        default:
                            if (boardData.endGame)
                            {
                                score -= KING_END[(63 - index) / 8 * 8 + index % 8];
                            }
                            else
                            {
                                score -= KING[(63 - index) / 8 * 8 + index % 8];
                                score -= KingSafety(board, i, false);
                            }
                            break;
                    }
                }
                index++;
            }
            return score;
        }

        public static void Holes(int[] board)
        {
            GetWhiteHoles(board);
            GetBlackHoles(board);
        }

        public static int GetWhiteHoles(int[] board)
        {
            int square = 51;
            while(square != 59)
            {
                if(IsHole(square, board, -10, 11))
                {
                    white_holes.Add(square);
                }
                square -= 10;
                if(square/10 == 3)
                {
                    square += 21;
                }
            }
            return white_holes.Count;
        }

        private static int GetBlackHoles(int[] board)
        {
            int square = 61;
            while (square != 69)
            {
                if (IsHole(square, board, 10, 21))
                {
                    black_holes.Add(square);
                }
                square += 10;
                if (square / 10 == 8)
                {
                    square -= 19;
                }
            }
            return black_holes.Count;
        }

        public static bool IsHole(int square, int[] board, int increment, int piece)
        {
            int og_square = square;
            if(square%10 != 1)
            {
                square -= 1;
                for(int i = 0; i < 2; i++)
                {
                    square += increment;
                    if (board[square] == piece)
                    {
                        return false;
                    }
                }
                square = og_square;
            }
            if (square % 10 != 8)
            {
                square += 1;
                for (int i = 0; i < 2; i++)
                {
                    square += increment;
                    if (board[square] == piece)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static int KnightBonus(int square, bool white, int[] board)
        {
            int bonus = 0;
            if(white)
            {
                if(black_holes.Contains(square))
                {
                    bonus += 10;
                    if(board[square-9] == 11 || board[square-11] == 11)
                    {
                        bonus += 10;
                    }
                }
            }
            else
            {
                if (white_holes.Contains(square))
                {
                    bonus += 10;
                    if (board[square + 9] == 21 || board[square + 11] == 21)
                    {
                        bonus += 10;
                    }
                }
            }
            return bonus;
        }

        private static int RookBonus(int square, bool white, int[] board)
        {
            int score = 20;
            int ogSquare = square;
            if(white)
            {
                while(square < 90)
                {
                    square += 10;
                    if(board[square] != 0)
                    {
                        if (board[square] == 11)
                        {
                            score = 0;
                            break;
                        }
                        else if(board[square] == 21)
                        {
                            score = 10;
                            break;
                        }
                    }
                }
            }
            else
            {
                while (square > 30)
                {
                    square -= 10;
                    if (board[square] != 0)
                    {
                        if (board[square] == 21)
                        {
                            score = 0;
                            break;
                        }
                        else if (board[square] == 11)
                        {
                            score = 10;
                            break;
                        }
                    }
                }
            }
            return score;
        }

        public static int BishopBonus(int square, bool white, int[] pieces, int[] board)
        {
            int score = 0;
            int ogSquare = square;
            bool dark = IsDarkSquare(square);
            if(white)
            {
                if(dark)
                {
                    for(int i = 16; i < 24; i++)
                    {
                        if(pieces[i] != 0 && IsDarkSquare(pieces[i]))
                        {
                            score += 6;
                        }
                    }
                    for(int i = 8; i < 16; i++)
                    {
                        if (pieces[i] != 0 && IsDarkSquare(pieces[i]))
                        {
                            score -= 6;
                        }
                    }
                }
                else
                {
                    for (int i = 16; i < 24; i++)
                    {
                        if (pieces[i] != 0 && !IsDarkSquare(pieces[i]))
                        {
                            score += 6;
                        }
                    }
                    for (int i = 8; i < 16; i++)
                    {
                        if (pieces[i] != 0 && !IsDarkSquare(pieces[i]))
                        {
                            score -= 6;
                        }
                    }
                }
            }
            else
            {
                if (dark)
                {
                    for (int i = 16; i < 24; i++)
                    {
                        if (pieces[i] != 0 && IsDarkSquare(pieces[i]))
                        {
                            score -= 6;
                        }
                    }
                    for (int i = 8; i < 16; i++)
                    {
                        if (pieces[i] != 0 && IsDarkSquare(pieces[i]))
                        {
                            score += 6;
                        }
                    }
                }
                else
                {
                    for (int i = 16; i < 24; i++)
                    {
                        if (pieces[i] != 0 && !IsDarkSquare(pieces[i]))
                        {
                            score -= 6;
                        }
                    }
                    for (int i = 8; i < 16; i++)
                    {
                        if (pieces[i] != 0 && !IsDarkSquare(pieces[i]))
                        {
                            score += 6;
                        }
                    }
                }
            }
            return score;
        }

        private static bool IsDarkSquare(int square)
        {
            return Math.Abs(square / 10 - square % 10) % 2 == 1;
        }

        private static int KingSafety(int[] board, int kingpos, bool white)
        {
            int score = 0;
            if (white)
            {
                if(kingpos/10 != 2)
                {
                    score -= 5;
                }
                if(kingpos%10 > 3 && kingpos%10 < 7)
                {
                    score -= 20;
                }
                else
                {
                    if(kingpos < 60)
                    {
                        if (board[kingpos + 10] != 11)
                        {
                            score -= 10;
                            if (board[kingpos + 20] != 11)
                            {
                                score -= 20;
                                if (board[kingpos + 30] != 11)
                                {
                                    score -= 20;
                                }
                            }
                        }
                        if (board[kingpos + 9] != 11)
                        {
                            score -= 10;
                            if (board[kingpos + 19] != 11)
                            {
                                score -= 20;
                                if (board[kingpos + 29] != 11)
                                {
                                    score -= 20;
                                }
                            }
                        }
                        if (board[kingpos + 11] != 11)
                        {
                            score -= 10;
                            if (board[kingpos + 21] != 11)
                            {
                                score -= 20;
                                if (board[kingpos + 31] != 11)
                                {
                                    score -= 20;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (kingpos / 10 != 9)
                {
                    score -= 5;
                }
                if (kingpos % 10 > 3 && kingpos % 10 < 7)
                {
                    score -= 20;
                }
                else
                {
                    if(kingpos > 60)
                    {
                        if (board[kingpos - 10] != 21)
                        {
                            score -= 10;
                            if (board[kingpos - 20] != 21)
                            {
                                score -= 20;
                                if (board[kingpos - 30] != 21)
                                {
                                    score -= 20;
                                }
                            }
                        }
                        if (board[kingpos - 9] != 21)
                        {
                            score -= 10;
                            if (board[kingpos - 19] != 21)
                            {
                                score -= 20;
                                if (board[kingpos - 29] != 21)
                                {
                                    score -= 20;
                                }
                            }
    ;
                        }
                        if (board[kingpos - 11] != 21)
                        {
                            score -= 10;
                            if (board[kingpos - 21] != 21)
                            {
                                score -= 20;
                                if (board[kingpos - 31] != 21)
                                {
                                    score -= 20;
                                }
                            }
                        }
                    }
                }
            }
            return score;
        }

        public static int PawnWeakness(int[] board, bool white, int square)
        {
            int score = 0;
            if(white)
            {
                //pawn is weak if its in a hole
                if(white_holes.Contains(square))
                {
                    score -= 25;
                }
                else
                {
                    square -= 1;
                    score -= 15;
                    //checks if isolated
                    while (square > 29)
                    {
                        square -= 10;
                        if (board[square] == 11 || board[square + 2] == 11)
                        {
                            score += 15;
                            break;
                        }
                    }
                }
            }
            else
            {
                //pawn is weak if its in a hole
                if (black_holes.Contains(square))
                {
                    score -= 25;
                }
                else
                {
                    square -= 1;
                    score -= 15;
                    //checks if isolated
                    while (square < 90)
                    {
                        square += 10;
                        if (board[square] == 21 || board[square + 2] == 21)
                        {
                            score += 15;
                            break;
                        }
                    }
                }
            }
            return score;
        }


    }
}
