import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Spinner from '../components/loadingComponent';


import axios from "axios";
import React from 'react';
import {Link} from 'react-router-dom'


import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useParams } from "react-router-dom";



const baseURL = "https://localhost:7191/api/";

const linkStyle = {
    width: "100%",
    color: "black"
}

export default function Profile() {
    const { Id } = useParams()
    // const [user, setUser] = React.useState(null);
    // const [isLoading, setLoading] = React.useState(true);

    // React.useEffect(() => {
    //         axios.get(`${baseURL}regions/getregions`).then((response) => {
    //             setUser(response.data);
    //             setLoading(false);
    //             });
    //     }, []);


//UNCOMMENT WHEN DOWNLOAND USER DATA WILL BE COMPLETED
    // if(isLoading)
    //     return(<Spinner/>)

    const user = {
        name: "Łukasz",
        surname: "Pierdzibąk",
        email: "piedziluki@gmail.com",
        login: "pierdzioszek"
    }
    const userRoutes = {
        beated: 23,
        planned: 31
    }
    return (
        <div className='container'>
            <h2>Witaj {user.name}!</h2>
            <hr/>

            <h4>Statystyki:</h4>
            <div className="profileStats">
                <Link to={`/journey`} style={linkStyle}>
                    <div className="statsContainer">
                    Pokonane drogi: <span>{userRoutes.beated}</span>
                    </div>
                </Link>
                <Link to={`/wishlist`} style={linkStyle}>
                    <div className="statsContainer">
                    Zaplanowane drogi: <span>{userRoutes.planned}</span>
                    </div>
                </Link>
            </div>
            <div className="manageProfile">
                <h4>Zarządzaj profilem</h4>
                <Form>
                    <Form.Group className="mb-3" controlId="formBasicName">
                        <Form.Label>Imię</Form.Label>
                        <Form.Control type="email" placeholder={user.name} />
                    </Form.Group>
                    
                    <Form.Group className="mb-3" controlId="formBasicSurname">
                        <Form.Label>Nazwisko</Form.Label>
                        <Form.Control type="email" placeholder={user.surname} />
                    </Form.Group>
                    
                    <Form.Group className="mb-3" controlId="formBasicEmail">
                        <Form.Label>Adres Email</Form.Label>
                        <Form.Control type="email" placeholder={user.email} />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicLogin">
                        <Form.Label>Login</Form.Label>
                        <Form.Control type="email" placeholder={user.login} />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicPassword">
                        <Form.Label>Nowe hasło</Form.Label>
                        <Form.Control type="password" placeholder="Hasło" />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicConfirmPassword">
                        <Form.Label>Powtórz hasło</Form.Label>
                        <Form.Control type="password" placeholder="Hasło" />
                    </Form.Group>
                    <Button variant="outline-success" type="submit">
                        Edytuj
                    </Button>
                </Form>
                <hr/>
                <Button variant="outline-danger">Zamknij konto</Button>
            </div>

        </div>
    );
}