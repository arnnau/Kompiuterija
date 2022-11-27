
import '../App.css';
import { Button, Stack } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';

function Home() {
    let navigate = useNavigate();
    function handleLoginClick() {
        navigate('/login');
    }
    function handleRegisterClick() {
        navigate('/register');
    }
    return (
        <div className="center">
            <Stack spacing={2} direction="column">
                <Button size="large" variant="contained" onClick={handleLoginClick}>Login</Button>
                <Button size="large" variant="contained" onClick={handleRegisterClick}>Register</Button>
            </Stack>
        </div>
    );
}

export default Home;
