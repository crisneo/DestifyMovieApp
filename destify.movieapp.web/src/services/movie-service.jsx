import axios from "axios";

export const getMovies = ()=>{
    return axios.get(`${import.meta.env.VITE_API_URL}/api/v1/movie`);
};

export const getActors = ()=>{
     return axios.get(`${import.meta.env.VITE_API_URL}/api/v1/actor`);
};