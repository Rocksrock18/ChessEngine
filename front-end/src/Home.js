import React from 'react';
import './Home.css';
import Gameboard from './Gameboard';
import Particles from 'react-particles-js';
import Options from './Options';
import { Route, Switch, Redirect } from 'react-router-dom';

class Home extends React.Component {

  render() {
    return (
      <div className="home-layout">

        <div className="background">
          <Particles
            params={{
              "particles": {
                "number": {
                  "value": 50,
                },
                "color": {
                  "value": "FFFFF0"
                },
                "line_linked": {
                  "enable": true,
                },
                "size": {
                  "value": 4,
                }
              }
            }}
          />
        </div>

      <Switch>
            <Route exact path="rocksrock18.github.io/chessengine/"  component={Options} />
            <Route path="rocksrock18.github.io/chessengine/play" component={Gameboard} />
            <Redirect to="rocksrock18.github.io/chessengine/" />
      </Switch>

      </div>
    );
  }
}

export default Home;