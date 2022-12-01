
import { Navigate, useParams } from "react-router-dom"
import axios from "axios";
import { Button, CircularProgress, Modal, Box, Typography } from "@mui/material";
import MenuBar from "../MenuBar";
import { Link } from "react-router-dom";
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
                        Are you sure you want to delete selected part?
                    </Typography>
                    <Button onClick={Delete}>Yes</Button>
                    <Button component={Link} to={"/parts/"}>No</Button>
                    </Box>
                </Modal>
              <div className="center"><CircularProgress /></div>
            </div>
      
          );
      }
      else return(<Navigate to="/parts" />);
        
}
export default DeletePart;