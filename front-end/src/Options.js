import React from 'react';
import './Options.css';

var select1 = ['Play As White', 'Play As Black'];
var select2 = ['option 4', 'option 5', 'option 6'];

class Options extends React.Component {
    render() {
        return (
            <div className="Options-layout">

                <div className="welcome-box">
                    <span className="welcome-header">
                        Bingus Inc. Chess Engine
          </span>
                </div>

                <div className="select-box">
                    <span className="select-header">
                        Select Color
          </span>
                </div>

                <div className="difficulty-box">

                    <ul className="difficulty-list1">

                        {select1.map((entry, index) => {
                            return (
                                <li className="difficulty-entry">

                                    <button className="difficulty-name"
                                        entry={entry.trim()}
                                        onClick={() => {
                                            var url = "/play/" + (index === 1 ? "white/" : "black/");
                                            console.log(index);
                                            window.open(url, "_self")
                                        }}
                                        index={index++}
                                    >

                                        <span>{entry}</span>

                                    </button>

                                </li>
                            )
                        })}

                    </ul>

                    {/* <ul className="difficulty-list2">

                        {select2.map((entry, index) => {
                            return (
                                <li className="difficulty-entry">

                                    <button className="difficulty-name"
                                        entry={entry.trim()}
                                        index={index++}
                                        onClick={() => {
                                            var url = "/play/option" + (index+3);
                                            console.log(index);
                                            window.open(url, "_self")
                                        }}
                                    >

                                        <span>{entry}</span>

                                    </button>

                                </li>
                            )
                        })}

                    </ul> */}

                </div>

            </div>
        );
    }
}

export default Options;
