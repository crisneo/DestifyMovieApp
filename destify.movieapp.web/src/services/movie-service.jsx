import axios from "axios";

export const getMovies = ()=>{
    return axios.get("https://localhost:7073/api/v1/movie");
};

export const getActors = ()=>{
     return axios.get("https://localhost:7073/api/v1/actor");
};