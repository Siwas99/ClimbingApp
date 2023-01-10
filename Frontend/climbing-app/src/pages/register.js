import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import axios from "axios";
import {useState} from 'react';
import { useSignIn } from 'react-auth-kit';
import { useNavigate } from 'react-router-dom';


import Button from 'react-bootstrap/Button';
import Alert from 'react-bootstrap/Alert';


const baseURL = "https://localhost:7191/api/";

const linkStyle = {
    color: "green"
}

function Login() {
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [email, setEmail] = useState("");
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [Error, setError] = useState(null);
    const signIn = useSignIn();
    const navigate = useNavigate();


    const handleSubmit = (event) => {
        event.preventDefault();
        axios.post(`${baseURL}auth/register`,{
            name: name,
            surname: surname,
            email: email,
            login: username,
            password: password
        }).then((response) => {
        if(signIn(
            {
            token: response.data,
            expiresIn: 3600,
            tokenType: "Bearer",
            authState: {login: username}
            }
        ))
        {
            navigate('/login');
        }
        else
        {
            setError(response.data);
        }
        }).catch(function(error){
        setError(error.response.data);
        });
    }

    return (
        <div className='container'>
            <h3>Rejestracja</h3>
            <form onSubmit={handleSubmit} className="form">
                <label>Imię</label>
                <input 
                    type="text" 
                    placeholder='Jan'
                    value={name}
                    required
                    onChange={(e) => setName(e.target.value)}
                />
                <label>Nazwisko</label>
                <input 
                    type="text" 
                    placeholder='Kowalski'
                    value={surname}
                    required
                    onChange={(e) => setSurname(e.target.value)}
                />
                <label>Adres Email</label>
                <input 
                    type="email" 
                    placeholder='example@mail.com'
                    value={email}
                    required
                    onChange={(e) => setEmail(e.target.value)}
                />
                <label>Nazwa użytkownika</label>
                <input 
                    type="text" 
                    placeholder='Nazwa użytkownika'
                    value={username}
                    required
                    onChange={(e) => setUsername(e.target.value)}
                />
                <label>Hasło</label>
                <input 
                    type="password" 
                    placeholder='Hasło'
                    value={password}
                    required
                    onChange={(e) => setPassword(e.target.value)}
                />
                <Button type="submit" variant="outline-success">Zarejestruj się</Button>
            </form>
            <br/>
            {Error ? <Alert variant="danger">{Error}</Alert> : "" }
        </div>
    )
}

export default Login;
