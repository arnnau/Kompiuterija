import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import axios from 'axios';
import MenuBar from '../MenuBar';
import { Modal } from '@mui/material';
import { IsTokenExpired } from '../Auth';
import { Navigate } from 'react-router-dom';

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

export default function Register() {
  const [confirm, setConfirm] = React.useState("")
  const [open, setOpen] = React.useState(false)
  const handleChange = (event) => {
    setConfirm(event.target.value);
  }
  const handleClose = () =>  {
    setOpen(false);
}
  const handleSubmit = (event) => {
    event.preventDefault();
    const url = "https://kompiuterija20221102215702.azurewebsites.net/register";
    const data = new FormData(event.currentTarget);
    if (data.get("password") === data.get("confirmPassword")) {
      const loginPayload = {
        email: data.get('email'),
        password: data.get('password'),
      };
      axios({
        method: 'post',
        url: url,
        data: JSON.stringify(loginPayload),
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        },

      }, { crossDomain: true })
        .then(response => {
          window.location.href = '/'
        })
        .catch(err => console.log(err));
    }
    else {
      setConfirm("");
      setOpen(true);
    }

  };
  if(!IsTokenExpired()) return(<Navigate to="/"/>)
  return (
    <div>
      <MenuBar />
      <Container component="main" maxWidth="xs">

        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>

          </Avatar>
          <Typography component="h1" variant="h5">
            Sign up
          </Typography>
          <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="confirmPassword"
              label="Confirm password"
              type="password"
              id="confirmPassword"
              autoComplete="confirmPassword"
              value={confirm}
              onChange={handleChange}
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign up
            </Button>
          </Box>
        </Box>
      </Container>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Passwords must match
          </Typography>
        </Box>
      </Modal>
    </div>

  );
}