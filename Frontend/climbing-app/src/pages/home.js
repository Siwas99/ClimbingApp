import '../App.css';

import LastActivityComponent from '../components/lastAcitivityComponent';

import Image from 'react-bootstrap/Image'
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { Link } from 'react-router-dom';
import { useState, useEffect } from 'react';
import axios from 'axios';


const baseURL = "https://localhost:7191/api/";

export default function Home() {    
    const [searchPhrase, setPhrase] = useState("");
    const [logs, setLogs] = useState("");

    useEffect(() => {
        getLogs();
    }, [])

    const getLogs = async ()=>{
        axios.get(`${baseURL}expeditionlogs/getlastest`)
        .then((response) => {
            if(response.status === 200)
                setLogs(response.data);
        })
    }
    return (
        <>
       <div className="mainImage">
            <Image src="img/me.jpg" fluid/>
        </div>
        <div className = "container">
            <Form className="d-flex mainSearchBar">
                <Form.Control
                type="search"
                placeholder=" np. Kaszanka dla Kierownika"
                className="me-2"
                aria-label="Search"
                onChange={(e) => setPhrase(e.target.value)}
                />
            <Link to = {`searchResults/${searchPhrase}`}>
                <Button variant="outline-success">Szukaj</Button>
            </Link>
            </Form>
            <div className="lastActivites">
            <h3>Ostatnio pokonane</h3>    
            {logs ? 
                logs.map(function(element, index){
                    return(
                        <LastActivityComponent  key = {index}
                                                name = {element.route.name}
                                                rock = {element.route.rock.name}
                                                grade = {element.route.difficulty}
                                                login = {element.user.login} />
                    )
                })
                 :
                    ""
            }
            </div>
        </div>
</>     
    )
}