using System;
using System.Collections.Generic;
using System.Text;
using static ChessEngine.Conversion.Pieces;

namespace ChessEngine
{
    public class MoveGen
    {
        public static BoardData boards = new BoardData();
        public static int numNodes = 0;
        //----------------------------------------------------------------------------------------------------------------------------------------
        private static int GetAttackers(int[] board, int kingpos, bool white)
        {
            int attackers = 0;
            if(white)
            {
                //pawn and knights
                if (board[kingpos + 21] == (int)BLACK_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos + 21);
                }
                else if(board[kingpos + 19] == (int)BLACK_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos + 19);
                }
                else if (board[kingpos + 8] == (int)BLACK_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos + 8);
                }
                else if (board[kingpos + 12] == (int)BLACK_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos + 12);
                }
                else if (board[kingpos - 8] == (int)BLACK_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos - 8);
                }
                else if (board[kingpos - 12] == (int)BLACK_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos - 12);
                }
                else if (board[kingpos - 21] == (int)BLACK_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos - 21);
                }
                else if (board[kingpos - 19] == (int)BLACK_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos - 19);
                }
                else if (board[kingpos + 9] == (int)BLACK_PAWN)
                {
                    attackers = attackers * 100 + (kingpos + 9);
                }
                else if (board[kingpos + 11] == (int)BLACK_PAWN)
                {
                    attackers = attackers * 100 + (kingpos + 11);
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i * 9];
                    if (piece != 0)
                    {
                        if (piece == (int)BLACK_BISHOP || piece == (int)BLACK_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos + i * 9);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i * 11];
                    if (piece != 0)
                    {
                        if (piece == (int)BLACK_BISHOP || piece == (int)BLACK_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos + i * 11);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i * 11];
                    if (piece != 0)
                    {
                        if (piece == (int)BLACK_BISHOP || piece == (int)BLACK_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos - i * 11);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i * 9];
                    if (piece != 0)
                    {
                        if (piece == (int)BLACK_BISHOP || piece == (int)BLACK_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos - i * 9);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i];
                    if (piece != 0)
                    {
                        if (piece == (int)BLACK_ROOK || piece == (int)BLACK_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos + i);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + 10 * i];
                    if (piece != 0)
                    {
                        if (piece == (int)BLACK_ROOK || piece == (int)BLACK_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos + 10 * i);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i];
                    if (piece != 0)
                    {
                        if (piece == (int)BLACK_ROOK || piece == (int)BLACK_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos - i);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - 10 * i];
                    if (piece != 0)
                    {
                        if (piece == (int)BLACK_ROOK || piece == (int)BLACK_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos - 10 * i);
                        }
                        break;
                    }
                }
            }
            else
            {
                //pawn and knights
                if (board[kingpos + 21] == (int)WHITE_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos + 21);
                }
                else if (board[kingpos + 19] == (int)WHITE_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos + 19);
                }
                else if (board[kingpos + 8] == (int)WHITE_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos + 8);
                }
                else if (board[kingpos + 12] == (int)WHITE_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos + 12);
                }
                else if (board[kingpos - 8] == (int)WHITE_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos - 8);
                }
                else if (board[kingpos - 12] == (int)WHITE_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos - 12);
                }
                else if (board[kingpos - 21] == (int)WHITE_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos - 21);
                }
                else if (board[kingpos - 19] == (int)WHITE_KNIGHT)
                {
                    attackers = attackers * 100 + (kingpos - 19);
                }
                else if (board[kingpos - 9] == (int)WHITE_PAWN)
                {
                    attackers = attackers * 100 + (kingpos - 9);
                }
                else if (board[kingpos - 11] == (int)WHITE_PAWN)
                {
                    attackers = attackers * 100 + (kingpos - 11);
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i * 9];
                    if (piece != 0)
                    {
                        if (piece == (int)WHITE_BISHOP || piece == (int)WHITE_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos + i * 9);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i * 11];
                    if (piece != 0)
                    {
                        if (piece == (int)WHITE_BISHOP || piece == (int)WHITE_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos + i * 11);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i * 11];
                    if (piece != 0)
                    {
                        if (piece == (int)WHITE_BISHOP || piece == (int)WHITE_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos - i * 11);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i * 9];
                    if (piece != 0)
                    {
                        if (piece == (int)WHITE_BISHOP || piece == (int)WHITE_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos - i * 9);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i];
                    if (piece != 0)
                    {
                        if (piece == (int)WHITE_ROOK || piece == (int)WHITE_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos + i);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + 10 * i];
                    if (piece != 0)
                    {
                        if (piece == (int)WHITE_ROOK || piece == (int)WHITE_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos + 10 * i);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i];
                    if (piece != 0)
                    {
                        if (piece == (int)WHITE_ROOK || piece == (int)WHITE_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos - i);
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - 10 * i];
                    if (piece != 0)
                    {
                        if (piece == (int)WHITE_ROOK || piece == (int)WHITE_QUEEN)
                        {
                            attackers = attackers * 100 + (kingpos - 10 * i);
                        }
                        break;
                    }
                }
            }
            return attackers;
        }
        public static bool InCheck(int[] board, int kingpos, bool white)
        {
            if (white)
            {
                //pawn, knights, king
                if (board[kingpos + 21] == (int)BLACK_KNIGHT || board[kingpos + 19] == (int)BLACK_KNIGHT ||
                    board[kingpos + 8] == (int)BLACK_KNIGHT || board[kingpos + 12] == (int)BLACK_KNIGHT ||
                    board[kingpos - 8] == (int)BLACK_KNIGHT || board[kingpos - 12] == (int)BLACK_KNIGHT ||
                    board[kingpos - 21] == (int)BLACK_KNIGHT || board[kingpos - 19] == (int)BLACK_KNIGHT ||
                    board[kingpos + 9] == (int)BLACK_PAWN || board[kingpos + 11] == (int)BLACK_PAWN ||
                    board[kingpos + 9] == (int)BLACK_KING || board[kingpos + 10] == (int)BLACK_KING ||
                    board[kingpos + 11] == (int)BLACK_KING || board[kingpos - 1] == (int)BLACK_KING ||
                    board[kingpos + 1] == (int)BLACK_KING || board[kingpos - 9] == (int)BLACK_KING ||
                    board[kingpos - 10] == (int)BLACK_KING || board[kingpos - 11] == (int)BLACK_KING)
                {
                    return true;
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i * 9];
                    if (piece != 0 && piece != (int)WHITE_KING)
                    {
                        if (piece == (int)BLACK_BISHOP || piece == (int)BLACK_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i * 11];
                    if (piece != 0 && piece != (int)WHITE_KING)
                    {
                        if (piece == (int)BLACK_BISHOP || piece == (int)BLACK_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i * 11];
                    if (piece != 0 && piece != (int)WHITE_KING)
                    {
                        if (piece == (int)BLACK_BISHOP || piece == (int)BLACK_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i * 9];
                    if (piece != 0 && piece != (int)WHITE_KING)
                    {
                        if (piece == (int)BLACK_BISHOP || piece == (int)BLACK_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i];
                    if (piece != 0 && piece != (int)WHITE_KING)
                    {
                        if (piece == (int)BLACK_ROOK || piece == (int)BLACK_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + 10 * i];
                    if (piece != 0 && piece != (int)WHITE_KING)
                    {
                        if (piece == (int)BLACK_ROOK || piece == (int)BLACK_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i];
                    if (piece != 0 && piece != (int)WHITE_KING)
                    {
                        if (piece == (int)BLACK_ROOK || piece == (int)BLACK_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - 10 * i];
                    if (piece != 0 && piece != (int)WHITE_KING)
                    {
                        if (piece == (int)BLACK_ROOK || piece == (int)BLACK_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
            }
            else
            {
                //pawn, knights, king
                if (board[kingpos + 21] == (int)WHITE_KNIGHT || board[kingpos + 19] == (int)WHITE_KNIGHT ||
                    board[kingpos + 8] == (int)WHITE_KNIGHT || board[kingpos + 12] == (int)WHITE_KNIGHT ||
                    board[kingpos - 8] == (int)WHITE_KNIGHT || board[kingpos - 12] == (int)WHITE_KNIGHT ||
                    board[kingpos - 21] == (int)WHITE_KNIGHT || board[kingpos - 19] == (int)WHITE_KNIGHT ||
                    board[kingpos - 9] == (int)WHITE_PAWN || board[kingpos - 11] == (int)WHITE_PAWN ||
                    board[kingpos + 9] == (int)WHITE_KING || board[kingpos + 10] == (int)WHITE_KING ||
                    board[kingpos + 11] == (int)WHITE_KING || board[kingpos -1] == (int)WHITE_KING ||
                    board[kingpos + 1] == (int)WHITE_KING || board[kingpos - 9] == (int)WHITE_KING ||
                    board[kingpos - 10] == (int)WHITE_KING || board[kingpos - 11] == (int)WHITE_KING)
                {
                    return true;
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i * 9];
                    if (piece != 0 && piece != (int)BLACK_KING)
                    {
                        if (piece == (int)WHITE_BISHOP || piece == (int)WHITE_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i * 11];
                    if (piece != 0 && piece != (int)BLACK_KING)
                    {
                        if (piece == (int)WHITE_BISHOP || piece == (int)WHITE_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i * 11];
                    if (piece != 0 && piece != (int)BLACK_KING)
                    {
                        if (piece == (int)WHITE_BISHOP || piece == (int)WHITE_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i * 9];
                    if (piece != 0 && piece != (int)BLACK_KING)
                    {
                        if (piece == (int)WHITE_BISHOP || piece == (int)WHITE_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + i];
                    if (piece != 0 && piece != (int)BLACK_KING)
                    {
                        if (piece == (int)WHITE_ROOK || piece == (int)WHITE_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos + 10 * i];
                    if (piece != 0 && piece != (int)BLACK_KING)
                    {
                        if (piece == (int)WHITE_ROOK || piece == (int)WHITE_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - i];
                    if (piece != 0 && piece != (int)BLACK_KING)
                    {
                        if (piece == (int)WHITE_ROOK || piece == (int)WHITE_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    int piece = board[kingpos - 10 * i];
                    if (piece != 0 && piece != (int)BLACK_KING)
                    {
                        if (piece == (int)WHITE_ROOK || piece == (int)WHITE_QUEEN)
                        {
                            return true;
                        }
                        break;
                    }
                }
            }
            return false;
        }
        public static HashSet<int> GetPinnedPieces(int[] board, int kingPos, bool white)
        {
            //contains locations of pinnedPieces
            HashSet<int> pieces = new HashSet<int>();

            bool encounteredFirst = false;
            int pinnedLoc = 0;
            //this for loop checks upwards
            for (int j = 1; j < 8; j++)
            {
                int piece = board[kingPos + j * 10];
                if (piece >= 0)
                {
                    if(piece > 0)
                    {
                        if (!encounteredFirst)
                        {
                            //if piece is of opposite color, no pinned pieces in this direction.
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                break;
                            }
                            //if piece is of same color, it may be a pinned piece
                            encounteredFirst = true;
                            pinnedLoc = kingPos + j * 10;
                        }
                        else
                        {
                            //if second piece is of opposing color, first piece encountered is a pinned piece if rook or queen.
                            if (white && piece > 20)
                            {
                                if(piece == (int)BLACK_QUEEN || piece == (int)BLACK_ROOK)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            else if(!white && piece < 20)
                            {
                                if (piece == (int)WHITE_QUEEN || piece == (int)WHITE_ROOK)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            encounteredFirst = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //this for loop checks downward
            encounteredFirst = false;
            for (int j = 1; j < 8; j++)
            {
                int piece = board[kingPos + j * -10];
                if (piece >= 0)
                {
                    if (piece > 0)
                    {
                        if (!encounteredFirst)
                        {
                            //if piece is of opposite color, no pinned pieces in this direction.
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                break;
                            }
                            //if piece is of same color, it may be a pinned piece
                            encounteredFirst = true;
                            pinnedLoc = kingPos + j * -10;
                        }
                        else
                        {
                            //if second piece is of opposing color, first piece encountered is a pinned piece if rook or queen.
                            if (white && piece > 20)
                            {
                                if (piece == (int)BLACK_QUEEN || piece == (int)BLACK_ROOK)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            else if (!white && piece < 20)
                            {
                                if (piece == (int)WHITE_QUEEN || piece == (int)WHITE_ROOK)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            encounteredFirst = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //this for loop checks left
            encounteredFirst = false;
            for (int j = 1; j < 8; j++)
            {
                int piece = board[kingPos - j];
                if (piece >= 0)
                {
                    if (piece > 0)
                    {
                        if (!encounteredFirst)
                        {
                            //if piece is of opposite color, no pinned pieces in this direction.
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                break;
                            }
                            //if piece is of same color, it may be a pinned piece
                            encounteredFirst = true;
                            pinnedLoc = kingPos - j;
                        }
                        else
                        {
                            //if second piece is of opposing color, first piece encountered is a pinned piece if rook or queen.
                            if (white && piece > 20)
                            {
                                if (piece == (int)BLACK_QUEEN || piece == (int)BLACK_ROOK)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            else if (!white && piece < 20)
                            {
                                if (piece == (int)WHITE_QUEEN || piece == (int)WHITE_ROOK)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            encounteredFirst = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //this for loop checks right
            encounteredFirst = false;
            for (int j = 1; j < 8; j++)
            {
                int piece = board[kingPos + j];
                if (piece >= 0)
                {
                    if (piece > 0)
                    {
                        if (!encounteredFirst)
                        {
                            //if piece is of opposite color, no pinned pieces in this direction.
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                break;
                            }
                            //if piece is of same color, it may be a pinned piece
                            encounteredFirst = true;
                            pinnedLoc = kingPos + j;
                        }
                        else
                        {
                            //if second piece is of opposing color, first piece encountered is a pinned piece if rook or queen.
                            if (white && piece > 20)
                            {
                                if (piece == (int)BLACK_QUEEN || piece == (int)BLACK_ROOK)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            else if (!white && piece < 20)
                            {
                                if (piece == (int)WHITE_QUEEN || piece == (int)WHITE_ROOK)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            encounteredFirst = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //this for loop checks first diagonal, goes towards top left
            encounteredFirst = false;
            for (int j = 1; j < 8; j++)
            {
                int piece = board[kingPos + j * 9];
                if (piece >= 0)
                {
                    if (piece > 0)
                    {
                        if (!encounteredFirst)
                        {
                            //if piece is of opposite color, no pinned pieces in this direction.
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                break;
                            }
                            //if piece is of same color, it may be a pinned piece
                            encounteredFirst = true;
                            pinnedLoc = kingPos + j * 9;
                        }
                        else
                        {
                            //if second piece is of opposing color, first piece encountered is a pinned piece if bishop or queen.
                            if (white && piece > 20)
                            {
                                if (piece == (int)BLACK_QUEEN || piece == (int)BLACK_BISHOP)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            else if (!white && piece < 20)
                            {
                                if (piece == (int)WHITE_QUEEN || piece == (int)WHITE_BISHOP)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            encounteredFirst = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //this for loop checks second diagonal, goes towards top right
            encounteredFirst = false;
            for (int j = 1; j < 8; j++)
            {
                int piece = board[kingPos + j * 11];
                if (piece >= 0)
                {
                    if (piece > 0)
                    {
                        if (!encounteredFirst)
                        {
                            //if piece is of opposite color, no pinned pieces in this direction.
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                break;
                            }
                            //if piece is of same color, it may be a pinned piece
                            encounteredFirst = true;
                            pinnedLoc = kingPos + j * 11;
                        }
                        else
                        {
                            //if second piece is of opposing color, first piece encountered is a pinned piece if bishop or queen.
                            if (white && piece > 20)
                            {
                                if (piece == (int)BLACK_QUEEN || piece == (int)BLACK_BISHOP)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            else if (!white && piece < 20)
                            {
                                if (piece == (int)WHITE_QUEEN || piece == (int)WHITE_BISHOP)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            encounteredFirst = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //this for loop checks third diagonal, goes towards bottom left
            encounteredFirst = false;
            for (int j = 1; j < 8; j++)
            {
                int piece = board[kingPos + j * -11];
                if (piece >= 0)
                {
                    if (piece > 0)
                    {
                        if (!encounteredFirst)
                        {
                            //if piece is of opposite color, no pinned pieces in this direction.
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                break;
                            }
                            //if piece is of same color, it may be a pinned piece
                            encounteredFirst = true;
                            pinnedLoc = kingPos + j * -11;
                        }
                        else
                        {
                            //if second piece is of opposing color, first piece encountered is a pinned piece if bishop or queen.
                            if (white && piece > 20)
                            {
                                if (piece == (int)BLACK_QUEEN || piece == (int)BLACK_BISHOP)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            else if (!white && piece < 20)
                            {
                                if (piece == (int)WHITE_QUEEN || piece == (int)WHITE_BISHOP)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            encounteredFirst = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //this for loop checks fourth diagonal, goes towards bottom right
            encounteredFirst = false;
            for (int j = 1; j < 8; j++)
            {
                int piece = board[kingPos + j * -9];
                if (piece >= 0)
                {
                    if (piece > 0)
                    {
                        if (!encounteredFirst)
                        {
                            //if piece is of opposite color, no pinned pieces in this direction.
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                break;
                            }
                            //if piece is of same color, it may be a pinned piece
                            encounteredFirst = true;
                            pinnedLoc = kingPos + j * -9;
                        }
                        else
                        {
                            //if second piece is of opposing color, first piece encountered is a pinned piece if bishop or queen.
                            if (white && piece > 20)
                            {
                                if (piece == (int)BLACK_QUEEN || piece == (int)BLACK_BISHOP)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            else if (!white && piece < 20)
                            {
                                if (piece == (int)WHITE_QUEEN || piece == (int)WHITE_BISHOP)
                                {
                                    pieces.Add(pinnedLoc);
                                }
                            }
                            encounteredFirst = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return pieces;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public static IEnumerable<int> GenerateMove(BoardData boardData, bool qui, int PV)
        {
            yield return PV;
            boards = boardData;
            int?[] sortedMoves = new int?[200];
            int[] pieceLocations = boardData.PieceLocations;
            int[] board = boardData.Board;
            int numRook = 0, numBishop = 0, numKnight = 0, numPawn = 0, numNon = 0, kingpos = 0, index = 0;
            int loc = 0;
            bool white = boardData.WhiteTurn;
            if(white)
            {
                kingpos = pieceLocations[4];
                index = 8;
            }
            else
            {
                kingpos = pieceLocations[28];
                index = 16;
            }

            HashSet<int> pinnedPieces = GetPinnedPieces(board, kingpos, white);
            if(boardData.InCheck)
            {
                int attackers = GetAttackers(board, kingpos, white);
                //two attackers when attackers >= 1000
                if (attackers >= 1000)
                {
                    //king moves
                    if (white)
                    {
                        index = 4;
                    }
                    else
                    {
                        index = 28;
                    }
                    loc = pieceLocations[index];
                    if (loc != 0)
                    {
                        foreach (int move in GenerateKingMove(board, qui, pinnedPieces.Contains(loc), kingpos, loc, white))
                        {
                            int piece = board[move % 100];
                            if (white)
                            {
                                if (piece == (int)BLACK_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)BLACK_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)BLACK_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)BLACK_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)BLACK_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                            else
                            {
                                if (piece == (int)WHITE_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)WHITE_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)WHITE_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)WHITE_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)WHITE_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                        }
                    }
                }
                else if (attackers > 0)
                {
                    //for pawns
                    for (int i = index; i < index + 8; i++)
                    {
                        loc = pieceLocations[i];
                        if (loc != 0)
                        {
                            foreach (int move in GeneratePawnMoveCheck(board, pinnedPieces.Contains(loc), kingpos, loc, white, attackers))
                            {
                                int piece = board[move % 100];
                                if (white)
                                {
                                    if (piece == (int)BLACK_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)BLACK_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)BLACK_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)BLACK_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)BLACK_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                                else
                                {
                                    if (piece == (int)WHITE_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)WHITE_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)WHITE_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)WHITE_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)WHITE_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                            }
                        }
                    }
                    //knight moves
                    if (white)
                    {
                        index = 1;
                    }
                    else
                    {
                        index = 25;
                    }
                    for (int i = index; i < index + 10; i += 5)
                    {
                        loc = pieceLocations[i];
                        if (loc != 0)
                        {
                            foreach (int move in GenerateKnightMoveCheck(board, pinnedPieces.Contains(loc), kingpos, loc, white, attackers))
                            {
                                int piece = board[move % 100];
                                if (white)
                                {
                                    if (piece == (int)BLACK_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)BLACK_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)BLACK_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)BLACK_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)BLACK_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                                else
                                {
                                    if (piece == (int)WHITE_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)WHITE_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)WHITE_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)WHITE_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)WHITE_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                            }
                        }
                    }
                    //bishop moves
                    if (white)
                    {
                        index = 2;
                    }
                    else
                    {
                        index = 26;
                    }
                    for (int i = index; i < index + 6; i += 3)
                    {
                        loc = pieceLocations[i];
                        if (loc != 0)
                        {
                            foreach (int move in GenerateBishopMoveCheck(board, pinnedPieces.Contains(loc), kingpos, loc, white, attackers))
                            {
                                int piece = board[move % 100];
                                if (white)
                                {
                                    if (piece == (int)BLACK_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)BLACK_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)BLACK_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)BLACK_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)BLACK_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                                else
                                {
                                    if (piece == (int)WHITE_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)WHITE_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)WHITE_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)WHITE_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)WHITE_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                            }
                        }
                    }
                    //rook moves
                    if (white)
                    {
                        index = 0;
                    }
                    else
                    {
                        index = 24;
                    }
                    for (int i = index; i < index + 14; i += 7)
                    {
                        loc = pieceLocations[i];
                        if (loc != 0)
                        {
                            foreach (int move in GenerateRookMoveCheck(board, pinnedPieces.Contains(loc), kingpos, loc, white, attackers))
                            {
                                int piece = board[move % 100];
                                if (white)
                                {
                                    if (piece == (int)BLACK_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)BLACK_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)BLACK_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)BLACK_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)BLACK_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                                else
                                {
                                    if (piece == (int)WHITE_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)WHITE_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)WHITE_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)WHITE_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)WHITE_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                            }
                        }
                    }
                    //queen moves
                    int[] potentialQueens = new int[9];
                    int numQueens = 1;
                    if (white)
                    {
                        potentialQueens[0] = 3;
                        for (int y = 32; y < 32 + boards.numPromotions; y++)
                        {
                            if (board[boards.PieceLocations[y]] == (int)WHITE_QUEEN)
                            {
                                potentialQueens[numQueens++] = y;
                            }
                        }
                    }
                    else
                    {
                        potentialQueens[0] = 27;
                        for (int y = 32; y < 32 + boards.numPromotions; y++)
                        {
                            if (board[boards.PieceLocations[y]] == (int)BLACK_QUEEN)
                            {
                                potentialQueens[numQueens++] = y;
                            }
                        }
                    }
                    for (int r = 0; r < numQueens; r++)
                    {
                        loc = pieceLocations[potentialQueens[r]];
                        if (loc != 0)
                        {
                            foreach (int move in GenerateQueenMoveCheck(board, pinnedPieces.Contains(loc), kingpos, loc, white, attackers))
                            {
                                int piece = board[move % 100];
                                if (white)
                                {
                                    if (piece == (int)BLACK_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)BLACK_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)BLACK_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)BLACK_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)BLACK_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                                else
                                {
                                    if (piece == (int)WHITE_QUEEN)
                                    {
                                        yield return move;
                                    }
                                    else if (piece == (int)WHITE_ROOK)
                                    {
                                        sortedMoves[numRook++] = move;
                                    }
                                    else if (piece == (int)WHITE_BISHOP)
                                    {
                                        sortedMoves[10 + numBishop++] = move;
                                    }
                                    else if (piece == (int)WHITE_KNIGHT)
                                    {
                                        sortedMoves[20 + numKnight++] = move;
                                    }
                                    else if (piece == (int)WHITE_PAWN)
                                    {
                                        sortedMoves[30 + numPawn++] = move;
                                    }
                                    else
                                    {
                                        sortedMoves[60 + numNon++] = move;
                                    }
                                }
                            }
                        }
                    }
                    //king moves
                    if (white)
                    {
                        index = 4;
                    }
                    else
                    {
                        index = 28;
                    }
                    loc = pieceLocations[index];
                    if (loc != 0)
                    {
                        foreach (int move in GenerateKingMove(board, qui, pinnedPieces.Contains(loc), kingpos, loc, white))
                        {
                            int piece = board[move % 100];
                            if (white)
                            {
                                if (piece == (int)BLACK_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)BLACK_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)BLACK_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)BLACK_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)BLACK_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                            else
                            {
                                if (piece == (int)WHITE_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)WHITE_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)WHITE_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)WHITE_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)WHITE_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //check for castling
                bool addCastle = true;
                if (white)
                {
                    if (boardData.WKCastle)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (board[kingpos + i] != 0 || GetAttackers(board, kingpos + i, true) != 0)
                            {
                                addCastle = false;
                                break;
                            }
                        }
                        if (addCastle)
                        {
                            sortedMoves[60 + numNon++] = -10;
                        }
                    }
                    addCastle = true;
                    if (boardData.WQCastle)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (board[kingpos - i] != 0 || GetAttackers(board, kingpos - i, true) != 0)
                            {
                                addCastle = false;
                                break;
                            }
                        }
                        if (addCastle)
                        {
                            sortedMoves[60 + numNon++] = -11;
                        }
                    }
                }
                else
                {
                    if (boardData.BKCastle)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (board[kingpos + i] != 0 || GetAttackers(board, kingpos + i, false) != 0)
                            {
                                addCastle = false;
                                break;
                            }
                        }
                        if (addCastle)
                        {
                            sortedMoves[60 + numNon++] = -20;
                        }
                    }
                    addCastle = true;
                    if (boardData.BQCastle)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (board[kingpos - i] != 0 || GetAttackers(board, kingpos - i, false) != 0)
                            {
                                addCastle = false;
                                break;
                            }
                        }
                        if (addCastle)
                        {
                            sortedMoves[60 + numNon++] = -21;
                        }
                    }
                }
                //for pawns
                for (int i = index; i < index + 8; i++)
                {
                    loc = pieceLocations[i];
                    if (loc != 0)
                    {
                        foreach (int move in GeneratePawnMove(board, qui, pinnedPieces.Contains(loc), kingpos, loc, white))
                        {
                            int piece = board[move % 100];
                            if (white)
                            {
                                if (piece == (int)BLACK_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)BLACK_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)BLACK_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)BLACK_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)BLACK_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                            else
                            {
                                if (piece == (int)WHITE_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)WHITE_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)WHITE_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)WHITE_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)WHITE_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                        }
                    }
                }
                //knight moves
                if (white)
                {
                    index = 1;
                }
                else
                {
                    index = 25;
                }
                for (int i = index; i < index + 10; i += 5)
                {
                    loc = pieceLocations[i];
                    if (loc != 0)
                    {
                        foreach (int move in GenerateKnightMove(board, qui, pinnedPieces.Contains(loc), kingpos, loc, white))
                        {
                            int piece = board[move % 100];
                            if (white)
                            {
                                if (piece == (int)BLACK_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)BLACK_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)BLACK_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)BLACK_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)BLACK_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                            else
                            {
                                if (piece == (int)WHITE_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)WHITE_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)WHITE_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)WHITE_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)WHITE_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                        }
                    }
                }
                //bishop moves
                if (white)
                {
                    index = 2;
                }
                else
                {
                    index = 26;
                }
                for (int i = index; i < index + 6; i += 3)
                {
                    loc = pieceLocations[i];
                    if (loc != 0)
                    {
                        foreach (int move in GenerateBishopMove(board, qui, pinnedPieces.Contains(loc), kingpos, loc, white))
                        {
                            int piece = board[move % 100];
                            if (white)
                            {
                                if (piece == (int)BLACK_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)BLACK_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)BLACK_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)BLACK_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)BLACK_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                            else
                            {
                                if (piece == (int)WHITE_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)WHITE_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)WHITE_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)WHITE_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)WHITE_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                        }
                    }
                }
                //rook moves
                if (white)
                {
                    index = 0;
                }
                else
                {
                    index = 24;
                }
                for (int i = index; i < index + 14; i += 7)
                {
                    loc = pieceLocations[i];
                    if (loc != 0)
                    {
                        foreach (int move in GenerateRookMove(board, qui, pinnedPieces.Contains(loc), kingpos, loc, white))
                        {
                            int piece = board[move % 100];
                            if (white)
                            {
                                if (piece == (int)BLACK_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)BLACK_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)BLACK_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)BLACK_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)BLACK_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                            else
                            {
                                if (piece == (int)WHITE_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)WHITE_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)WHITE_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)WHITE_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)WHITE_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                        }
                    }
                }
                //queen moves
                int[] potentialQueens = new int[9];
                int numQueens = 1;
                if (white)
                {
                    potentialQueens[0] = 3;
                    for (int y = 32; y < 32 + boards.numPromotions; y++)
                    {
                        if (board[boards.PieceLocations[y]] == (int)WHITE_QUEEN)
                        {
                            potentialQueens[numQueens++] = y;
                        }
                    }
                }
                else
                {
                    potentialQueens[0] = 27;
                    for (int y = 32; y < 32 + boards.numPromotions; y++)
                    {
                        if (board[boards.PieceLocations[y]] == (int)BLACK_QUEEN)
                        {
                            potentialQueens[numQueens++] = y;
                        }
                    }
                }
                for (int r = 0; r < numQueens; r++)
                {
                    loc = pieceLocations[potentialQueens[r]];
                    if (loc != 0)
                    {
                        foreach (int move in GenerateQueenMove(board, qui, pinnedPieces.Contains(loc), kingpos, loc, white))
                        {
                            int piece = board[move % 100];
                            if (white)
                            {
                                if (piece == (int)BLACK_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)BLACK_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)BLACK_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)BLACK_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)BLACK_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                            else
                            {
                                if (piece == (int)WHITE_QUEEN)
                                {
                                    yield return move;
                                }
                                else if (piece == (int)WHITE_ROOK)
                                {
                                    sortedMoves[numRook++] = move;
                                }
                                else if (piece == (int)WHITE_BISHOP)
                                {
                                    sortedMoves[10 + numBishop++] = move;
                                }
                                else if (piece == (int)WHITE_KNIGHT)
                                {
                                    sortedMoves[20 + numKnight++] = move;
                                }
                                else if (piece == (int)WHITE_PAWN)
                                {
                                    sortedMoves[30 + numPawn++] = move;
                                }
                                else
                                {
                                    sortedMoves[60 + numNon++] = move;
                                }
                            }
                        }
                    }
                }
                //king moves
                if (white)
                {
                    index = 4;
                }
                else
                {
                    index = 28;
                }
                loc = pieceLocations[index];
                if (loc != 0)
                {
                    foreach (int move in GenerateKingMove(board, qui, pinnedPieces.Contains(loc), kingpos, loc, white))
                    {
                        int piece = board[move % 100];
                        if (white)
                        {
                            if (piece == (int)BLACK_QUEEN)
                            {
                                yield return move;
                            }
                            else if (piece == (int)BLACK_ROOK)
                            {
                                sortedMoves[numRook++] = move;
                            }
                            else if (piece == (int)BLACK_BISHOP)
                            {
                                sortedMoves[10 + numBishop++] = move;
                            }
                            else if (piece == (int)BLACK_KNIGHT)
                            {
                                sortedMoves[20 + numKnight++] = move;
                            }
                            else if (piece == (int)BLACK_PAWN)
                            {
                                sortedMoves[30 + numPawn++] = move;
                            }
                            else
                            {
                                sortedMoves[60 + numNon++] = move;
                            }
                        }
                        else
                        {
                            if (piece == (int)WHITE_QUEEN)
                            {
                                yield return move;
                            }
                            else if (piece == (int)WHITE_ROOK)
                            {
                                sortedMoves[numRook++] = move;
                            }
                            else if (piece == (int)WHITE_BISHOP)
                            {
                                sortedMoves[10 + numBishop++] = move;
                            }
                            else if (piece == (int)WHITE_KNIGHT)
                            {
                                sortedMoves[20 + numKnight++] = move;
                            }
                            else if (piece == (int)WHITE_PAWN)
                            {
                                sortedMoves[30 + numPawn++] = move;
                            }
                            else
                            {
                                sortedMoves[60 + numNon++] = move;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < sortedMoves.Length; i++)
            {
                int? move = sortedMoves[i];
                if (move == null)
                {
                    if(i < 10)
                    {
                        i = 9;
                    }
                    else if(i < 20)
                    {
                        i = 19;
                    }
                    else if(i < 30)
                    {
                        i = 29;
                    }
                    else if(i < 60)
                    {
                        i = 59;
                    }
                    else
                    {
                        yield break;
                    }
                }
                else
                {
                    yield return (int)move;
                }
            }
            yield break;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private static IEnumerable<int> GenerateBishopMove(int[] board, bool qui, bool isPinned, int kingpos, int loc, bool white)
        {
            if(isPinned)
            {
                int difference = kingpos-loc;
                if(difference%9 == 0)
                {
                    //this for loop checks first diagonal, goes towards top left
                    for (int j = 1; j < 8; j++)
                    {
                        int piece = board[loc + j * 9];
                        if (piece >= 0)
                        {
                            if (piece == 0)
                            {
                                if (!qui)
                                {
                                    yield return loc * 100 + loc + j * 9;
                                }
                            }
                            else
                            {
                                if (white && piece > 20 || !white && piece < 20)
                                {
                                    yield return loc * 100 + loc + j * 9;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    //this for loop checks fourth diagonal, goes towards bottom right
                    for (int j = 1; j < 8; j++)
                    {
                        int piece = board[loc + j * -9];
                        if (piece >= 0)
                        {
                            if (piece == 0)
                            {
                                if (!qui)
                                {
                                    yield return loc * 100 + loc + j * -9;
                                }
                            }
                            else
                            {
                                if (white && piece > 20 || !white && piece < 20)
                                {
                                    yield return loc * 100 + loc + j * -9;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if(difference%11 == 0)
                {
                    //this for loop checks second diagonal, goes towards top right
                    for (int j = 1; j < 8; j++)
                    {
                        int piece = board[loc + j * 11];
                        if (piece >= 0)
                        {
                            if (piece == 0)
                            {
                                if (!qui)
                                {
                                    yield return loc * 100 + loc + j * 11;
                                }
                            }
                            else
                            {
                                if (white && piece > 20 || !white && piece < 20)
                                {
                                    yield return loc * 100 + loc + j * 11;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    //this for loop checks third diagonal, goes towards bottom left
                    for (int j = 1; j < 8; j++)
                    {
                        int piece = board[loc + j * -11];
                        if (piece >= 0)
                        {
                            if (piece == 0)
                            {
                                if (!qui)
                                {
                                    yield return loc * 100 + loc + j * -11;
                                }
                            }
                            else
                            {
                                if (white && piece > 20 || !white && piece < 20)
                                {
                                    yield return loc * 100 + loc + j * -11;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                //this for loop checks first diagonal, goes towards top left
                for (int j = 1; j < 8; j++)
                {
                    int piece = board[loc + j * 9];
                    if (piece >= 0)
                    {
                        if (piece == 0)
                        {
                            if (!qui)
                            {
                                yield return loc * 100 + loc + j * 9;
                            }
                        }
                        else
                        {
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                yield return loc * 100 + loc + j * 9;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //this for loop checks second diagonal, goes towards top right
                for (int j = 1; j < 8; j++)
                {
                    int piece = board[loc + j * 11];
                    if (piece >= 0)
                    {
                        if (piece == 0)
                        {
                            if(!qui)
                            {
                                yield return loc * 100 + loc + j * 11;
                            }
                        }
                        else
                        {
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                yield return loc * 100 + loc + j * 11;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //this for loop checks third diagonal, goes towards bottom left
                for (int j = 1; j < 8; j++)
                {
                    int piece = board[loc + j * -11];
                    if (piece >= 0)
                    {
                        if (piece == 0)
                        {
                            if(!qui)
                            {
                                yield return loc * 100 + loc + j * -11;
                            }
                        }
                        else
                        {
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                yield return loc * 100 + loc + j * -11;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //this for loop checks fourth diagonal, goes towards bottom right
                for (int j = 1; j < 8; j++)
                {
                    int piece = board[loc + j * -9];
                    if (piece >= 0)
                    {
                        if (piece == 0)
                        {
                            if(!qui)
                            {
                                yield return loc * 100 + loc + j * -9;
                            }
                        }
                        else
                        {
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                yield return loc * 100 + loc + j * -9;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            yield break;
        }
        private static IEnumerable<int> GenerateRookMove(int[] board, bool qui, bool isPinned, int kingpos, int loc, bool white)
        {
            if (isPinned)
            {
                int r = loc / 10;
                int c = loc % 10;
                if(r == kingpos/10)
                {
                    //this for loop checks left
                    for (int j = 1; j < 8; j++)
                    {
                        int piece = board[loc - j];
                        if (piece >= 0)
                        {
                            if (piece == 0)
                            {
                                if (!qui)
                                {
                                    yield return loc * 100 + loc - j;
                                }
                            }
                            else
                            {
                                if (white && piece > 20 || !white && piece < 20)
                                {
                                    yield return loc * 100 + loc - j;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    //this for loop checks right
                    for (int j = 1; j < 8; j++)
                    {
                        int piece = board[loc + j];
                        if (piece >= 0)
                        {
                            if (piece == 0)
                            {
                                if (!qui)
                                {
                                    yield return loc * 100 + loc + j;
                                }
                            }
                            else
                            {
                                if (white && piece > 20 || !white && piece < 20)
                                {
                                    yield return loc * 100 + loc + j;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if (c == kingpos % 10)
                {
                    //this for loop checks upwards
                    for (int j = 1; j < 8; j++)
                    {
                        int piece = board[loc + j * 10];
                        if (piece >= 0)
                        {
                            if (piece == 0)
                            {
                                if (!qui)
                                {
                                    yield return loc * 100 + loc + j * 10;
                                }
                            }
                            else
                            {
                                if (white && piece > 20 || !white && piece < 20)
                                {
                                    yield return loc * 100 + loc + j * 10;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    //this for loop checks downward
                    for (int j = 1; j < 8; j++)
                    {
                        int piece = board[loc + j * -10];
                        if (piece >= 0)
                        {
                            if (piece == 0)
                            {
                                if (!qui)
                                {
                                    yield return loc * 100 + loc + j * -10;
                                }
                            }
                            else
                            {
                                if (white && piece > 20 || !white && piece < 20)
                                {
                                    yield return loc * 100 + loc + j * -10;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                //this for loop checks upwards
                for (int j = 1; j < 8; j++)
                {
                    int piece = board[loc + j * 10];
                    if (piece >= 0)
                    {
                        if (piece == 0)
                        {
                            if (!qui)
                            {
                                yield return loc * 100 + loc + j * 10;
                            }
                        }
                        else
                        {
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                yield return loc * 100 + loc + j * 10;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //this for loop checks downward
                for (int j = 1; j < 8; j++)
                {
                    int piece = board[loc + j * -10];
                    if (piece >= 0)
                    {
                        if (piece == 0)
                        {
                            if (!qui)
                            {
                                yield return loc * 100 + loc + j * -10;
                            }
                        }
                        else
                        {
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                yield return loc * 100 + loc + j * -10;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //this for loop checks left
                for (int j = 1; j < 8; j++)
                {
                    int piece = board[loc -j];
                    if (piece >= 0)
                    {
                        if (piece == 0)
                        {
                            if (!qui)
                            {
                                yield return loc * 100 + loc - j;
                            }
                        }
                        else
                        {
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                yield return loc * 100 + loc -j;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //this for loop checks right
                for (int j = 1; j < 8; j++)
                {
                    int piece = board[loc + j];
                    if (piece >= 0)
                    {
                        if (piece == 0)
                        {
                            if (!qui)
                            {
                                yield return loc * 100 + loc + j;
                            }
                        }
                        else
                        {
                            if (white && piece > 20 || !white && piece < 20)
                            {
                                yield return loc * 100 + loc + j;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            yield break;
        }
        private static IEnumerable<int> GenerateQueenMove(int[] board, bool qui, bool isPinned, int kingpos, int loc, bool white)
        {
            foreach(int i in GenerateBishopMove(board, qui, isPinned, kingpos, loc, white))
            {
                yield return i;
            }
            foreach (int i in GenerateRookMove(board, qui, isPinned, kingpos, loc, white))
            {
                yield return i;
            }
        }
        private static IEnumerable<int> GenerateKnightMove(int[] board, bool qui, bool isPinned, int kingpos, int loc, bool white)
        {
            if (isPinned)
            {
                yield break;
            }
            else
            {
                int piece = board[loc + 21];
                if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
                {
                    yield return loc * 100 + loc + 21;
                }

                piece = board[loc + 19];
                if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
                {
                    yield return loc * 100 + loc + 19;
                }

                piece = board[loc + 8];
                if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
                {
                    yield return loc * 100 + loc + 8;
                }

                piece = board[loc + 12];
                if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
                {
                    yield return loc * 100 + loc + 12;
                }

                piece = board[loc - 8];
                if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
                {
                    yield return loc * 100 + loc - 8;
                }

                piece = board[loc - 12];
                if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
                {
                    yield return loc * 100 + loc - 12;
                }

                piece = board[loc - 21];
                if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
                {
                    yield return loc * 100 + loc - 21;
                }

                piece = board[loc - 19];
                if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
                {
                    yield return loc * 100 + loc - 19;
                }
            }
        }
        private static IEnumerable<int> GeneratePawnMove(int[] board, bool qui, bool isPinned, int kingpos, int loc, bool white)
        {
            if (isPinned)
            {
                if(kingpos / 10 != loc / 10)
                {
                    if((kingpos - loc) % 9 == 0)
                    {
                        if (white)
                        {
                            int after = board[loc + 9];
                            if(after > 20)
                            {
                                yield return loc * 100 + loc + 9;
                            }
                            else if((loc + 9) == boards.Enpassant)
                            {
                                yield return (loc * 100 + loc + 9) * 100;
                            }
                        }
                        else
                        {
                            int after = board[loc - 9];
                            if (after / 10 == 1)
                            {
                                yield return loc * 100 + loc - 9;
                            }
                            else if ((loc - 9) == boards.Enpassant)
                            {
                                yield return (loc * 100 + loc - 9) * 100;
                            }
                        }
                    }
                    else if((kingpos - loc) % 11 == 0)
                    {
                        if (white)
                        {
                            int after = board[loc + 11];
                            if (after > 20)
                            {
                                yield return loc * 100 + loc + 11;
                            }
                            else if ((loc + 11) == boards.Enpassant)
                            {
                                yield return (loc * 100 + loc + 11) * 100;
                            }
                        }
                        else
                        {
                            int after = board[loc - 11];
                            if (after / 10 == 1)
                            {
                                yield return loc * 100 + loc - 11;
                            }
                            else if ((loc - 11) == boards.Enpassant)
                            {
                                yield return (loc * 100 + loc - 11) * 100;
                            }
                        }
                    }
                    else if ((kingpos - loc) % 10 == 0)
                    {
                        if (white)
                        {
                            int after = board[loc + 10];
                            if(!qui && after == 0)
                            {
                                yield return loc * 100 + loc + 10;
                                after = board[loc + 20];
                                if(!qui && loc / 10 == 3 && after == 0)
                                {
                                    yield return loc * 100 + loc + 20;
                                }
                            }
                        }
                        else
                        {
                            int after = board[loc - 10];
                            if (!qui && after == 0)
                            {
                                yield return loc * 100 + loc - 10;
                                after = board[loc - 20];
                                if (!qui && loc / 10 == 8 && after == 0)
                                {
                                    yield return loc * 100 + loc - 20;
                                }
                            }
                        }
                    }
                }
            }        
            else if(white)
            {
                int after = board[loc + 11];
                if(after > 20)
                {
                    yield return loc * 100 + loc + 11;
                }
                else if ((loc + 11) == boards.Enpassant)
                {
                    yield return (loc * 100 + loc + 11) * 100;
                }
                after = board[loc + 9];
                if(after > 20)
                {
                    yield return loc * 100 + loc + 9;
                }
                else if ((loc + 9) == boards.Enpassant)
                {
                    yield return (loc * 100 + loc + 9) * 100;
                }
                after = board[loc + 10];
                if((!qui && after == 0) || ((loc/10) == 8 && after == 0))
                {
                    yield return loc * 100 + loc + 10;
                    after = board[loc + 20];
                    if(!qui && loc / 10 == 3 && after == 0)
                    {
                        yield return loc * 100 + loc + 20;
                    }
                }
            }
            else
            {
                int after = board[loc - 9];
                if ((after > 0 && after < 20))
                {
                    yield return loc * 100 + loc - 9;
                }
                else if ((loc - 9) == boards.Enpassant)
                {
                    yield return (loc * 100 + loc - 9) * 100;
                }
                after = board[loc - 11];
                if ((after > 0 && after < 20))
                {
                    yield return loc * 100 + loc - 11;
                }
                else if ((loc - 11) == boards.Enpassant)
                {
                    yield return (loc * 100 + loc - 11) * 100;
                }
                after = board[loc - 10];
                if (!qui && after == 0 || ((loc/10) == 3 && after == 0))
                {
                    yield return loc * 100 + loc - 10;
                    after = board[loc - 20];
                    if (!qui && loc / 10 == 8 && after == 0)
                    {
                        yield return loc * 100 + loc - 20;
                    }
                }
            }
            yield break;
        }
        private static IEnumerable<int> GenerateKingMove(int[] board, bool qui, bool isPinned, int kingpos, int loc, bool white)
        {
            //uses loc instead of kingpos for consistency
            int piece = board[loc + 10];
            if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
            {
                if(!InCheck(board, loc+10, white))
                {
                    yield return loc * 100 + loc + 10;
                }                
            }
            piece = board[loc + 9];
            if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
            {
                if (!InCheck(board, loc+9, white))
                {
                    yield return loc * 100 + loc + 9;
                }
            }
            piece = board[loc + 11];
            if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
            {
                if (!InCheck(board, loc+11, white))
                {
                    yield return loc * 100 + loc + 11;
                }
            }
            piece = board[loc - 1];
            if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
            {
                if (!InCheck(board, loc-1, white))
                {
                    yield return loc * 100 + loc - 1;
                }
            }
            piece = board[loc + 1];
            if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
            {
                if (!InCheck(board, loc+1, white))
                {
                    yield return loc * 100 + loc + 1;
                }
            }
            piece = board[loc - 10];
            if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
            {
                if (!InCheck(board, loc-10, white))
                {
                    yield return loc * 100 + loc - 10;
                }
            }
            piece = board[loc - 9];
            if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
            {
                if (!InCheck(board, loc-9, white))
                {
                    yield return loc * 100 + loc - 9;
                }
            }
            piece = board[loc - 11];
            if ((white && piece > 20) || (!white && piece > 0 && piece < 20) || (!qui && piece == 0))
            {
                if (!InCheck(board, loc-11, white))
                {
                    yield return loc * 100 + loc - 11;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private static IEnumerable<int> GeneratePawnMoveCheck(int[] board, bool isPinned, int kingpos, int loc, bool white, int attacker)
        {
            if (isPinned)
            {
                yield break;
            }
            else
            {
                int ray = FindRayDirection(kingpos, attacker);
                if (white)
                {
                    if (loc + 9 == attacker || loc + 11 == attacker)
                    {
                        yield return loc * 100 + attacker;
                    }
                    if (ray == 0)
                    {
                        yield break;
                    }
                    else
                    {
                        if ((loc + 10 - attacker) % ray == 0 && board[loc + 10] == 0 &&
                            ((((loc + 10) > attacker) && ((loc + 10) < kingpos)) || (((loc + 10) <= attacker) && ((loc + 10) > kingpos))))
                        {
                            yield return loc * 100 + loc + 10;
                        }
                        if (loc / 10 == 3 && (loc + 20 - attacker) % ray == 0 && board[loc + 10] == 0 && board[loc + 20] == 0 &&
                                ((((loc + 20) > attacker) && ((loc + 20) < kingpos)) || (((loc + 20) <= attacker) && ((loc + 20) > kingpos))))
                        {
                            yield return loc * 100 + loc + 20;
                        }
                        if (loc + 9 == boards.Enpassant && (loc + 9 - attacker) % ray == 0 &&
                            ((((loc + 9) > attacker) && ((loc + 9) < kingpos)) || (((loc + 9) <= attacker) && ((loc + 9) > kingpos))))
                        {
                            yield return (loc * 100 + loc + 9) * 100;
                        }
                        if (loc + 11 == boards.Enpassant && (loc + 11 - attacker) % ray == 0 &&
                            ((((loc + 11) > attacker) && ((loc + 11) < kingpos)) || (((loc + 11) <= attacker) && ((loc + 11) > kingpos))))
                        {
                            yield return (loc * 100 + loc + 11) * 100;
                        }
                    }
                }
                else
                {
                    if (loc - 9 == attacker || loc - 11 == attacker)
                    {
                        yield return loc * 100 + attacker;
                    }
                    if (ray == 0)
                    {
                        yield break;
                    }
                    else
                    {
                        if ((loc - 10 - attacker) % ray == 0 && board[loc - 10] == 0 &&
                            ((((loc - 10) > attacker) && ((loc - 10) < kingpos)) || (((loc - 10) <= attacker) && ((loc - 10) > kingpos))))
                        {
                            yield return loc * 100 + loc - 10;
                        }
                        if (loc / 10 == 8 && (loc - 20 - attacker) % ray == 0 && board[loc - 10] == 0 && board[loc - 20] == 0 &&
                            ((((loc - 20) > attacker) && ((loc - 20) < kingpos)) || (((loc - 20) <= attacker) && ((loc - 20) > kingpos))))
                        {
                            yield return loc * 100 + loc - 20;
                        }
                        if (loc - 9 == boards.Enpassant && (loc - 9 - attacker) % ray == 0 &&
                            ((((loc - 9) > attacker) && ((loc - 9) < kingpos)) || (((loc - 9) <= attacker) && ((loc - 9) > kingpos))))
                        {
                            yield return (loc * 100 + loc - 9) * 100;
                        }
                        if (loc - 11 == boards.Enpassant && (loc - 11 - attacker) % ray == 0 &&
                            ((((loc - 11) > attacker) && ((loc - 11) < kingpos)) || (((loc - 11) <= attacker) && ((loc - 11) > kingpos))))
                        {
                            yield return (loc * 100 + loc - 11) * 100;
                        }
                    }
                }
            }
        }
        private static IEnumerable<int> GenerateKnightMoveCheck(int[] board, bool isPinned, int kingpos, int loc, bool white, int attacker)
        {
            if (isPinned)
            {
                yield break;
            }
            else
            {
                int ray = FindRayDirection(kingpos, attacker);
                if (ray == 0)
                {
                    if(loc + 21 == attacker || loc + 19 == attacker ||
                    loc + 8 == attacker || loc + 12 == attacker ||
                    loc - 8 == attacker || loc - 12 == attacker ||
                    loc - 21 == attacker || loc - 19 == attacker)
                    {
                        yield return loc * 100 + attacker;
                    }
                }
                else
                {
                    if ((loc + 21 - attacker) % ray == 0 &&
                        ((((loc + 21) >= attacker) && ((loc + 21) < kingpos)) || (((loc + 21) <= attacker) && ((loc + 21) > kingpos))))
                    {
                        yield return loc * 100 + loc + 21;
                    }
                    if(((loc + 19 - attacker) % ray == 0) &&
                        ((((loc + 19) >= attacker) && ((loc + 19) < kingpos)) || (((loc + 19) <= attacker) && ((loc + 19) > kingpos))))
                    {
                        yield return loc * 100 + loc + 19;
                    }
                    if ((loc + 8 - attacker) % ray == 0 &&
                        ((((loc+8) >= attacker) && ((loc+8) < kingpos)) || (((loc+8) <= attacker) && ((loc+8) > kingpos))))
                    {
                        yield return loc * 100 + loc + 8;
                    }
                    if ((loc + 12 - attacker) % ray == 0 &&
                        ((((loc + 12) >= attacker) && ((loc + 12) < kingpos)) || (((loc + 12) <= attacker) && ((loc + 12) > kingpos))))
                    {
                        yield return loc * 100 + loc + 12;
                    }
                    if ((loc - 8 - attacker) % ray == 0 &&
                        ((((loc - 8) >= attacker) && ((loc - 8) < kingpos)) || (((loc - 8) <= attacker) && ((loc - 8) > kingpos))))
                    {
                        yield return loc * 100 + loc - 8;
                    }
                    if ((loc - 12 - attacker) % ray == 0 &&
                        ((((loc - 12) >= attacker) && ((loc - 12) < kingpos)) || (((loc - 12) <= attacker) && ((loc - 12) > kingpos))))
                    {
                        yield return loc * 100 + loc - 12;
                    }
                    if ((loc - 21 - attacker) % ray == 0 &&
                        ((((loc - 21) >= attacker) && ((loc - 21) < kingpos)) || (((loc - 21) <= attacker) && ((loc - 21) > kingpos))))
                    {
                        yield return loc * 100 + loc - 21;
                    }
                    if ((loc - 19 - attacker) % ray == 0 &&
                        ((((loc - 19) >= attacker) && ((loc - 19) < kingpos)) || (((loc - 19) <= attacker) && ((loc - 19) > kingpos))))
                    {
                        yield return loc * 100 + loc - 19;
                    }
                }
            }
        }
        private static IEnumerable<int> GenerateBishopMoveCheck(int[] board, bool isPinned, int kingpos, int loc, bool white, int attacker)
        {
            if (isPinned)
            {
                yield break;
            }
            else
            {
                int counter = FindRayDirection(kingpos,attacker);
                if(counter == 0)
                {
                    if ((attacker - loc) % 9 == 0 || (attacker - loc) % 11 == 0)
                    {
                        if (CanMove(board, loc, attacker, FindRayDirection(attacker, loc)))
                        {
                            yield return loc * 100 + attacker;
                        }
                    }
                    yield break;
                }
                
                for (int i = attacker; i != kingpos; i+=counter)
                {
                    if((i-loc)%9 == 0 || (i-loc)%11 == 0)
                    {
                        if(CanMove(board, loc, i, FindRayDirection(i, loc)))
                        {
                            yield return loc * 100 + i;
                        }
                    }
                }
            }
        }
        private static IEnumerable<int> GenerateRookMoveCheck(int[] board, bool isPinned, int kingpos, int loc, bool white, int attacker)
        {
            if (isPinned)
            {
                yield break;
            }
            else
            {
                int counter = FindRayDirection(kingpos, attacker);
                if (counter == 0)
                {
                    if ((attacker - loc) % 10 == 0 || attacker/10 == loc/10)
                    {
                        if (CanMove(board, loc, attacker, FindRayDirection(attacker, loc)))
                        {
                            yield return loc * 100 + attacker;
                        }
                    }
                    yield break;
                }
                for (int i = attacker; i != kingpos; i += counter)
                {
                    if ((i - loc) % 10 == 0 || i / 10 == loc / 10)
                    {
                        if (CanMove(board, loc, i, FindRayDirection(i, loc)))
                        {
                            yield return loc * 100 + i;
                        }
                    }
                }
            }
        }
        private static IEnumerable<int> GenerateQueenMoveCheck(int[] board, bool isPinned, int kingpos, int loc, bool white, int attacker)
        {
            if (isPinned)
            {
                yield break;
            }
            else
            {
                foreach(int move in GenerateBishopMoveCheck(board,isPinned,kingpos,loc,white,attacker))
                {
                    yield return move;
                }
                foreach (int move in GenerateRookMoveCheck(board, isPinned, kingpos, loc, white, attacker))
                {
                    yield return move;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public static int FindRayDirection(int kingpos, int attacker)
        {
            int diff = kingpos - attacker;
            if(diff % 10 == 0)
            {
                return 10 * (diff / Math.Abs(diff));
            }
            else if(diff % 9 == 0)
            {
                return 9 * (diff / Math.Abs(diff));
            }
            else if(diff % 11 == 0)
            {
                return 11 * (diff / Math.Abs(diff));
            }
            else if(attacker/10 == kingpos/10)
            {
                return diff / Math.Abs(diff);
            }
            return 0;
        }
        private static bool CanMove(int[] board, int loc, int dest, int counter)
        {
            for(int i = loc + counter; i != dest; i += counter)
            {
                if(board[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
