import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import axios from 'axios';
import MenuBar from '../MenuBar';

export const setAuthToken = token => {
    if (token) {
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    }
    else
        delete axios.defaults.headers.common["Authorization"];
 }

export default function SignIn() {
  const handleSubmit = (event) => {
    event.preventDefault();
    const url = "https://kompiuterija20221102215702.azurewebsites.net/login";
    const data = new FormData(event.currentTarget);
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
       //get token from response
       const token  =  response.data.token;
 
       //set JWT token to local
       localStorage.setItem("token", token);
 
       //set token to axios common header
       setAuthToken(token);
 
    //redirect user to home page
       window.location.href = '/'
     })
     .catch(err => console.log(err));
  };

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
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
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
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign In
            </Button>
            <Grid container>
              <Grid item>
                <Link href="/register" variant="body2">
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </div>
      
  );
}