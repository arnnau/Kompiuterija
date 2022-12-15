import * as React from 'react';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import axios from "axios";
import MenuBar from "../MenuBar";
import { useEffect, useState } from 'react';
import { Select, Modal } from '@mui/material';
import { MenuItem } from '@mui/material';
import { CircularProgress } from '@mui/material';
import { FormControl } from '@mui/material';
import { useNavigate, useParams } from 'react-router-dom';

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

const EditPart = () => {
    const [part, setPart] = React.useState("");
    const [name, setName] = React.useState("");
    const [open, setOpen] = React.useState(false);
    let navigate = useNavigate();
    const [computer, setComputer] = React.useState(0);
    const [loading, setLoading] = useState(true);
    let { partId } = useParams();
    const handleNameChange = (event) => {
      setName(event.target.value);
  };
    const getPart = async () => {
      const token = localStorage.getItem("token");
      const url = "https://kompiuterija20221102215702.azurewebsites.net/parts/"+partId;
      try {
        const { data } = await axios.get(
          url, { headers: { "Authorization": `Bearer ${token}` } }
        )
        const part = data;
        setComputer(part.computerId);
        setPart(part.type);
        setName(part.name)
        setLoading(false);
      }
      catch (err) {
        setPart(0);
        setLoading(false);
      };
    }
    const handleOpen = () => setOpen(true);
    const handleClose = () =>  {
        setOpen(false);
        navigate(-1);
    }
    const handleChange = (event) => {
        setPart(event.target.value);
    };
    const handleSubmit = async (event) => {
        event.preventDefault();
        const url = "https://kompiuterija20221102215702.azurewebsites.net/parts";
        const token = localStorage.getItem("token");
        const data = new FormData(event.currentTarget);
        const numericId = parseInt(partId)
        const loginPayload = {
          id: numericId,
          computerId: computer,
          name: data.get('name'),
          type: part
        };
        await axios({
            method: 'put',
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
      useEffect(() => {
        getPart();
      }, []);
      if (loading) {
        return (
          <div>
            <MenuBar />
            <div className="center"><CircularProgress /></div>
          </div>
    
        );
      }
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
                Update part
              </Typography>
              <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
              <FormControl sx={{ m: 1, minWidth: 300 }}>
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  id="name"
                  label="Part name"
                  name="name"
                  value={name}
                  autoComplete="name"
                  onChange={handleNameChange}
                  autoFocus
                />
                <Select
                    labelId="typeLabel"
                    id="type"
                    required
                    value={part}
                    onChange={handleChange}
                >
                    <MenuItem value="">
                      <em>Select part type</em>
                    </MenuItem>
                        <MenuItem key={1} value="GPU">GPU</MenuItem>
                        <MenuItem key={2} value="CPU">CPU</MenuItem>
                        <MenuItem key={3} value="RAM">RAM</MenuItem>
                        <MenuItem key={4} value="PSU">PSU</MenuItem>
                        <MenuItem key={5} value="HDD">HDD</MenuItem>
                        <MenuItem key={6} value="SSD">SSD</MenuItem>
                </Select>
                <Button
                  type="submit"
                  fullWidth
                  variant="contained"
                  sx={{ mt: 3, mb: 2 }}
                >
                  Update part
                </Button>
                </FormControl>
                <Modal
                    open={open}
                    onClose={handleClose}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={style}>
                    <Typography id="modal-modal-title" variant="h6" component="h2">
                        Part has been successfully updated
                    </Typography>
                    </Box>
                </Modal>
              </Box>
            </Box>
          </Container>
        </div>
          
      );
    }
  export default EditPart;