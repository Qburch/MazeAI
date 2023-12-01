import axios from "axios";

const endpoint = "https://localhost:50000/api/maze";

const runMazeAI = (grid) => {
    const config = {
        method: "POST",
        url: endpoint,
        data: grid,
        withCredentials: false,
        crossdomain: true,
        headers: {"Content-Type": "application/json"}
    };
    return axios(config);
}


export { runMazeAI }