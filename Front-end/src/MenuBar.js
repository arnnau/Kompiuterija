import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import { Menu, MenuItem } from '@mui/material';
import { Link } from 'react-router-dom';
import { IsTokenExpired, LogOut } from './Auth';

export default function MenuBar() {
  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  if(IsTokenExpired()) {
    return(
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <IconButton
            size="large"
            id="basic-button"
            aria-controls={open ? 'basic-menu' : undefined}
            aria-haspopup="true"
            aria-expanded={open ? 'true' : undefined}
            onClick={handleClick}
            sx={{ mr: 2 }}
          >
            <MenuIcon />
          </IconButton>
          <Menu
              id="basic-menu"
              anchorEl={anchorEl}
              open={open}
              onClose={handleClose}
              MenuListProps={{
                'aria-labelledby': 'basic-button',
              }}
            >
              <MenuItem component={Link} to={'/'} onClick={handleClose}>Home</MenuItem>
              <MenuItem component={Link} to={'/shops'} onClick={handleClose}>Shops</MenuItem>
              <MenuItem component={Link} to={'/computers'} onClick={handleClose}>Computers</MenuItem>
            </Menu>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Kompiuterija
          </Typography>
          <Button component={Link} to={'/login'} color="inherit">Login</Button>
        </Toolbar>
      </AppBar>
    </Box>);
  }
  else return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <IconButton
            size="large"
            id="basic-button"
            aria-controls={open ? 'basic-menu' : undefined}
            aria-haspopup="true"
            aria-expanded={open ? 'true' : undefined}
            onClick={handleClick}
            sx={{ mr: 2 }}
          >
            <MenuIcon />
          </IconButton>
          <Menu
              id="basic-menu"
              anchorEl={anchorEl}
              open={open}
              onClose={handleClose}
              MenuListProps={{
                'aria-labelledby': 'basic-button',
              }}
            >
              <MenuItem component={Link} to={'/'} onClick={handleClose}>Home</MenuItem>
              <MenuItem component={Link} to={'/shops'} onClick={handleClose}>Shops</MenuItem>
              <MenuItem component={Link} to={'/computers'} onClick={handleClose}>Computers</MenuItem>
            </Menu>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Kompiuterija
          </Typography>
          <Button onClick={LogOut} color="inherit">Logout</Button>
        </Toolbar>
      </AppBar>
    </Box>
  );
}