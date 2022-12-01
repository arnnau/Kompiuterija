
import { Navigate, useParams } from "react-router-dom"
import axios from "axios";
import { CircularProgress } from "@mui/material";
import MenuBar from "../MenuBar";
import { useEffect } from "react";
import { useState } from "react";

const DeleteComputer = () => {
    const [loading, setLoading] = useState(true);
    let { computerId } = useParams();
    const Delete = async () => {
        
        const url = "https://kompiuterija20221102215702.azurewebsites.net/computers/"+computerId;
            const token = localStorage.getItem("token");
            await axios({
                method: 'delete',
                url: url,
                data: {
                    id: computerId
                },
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    "Authorization": `Bearer ${token}`
                },
                
            }, { crossDomain: true })
             .catch(err => {
                console.log(err)
             });
             setLoading(false);
    }
    useEffect(() => {
        Delete();
      }, []);
      if(loading) {
        return (
            <div>
              <MenuBar />
              <div className="center"><CircularProgress /></div>
            </div>
      
          );
      }
      else return(<Navigate to="/computers" />);
        
}
export default DeleteComputer;