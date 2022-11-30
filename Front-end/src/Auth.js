import React from 'react';
import { Navigate } from 'react-router-dom';
import jwtDecode from 'jwt-decode';

export function RequireAuth({ children }) {
    var isExpired = false;
    let isAuthenticated = localStorage.getItem("token") ? true : false;
    if (isAuthenticated) {
      const token = localStorage.getItem("token");
      const decodedJwt = jwtDecode(token);
      console.log(decodedJwt.exp * 1000);
      console.log(Date.now());
      if (decodedJwt.exp * 1000 < Date.now()) {
        isExpired = true;
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
  var isExpired = false;
    let isAuthenticated = localStorage.getItem("token") ? true : false;
    if (isAuthenticated) {
      const token = localStorage.getItem("token");
      const decodedJwt = jwtDecode(token);
      console.log(decodedJwt.exp * 1000);
      console.log(Date.now());
      if (decodedJwt.exp * 1000 < Date.now()) {
        isExpired = true;
      }
    }
  
    return isExpired ? true : false;
}