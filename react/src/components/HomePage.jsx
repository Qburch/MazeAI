import React, { useState, useEffect } from "react";
import * as mazeService from "../services/mazeService"

const defaultGrid = [[1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
[1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
[1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1],
[1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1],
[1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1],
[1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1],
[1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1],
[1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1],
[1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1],
[1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1],
[1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1],
[1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1],
[1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1],
[1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1],
[1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1],
[1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1],
[1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1],
[1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1],
[1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1],
[1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1],
[1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1]];






const HomePage = () => {

    const [grid] = useState(defaultGrid);

    const runGame = () => {
        console.log("click");
        mazeService.create(grid).then(onCreateSuccess).catch(onCreateError);
        setState(grid.length * 1 + 1); // initial pos
    }

    const [state, setState] = useState();
    const [isDone, setIsDone] = useState(false);

    const onCreateSuccess = async () => {
        debugger;
        let lastEpisodeSteps = -1;
        let sameSteps = 0;

        for(let i = 0; i < 100; i++){
            console.log("episode " + i);
            
            let steps = 0;
            while (!isDone && steps < 200){
                await mazeService.move().then(onMoveSuccess).catch(onMoveError);
                if (steps === 199) console.log("Could not find the exit");
                steps++;
            }

            if(steps === 200){
                 console.log("Could not find the exit");
                 break;
            }

            if (steps === lastEpisodeSteps) sameSteps++;
            else sameSteps = 0;

            if (sameSteps === 4){
                console.log("most efficient path found in " + steps + " steps on episode " + (i + 4));
                
                break;
            }

            lastEpisodeSteps = steps;
            if (i === 99) console.log("Could not find most efficient path consistently");
        }

    }

    const onMoveSuccess = async (response) => {
        await new Promise(() => {
            setIsDone(response.data.item.isDone);
            setState(response.data.item.nextState);
        });
    }

    const onMoveError = () => {
        console.log("Error Moving Piece");
    }

    const onCreateError = () => {
        console.log("Error creating maze");
    }

    return (
        <React.Fragment>
            <h1>Home Page</h1>
            <button className="btn btn-primary" onClick={runGame}>Start</button>
            <p>State {state} : {isDone} </p>
        </React.Fragment>
    );
}

export default HomePage;