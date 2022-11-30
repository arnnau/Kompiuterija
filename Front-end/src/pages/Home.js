
import '../App.css';
import { Button, Stack } from '@mui/material';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import MenuBar from '../MenuBar.js';
import { IsTokenExpired } from '../Auth.js';
import { CardActionArea, CardMedia, CardContent, Typography, Card } from '@mui/material';

function Home() {
    let navigate = useNavigate();
    function handleLoginClick() {
        navigate('/login');
    }
    function handleRegisterClick() {
        navigate('/register');
    }
    if (!IsTokenExpired) {
        return (
            <div>
                <MenuBar />
                <div className="center">
                    <Stack spacing={2} direction="column">
                        <Button size="large" variant="contained" onClick={handleLoginClick}>Login</Button>
                        <Button size="large" variant="contained" onClick={handleRegisterClick}>Register</Button>
                    </Stack>
                </div>
            </div>

        );
    }
    else {
        return (
            <div>
                <MenuBar />
                <div className="center">
                    <Stack spacing={2} direction="column">
                        <Card sx={{ maxWidth: 345 }} key={1}>
                            <CardActionArea component={Link} to="/shops">
                                <CardMedia
                                    component="img"
                                    height="140"
                                    src={require("../public/shop.jpg")}
                                    alt={"Shops"}
                                />
                                <CardContent>
                                    <Typography gutterBottom variant="h5" component="div">
                                        Shops
                                    </Typography>
                                </CardContent>
                            </CardActionArea>

                        </Card>
                        <Card sx={{ maxWidth: 345 }} key={2}>
                            <CardActionArea component={Link} to="/computers">
                                <CardMedia
                                    component="img"
                                    height="140"
                                    src={require("../public/computer.jpg")}
                                    alt={"Computers"}
                                />
                                <CardContent>
                                    <Typography gutterBottom variant="h5" component="div">
                                        Computers
                                    </Typography>
                                </CardContent>
                            </CardActionArea>

                        </Card>
                        <Card sx={{ maxWidth: 345 }} key={3}>
                            <CardActionArea component={Link} to="/parts">
                                <CardMedia
                                    component="img"
                                    height="140"
                                    src={require("../public/part.jpg")}
                                    alt={"Parts"}
                                />
                                <CardContent>
                                    <Typography gutterBottom variant="h5" component="div">
                                        Parts
                                    </Typography>
                                </CardContent>
                            </CardActionArea>

                        </Card>
                    </Stack>

                </div>
            </div>

        );
    }
}

export default Home;
