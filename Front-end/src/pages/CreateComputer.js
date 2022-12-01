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
import axios from "axios";
import MenuBar from "../MenuBar";
import { useEffect, useState } from 'react';
import { Select, Modal } from '@mui/material';
import { MenuItem } from '@mui/material';
import { Navigate } from 'react-router-dom';

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

const CreateComputer = () => {
    const [shop, setShop] = React.useState('');
    const [open, setOpen] = React.useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () =>  {
        setOpen(false);
        window.location.href = '/computers';
    }
    const handleChange = (event) => {
        setShop(event.target.value);
    };
    const [shops, setShops] = useState([]);
    const fetchShops = async () => {
        const token = localStorage.getItem("token");
        const { data } = await axios.get(
          "https://kompiuterija20221102215702.azurewebsites.net/shops", { headers: { "Authorization": `Bearer ${token}` } }
        );
        const shops = data;
        setShops(shops);
      };
    
      useEffect(() => {
        fetchShops();
      }, []);
    const handleSubmit = (event) => {
        event.preventDefault();
        const url = "https://kompiuterija20221102215702.azurewebsites.net/computers";
        const token = localStorage.getItem("token");
        const data = new FormData(event.currentTarget);
        const loginPayload = {
          shopId: shop,
          name: data.get('name')
        };
        axios({
            method: 'post',
            url: url,
            data: JSON.stringify(loginPayload),
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                "Authorization": `Bearer ${token}`
            },
            
        }, { crossDomain: true }).then(handleOpen())
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
              <Typography component="h1" variant="h5">
                Create new computer
              </Typography>
              <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  id="name"
                  label="Computer name"
                  name="name"
                  autoComplete="name"
                  autoFocus
                />
                <Select
                    labelId="shopId"
                    id="shopId"
                    value={shop}
                    label="Shop"
                    onChange={handleChange}
                >
                    {shops.map((shop) => (
                        <MenuItem key={shop.id} value={shop.id}>{shop.address}, {shop.city}</MenuItem>
                    ))}
                </Select>
                <Button
                  type="submit"
                  fullWidth
                  variant="contained"
                  sx={{ mt: 3, mb: 2 }}
                >
                  Add computer
                </Button>
                <Modal
                    open={open}
                    onClose={handleClose}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={style}>
                    <Typography id="modal-modal-title" variant="h6" component="h2">
                        New computer has been successfully created
                    </Typography>
                    </Box>
                </Modal>
              </Box>
            </Box>
          </Container>
        </div>
          
      );
            }
  export default CreateComputer;