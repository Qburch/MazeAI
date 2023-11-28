import axios from "axios";

const endpoint = "https://localhost:50000/api/maze";

const create = (grid) => {
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

const move = async () => {
    const config = {
        method: "GET",
        url: `${endpoint}`,
        withCredentials: false,
        crossdomain: true,
        headers: {"Content-Type": "application/json"}
    };
    return axios(config);
}

const reset = () => {
    const config = {
        method: "PUT",
        url: endpoint,
        withCredentials: false,
        crossdomain: true,
        headers: {"Content-Type": "application/json"}
    };
    return axios(config);
}

export { create, move, reset }