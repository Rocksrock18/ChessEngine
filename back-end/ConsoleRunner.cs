using System;
using System.Collections.Generic;
using ChessEngine;
using static ChessEngine.Conversion.Pieces;

namespace ChessEngineRunner
{
    class ConsoleRunner
    {
        static void Main(string[] args)
        {

            /*
             TO DO:

            Chess Essentials:
             [DONE] Pawn Promotion
             [DONE] Castling
             [DONE] En Passant
             [DONE] Draw by repetition
            
            Speed Enhancers:
             [DONE] Transposition Tables (Zobrist Hashing)
             [DONE] Principle Variation
             [DONE] Aspiration Windows
             [DONE] Null Move Pruning (disable towards end game to avoid zugzwang positions)

            Accuracy Enhancers:
             [DONE] "Hole" detection and bonuses if knight occupies hole
             [DONE] Space (based on pawn structure)
             [DONE] Bonuses for rooks on open or semi open files
             [DONE] Bonuses for bishops that are on same color as opposing pawn, opposite color of friendly pawn
             [DONE] Deductions for poor king safety prior to end game
             [DONE] Deductions for weak pawns

            General Purpose:
             [DONE] Change engine to have a time limit as well as a depth limit (stops at whichever comes first)
             [DONE] Add Syzgy Table bases
             [DONE] End game detection & King bonus square adjustments in end game
             [DONE] Bug Fixes (run full game with VerifyBoard enabled in BoardData to look for bugs)
             [DONE] Optimizations (all but move gen have been optimized)

            */

            //for testing
            //String test = "2B5/PKP5/2P5/8/8/8/k7/8 w - - 0 1";
            //starting position
            String test = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq h8 0 1";


            BoardData board = new BoardData(test);
            Conversion c = new Conversion();

            board.DisplayBoard();

            Engine com = new Engine(board);

            int depth = 8;
            int stop_time = 4000;

            /*
            //---------------------------------------------------------------------------------------------------------------------------
            //uncomment this if you want to perform an efficiency test. Tip: More tests = more accurate results
            
            double numTests = 10000;
            var timer1 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < numTests; i++)
            {
                com.GetBestMove(1, 1000);
            }
            timer1.Stop();
            double time1 = timer1.ElapsedMilliseconds;
            Console.WriteLine("\nTime taken: " + time1 + "ms, or an average of " + numTests * 1000 / time1 + " calls per second.");

            //% of total time is under the assumption that the time taken for the computer to choose a move is 5 seconds
            //Only accurate if numTests is close to the amount of times the tested operation is performed when choosing a move
            Console.WriteLine("Roughly " + (time1 * 100 / 5000) + "% of total time taken.");
            Console.WriteLine();
            
            //---------------------------------------------------------------------------------------------------------------------------
            */

            //numMoves is a necessary precaution, as there is currently no detection for draw by lack of material
            int numMoves = 0;

            //this while loop simulates a chess game.
            while (numMoves < 300)
            {
                Console.WriteLine("Enter a command: ");
                String command = Console.ReadLine();
                //h command denotes a "human" move; simply plays the move entered
                //format: "h " + [beforeSquare] + [afterSquare] + [Enpassant Flag]"
                //Enpassant flag = (isEnPassantMove ? [any character] : [null])
                if(command.Substring(0,1).Equals("h"))
                {
                    Console.WriteLine("\nMove number: " + ++numMoves);
                    string hmove = command.Substring(2, command.Length - 2);
                    com.DoMove(c.MoveToNum(hmove, board));
                }
                //c command denotes a "computer" move; computer calculates and plays move for whichever player's turn it is
                else if(command.Equals("c"))
                {
                    Console.WriteLine("\n\nThinking... ");
                    var timer3 = System.Diagnostics.Stopwatch.StartNew();
                    int move = com.PerformBestMove(depth, stop_time);
                    timer3.Stop();
                    double time3 = timer3.ElapsedMilliseconds;
                    Console.WriteLine("\nMove number: " + ++numMoves);
                    Console.WriteLine("Nodes visited: " + com.numNodes);
                    Console.WriteLine("Time taken: " + time3/1000.0 + " seconds");
                    Console.WriteLine("Nodes/s: " + com.numNodes/(time3/1000));
                    com.numNodes = 0;
                }
                //auto command denotes an auto finish. Computer will play for both sides for the remainder of the game
                else if(command.Equals("auto"))
                {
                    while(numMoves < 300)
                    {
                        Console.WriteLine("\n\nThinking... ");
                        var timer3 = System.Diagnostics.Stopwatch.StartNew();
                        int move = com.PerformBestMove(depth, stop_time);
                        timer3.Stop();
                        double time3 = timer3.ElapsedMilliseconds;
                        Console.WriteLine("\nMove number: " + ++numMoves);
                        Console.WriteLine("Nodes visited: " + com.numNodes);
                        Console.WriteLine("Time taken: " + time3 / 1000.0 + " seconds");
                        Console.WriteLine("Nodes/s: " + com.numNodes / (time3 / 1000));
                        com.numNodes = 0;
                        board.DisplayBoard();
                        Console.WriteLine();
                        if (com.isMate || com.DBR)
                        {
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command.\n");
                    continue;
                }
                board.DisplayBoard();
                Console.WriteLine();
                if (com.isMate || com.DBR)
                {
                    break;
                }
            }

            if (numMoves >= 300)
            {
                Console.WriteLine("The move limit of 300 was reached");
            }
            else if(com.DBR)
            {
                Console.WriteLine("Draw By Repetition");
            }
            else if(com.isMate)
            {
                Console.WriteLine("You have been checkmated");
            }
            else
            {
                Console.WriteLine("Stalemate");
            }


            Console.ReadKey();
        }
    }
}
