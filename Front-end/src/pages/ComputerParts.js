import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Axios from "axios";
import { Stack } from "@mui/system";
import { Button, Card, CardContent, Typography, CardActionArea, CardMedia } from "@mui/material";
import MenuBar from "../MenuBar";
import {CircularProgress} from "@mui/material";


const ComputerParts = () => {
  const [parts, setParts] = useState([]);
  const [loading, setLoading] = useState(true);
  let { computerId } = useParams();
  const fetchParts = async () => {
  const token = localStorage.getItem("token");
  const url = "https://kompiuterija20221102215702.azurewebsites.net/computers/" + computerId + "/parts";
  try {
    const { data } = await Axios.get(
      url, { headers: { "Authorization": `Bearer ${token}` } }
    )
    const parts = data;
    setParts(parts);
    setLoading(false);
  }
  catch (err) {
    setParts(0);
    setLoading(false);
  };
  
};

useEffect(() => {
  fetchParts();
}, []);
if(loading) {
  return(
  <div>
    <MenuBar />
    <div className="center"><CircularProgress /></div>
  </div>
    
    );
}
if(parts == 0) {
  return (
    <div>
    <MenuBar />
    <div className="center">
    <Typography gutterBottom variant="h5" component="div">
                No parts found
              </Typography>
    </div>
    </div>
  );
}
else return (
    <div>
      <MenuBar />
        <div className="center">
        <Stack spacing={2} direction="column">
          {parts.map((part) => (
            <Card sx={{ maxWidth: 345 }} key={part.id}>
              <CardActionArea>
                <CardContent>
                  <Typography gutterBottom variant="h5" component="div">
                    {part.name}
                  </Typography>
                </CardContent>
              </CardActionArea>
              
            </Card>
          ))}
        </Stack>

      </div>
    </div>
    
  );
};

export default ComputerParts;
