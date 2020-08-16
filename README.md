# Chess Engine

A chess engine that you can play against.

## Resources

* Active site: https://chessengine-play.herokuapp.com/
* Front-end code: https://github.com/arnoldmak12/FrontEnd-ChessEngine

## Table of Contents

- [How it Works](#how-it-works)
    - [Minimax](#minimax)
        - [Pruning](#pruning)
            - [Alpha-beta pruning](#alpha-beta-pruning)
            - [Null move pruning](#null-move-pruning)
            - [Move heuristics](#move-heuristics)
    - [Quiescence Search](#quiescence-search)
    - [Transposition Tables](#transposition-tables)
        - [Hashing](#hashing)
        - [Principal Variation](#principal-variation)
    - [Evaluation](#evaluation)
- [Built With](#built-with)
- [Further Reading](#further-reading)
- [Authors](#authors)

## How It Works

The engine aims to determine the best move from a given chess position. To do so, a tree of possible future game states is constructed from the current position. By traversing through the tree, we can determine which future positions would be favorable, and aim to take the path that would take us to the most favorable position. 

The way we calculate this is done with the **minimax algorithm.**

### Minimax

The minimax algorithm helps dictate which path is the best one to take from a given node in the tree.

#### Pruning
 
Chess is a complicated game, with an average of around 35 possible moves for a given position. 
* At a depth of 6, the tree of possible game states would have a staggering **1,838,265,625** nodes.

In order to greatly reduce the number of nodes we have to look through, we can determine which branches do not need to be searched, effectively *pruning* them from the tree. There are many different ways we can decide to skip a branch.
 
#### Alpha Beta Pruning
 
The most common type of pruning is alpha beta pruning.

#### Null Move Pruning

In most chess positions, doing something is better than doing nothing at all. That is, if it were possible to "pass" your turn to your opponent, chances are you had a better move available. This is where the idea of null move pruning comes in:
* We pass on a turn (perform a *null move*) and search the tree to a limited depth. 
* If this results in an alpha beta cutoff, chances are there is another move available to us that will also produce a cutoff.
* Hence, we can prune the branch without searching further.

Certain conditions need to be met before performing a null move: 
1. If you are currently in check, a null move would result in an illegal position. 
2. If you are currently in the end game, a null move could give an innacurate evaluation.

*Zugzwang* is a term that refers to chess positions where the best option would be to pass your turn to the opponent. **This contradicts our assumption** made for null move pruning. Since these positions are more common in the end game, null move pruning is disabled there.

#### Move Heuristics

The effectiveness of alpha beta pruning can be greatly enhanced by the order in which moves are checked. The sooner the best move is found, the sooner a cutoff will be produced, and the more work we can avoid.

* While we cant know which move will be considered best, we can make an educated guess by ordering the moves based on certain heuristics.

Capturing a piece tends to lead to a bigger change in evaluation, so these are checked first. Captures are ordered based on *MVVLVA* move ordering, which stands for **Most Valuabe Victim, Least Valuable Attacker.**
* Taking a piece of high value (victim) has a great chance of being the best move.
* Taking a piece with a low value piece (attacker) tends to be a good idea, as they are likely to be captured by your opponent.

### Quiescence Search

Imagine a chess player looked 5 moves ahead and saw they could take one of their opponent's pawns with the queen on the final move. They think this is a great variation for them, since they've won a pawn.

But by stopping at 5 moves, they missed that the pawn was defended, so their opponent could simply **recapture the queen** with another pawn. What was thought to be winning material actually turned out to be **a catastrophic blunder.**

This is a common problem known as the **horizon effect**. One move can drastically change who has the advantage in a game of chess. Instead of stopping at a certain depth, it's much better to stop when the position is *quiet*, or when the evaulation is unlikely to change drastically after another move.

A chess position is considered **quiet** when:
1. There are no moves that capture a piece.
2. There are no moves that put the king in check.
3. There are no pawn promotions available.

After the main tree hits the max depth, the branch enters a **quiescence search** where only captures, checks, and promotions are considered, ending once the position is quiet. This search has a much smaller branching factor and can reach a depth of **over 20 moves** in certain branches.

### Transposition Tables

Consider the following move sequences:
'''
1. e4 e5
2. Nf3 Nc6
'''
and
'''
1. Nf3 Nc6
2. e4 e5
'''

## Built With
* React
* C#
* Deployed FrontEnd to Heroku
* Deployed BackEnd to Azure

## Further Reading

- [Minimax algorithm](https://www.baeldung.com/java-minimax-algorithm)
- [Evaluating a chess position](https://www.chessprogramming.org/Evaluation)
- [Zobrist hashing](https://iq.opengenus.org/zobrist-hashing-game-theory/)
- [General chess programming info](https://www.chessprogramming.org/Main_Page)

## Authors
* [Jasen Lai](https://www.linkedin.com/in/jasenlai/) - The Ohio State University
* [Arnold Makarov](https://www.linkedin.com/in/arnoldmakarov/) - The Ohio State University
* [Jacob Maxson](https://www.linkedin.com/in/jacob-maxson-63869018a/) - The Ohio State University
* Jaewook Lee - The Ohio State University
