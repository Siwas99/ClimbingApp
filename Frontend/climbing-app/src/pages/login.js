import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import axios from "axios";
import {useState} from 'react';
import {Link, useNavigate} from 'react-router-dom'
import { useSignIn } from 'react-auth-kit';



import Button from 'react-bootstrap/Button';
import Alert from 'react-bootstrap/Alert';

import Form from 'react-bootstrap/Form';

const baseURL = "https://localhost:7191/api/";

const linkStyle = {
    color: "green"
}

function Login(props) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [user, setUser] = useState(false);
  const [Error, setError] = useState(null);

  const signIn = useSignIn();
  const navigate = useNavigate();


  const handleSubmit = (event) => {
    event.preventDefault();
    axios.post(`${baseURL}auth/login`,{
      login: username,
      password: password
    }).then((response) => {
      const role = response.data.value[1];
      if(signIn(
        {
        token: response.data,
        expiresIn: 3600,
        tokenType: "Bearer",
        authState: {login: username,
                    role: role}
        }
      ))
      {
        // navigate(-1);
        }else{
          setError(response.data);
      }
    }).catch(function(error){
      setError(error.response.data);
    });
  }
  
  return (
    <div className='container'>
    <h3>Logowanie</h3>
      <form onSubmit={handleSubmit} className="form">
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
        <Button type="submit" variant="outline-success">Zaloguj się</Button>
      </form>
      <p>
          Nie masz konta? <Link to="/register" style={linkStyle}>Zarejestruj się!</Link>
      </p>
      {Error ? <Alert variant="danger">{Error}</Alert> : "" }
    </div>
  )
}

export default Login;
