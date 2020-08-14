using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static ChessEngine.Conversion.Pieces;
using static ChessEngine.Conversion.Squares;

namespace ChessEngine
{
    public class BoardData
    {
        //size 120 2 row buffers north and south, 1 col buffer east and west
        //actual Board from 21 to 98
        public int[] Board { get; private set; }

        //size 32, starting pieces: a1-h1, a2-h2, a7-h7, a8-h8, 0 if they don't exist
        //promotions occupy spaces 32-48
        public int[] PieceLocations { get; private set; }

        //true equals whites turn
        public bool WhiteTurn { get; private set; }

        //accounts for direct checks and discovered checks
        public bool InCheck { get; private set; }
        public Stack<bool> PrevMoveCheck { get; private set; }

        //castle rights, true equals able to castle
        public bool WKCastle { get; private set; }
        public bool WQCastle { get; private set; }
        public bool BKCastle { get; private set; }
        public bool BQCastle { get; private set; }

        //tracks number of moves made by each rook and king. Used for determining castling privelages.
        public int[] NumKingRookMoves { get; private set; }

        //square location of available Enpassant capture, 0 otherwise
        public int Enpassant { get; private set; }
        public Stack<int> EnPassantHistory { get; private set; }

        //keeps track of indexes for captured peices, in order for which they were captured
        public Stack<int> capturedIndex;

        //tracks info for pawn promotions
        public Stack<bool> PrevMovePromotion { get; private set; }
        public Stack<int> PrevIndexPromotion { get; private set; }
        public int numPromotions;

        //tracks history of moves
        public Stack<int> MoveHistory { get; private set; }

        //current key of board
        public Zobrist z;
        public ulong key;

        //denotes if board is in end game or not
        public bool endGame;

        public BoardData()
        {
            Board = new int[120];
            for (int i = 0; i < Board.Length; i++)
            {
                if (i < 21 || i > 98 || i % 10 == 0 || i % 10 == 9)
                {
                    Board[i] = -1;
                }
                else
                {
                    Board[i] = 0;
                }
            }
            Board[21] = (int)WHITE_ROOK;
            Board[22] = (int)WHITE_KNIGHT;
            Board[23] = (int)WHITE_BISHOP;
            Board[24] = (int)WHITE_QUEEN;
            Board[25] = (int)WHITE_KING;
            Board[26] = (int)WHITE_BISHOP;
            Board[27] = (int)WHITE_KNIGHT;
            Board[28] = (int)WHITE_ROOK;
            Board[31] = (int)WHITE_PAWN;
            Board[32] = (int)WHITE_PAWN;
            Board[33] = (int)WHITE_PAWN;
            Board[34] = (int)WHITE_PAWN;
            Board[35] = (int)WHITE_PAWN;
            Board[36] = (int)WHITE_PAWN;
            Board[37] = (int)WHITE_PAWN;
            Board[38] = (int)WHITE_PAWN;

            Board[91] = (int)BLACK_ROOK;
            Board[92] = (int)BLACK_KNIGHT;
            Board[93] = (int)BLACK_BISHOP;
            Board[94] = (int)BLACK_QUEEN;
            Board[95] = (int)BLACK_KING;
            Board[96] = (int)BLACK_BISHOP;
            Board[97] = (int)BLACK_KNIGHT;
            Board[98] = (int)BLACK_ROOK;
            Board[81] = (int)BLACK_PAWN;
            Board[82] = (int)BLACK_PAWN;
            Board[83] = (int)BLACK_PAWN;
            Board[84] = (int)BLACK_PAWN;
            Board[85] = (int)BLACK_PAWN;
            Board[86] = (int)BLACK_PAWN;
            Board[87] = (int)BLACK_PAWN;
            Board[88] = (int)BLACK_PAWN;

            //32 pieces, potential for 16 promotions
            PieceLocations = new int[48];
            int index = 21;
            for(int j = 0; j < 32; j++)
            {
                if(index == 41)
                {
                    index += 40;
                }
                PieceLocations[j] = index;
                if(index % 10 == 8)
                {
                    index += 3;
                }
                else
                {
                    index++;
                }
                
            }
            for(int k = 32; k < 48; k++)
            {
                PieceLocations[k] = 0;
            }

            WhiteTurn = true;

            InCheck = false;
            PrevMoveCheck = new Stack<bool>();
            PrevMoveCheck.Push(InCheck);

            WKCastle = true;
            WQCastle = true;
            BKCastle = true;
            BQCastle = true;

            Enpassant = 0;
            EnPassantHistory = new Stack<int>();
            EnPassantHistory.Push(0);

            NumKingRookMoves = new int[6];
            for(int k = 0; k < 6; k++)
            {
                NumKingRookMoves[k] = 0;
            }

            capturedIndex = new Stack<int>();

            PrevMovePromotion = new Stack<bool>();
            numPromotions = 0;
            PrevIndexPromotion = new Stack<int>();

            MoveHistory = new Stack<int>();

            z = new Zobrist();
            key = z.Hash(this);

            endGame = false;
    }

        public BoardData(String fen)
        {
            Board = new int[120];
            fen = fen.Replace("/", "");

            for (int i = 0; i < Board.Length; i++)
            {
                if (i < 21 || i > 98 || i % 10 == 0 || i % 10 == 9)
                {
                    Board[i] = -1;
                }
                else
                {
                    Board[i] = 0;
                }
            }

            PieceLocations = new int[48];
            int wPawns = 0;
            int bPawns = 0;
            int wKnights = 0;
            int bKnights = 0;
            int wBishops = 0;
            int bBishops = 0;
            int wRooks = 0;
            int bRooks = 0;
            int count = 91;
            bool white_queen_found = false;
            bool black_queen_found = false;
            int num_extra_queens = 0;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (Char.IsDigit(fen[(r * 8) + c]))
                    {
                        String replace = "";

                        for (int j = 0; j < (int)Char.GetNumericValue(fen[(r * 8) + c]); j++)
                        {
                            Board[count + c] = 0;
                            replace += "+";
                        }

                        int place = fen.IndexOf(fen[(r * 8) + c]);
                        fen = fen.Remove(place, 1).Insert(place, replace);

                        c += (int)Char.GetNumericValue(fen[(r * 8) + c]);
                    }
                    else if (fen[(r * 8) + c] == 'P')
                    {
                        Board[count + c] = (int)WHITE_PAWN;
                        PieceLocations[8 + wPawns] = count + c;
                        wPawns++;
                    }
                    else if (fen[(r * 8) + c] == 'p')
                    {
                        Board[count + c] = (int)BLACK_PAWN;
                        PieceLocations[16 + bPawns] = count + c;
                        bPawns++;
                    }
                    else if (fen[(r * 8) + c] == 'N')
                    {
                        Board[count + c] = (int)WHITE_KNIGHT;
                        PieceLocations[1 + (wKnights * 5)] = count + c;
                        wKnights++;
                    }
                    else if (fen[(r * 8) + c] == 'n')
                    {
                        Board[count + c] = (int)BLACK_KNIGHT;
                        PieceLocations[25 + (bKnights * 5)] = count + c;
                        bKnights++;
                    }
                    else if (fen[(r * 8) + c] == 'B')
                    {
                        Board[count + c] = (int)WHITE_BISHOP;
                        PieceLocations[2 + (wBishops * 3)] = count + c;
                        wBishops++;
                    }
                    else if (fen[(r * 8) + c] == 'b')
                    {
                        Board[count + c] = (int)BLACK_BISHOP;
                        PieceLocations[26 + (bBishops * 3)] = count + c;
                        bBishops++;
                    }
                    else if (fen[(r * 8) + c] == 'R')
                    {
                        Board[count + c] = (int)WHITE_ROOK;
                        PieceLocations[0 + (wRooks * 7)] = count + c;
                        wRooks++;
                    }
                    else if (fen[(r * 8) + c] == 'r')
                    {
                        Board[count + c] = (int)BLACK_ROOK;
                        PieceLocations[24 + (bRooks * 7)] = count + c;
                        bRooks++;
                    }
                    else if (fen[(r * 8) + c] == 'Q')
                    {
                        Board[count + c] = (int)WHITE_QUEEN;
                        if (!white_queen_found)
                        {
                            PieceLocations[3] = count + c;
                            white_queen_found = true;
                        }
                        else
                        {
                            PieceLocations[num_extra_queens++] = count + c;
                        }
                    }
                    else if (fen[(r * 8) + c] == 'q')
                    {
                        Board[count + c] = (int)BLACK_QUEEN;
                        if(!black_queen_found)
                        {
                            PieceLocations[27] = count + c;
                            black_queen_found = true;
                        }
                        else
                        {
                            PieceLocations[num_extra_queens++] = count + c;
                        }
                    }
                    else if (fen[(r * 8) + c] == 'K')
                    {
                        Board[count + c] = (int)WHITE_KING;
                        PieceLocations[4] = count + c;
                    }
                    else if (fen[(r * 8) + c] == 'k')
                    {
                        Board[count + c] = (int)BLACK_KING;
                        PieceLocations[28] = count + c;
                    }
                    else
                    {
                        Board[count + c] = 0;
                    }
                }
                count -= 10;
            }

            WhiteTurn = (fen.IndexOf('w') != -1);

            if(WhiteTurn)
            {
                InCheck = MoveGen.InCheck(Board, PieceLocations[4], true);
            }
            else
            {
                InCheck = MoveGen.InCheck(Board, PieceLocations[28], false);
            }
            PrevMoveCheck = new Stack<bool>();
            PrevMoveCheck.Push(InCheck);

            fen = ConcatFen(fen);
            if (fen.Substring(fen.Length - 1).IndexOf('-') != -1)
            {
                Enpassant = 0;
            }
            else
            {
                String letter = fen.Substring(fen.Length - 2, 1);
                String number = fen.Substring(fen.Length - 1, 1);

                Enpassant = 10 * (1 + Int32.Parse(number));

                if (letter.Equals("a"))
                {
                    Enpassant += 1;
                }
                else if (letter.Equals("b"))
                {
                    Enpassant += 2;
                }
                else if (letter.Equals("c"))
                {
                    Enpassant += 3;
                }
                else if (letter.Equals("d"))
                {
                    Enpassant += 4;
                }
                else if (letter.Equals("e"))
                {
                    Enpassant += 5;
                }
                else if (letter.Equals("f"))
                {
                    Enpassant += 6;
                }
                else if (letter.Equals("g"))
                {
                    Enpassant += 7;
                }
                else if (letter.Equals("h"))
                {
                    Enpassant += 8;
                }
            }
            EnPassantHistory = new Stack<int>();
            EnPassantHistory.Push(Enpassant);

            WKCastle = (fen.Substring(fen.Length - 6).IndexOf('K') != -1);
            WQCastle = (fen.Substring(fen.Length - 6).IndexOf('Q') != -1);
            BKCastle = (fen.Substring(fen.Length - 6).IndexOf('k') != -1);
            BQCastle = (fen.Substring(fen.Length - 6).IndexOf('q') != -1);

            NumKingRookMoves = new int[6];

            NumKingRookMoves[0] = (WQCastle ? 0 : 1);
            NumKingRookMoves[1] = (Board[25] == (int)WHITE_KING ? 0 : 1);
            NumKingRookMoves[2] = (WKCastle ? 0 : 1);
            NumKingRookMoves[3] = (BQCastle ? 0 : 1);
            NumKingRookMoves[4] = (Board[95] == (int)BLACK_KING ? 0 : 1);
            NumKingRookMoves[5] = (BKCastle ? 0 : 1);

            CorrectRookLocations();


            capturedIndex = new Stack<int>();

            PrevMovePromotion = new Stack<bool>();
            numPromotions = num_extra_queens;
            PrevIndexPromotion = new Stack<int>();

            MoveHistory = new Stack<int>();

            z = new Zobrist();
            key = z.Hash(this);

            UpdateEnd();
        }

        private string ConcatFen(string oldFen)
        {
            string fen = oldFen;
            int numSpaces = 0;
            int extraChars = 0;
            while(numSpaces < 2)
            {
                extraChars++;
                string s = fen.Substring(fen.Length - extraChars, 1);
                if(s.Equals(" "))
                {
                    numSpaces++;
                }
            }
            fen = fen.Substring(0, fen.Length - extraChars);
            return fen;
        }

        public string GetFen()
        {
            string fen = "";
            int numZeros = 0;
            for (int i = 91; i != 29; i++)
            {
                if (i % 10 == 9)
                {
                    i -= 18;
                    if(numZeros > 0)
                    {
                        fen = fen + numZeros;
                        numZeros = 0;
                    }
                    fen = fen + "/";
                }
                int piece = Board[i];
                if(piece == 0)
                {
                    numZeros++;
                    continue;
                }
                if(numZeros > 0)
                {
                    fen = fen + numZeros;
                    numZeros = 0;
                }
                fen = fen + ConvertNumToPiece(piece);
            }
            if (numZeros > 0)
            {
                fen = fen + numZeros;
            }
            fen += " " + (WhiteTurn ? "w" : "b");
            return fen;
        }

        private string ConvertNumToPiece(int num)
        {
            switch(num)
            {
                case 11:
                    return "P";
                case 12:
                    return "N";
                case 13:
                    return "B";
                case 14:
                    return "R";
                case 15:
                    return "Q";
                case 16:
                    return "K";
                case 21:
                    return "p";
                case 22:
                    return "n";
                case 23:
                    return "b";
                case 24:
                    return "r";
                case 25:
                    return "q";
                case 26:
                    return "k";
            }
            return "";
        }

        public int NumPieces()
        {
            int numPieces = 0;
            for (int i = 21; i <= 98; i++)
            {
                if (i % 10 == 9)
                {
                    i += 2;
                }
                if(Board[i] != 0)
                {
                    numPieces++;
                }
            }
            return numPieces;
        }

        private void CorrectRookLocations()
        {
            if (NumKingRookMoves[2] == 0)
            {
                if(PieceLocations[7] != 28)
                {
                    int oldLoc = PieceLocations[7];
                    PieceLocations[7] = PieceLocations[0];
                    PieceLocations[0] = oldLoc;
                }
            }

            if (NumKingRookMoves[5] == 0)
            {
                if (PieceLocations[31] != 98)
                {
                    int oldLoc = PieceLocations[31];
                    PieceLocations[31] = PieceLocations[24];
                    PieceLocations[24] = oldLoc;
                }
            }
        }

        public void DisplayBoard()
        {
            int index = 91;
            Console.WriteLine("  +---+---+---+---+---+---+---+---+");
            while(index > 20)
            {
                if (index % 10 == 1)
                {
                    Console.Write((index-10) / 10 + " |");
                }
                int piece = Board[index];
                if(piece == 0)
                {
                    Console.Write("   |");
                }
                else
                {
                    Console.Write(" " + Enum.GetName(typeof(Conversion.BoardRep), piece) + " |");
                }
                if(index%10 == 8)
                {
                    index -= 17;
                    Console.WriteLine();
                    Console.WriteLine("  +---+---+---+---+---+---+---+---+");
                }
                else
                {
                    index++;
                }
            }
            Console.WriteLine("    a   b   c   d   e   f   g   h");
            Console.WriteLine("Key: " + key);
        }

        public int MakeMove(int move, int captured)
        {
            //-3 is flag for null move
            if (move != -3)
            {
                MoveHistory.Push(move);
                //flags for castling. -10 = WKC, -11 = WQC, -20 = BKC, -21 = BQC
                if (move <= -10)
                {
                    HandleCastleMove(move);
                    return 0;
                }
                //denotes an enpassant capture
                else if(move >= 100000)
                {
                    return HandleEnPassant(move);
                }
                else
                {
                    int beforeSquare = move / 100;
                    int afterSquare = move % 100;
                    int piece = Board[beforeSquare];
                    int retCapture = Board[afterSquare];
                    UpdateBoardData(beforeSquare, afterSquare, piece, retCapture);
                    return retCapture;
                }
            }
            WhiteTurn = !WhiteTurn;
            Enpassant = 0;
            EnPassantHistory.Push(0);
            return 0;
        }

        public void ReverseMove(int move, int captured)
        {
            if(move != -3)
            {
                MoveHistory.Pop();
                //flags for castling. -10 = WKC, -11 = WQC, -20 = BKC, -21 = BQC
                if (move <= -10)
                {
                    HandleReverseCastleMove(move);
                }
                //en passant
                else if (move >= 100000)
                {
                    HandleReverseEnPassant(move);
                }
                else
                {
                    int beforeSquare = move % 100;
                    int afterSquare = move / 100;
                    int piece = Board[beforeSquare];
                    ReverseBoardData(beforeSquare, afterSquare, piece, captured);
                }
            }
            else
            {
                WhiteTurn = !WhiteTurn;
                EnPassantHistory.Pop();
                Enpassant = EnPassantHistory.Peek();
            }
        }

        private void ReverseBoardData(int beforeSquare, int afterSquare, int piece, int captured)
        {
            //update Board
            Board[beforeSquare] = captured;

            //check for pawn promotion
            bool promotion = PrevMovePromotion.Pop();
            if (promotion)
            {
                PieceLocations[PrevIndexPromotion.Pop()] = afterSquare;
                numPromotions--;
                PieceLocations[32 + numPromotions] = 0;
                if(WhiteTurn)
                {
                    Board[afterSquare] = (int)BLACK_PAWN;
                }
                else
                {
                    Board[afterSquare] = (int)WHITE_PAWN;
                }
            }
            else
            {
                Board[afterSquare] = piece;
                //updating piece locations
                for (int i = 0; i < 48; i++)
                {
                    if (PieceLocations[i] == beforeSquare)
                    {
                        PieceLocations[i] = afterSquare;
                        //handles castling
                        switch (i)
                        {
                            case 0:
                                NumKingRookMoves[0] = NumKingRookMoves[0] - 1;
                                break;
                            case 4:
                                NumKingRookMoves[1] = NumKingRookMoves[1] - 1;
                                break;
                            case 7:
                                NumKingRookMoves[2] = NumKingRookMoves[2] - 1;
                                break;
                            case 24:
                                NumKingRookMoves[3] = NumKingRookMoves[3] - 1;
                                break;
                            case 28:
                                NumKingRookMoves[4] = NumKingRookMoves[4] - 1;
                                break;
                            case 31:
                                NumKingRookMoves[5] = NumKingRookMoves[5] - 1;
                                break;
                        }

                        break;
                    }
                }
            }

            //update PieceLocations if a piece had been captured previously
            if (captured != 0)
            {
                int index = capturedIndex.Pop();
                PieceLocations[index] = beforeSquare;
                //handles castling (reverse rook capture requires a decrement to the number of moves it has made; castle flags are updated later)
                switch (index)
                {
                    case 0:
                        NumKingRookMoves[0] = NumKingRookMoves[0] - 1;
                        break;
                    case 7:
                        NumKingRookMoves[2] = NumKingRookMoves[2] - 1;
                        break;
                    case 24:
                        NumKingRookMoves[3] = NumKingRookMoves[3] - 1;
                        break;
                    case 31:
                        NumKingRookMoves[5] = NumKingRookMoves[5] - 1;
                        break;
                }
            }

            //update castle booleans
            UpdateCastle();

            //update Enpassant
            EnPassantHistory.Pop();
            Enpassant = EnPassantHistory.Peek();

            //update check
            PrevMoveCheck.Pop();
            InCheck = PrevMoveCheck.Peek();

            //switch turns
            WhiteTurn = !WhiteTurn;

            //update end game state
            UpdateEnd();

            //update hash of board
            if (promotion)
            {
                key = z.Hash(this);
            }
            else
            {
                key = z.ReverseUpdateHash(key, afterSquare, beforeSquare, captured, piece);
            }

            //VerifyBoard();
        }

        private void UpdateBoardData(int beforeSquare, int afterSquare, int piece, int captured)
        {
            //previous square becomes empty
            Board[beforeSquare] = 0;

            //put proper piece in new square
            bool promotion = false;
            if (piece == (int)WHITE_PAWN && afterSquare / 10 == 9)
            {
                promotion = true;
                for (int i = 8; i < 16; i++)
                {
                    if(PieceLocations[i] == beforeSquare)
                    {
                        PrevIndexPromotion.Push(i);
                        PieceLocations[32 + numPromotions] = afterSquare;
                        PieceLocations[i] = 0;
                        numPromotions++;
                    }
                }
                Board[afterSquare] = (int)WHITE_QUEEN;
            }
            else if (piece == (int)BLACK_PAWN && afterSquare / 10 == 2)
            {
                promotion = true;
                for (int i = 16; i < 24; i++)
                {
                    if (PieceLocations[i] == beforeSquare)
                    {
                        PrevIndexPromotion.Push(i);
                        PieceLocations[32 + numPromotions] = afterSquare;
                        PieceLocations[i] = 0;
                        numPromotions++;
                    }
                }
                Board[afterSquare] = (int)BLACK_QUEEN;
            }
            else
            {
                Board[afterSquare] = piece;
            }
            PrevMovePromotion.Push(promotion);

            //update piece location of captured piece, if applicable
            if (captured != 0)
            {
                for (int i = 0; i < 48; i++)
                {
                    if (PieceLocations[i] == afterSquare)
                    {
                        capturedIndex.Push(i);
                        PieceLocations[i] = 0;
                        //handles castling (treating a rook capture as a rook move ensures castling flags are set properly)
                        switch (i)
                        {
                            case 0:
                                NumKingRookMoves[0] = NumKingRookMoves[0] + 1;
                                break;
                            case 7:
                                NumKingRookMoves[2] = NumKingRookMoves[2] + 1;
                                break;
                            case 24:
                                NumKingRookMoves[3] = NumKingRookMoves[3] + 1;
                                break;
                            case 31:
                                NumKingRookMoves[5] = NumKingRookMoves[5] + 1;
                                break;
                        }
                        break;
                    }
                }
            }

            //update piece location of moving piece
            for (int i = 0; i < 48; i++)
            {
                if(PieceLocations[i] == beforeSquare)
                {
                    PieceLocations[i] = afterSquare;
                    //handles castling
                    switch (i)
                    {
                        case 0:
                            NumKingRookMoves[0] = NumKingRookMoves[0] + 1;
                            break;
                        case 4:
                            NumKingRookMoves[1] = NumKingRookMoves[1] + 1;
                            break;
                        case 7:
                            NumKingRookMoves[2] = NumKingRookMoves[2] + 1;
                            break;
                        case 24:
                            NumKingRookMoves[3] = NumKingRookMoves[3] + 1;
                            break;
                        case 28:
                            NumKingRookMoves[4] = NumKingRookMoves[4] + 1;
                            break;
                        case 31:
                            NumKingRookMoves[5] = NumKingRookMoves[5] + 1;
                            break;
                    }
                    break;
                }
            }

            //update castle booleans
            UpdateCastle();

            //update Enpassant
            Enpassant = EnpassantSquare(beforeSquare, afterSquare, piece);
            EnPassantHistory.Push(Enpassant);

            //update check
            UpdateCheck(beforeSquare, afterSquare, piece);
            PrevMoveCheck.Push(InCheck);

            //switch turns
            WhiteTurn = !WhiteTurn;
            UpdateEnd();

            //update hash for board
            if (promotion)
            {
                key = z.Hash(this);
            }
            else
            {
                key = z.UpdateHash(key, afterSquare, beforeSquare, captured, piece);
            }
            
            //VerifyBoard();
        }

        private static int EnpassantSquare(int beforeSquare, int afterSquare, int piece)
        {
            if (piece == (int)WHITE_PAWN || piece == (int)BLACK_PAWN)
            {
                if (Math.Abs(beforeSquare / 10 - afterSquare / 10) == 2)
                {
                    return (beforeSquare / 10 + afterSquare / 10) * 5 + beforeSquare % 10;
                }
            }
            return 0;
        }

        private void HandleCastleMove(int move)
        {
            if(move == -10)
            {
                WKCastle = false;
                WQCastle = false;
                //white king
                PieceLocations[4] = 27;
                //white kingside rook
                PieceLocations[7] = 26;
                Board[27] = (int)WHITE_KING;
                Board[26] = (int)WHITE_ROOK;
                Board[25] = 0;
                Board[28] = 0;
                NumKingRookMoves[1] = NumKingRookMoves[1] + 1;
                NumKingRookMoves[2] = NumKingRookMoves[2] + 1;
            }
            else if(move == -11)
            {
                WQCastle = false;
                WKCastle = false;
                //white king
                PieceLocations[4] = 23;
                //white queenside rook
                PieceLocations[0] = 24;
                Board[23] = (int)WHITE_KING;
                Board[24] = (int)WHITE_ROOK;
                Board[25] = 0;
                Board[21] = 0;
                NumKingRookMoves[1] = NumKingRookMoves[1] + 1;
                NumKingRookMoves[0] = NumKingRookMoves[0] + 1;
            }
            else if (move == -20)
            {
                BKCastle = false;
                BQCastle = false;
                //black king
                PieceLocations[28] = 97;
                //black kingside rook
                PieceLocations[31] = 96;
                Board[97] = (int)BLACK_KING;
                Board[96] = (int)BLACK_ROOK;
                Board[95] = 0;
                Board[98] = 0;
                NumKingRookMoves[4] = NumKingRookMoves[4] + 1;
                NumKingRookMoves[5] = NumKingRookMoves[5] + 1;
            }
            else if (move == -21)
            {
                BQCastle = false;
                BKCastle = false;
                //black king
                PieceLocations[28] = 93;
                //black queenside rook
                PieceLocations[24] = 94;
                Board[93] = (int)BLACK_KING;
                Board[94] = (int)BLACK_ROOK;
                Board[95] = 0;
                Board[91] = 0;
                NumKingRookMoves[4] = NumKingRookMoves[4] + 1;
                NumKingRookMoves[3] = NumKingRookMoves[3] + 1;
            }

            Enpassant = 0;
            EnPassantHistory.Push(Enpassant);

            //castling is special case; regular UpdateCheck method will not work
            if (WhiteTurn)
            {
                InCheck = MoveGen.InCheck(Board, PieceLocations[28], false);
            }
            else
            {
                InCheck = MoveGen.InCheck(Board, PieceLocations[4], true);
            }
            PrevMoveCheck.Push(InCheck);
            WhiteTurn = !WhiteTurn;

            UpdateEnd();

            key = z.Hash(this);

            //VerifyBoard();
        }

        private void HandleReverseCastleMove(int move)
        {
            if (move == -10)
            {
                //white king
                PieceLocations[4] = 25;
                //white kingside rook
                PieceLocations[7] = 28;
                Board[25] = (int)WHITE_KING;
                Board[28] = (int)WHITE_ROOK;
                Board[27] = 0;
                Board[26] = 0;
                NumKingRookMoves[1] = NumKingRookMoves[1] - 1;
                NumKingRookMoves[2] = NumKingRookMoves[2] - 1;
            }
            else if (move == -11)
            {
                //white king
                PieceLocations[4] = 25;
                //white queenside rook
                PieceLocations[0] = 21;
                Board[25] = (int)WHITE_KING;
                Board[21] = (int)WHITE_ROOK;
                Board[23] = 0;
                Board[24] = 0;
                NumKingRookMoves[1] = NumKingRookMoves[1] - 1;
                NumKingRookMoves[0] = NumKingRookMoves[0] - 1;
            }
            else if (move == -20)
            {
                //black king
                PieceLocations[28] = 95;
                //black kingside rook
                PieceLocations[31] = 98;
                Board[95] = (int)BLACK_KING;
                Board[98] = (int)BLACK_ROOK;
                Board[97] = 0;
                Board[96] = 0;
                NumKingRookMoves[4] = NumKingRookMoves[4] - 1;
                NumKingRookMoves[5] = NumKingRookMoves[5] - 1;
            }
            else if (move == -21)
            {
                //black king
                PieceLocations[28] = 95;
                //black queenside rook
                PieceLocations[24] = 91;
                Board[95] = (int)BLACK_KING;
                Board[91] = (int)BLACK_ROOK;
                Board[93] = 0;
                Board[94] = 0;
                NumKingRookMoves[4] = NumKingRookMoves[4] - 1;
                NumKingRookMoves[3] = NumKingRookMoves[3] - 1;
            }

            //update castle booleans
            UpdateCastle();

            EnPassantHistory.Pop();
            Enpassant = EnPassantHistory.Peek();

            PrevMoveCheck.Pop();
            InCheck = PrevMoveCheck.Peek();

            WhiteTurn = !WhiteTurn;

            UpdateEnd();

            key = z.Hash(this);

            //VerifyBoard();
        }

        private int HandleEnPassant(int move)
        {
            //gets rid of flag used to denote enpassant
            move /= 100;

            //update Board
            Board[move % 100] = Board[move / 100];
            Board[move/100] = 0;
            PrevMovePromotion.Push(false);

            int captured = 0;
            int afterSquare = 0;
            int beforeSquare = move / 100;
            if (WhiteTurn)
            {
                afterSquare = move % 100 - 10;
                captured = Board[afterSquare];
            }
            else
            {
                afterSquare = move % 100 + 10;
                captured = Board[afterSquare];
            }

            //remove pawn captured via enpassant
            Board[afterSquare] = 0;

            //updating piece locations

            for (int i = 8; i < 24; i++)
            {
                //goes here if capturing a piece
                if (PieceLocations[i] == afterSquare)
                {
                    capturedIndex.Push(i);
                    PieceLocations[i] = 0;
                    break;
                }
            }

            for (int i = 8; i < 24; i++)
            {
                if (PieceLocations[i] == beforeSquare)
                {
                    PieceLocations[i] = move % 100;
                    break;
                }
            }
            
            Enpassant = 0;
            EnPassantHistory.Push(Enpassant);

            //instead of 2 rays, enpassant needs to check 3 rays to properly determine if king is in check
            UpdateCheckEnPassant(move / 100, move % 100, afterSquare, Board[move % 100]);
            PrevMoveCheck.Push(InCheck);

            WhiteTurn = !WhiteTurn;

            UpdateEnd();

            key = z.Hash(this);

            //VerifyBoard();

            return captured;
        }

        private void HandleReverseEnPassant(int move)
        {
            //gets rid of enpassant flag
            move /= 100;

            //update Board
            Board[move / 100] = Board[move % 100];
            Board[move % 100] = 0;

            PrevMovePromotion.Pop();

            int afterSquare = move % 100;
            int beforeSquare = 0;

            if (WhiteTurn)
            {
                beforeSquare = move % 100 + 10;
                Board[beforeSquare] = 11;
            }
            else
            {
                beforeSquare = move % 100 - 10;
                Board[beforeSquare] = 21;
            }

            //updating piece locations

            for (int i = 8; i < 24; i++)
            {
                //goes here if capturing a piece
                if (PieceLocations[i] == afterSquare)
                {
                    PieceLocations[i] = move / 100;
                    break;
                }
            }

            //restore pawn captured via enpassant
            int index = capturedIndex.Pop();
            PieceLocations[index] = beforeSquare;

            //update Enpassant
            EnPassantHistory.Pop();
            Enpassant = EnPassantHistory.Peek();

            PrevMoveCheck.Pop();
            InCheck = PrevMoveCheck.Peek();

            WhiteTurn = !WhiteTurn;

            UpdateEnd();

            key = z.Hash(this);

            //VerifyBoard();
        }

        public void UpdateEnd()
        {
            endGame = PieceLocations[3] == 0 && PieceLocations[27] == 0 && CannotCastle();
        }

        private void UpdateCastle()
        {
            WQCastle = (NumKingRookMoves[0] == 0 && NumKingRookMoves[1] == 0);
            WKCastle = (NumKingRookMoves[2] == 0 && NumKingRookMoves[1] == 0);
            BQCastle = (NumKingRookMoves[3] == 0 && NumKingRookMoves[4] == 0);
            BKCastle = (NumKingRookMoves[5] == 0 && NumKingRookMoves[4] == 0);
        }

        private bool CannotCastle()
        {
            return !WKCastle && !WQCastle && !BKCastle && !BQCastle;
        }

        public void UpdateCheckEnPassant(int beforeSquare, int afterSquare, int capturedSquare, int piece)
        {
            UpdateCheck(beforeSquare, afterSquare, piece);
            if(!InCheck)
            {
                UpdateCheck(capturedSquare, afterSquare, piece);
            }

        }

        public void UpdateCheck(int beforeSquare, int afterSquare, int piece)
        {
            int ray;
            int kingpos;
            if(WhiteTurn)
            {
                kingpos = PieceLocations[28];
                ray = MoveGen.FindRayDirection(kingpos, afterSquare);
                InCheck = (ray == 0 ? CanAttackB(Board[afterSquare]%10, afterSquare - kingpos, 0) : BlackUnderAttack(kingpos, ray));
                //look for discovered check
                if(!InCheck)
                {
                    ray = MoveGen.FindRayDirection(kingpos, beforeSquare);
                    InCheck = (ray == 0 ? false : BlackUnderAttack(kingpos, ray));
                }
            }
            else
            {
                kingpos = PieceLocations[4];
                ray = MoveGen.FindRayDirection(kingpos, afterSquare);
                InCheck = (ray == 0 ? CanAttackW(Board[afterSquare]%10, afterSquare - kingpos, 0) : WhiteUnderAttack(kingpos, ray));
                //look for discovered check
                if (!InCheck)
                {
                    ray = MoveGen.FindRayDirection(kingpos, beforeSquare);
                    InCheck = (ray == 0 ? false : WhiteUnderAttack(kingpos, ray));
                }
            }
        }

        public bool WhiteUnderAttack(int loc, int ray)
        {
            int og_loc = loc;
            int piece;
            while (loc > 20 && loc < 99)
            {
                loc -= ray;
                piece = Board[loc];
                if(piece != 0)
                {
                    if (piece == -1 || (piece > 10 && piece < 20))
                    {
                        return false;
                    }
                    return CanAttackW(piece % 10, loc - og_loc, ray);
                }
            }
            return false;
        }

        public bool BlackUnderAttack(int loc, int ray)
        {
            int og_loc = loc;
            int piece;
            while (loc > 20 && loc < 99)
            {
                loc -= ray;
                piece = Board[loc];
                if(piece != 0)
                {
                    if (piece == -1 || piece > 20)
                    {
                        return false;
                    }
                    return CanAttackB(piece % 10, loc - og_loc, ray);
                }
            }
            return false;
        }

        public bool CanAttackW(int attacker, int diff, int ray)
        {
            switch (ray)
            {
                case -9:
                    return (attacker == 3) || (attacker == 5) || (attacker == 1 && diff == 9);
                case 9:
                    return (attacker == 3) || (attacker == 5);
                case -11:
                    return (attacker == 3) || (attacker == 5) || (attacker == 1 && diff == 11);
                case 11:
                    return (attacker == 3) || (attacker == 5);
                case 10:
                    return (attacker == 4) || (attacker == 5);
                case -10:
                    return (attacker == 4) || (attacker == 5);
                case 1:
                    return (attacker == 4) || (attacker == 5);
                case -1:
                    return (attacker == 4) || (attacker == 5);
                case 0:
                    return attacker == 2 && (Math.Abs(diff) == 8 || Math.Abs(diff) == 12 || Math.Abs(diff) == 19 || Math.Abs(diff) == 21);
            }
            return false;
        }

        public bool CanAttackB(int attacker, int diff, int ray)
        {
            switch (ray)
            {
                case 9:
                    return (attacker == 3) || (attacker == 5) || (attacker == 1 && diff == -9);
                case -9:
                    return (attacker == 3) || (attacker == 5);
                case -11:
                    return (attacker == 3) || (attacker == 5);
                case 11:
                    return (attacker == 3) || (attacker == 5) || (attacker == 1 && diff == -11);
                case 10:
                    return (attacker == 4) || (attacker == 5);
                case -10:
                    return (attacker == 4) || (attacker == 5);
                case 1:
                    return (attacker == 4) || (attacker == 5);
                case -1:
                    return (attacker == 4) || (attacker == 5);
                case 0:
                    return attacker == 2 && (Math.Abs(diff) == 8 || Math.Abs(diff) == 12 || Math.Abs(diff) == 19 || Math.Abs(diff) == 21);
            }
            return false;
        }

        //debugger methods
        private void VerifyBoard()
        {
            if(WhiteTurn)
            {
                if(PieceLocations[4] == 0)
                {
                    //broken
                    Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                    Console.WriteLine("Error: White King is not on board");
                    DisplayBoard();
                    Console.ReadKey();
                }
                if(InCheck != MoveGen.InCheck(Board, PieceLocations[4], true))
                {
                    //broken
                    Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                    Console.WriteLine("Error: InCheck Property of board does not match result of MoveGen");
                    DisplayBoard();
                    Console.ReadKey();
                }
            }
            else
            {
                if (PieceLocations[28] == 0)
                {
                    //broken
                    Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                    Console.WriteLine("Error: Black King is not on board");
                    DisplayBoard();
                    Console.ReadKey();
                }
                if (InCheck != MoveGen.InCheck(Board, PieceLocations[28], false))
                {
                    //broken
                    Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                    Console.WriteLine("Error: InCheck Property of board does not match result of MoveGen");
                    DisplayBoard();
                    Console.ReadKey();
                }
            }
            ulong properKey = z.Hash(this);
            if(properKey != key)
            {
                //broken
                Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                Console.WriteLine("Error: Zobrist key of board does not match Zobrist key that was stored");
                DisplayBoard();
                Console.ReadKey();
            }
            HashSet<int> occupiedSquares = new HashSet<int>();
            for(int i = 0; i < 32+numPromotions; i++)
            {
                if(PieceLocations[i] != 0)
                {
                    if(occupiedSquares.Contains(PieceLocations[i]))
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Two pieces share the square: " + PieceLocations[i]);
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    else
                    {
                        occupiedSquares.Add(PieceLocations[i]);
                    }
                }
            }
            for (int i = 21; i < 99; i++)
            {
                if(i%10 == 9)
                {
                    i += 2;
                }
                int piece = Board[i];
                if(piece != 0)
                {
                    VerifyPiece(piece, i);
                }
            }
        }

        private void VerifyPiece(int piece, int square)
        {
            switch (piece)
            {
                case 11:
                    if(PieceLocations[8] == square || PieceLocations[9] == square || PieceLocations[10] == square || PieceLocations[11] == square ||
                       PieceLocations[12] == square || PieceLocations[13] == square || PieceLocations[14] == square || PieceLocations[15] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 12:
                    if (PieceLocations[1] == square || PieceLocations[6] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 13:
                    if (PieceLocations[2] == square || PieceLocations[5] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 14:
                    if (PieceLocations[0] == square || PieceLocations[7] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 15:
                    bool isPromotedPiece = false;
                    for (int i = 32; i < 32+numPromotions; i++)
                    {
                        if (PieceLocations[i] == square)
                        {
                            isPromotedPiece = true;
                            break;
                        }
                    }
                    if (PieceLocations[3] == square || isPromotedPiece)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 16:
                    if (PieceLocations[4] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 21:
                    if (PieceLocations[16] == square || PieceLocations[17] == square || PieceLocations[18] == square || PieceLocations[19] == square ||
                       PieceLocations[20] == square || PieceLocations[21] == square || PieceLocations[22] == square || PieceLocations[23] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 22:
                    if (PieceLocations[25] == square || PieceLocations[30] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 23:
                    if (PieceLocations[26] == square || PieceLocations[29] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 24:
                    if (PieceLocations[24] == square || PieceLocations[31] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 25:
                    bool isPromotedPieceB = false;
                    for (int i = 32; i < 32 + numPromotions; i++)
                    {
                        if (PieceLocations[i] == square)
                        {
                            isPromotedPieceB = true;
                            break;
                        }
                    }
                    if (PieceLocations[27] == square || isPromotedPieceB)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
                case 26:
                    if (PieceLocations[28] == square)
                    {
                        //valid
                    }
                    else
                    {
                        //broken
                        Console.WriteLine("Board Broke at node: " + MoveGen.numNodes);
                        Console.WriteLine("Error: Board claims piece code: " + piece + " is on square " + square + "," +
                                          "but there is no corresponding piece in PieceLocations.");
                        DisplayBoard();
                        Console.ReadKey();
                    }
                    break;
            }
        }

    }
}
