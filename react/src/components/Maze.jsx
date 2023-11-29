import React, { useState} from "react";
import { FiPlay } from "react-icons/fi";
import "./Maze.css"

const Maze = (props) => {
    const maxRows = props.rows;
    const maxCols = props.cols;
    const defaultMaze = props.default;
    const winningPos = [maxRows - 1, maxCols - 2];
    const initialPos = [1,1];
    
    const [maze, setMaze] = useState(initateGrid(maxRows, maxCols));
    const [path] = useState(null);



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
        else if (path && path.includes(row * maxCols + col)) str += " bg-info";
        else str += " bg-light";
        return str;
    }

    const onResetClicked = () => {
        setMaze(initateGrid(maxRows, maxCols));
    }

    const onDefaultClicked = () => {
        setMaze(defaultMaze);
    }

    const mapRowToDispayRow = (rowArr, rowIdx) => {
        const onSquareClicked = (row, col) => {
            setMaze((prevState) => {
                let pd = [...prevState];
                pd[row][col] = pd[row][col] === 0 ? 1 : 0;
                return pd;
            })
        }

        const mapValueToDisplaySquare = (val, colIdx) => {
            if(rowIdx === 0 || rowIdx === maxRows - 1 || colIdx === 0 || colIdx === maxCols - 1){
                return (
                    <div className={getSquareClasses(val)} key={"maze-square-"+rowIdx + "-" +colIdx}>
                        { }
                    </div>
                )
            }
            return (
                <div className={getSquareClasses(val, rowIdx, colIdx)} onClick={() => onSquareClicked(rowIdx, colIdx)} key={"maze-square-"+rowIdx + "-" +colIdx} >
                    { }
                </div>
            )
        }

        return(
            <div className="row" key={"maze-row-" + rowIdx}>
                {rowArr.map(mapValueToDisplaySquare)}
            </div>
        )
    }

    return (
        <div className="d-flex my-5">
            <div className="col-3 ms-auto">
                <h4>Create your own maze or choose the default option</h4>
                <p>Fill in the maze by clicking on squares to change their color</p>
                <p>Then click Start to generate the AI which finds the most efficient path in the maze</p>
                <p>Wait a moment ... and see the spectacular results</p>
                <br></br>
                <div>White - Open Space</div>
                <div>Black - Wall</div>
                <div>Green - Starting Position</div>
                <div>Blue - Path Taken</div>
            </div>
            <div className="col maze-grid">
                {(maze && maze.length > 0) && maze.map(mapRowToDispayRow)}
                {(!maze || maze.length === 1) && <div>No maze to display</div>}
                <button className="btn btn-outline-danger my-2 maze-btn ms-auto me-0"><FiPlay /> Start</button>
            </div>
            <div className="me-auto">
                <button className="btn btn-primary my-2 maze-btn" onClick={onResetClicked}>Reset to Blank</button>
                {(defaultMaze && defaultMaze.length === maxRows && defaultMaze[0].length === maxCols ) 
                    && (
                    <button className="btn btn-info my-2 maze-btn" onClick={onDefaultClicked}>Set to Default</button>
                )}
            </div>
        </div>

    )
}

export default Maze;
