import '../App.css';

import { useState, useEffect } from 'react';
import axios from 'axios';
import { Alert, Form, Button } from 'react-bootstrap';


const baseURL = "https://localhost:7191/api/";

export default function AdminPanel(props) {
    const [userLogin, setUserLogin] = useState("");
    const [alert, setAlert] = useState({isVisible: false, message: "", variant: ""});

    const handlePromote = async () => {
        await axios.post(`${baseURL}users/promoteuser?login=${userLogin}`)
        .then(response => {
            if(response.data === true)
                setAlert({isVisible: true, message: `Użytkownik ${userLogin} został administratorem.`, variant: "success"});
            else
                setAlert({isVisible: true, message: `Nie znaleziono użytkownika ${userLogin} w bazie.`, variant: "danger"});
        }).catch(response => {
            setAlert({isVisible: true, message: "Wystąpił nieoczekiwany błąd.", variant: "danger"});
        });
    }
    
    const handleDelete = async () => {
        await axios.post(`${baseURL}users/deletebylogin?login=${userLogin}`)
        .then(response => {
            if(response.data === true)
                setAlert({isVisible: true, message: `Użytkownik ${userLogin} został usunięty.`, variant: "success"});
            else
                setAlert({isVisible: true, message: `Nie znaleziono użytkownika ${userLogin} w bazie.`, variant: "danger"});
        }).catch(response => {
            setAlert({isVisible: true, message: "Wystąpił nieoczekiwany błąd.", variant: "danger"});
        });
    }

    return(
        <div className="container">
            <h2> Panel Administratora</h2>
            <hr/>
            <Form>
            <Form.Group className="mb-3" controlId="">
                <Form.Label>Nazwa użytkownika</Form.Label>
                <Form.Control type="text" placeholder="Wprowadź nazwę użytkownika" value = {userLogin} onChange={(e) => {setUserLogin(e.target.value)}}/>
            </Form.Group>

            <div className="d-flex" style={{justifyContent: "space-between"}}>
                <Button variant="outline-success" onClick={handlePromote}>
                    Awansuj na administratora
                </Button>

                <Button variant="outline-danger" onClick={handleDelete}>
                    Usuń
                </Button>
            </div>
            </Form>
            <br/>
            {
                alert.isVisible ?
                
                <Alert variant={alert.variant}>
                {alert.message}
                </Alert> :
                ""
            } 
        </div>
    )
}