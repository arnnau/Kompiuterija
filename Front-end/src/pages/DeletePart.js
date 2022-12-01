
import { Navigate, useParams } from "react-router-dom"
import axios from "axios";
import { CircularProgress } from "@mui/material";
import MenuBar from "../MenuBar";
import { useEffect } from "react";
import { useState } from "react";

const DeletePart = () => {
    const [loading, setLoading] = useState(true);
    let { partId } = useParams();
    const Delete = async () => {
        
        const url = "https://kompiuterija20221102215702.azurewebsites.net/parts/"+partId;
            const token = localStorage.getItem("token");
            await axios({
                method: 'delete',
                url: url,
                data: {
                    id: partId
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
      else return(<Navigate to="/parts" />);
        
}
export default DeletePart;