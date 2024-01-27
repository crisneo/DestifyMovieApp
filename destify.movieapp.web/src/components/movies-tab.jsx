import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import PropTypes from 'prop-types';
import { getMovies } from '../services/movie-service';

export const MoviesTab = () => {
  const [movies, setMovies] = React.useState([]);
    React.useEffect(()=>{
      loadMovies();
    },[]);
  
    const loadMovies = async () => {
        var res = await getMovies();
        setMovies(res.data);
    };
    return (
      movies?
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Title</TableCell>
            <TableCell align="right">Description</TableCell>
            <TableCell align="right">Ratings</TableCell>
           
          </TableRow>
        </TableHead>
        <TableBody>
          {movies.map((row) => (
            <TableRow
              key={row.name}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {row.title}
              </TableCell>
              <TableCell align="right">{row.description}</TableCell>
              <TableCell align="right">{row.ratings?.length}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>:<span>{'Loading..'}</span>);
};