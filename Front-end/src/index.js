import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Home from './pages/Home.js';
import Login from './pages/Login.js';
import NoPage from './pages/NoPage';
import Shops from './pages/Shops';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

const parseJwt = (token) => {
  try {
    return JSON.parse(atob(token.split(".")[1]));
  } catch (e) {
    return null;
  }
};

function RequireAuth({ children }) {
  var isExpired = false;
  let isAuthenticated = localStorage.getItem("token") ? true : false;
  if(isAuthenticated) {
    const token = localStorage.getItem("token");
    const decodedJwt = parseJwt(token);
    if (decodedJwt.exp * 1000 < Date.now()) {
      isExpired = true;
      localStorage.removeItem("token");
    }
  }
  
  return isExpired ? <Navigate to='/'/> : children;
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <ThemeProvider theme={darkTheme}>
    <CssBaseline />
    <BrowserRouter>
      <Routes>
        <Route exact path="/" element={<Home />}/>
        <Route exact path="/login" element={<Login />} />
        <Route
        path="/shops"
        element={
          // Good! Do your composition here instead of wrapping <Route>.
          // This is really just inverting the wrapping, but it's a lot
          // more clear which components expect which props.
          <RequireAuth>
            <Shops />
          </RequireAuth>
        }
      />
        <Route exact path="/*" element={<NoPage />} />
      </Routes>
    </BrowserRouter>
  </ThemeProvider>
  
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
