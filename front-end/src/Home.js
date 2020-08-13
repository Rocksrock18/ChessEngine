import React from 'react';
import './Home.css';
import Gameboard from './Gameboard';
// import Particles from 'react-particles-js';
import Options from './Options';
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom';

class Home extends React.Component {

  render() {
    return (
      <Router>
      <div className="home-layout">

        <div className="background">
          {/* <Particles
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
          /> */}
        </div>

      <Switch>
            <Route exact path="/ChessEngine">
              <Options/>
            </Route>
            <Route path="/ChessEngine/play">
              <Gameboard/>
            </Route>
            <Redirect to="rocksrock18.github.io/ChessEngine" />
      </Switch>

      </div>
      </Router>
    );
  }
}

export default Home;