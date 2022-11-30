import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Routes, Route, Navigate, useParams } from 'react-router-dom';
import Home from './pages/Home.js';
import Login from './pages/Login.js';
import NoPage from './pages/NoPage';
import Shops from './pages/Shops';
import ShopComputers from './pages/ShopComputers';
import Computers from './pages/Computers';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import jwtDecode from 'jwt-decode';
import ComputerParts from './pages/ComputerParts';
import Parts from './pages/Parts';
import { RequireAuth } from './Auth.js';
import CreateComputer from './pages/CreateComputer';
import CreatePart from './pages/CreatePart';

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <ThemeProvider theme={darkTheme}>
    <CssBaseline />
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route
          path="/shops"
          element={
            <RequireAuth>
              <Shops />
            </RequireAuth>
          }
        />
        <Route
          path="/shops/:shopId"
          element={
            <RequireAuth>
              <ShopComputers />
            </RequireAuth>
          }
        />
        <Route
          path="/shops/:shopId/create"
          element={
            <RequireAuth>
              <CreateComputer />
            </RequireAuth>
          }
        />
        <Route
          path="/computers"
          element={
            <RequireAuth>
              <Computers />
            </RequireAuth>
          }
        />
        <Route
          path="/computers/:computerId"
          element={
            <RequireAuth>
              <ComputerParts />
            </RequireAuth>
          }
        />
        <Route
          path="/computers/:computerId/create"
          element={
            <RequireAuth>
              <CreatePart />
            </RequireAuth>
          }
        />
        <Route
          path="/parts"
          element={
            <RequireAuth>
              <Parts />
            </RequireAuth>
          }
        />
        <Route path="/*" element={<NoPage />} />
      </Routes>
    </BrowserRouter>
  </ThemeProvider>

);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
