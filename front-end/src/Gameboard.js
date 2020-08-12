/* global $ */
import React from 'react';
import Chess from 'chess.js';
import $ from 'jquery'
import './Gameboard.css';
import Chessboard from 'chessboardjsx';
import PropTypes from "prop-types";

var game = new Chess();

class Gameboard extends React.Component {
  static propTypes = { children: PropTypes.func };

  state = {
    fen: "start",
    history: [],
    gameEnd: false,
    whiteMove: "",
    blackMove: "",
    turn: ""
  };

  componentDidMount() {
    var turn = "";

    //Set the move for the player that went
    if(window.location.href.includes("white")){
      console.log("White goes first")
      turn = "w";
    }
    else{
      console.log("Black goes first")
      turn = "b";
      $.ajax({
        type: 'GET',   
        url: 'https://localhost:44338/api/values?fen=rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1&move=1111',
        dataType: 'text',
        success: 
        (data) => {

          game.move({
             to: String(data).substring(2,4),
             from: String(data).substring(0,2),
             promotion: (data.length === 5 ? String(data).substring(4) : "q")
           });

            this.setState({
              fen: game.fen()
            })

           console.log("New FEN: "+ game.fen());
           
            this.setState({
              whiteMove: String(data).substring(0,4)
            })
          
        },
        // error: fxnerrorptr
        error: function (jqXHR, error, errorThrown) {
          alert(jqXHR.responseText
          +"\n" + error
          +"\n" + errorThrown);
        }
      }); // always promote to a queen for example simplicity
    }

    this.setState({ 
      fen: game.fen(),
      turn: turn
     });
  }

  onDrop = ({ sourceSquare, targetSquare, piece }) => {
    
    var beforeFen = game.fen();

    console.log(piece);

    if (!piece.includes(this.state.turn)) return;

    // see if the move is legal
    let move = game.move({
      from: sourceSquare,
      to: targetSquare,
      promotion: "q"
    });
    

    // illegal move
    if (move === null) return;

    this.setState({
      fen: game.fen(),
    });

    let postContents = "" + sourceSquare + targetSquare;
    var state = this;

    if(this.state.turn === "w"){
      this.setState({
        whiteMove: postContents
      })
    }
    else{
      this.setState({
        blackMove: postContents
      })
    }

      $.ajax({
        type: 'GET',   
        url: 'https://https://chessbackendapi.azurewebsites.net/api/values?fen=' + beforeFen + '&move=' + postContents,
        // url: 'https://localhost:44338/api/values?fen=' + beforeFen + '&move=' + postContents,
        dataType: 'text',
        success: 
        (data) => {

          console.log("FEN Passed to API: "+ 'https://localhost:44338/api/values?fen=' + beforeFen);
          // console.log("Black's Move: " + data);

          game.move({
             to: String(data).substring(2,4),
             from: String(data).substring(0,2),
             promotion: (data.length === 5 ? String(data).substring(4) : "q")
           });

            state.setState({
              fen: game.fen()
            })

           console.log("New FEN: "+ game.fen());
           if(state.state.turn === "b"){
            state.setState({
              whiteMove: String(data).substring(0,4)
            })
          }
          else{
            state.setState({
              blackMove: String(data).substring(0,4)
            })
          }

           if(game.game_over()){
            console.log("GAME OVER");
            state.setState({
              gameEnd: true
            });
          }
        },
        // error: fxnerrorptr
        error: function (jqXHR, error, errorThrown) {
          alert(jqXHR.responseText
          +"\n" + error
          +"\n" + errorThrown);
        }
      }); // always promote to a queen for example simplicity

    this.setState({
      fen: game.fen(),
    });
  }

  onMoveEnd(){
    if(game.game_over()){
      console.log("GAME OVER");
      this.state.setState({
        gameEnd: true
      });
    }
  }

  render() {
    return (
      <div className="gameboard-layout">

        <div className="gameboard-box">

          <Chessboard
            position={this.state.fen}
            darkSquareStyle={{ backgroundColor: '#B0C4DE' }}
            lightSquareStyle={{ backgroundColor: 'white' }}
            orientation={(this.state.turn === 'b' ? 'black' : 'white')}
            onDrop={this.onDrop}
            onMoveEnd={this.onMoveEnd}
            width="640"
            onMouseOutSquare={this.onMouseOverSquare}
            onMouseoverSquare={this.onMouseoverSquare}
            
          />

          <div className="moves">

            <div className="black">
                <h1>Black's Move</h1>
                <h2>{this.state.blackMove}</h2>
            </div>

            <div className="white">
                <h1>White's Move</h1>
                <h2>{this.state.whiteMove}</h2>
            </div>

          </div>
        
        </div>

        {this.state.gameEnd ?
          <div>
              <h1>Game Over</h1>
          </div>:null}

      </div>

    );
  }
}

export default Gameboard;