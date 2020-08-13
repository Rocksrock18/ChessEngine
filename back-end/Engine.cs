using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ChessEngine
{
    public class Engine
    {
        //4 numbers representing move
        private int bestMove;
        //true when debugging
        private bool DEBUG;
        //transposition tables for evaluations and PV
        private Dictionary<ulong, int> ttew;
        private Dictionary<ulong, int> ttpvw;
        private Dictionary<ulong, int> tteb;
        private Dictionary<ulong, int> ttpvb;

        //needed for draw by repetition
        private Dictionary<ulong, int> dbrw;
        private Dictionary<ulong, int> dbrb;
        public bool DBR;
        private Timer timer;

        public int numNodes;
        public bool stop;
        public int totalDepth;
        public bool isMate;
        public bool inWindow;
        //board data
        BoardData board;

        public Engine(BoardData board1)
        {
            board = board1;

            ttew = new Dictionary<ulong, int>();
            ttpvw = new Dictionary<ulong, int>();
            tteb = new Dictionary<ulong, int>();
            ttpvb = new Dictionary<ulong, int>();

            dbrw = new Dictionary<ulong, int>();
            dbrb = new Dictionary<ulong, int>();
            //add starting position
            dbrw.Add(board.key, 1);
            dbrb.Add(board.key, 1);
            DBR = false;

            bestMove = -1;
            numNodes = 0;
            stop = false;
            isMate = false;
            inWindow = false;
            DEBUG = false;
            totalDepth = 0;
        }
        public int GetBestMove(int depth, int time)
        {
            SetTimer(time);
            int score = CalculateBestMove(depth, false);
            Console.WriteLine("\nComputer Evaluation: " + score);
            Console.WriteLine("Move Suggested: " + bestMove);
            Console.WriteLine("Max Depth Reached: " + totalDepth);
            totalDepth = 0;
            UpdateMate(score);
            return bestMove;
        }

        private void SetTimer(int time)
        {
            stop = false;
            timer = new Timer(time);
            timer.Elapsed += FinishComputation;
            timer.Enabled = true;
        }

        public int PerformBestMove(int depth, int time)
        {
            SetTimer(time);
            Conversion c = new Conversion();
            int score = CalculateBestMove(depth, true);
            Console.WriteLine("\nComputer Evaluation: " + score);//stockfish scale: 209
            Console.WriteLine("Move Performed: " + c.NumToMove(bestMove));
            Console.WriteLine("Max Depth Reached: " + totalDepth);
            totalDepth = 0;
            board.MakeMove(bestMove, 0);
            ulong key = board.key;
            if (!board.WhiteTurn)
            {
                DBR = AddPositionW(key);
            }
            else
            {
                DBR = AddPositionB(key);
            }
            CheckForMate();
            return bestMove;
        }

        public void FinishComputation(Object source, ElapsedEventArgs e)
        {
            stop = true;
            ResetTimer();
        }

        public void ResetTimer()
        {
            timer.Stop();
        }

        public void DoMove(int move)
        {
            board.MakeMove(move, 0);
            ulong key = board.key;
            if (!board.WhiteTurn)
            {
                DBR = AddPositionW(key);
            }
            else
            {
                DBR = AddPositionB(key);
            }
            CheckForMate();
        }

        public void CheckForMate()
        {
            if(board.InCheck)
            {
                int numMoves = 0;
                foreach (int move in MoveGen.GenerateMove(board, false, 0))
                {
                    if (move == 0)
                    {
                        continue;
                    }
                    numMoves++;
                }
                if (numMoves == 0)
                {
                    isMate = true;
                }
            }
        }

        public void UpdateMate(int score)
        {
            if (score == int.MaxValue || score == int.MinValue)
            {
                int numMoves = 0;
                foreach (int move in MoveGen.GenerateMove(board, false, 0))
                {
                    if (move == 0)
                    {
                        continue;
                    }
                    numMoves++;
                }
                if (numMoves == 0)
                {
                    isMate = true;
                }
            }
        }

        private int CalculateBestMove(int depth, bool output)
        {
            Conversion c = new Conversion();
            if (board.NumPieces() < 8)
            {
                String mv = EndGameTableBase();
                bestMove = c.MoveToNum(mv, board);
                return 0;
            }
            int score = 0;
            numNodes = 0;
            for (int i = 1; i <= depth; i++)
            {
                inWindow = false;
                totalDepth = 0;
                if (board.WhiteTurn)
                {
                    if (i == 1)
                    {
                        score = AlphaBetaMax(int.MinValue, int.MaxValue, i, i);
                    }
                    else
                    {
                        int alpha = score;
                        int beta = score;
                        do
                        {
                            alpha -= 50;
                            beta += 50;
                            score = AlphaBetaMax(alpha, beta, i, i);
                        } while (!inWindow && score != int.MinValue && score != int.MaxValue);
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        score = AlphaBetaMin(int.MinValue, int.MaxValue, i, i);
                    }
                    else
                    {
                        int alpha = score;
                        int beta = score;
                        do
                        {
                            alpha -= 50;
                            beta += 50;
                            score = AlphaBetaMin(alpha, beta, i, i);
                        } while (!inWindow && score != int.MinValue && score != int.MaxValue);
                    }
                }
                if (output)
                {
                    Console.WriteLine(c.NumToMove(bestMove) + " " + i + " " + score);
                }
                totalDepth += i;
                if (score == int.MinValue || score == int.MaxValue || stop)
                {
                    break;
                }
            }
            ttew = new Dictionary<ulong, int>();
            ttpvw = new Dictionary<ulong, int>();
            tteb = new Dictionary<ulong, int>();
            ttpvb = new Dictionary<ulong, int>();
            ResetTimer();
            return score;
        }

        private int AlphaBetaMax(int alpha, int beta, int depthLeft, int initialDepth)
        {
            if (depthLeft == 0)
            {
                int score = QuiescenceMax(alpha, beta, 0);
                return score;
            }
            numNodes++;
            //uncomment when debugging
            MoveGen.numNodes++;
            ulong currentKey = board.key;
            //draw by repetition
            if (initialDepth != depthLeft && AddPositionB(currentKey))
            {
                return 0;
            }
            //null move pruning
            if (depthLeft == initialDepth - 1 && !board.InCheck && !board.endGame)
            {
                //try null move
                board.MakeMove(-3, 0);
                int score = AlphaBetaMin(alpha, beta, 0, initialDepth);
                board.ReverseMove(-3, 0);
                //if null move finds cutoff, its assumed another move will as well
                if (score >= beta)
                {
                    RemPositionB(currentKey);
                    return beta;
                }
                if (score > alpha)
                {
                    alpha = score;
                }
            }
            //set up PV
            int PV = 0;
            int nodeBestMove = 0;
            int nodeBestScore = 0;
            if (ttpvw.ContainsKey(currentKey))
            {
                PV = ttpvw[currentKey];
            }
            foreach(int move in MoveGen.GenerateMove(board,false,PV))
            {
                //no PV
                if (move == 0)
                {
                    continue;
                }
                int captured = board.MakeMove(move, 0);
                int score = AlphaBetaMin(alpha, beta, depthLeft - 1, initialDepth);
                board.ReverseMove(move, captured);
                if (nodeBestMove == 0 || nodeBestScore < score)
                {
                    nodeBestMove = move;
                    nodeBestScore = score;
                }
                if (score >= beta)
                {
                    if (initialDepth == depthLeft)
                    {
                        bestMove = move;
                    }
                    else
                    {
                        RemPositionB(currentKey);
                    }
                    UpdatePVW(currentKey, nodeBestMove);
                    if (score == int.MaxValue)
                    {
                        return score;
                    }
                    return beta;
                }
                if(score > alpha)
                {
                    if (initialDepth == depthLeft)
                    {
                        inWindow = true;
                        bestMove = move;
                    }
                    alpha = score;
                }
            }
            if (initialDepth != depthLeft)
            {
                RemPositionB(currentKey);
            }
            UpdatePVW(currentKey, nodeBestMove);
            //no legal moves
            if (nodeBestMove == 0)
            {
                if (board.InCheck)
                {
                    return int.MinValue;
                }
                else
                {
                    return 0;
                }
            }
            //checkmate found
            if (nodeBestScore == int.MinValue)
            {
                return int.MinValue;
            }
            return alpha;
        }

        private int AlphaBetaMin(int alpha, int beta, int depthLeft, int initialDepth)
        {
            if (depthLeft == 0)
            {
                int score = QuiescenceMin(alpha, beta, 0);
                return score;
            }
            numNodes++;
            MoveGen.numNodes++;
            ulong currentKey = board.key;
            //draw by repetition
            if (initialDepth != depthLeft && AddPositionW(currentKey))
            {
                return 0;
            }
            //null move pruning
            if (depthLeft == initialDepth - 1 && !board.InCheck && !board.endGame)
            {
                //try null move
                board.MakeMove(-3, 0);
                int score = AlphaBetaMax(alpha, beta, 0, initialDepth);
                board.ReverseMove(-3, 0);
                //if null move causes cutoff, its assumed another move will also

                if (score <= alpha)
                {
                    RemPositionW(currentKey);
                    return alpha;
                }
                if (score < beta)
                {
                    beta = score;
                }
            }

            //set up PV
            int PV = 0;
            int nodeBestMove = 0;
            int nodeBestScore = 0;
            if (ttpvb.ContainsKey(currentKey))
            {
                PV = ttpvb[currentKey];
            }
            foreach (int move in MoveGen.GenerateMove(board,false,PV))
            {
                //no PV
                if (move == 0)
                {
                    continue;
                }
                int captured = board.MakeMove(move, 0);
                int score = AlphaBetaMax(alpha, beta, depthLeft - 1, initialDepth);
                board.ReverseMove(move, captured);
                if (nodeBestMove == 0 || nodeBestScore > score)
                {
                    nodeBestMove = move;
                    nodeBestScore = score;
                }
                if (score <= alpha)
                {
                    if (initialDepth == depthLeft)
                    {
                        bestMove = move;
                    }
                    else
                    {
                        RemPositionW(currentKey);
                    }
                    UpdatePVB(currentKey, nodeBestMove);
                    if (score == int.MinValue)
                    {
                        return score;
                    }
                    return alpha;
                }
                if(score < beta)
                {
                    if (initialDepth == depthLeft)
                    {
                        inWindow = true;
                        bestMove = move;
                    }
                    beta = score;
                }
            }
            if(initialDepth != depthLeft)
            {
                RemPositionW(currentKey);
            }
            UpdatePVB(currentKey, nodeBestMove);
            //no legal moves
            if (nodeBestMove == 0)
            {
                if(board.InCheck)
                {
                    return int.MaxValue;
                }
                else
                {
                    return 0;
                }
            }
            if(nodeBestScore == int.MaxValue)
            {
                return int.MaxValue;
            }
            return beta;
        }

        private int QuiescenceMax(int alpha, int beta, int extraDepth)
        {
            numNodes++;
            MoveGen.numNodes++;
            if(extraDepth > totalDepth)
            {
                totalDepth = extraDepth;
            }
            int standPat;
            ulong key = board.key;
            if(ttew.ContainsKey(key))
            {
                standPat = ttew[key];
            }
            else
            {
                standPat = Eval.Evaluate(board);
                UpdateTTW(key, standPat);
            }
            if (standPat >= beta)
            {
                return beta;
            }
            if (standPat > alpha)
            {
                alpha = standPat;
            }
            int score = 0;
            foreach (int move in MoveGen.GenerateMove(board, true, 0))
            {
                if (move == 0)
                {
                    continue;
                }
                int captured = board.MakeMove(move, 0);
                score = QuiescenceMin(alpha, beta, extraDepth++);
                board.ReverseMove(move, captured);
                if (score >= beta)
                {
                    return beta;
                }
                if (score > alpha)
                {
                    alpha = score;
                }
            }
            return alpha;
        }

        private int QuiescenceMin(int alpha, int beta, int extraDepth)
        {
            numNodes++;
            MoveGen.numNodes++;
            if (extraDepth > totalDepth)
            {
                totalDepth = extraDepth;
            }
            int standPat;
            ulong key = board.key;
            if (tteb.ContainsKey(key))
            {
                standPat = tteb[key];
            }
            else
            {
                standPat = Eval.Evaluate(board);
                UpdateTTB(key, standPat);
            }
            if (standPat <= alpha)
            {
                return alpha;
            }
            if (standPat < beta)
            {
                beta = standPat;
            }
            int score = 0;
            foreach (int move in MoveGen.GenerateMove(board, true, 0))
            {
                if (move == 0)
                {
                    continue;
                }
                int captured = board.MakeMove(move, 0);
                score = QuiescenceMax(alpha, beta, extraDepth++);
                board.ReverseMove(move, captured);
                if (score <= alpha)
                {
                    return alpha;
                }
                if (score < beta)
                {
                    beta = score;
                }
            }
            return beta;
        }

        private void UpdateTTW(ulong key, int evaluation)
        {
            ttew.Add(key, evaluation);
        }

        private void UpdatePVW(ulong key, int pv)
        {
            if (ttpvw.ContainsKey(key))
            {
                ttpvw[key] = pv;
            }
            else
            {
                ttpvw.Add(key, pv);
            }
        }

        private void UpdateTTB(ulong key, int evaluation)
        {
            tteb.Add(key, evaluation);
        }

        private void UpdatePVB(ulong key, int pv)
        {
            if (ttpvb.ContainsKey(key))
            {
                ttpvb[key] = pv;
            }
            else
            {
                ttpvb.Add(key, pv);
            }
        }

        private bool AddPositionW(ulong key)
        {
            if(dbrw.ContainsKey(key))
            {
                if (dbrw[key] == 2)
                {
                    return true;
                }
                dbrw[key] = dbrw[key] + 1;
            }
            else
            {
                dbrw.Add(key, 1);
            }
            return false;
        }

        private bool AddPositionB(ulong key)
        {
            if (dbrb.ContainsKey(key))
            {
                if (dbrb[key] == 2)
                {
                    return true;
                }
                dbrb[key] = dbrb[key] + 1;
            }
            else
            {
                dbrb.Add(key, 1);
            }
            return false;
        }

        private void RemPositionW(ulong key)
        {
            if (dbrw[key] == 1)
            {
                dbrw.Remove(key);
            }
            else
            {
                dbrw[key] = dbrw[key] - 1;
            }
        }

        private void RemPositionB(ulong key)
        {
            if (dbrb[key] == 1)
            {
                dbrb.Remove(key);
            }
            else
            {
                dbrb[key] = dbrb[key] - 1;
            }
        }

        private string EndGameTableBase()
        {
            string fen = ParseFen();
            var html = @"https://syzygy-tables.info/?fen=";
            html += fen;
            Console.WriteLine(html);
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//body/div[@class='right-side']/div/section");

            string moves = node.OuterHtml;
            string search = "data-uci=\"";

            moves = moves.Substring(moves.IndexOf(search) + search.Length);

            string move = moves.Substring(0, 4);

            return move;
        }
        private string ParseFen()
        {
            string fen = board.GetFen();
            fen = fen.Replace(' ', '_');
            return fen;
        }
    }
}
