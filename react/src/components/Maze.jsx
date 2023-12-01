import React, { useState } from "react";
import { FiPlay } from "react-icons/fi";
import { runMazeAI } from "../services/mazeService"
import toastr from "toastr"
import "./Maze.css"

const Maze = (props) => {
    const maxRows = props.rows;
    const maxCols = props.cols;
    const defaultMaze = props.default;
    const winningPos = [maxRows - 1, maxCols - 2];
    const initialPos = [1,1];
    
    const [maze, setMaze] = useState(initateGrid(maxRows, maxCols));
    const [path, setPath] = useState([]);
    const [hasStarted, setHasStarted] = useState(false);
    const [gameInfo, setGameInfo] = useState(null);


    function initateGrid (rows, cols) {
        let grid = [];
        for(let row = 0; row < rows; row++){
            grid.push(new Array(cols).fill(0));
            grid[row][0] = 1;
            grid[row][cols - 1] = 1;
        }
        for(let col = 1; col < cols - 1; col++){
            grid[0][col] = 1;
            grid[rows - 1][col] = 1;
        }
        grid[winningPos[0]][winningPos[1]] = 0;
        return grid;
    }

    const getSquareClasses = (val, row, col) => {
        let str = "col border border-dark";
        if(val === 1) str += " bg-dark";
        else if(row === initialPos[0] && col === initialPos[1]) str += " bg-success";
        else if (path && path.length > 0 && path.lastIndexOf(row * maxCols + col) === path.length- 1) str += " bg-warning";
        else if (path && path.length > 0 && path.indexOf(row * maxCols + col) > -1) str += " bg-info";
        else str += " bg-light";
        return str;
    }

    const onSquareClicked = (row, col) => {
        setMaze((prevState) => {
            let pd = [...prevState];
            pd[row][col] = pd[row][col] === 0 ? 1 : 0;
            return pd;
        })
    }
    const onResetClicked = () => {
        setPath([]);
        setHasStarted(false);
        setMaze(initateGrid(maxRows, maxCols));
    }

    const onDefaultClicked = () => {
        setPath([]);
        setHasStarted(false);
        setMaze(defaultMaze);
    }

    const onStartClicked = () => {
        setPath([]);
        setHasStarted(true)
        runMazeAI(maze).then(onStartSuccess).catch(onStartError);
    }

    const onStartSuccess = async (response) => {
        const res = response.data.item;
    
        for(let i = 0; i < res.gamesData.length; i++) {
            const curGame = res.gamesData[i];
            setGameInfo({
                game: i + 1,
                steps: curGame.stateMoves.length
            });

            for (let j = 0; j < curGame.stateMoves.length; j++){
                
                let curState = curGame.stateMoves[j];
                console.log(path);
                if (j === 0) setPath([curState]);
                else setPath((prevState) => {
                    let pd = [...prevState];
                    pd.push(curState);
                    return pd;
                });
                if (i < 10) await new Promise((resolve) => setTimeout(resolve, 25)); 
                else if (i >= 10 && i < 20) await new Promise((resolve) => setTimeout(resolve, 50)); 
                else await new Promise((resolve) => setTimeout(resolve, 100)); 
            }
            await new Promise((resolve) => setTimeout(resolve, 2000));
        }
        if (!res.foundMostEfficientPath)  toastr.error("Could not find the most efficient path");
        else toastr.success("Found the most efficient path");
    }
    const onStartError = () => {
        console.log("Error starting the AI");
    }

    const mapRowToDispayRow = (rowArr, rowIdx) => {
        return(
            <div className="row" key={"maze-row-" + rowIdx}>
                {rowArr.map(mapValueToDisplaySquare)}
            </div>
        );

        function mapValueToDisplaySquare(val, colIdx) {
            let disableClick = (rowIdx === 0 || rowIdx === maxRows - 1
                            || colIdx === 0 ||colIdx === maxCols - 1)
            return (
                <div 
                    className={getSquareClasses(val, rowIdx, colIdx)}
                    onClick={() => !disableClick ? onSquareClicked(rowIdx, colIdx) : null}
                    key={"maze-square-"+rowIdx + "-" +colIdx}
                >
                    { }
                </div>
            );
        }
    }


    const getInstructions = () => {
        return(
                <div className="card border border-dark rounded">
                    <div className="card-header">
                        <h4 className="card-title">Create your own maze or choose the default option</h4>
                    </div>
                    <div className="card-body">
                        <p>Fill in the maze by clicking on squares to change their color!</p>
                        <p>Click Start to generate the AI which finds the most efficient path in the maze... wait a moment ... and see the spectacular results!</p>
                    </div>
                </div>
        )
    }

    const getGame = () => {
        return (
            <div className="card border border-dark rounded">
            <div className="card-header">
                <h4 className="card-title">Game {gameInfo.game}</h4>
            </div>
            <div className="card-body">
                <p>Exit Found in {gameInfo.steps} steps!</p>
            </div>
        </div>
        )
    }

    return (
        <div className="d-flex-md my-md-0 my-3 px-3 px-md-0">
            <div className="col-md-4 ms-auto mt-md-5 pt-md-2">
                {(!hasStarted || !gameInfo) && getInstructions()}
                {(hasStarted && gameInfo) && getGame()}
            </div>
            <div className="col maze-grid ms-md-5 me-md-4 mx-auto">
                <div className="d-flex">
                    <button className="btn btn-primary my-2 maze-btn" onClick={onResetClicked}>Reset</button>
                    {(defaultMaze && defaultMaze.length === maxRows && defaultMaze[0].length === maxCols ) 
                        && (
                        <button className="btn btn-info my-2 mx-2 maze-btn" onClick={onDefaultClicked}>Default</button>
                    )}
                </div>
                {(maze && maze.length > 0) && maze.map(mapRowToDispayRow)}
                {(!maze || maze.length === 0) && <div>No maze to display</div>}
                {!hasStarted && (
                    <button className="btn btn-outline-danger my-2 maze-btn ms-auto me-0" onClick={onStartClicked}><FiPlay /> Start</button>
                )}
            </div>

            <div className="me-auto mt-md-5 pt-md-2">
                <div className="card border border-dark rounded bg-danger-subtle">
                    <div className="card-body">
                        <div className="d-flex"> 
                                <div className="legend-ele bg-white border border-dark"/>
                                <div className="p-1">- Open Space</div>
                            </div>
                            <div className="d-flex mt-1"> 
                                <div className="legend-ele bg-black border border-light" />
                                <div className="p-1 ">- Wall</div>
                            </div>
                            <div className="d-flex mt-1"> 
                                <div className="legend-ele bg-success border border-light" />
                                <div className="p-1 ">- Starting Position</div>
                            </div>
                            <div className="d-flex mt-1"> 
                                <div className="legend-ele bg-info border border-light" />
                                <div className="p-1 ">- Path Taken</div>
                            </div>
                    </div>
                </div>
            </div>

        </div>

    )
}

export default Maze;
