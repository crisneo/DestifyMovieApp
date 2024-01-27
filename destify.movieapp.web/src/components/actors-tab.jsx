import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import PropTypes from 'prop-types';
import { getActors } from '../services/movie-service';

export const ActorsTab = () => {
  const [actors, setActors] = React.useState([]);
    React.useEffect(()=>{
      loadActors();
    },[]);
  
    const loadActors = async () => {
        var res = await getActors();
        setActors(res.data);
    };
    return (actors?
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell align="right">Country</TableCell>
           
          </TableRow>
        </TableHead>
        <TableBody>
          {actors.map((row) => (
            <TableRow
              key={row.name}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {row.name}
              </TableCell>
              <TableCell align="right">{row.country}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>:<span>{'Loading..'}</span>);
};