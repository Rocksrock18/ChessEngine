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

The engine aims to determine the best move from a given chess position.

### Minimax

 The minimax algorithm helps dictate which path is the best one to take from a given node in the tree.
 
 #### Pruning
 
 Chess is a complicated game, with

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
