import React from 'react';
import { Navigate } from 'react-router-dom';
import jwtDecode from 'jwt-decode';

export function RequireAuth({ children }) {
  var isExpired = true;
  let isAuthenticated = localStorage.getItem("token") ? true : false;
  if (isAuthenticated) {
    const token = localStorage.getItem("token");
    const decodedJwt = jwtDecode(token);
    if (decodedJwt.exp * 1000 >= Date.now()) {
      isExpired = false;
    }
  }

  return isExpired ? <Navigate to="/login" /> : children;
}
export function GetRole() {
  const token = localStorage.getItem("token");
  const decodedJwt = jwtDecode(token);
  return decodedJwt.role;
}
export function IsTokenExpired() {
  var isExpired = true;
  let isAuthenticated = localStorage.getItem("token") ? true : false;
  if (isAuthenticated) {
    const token = localStorage.getItem("token");
    const decodedJwt = jwtDecode(token);
    if (decodedJwt.exp * 1000 >= Date.now()) {
      isExpired = false;
    }
  }

  return isExpired;
}
export function LogOut() {
  localStorage.removeItem("token");
  window.location.href = '/';
}