
import { Link, Navigate, useParams } from "react-router-dom"
import axios from "axios";
import { CircularProgress, Modal, Box, Typography, Button } from "@mui/material";
import MenuBar from "../MenuBar";
import { useState } from "react";

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
      if(loading) {
        return (
            <div>
              <MenuBar />
              <Modal
                    open={true}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={style}>
                    <Typography id="modal-modal-title" variant="h6" component="h2">
                        Are you sure you want to delete selected computer?
                    </Typography>
                    <Button onClick={Delete}>Yes</Button>
                    <Button component={Link} to={"/computers"}>No</Button>
                    </Box>
                </Modal>
              <div className="center"><CircularProgress /></div>
            </div>
      
          );
      }
      else return(<Navigate to="/computers" />);
        
}
export default DeleteComputer;