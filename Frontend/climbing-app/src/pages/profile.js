import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Spinner from '../components/loadingComponent';


import axios from "axios";
import {useState, useEffect} from 'react';
import {Link} from 'react-router-dom'


import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Alert from 'react-bootstrap/Alert';
import { useParams, useNavigate } from "react-router-dom";
import { useSignOut, useAuthUser, useSignIn } from 'react-auth-kit';




const baseURL = "https://localhost:7191/api/";

const linkStyle = {
    width: "100%",
    color: "black"
}

export default function Profile() {
    const { Id } = useParams()
    const [user, setUser] = useState("");
    const [logs, setLogs] = useState("");
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [email, setEmail] = useState("");
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [isMatched, setMatch] = useState(true);
    const [showAlert, setAlert] = useState({isVisiable: false, message: "", variant: ""});
    

    const [secondPassword, setSecondPassword] = useState("")
    const [wishlist, setWishlist] = useState("");
    const [isLoading, setLoading] = useState("");
    const auth = useAuthUser();
    const signOut = useSignOut();
    const signIn = useSignIn();
    const navigate = useNavigate();


    useEffect(() => {
        getUser()
    }, []);

    const getUser =  () => {
        axios.post(`${baseURL}users/getbylogin?login=${auth().login}`)
        .then((response) => {
            setUser(response.data);
            axios.post(`${baseURL}expeditionlogs/getexpeditionlogsbylogin?login=${auth().login}`)
            .then(response => {
                setLogs(response.data);
                axios.post(`${baseURL}wishlist/getwishlistbylogin?login=${auth().login}`)
                .then(response => {
                    setWishlist(response.data);
                    setLoading(false);
                })
            })
        })
    }
    console.log(auth());

    const closeAccount = () => {
        axios.post(`${baseURL}users/delete?id=${user.userId}`)
        .then(response => {
            if(response.data === true)
                signOut();
                navigate("/");
                window.location.reload();
        }).catch(() => {
            setAlert({isVisible: true, message: "Podczas usuwania konta wystąpił błąd.", variant: "danger"});
        })
    }

    const updateAccount = () => {
        if(password !== secondPassword){
            setAlert({isVisible: true, message:"Podane hasła różnią się." , variant: "danger"} );
            return;
        }

        axios.post(`${baseURL}users/update`, {
            userId: user.userId,
            name: name,
            surname: surname,
            email: email,
            login: login,
            password: password
        }).then(response => {
            if(response.data === true ){
                if(signIn(
                {
                    token: response.data.token,
                    expiresIn: 7200,
                    tokenType: "Bearer",
                    authState: {login: response.data.login,
                                role: user.role}
                }
                ))
                setUser(response.data);
                setAlert({isVisible: true, message:"Pomyślnie edytowano użytkownika", variant: "success"} );
            }
            else
                setAlert({isVisible: true, message:"Podczas edycji użytkownika wystąpił błąd", variant: "danger"} );
        }).catch(response => {
            setAlert({isVisible: true, message:"Podczas edycji użytkownika wystąpił błąd", variant: "danger"} );
        })
    }

    if(isLoading)
        return(<Spinner/>)

    return (
        <div className='container'>
            <h2>Witaj {user.name}!</h2>
            <hr/>

            <h4>Statystyki:</h4>
            <div className="profileStats">
                <Link to={`/journey`} style={linkStyle}>
                    <div className="statsContainer">
                    Pokonane drogi: <span>{logs.length}</span>
                    </div>
                </Link>
                <Link to={`/wishlist`} style={linkStyle}>
                    <div className="statsContainer">
                    Zaplanowane drogi: <span>{wishlist.length}</span>
                    </div>
                </Link>
            </div>
            <div className="manageProfile">
                <h4>Zarządzaj profilem</h4>
                <Form>
                    <Form.Group className="mb-3" controlId="formBasicName">
                        <Form.Label>Imię</Form.Label>
                        <Form.Control type="text" placeholder={user.name} value={name} onChange={(e) => {setName(e.target.value)} }/>
                    </Form.Group>
                    
                    <Form.Group className="mb-3" controlId="formBasicSurname">
                        <Form.Label>Nazwisko</Form.Label>
                        <Form.Control type="text" placeholder={user.surname} value={surname} onChange={(e) => {setSurname( e.target.value)} }/>
                    </Form.Group>
                    
                    <Form.Group className="mb-3" controlId="formBasicEmail">
                        <Form.Label>Adres Email</Form.Label>
                        <Form.Control type="email" placeholder={user.email} value={email} onChange={(e) => {setEmail( e.target.value)} }/>
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicLogin">
                        <Form.Label>Login</Form.Label>
                        <Form.Control type="text" placeholder={user.login} value={login} onChange={(e) => {setLogin( e.target.value)} }/>
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicPassword">
                        <Form.Label>Nowe hasło</Form.Label>
                        <Form.Control type="password" placeholder="Hasło" value={password} onChange={(e) => {setPassword(e.target.value)} }/>
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicConfirmPassword">
                        <Form.Label>Powtórz hasło</Form.Label>
                        <Form.Control type="password" placeholder="Hasło"  value={secondPassword} onChange={(e) => { setSecondPassword(e.target.value)} }/>
                    </Form.Group>
                    <Button variant="outline-success"  onClick = {updateAccount}>
                        Edytuj
                    </Button>
                    {showAlert.isVisible ? 
                        <Alert variant={showAlert.variant} style={{marginTop:"2%"}}>
                            {showAlert.message}
                        </Alert> :
                        ""
                    }
                </Form>
                <hr/>
                {
                    auth().role === "Admin" ?
                    <><Button variant="outline-primary" onClick = {() => navigate("/adminPanel")}>Panel Administratora</Button> <hr/> </>:
                    ""
                }
                <Button variant="outline-danger" onClick={closeAccount}>Zamknij konto</Button>
            </div>

        </div>
    );
}