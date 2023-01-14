import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from "axios";
import React from 'react';

import Image from 'react-bootstrap/Image'
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';

import Route from '../components/RouteRowComponent';
import RockInformation from '../components/RockInformation';
import MapComponent from '../components/mapComponent';
import Spinner from '../components/loadingComponent';
import { Alert, Button } from 'react-bootstrap';

import { useParams } from "react-router-dom";
import { useAuthUser } from 'react-auth-kit';


const baseURL = "https://localhost:7191/api/";

function Element(props) {
    const auth = useAuthUser();
    const role = auth() !== null ? auth().role : "";

    const { Id } = useParams();
    const [isLoading, setLoading] = React.useState(true);
    const [routes, setRoutes] = React.useState(null);
    const [rock, setRock] = React.useState(null);
    const [dominantFormations, setDominantFormations] = React.useState(null);


    //FORM
    const [number, setNumber] = React.useState(0);
    const [name, setName] = React.useState("");
    const [grade, setGrade] = React.useState("");
    const [description, setDescription] = React.useState("");
    const [author, setAuthor] = React.useState("");
    const [year, setYear] = React.useState("");

    //
    const [success, setSuccess] = React.useState(false);
    const [error, setError] = React.useState(false);

    React.useEffect(() => {
        getRoutes();
    }, []);

    const handleSubmit = () => {
         const newRoute = {
            number: number,
            name: name,
            difficulty: grade,
            author: author,
            year: year,
            description: description
        }

        axios({
            method: 'PUT',
            url: `${baseURL}routes/insert?rockId=${Id}`,
            data: JSON.stringify(newRoute),
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        }).then(response => {
            if(response)//probably response should be converted to bool
                setSuccess(true);
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });
    }
        const handleEdit = (routeId) =>{
        axios({
            method: 'POST',
            url: `${baseURL}routes/update`,
            data: {
                routeId: routeId,
                number: number,
                name: name,
                difficulty: grade,
                author: author,
                year: year,
                description: description,
                rockId: Id
            },
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        }).then(response => {
            if(response) {
                setSuccess(true);
                getRoutes();
            }
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });
    }

    const handleDelete = (id) =>{
        axios.post(`${baseURL}routes/delete?routeId=${id}`).then(response => {
            if(response) {
                setSuccess(true);
                getRoutes();
            }
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });
    }

    const getRoutes = () => {
        axios.post(`${baseURL}routes/getroutesbyrockid?rockId=${Id}`).then((response) => {
            setRoutes(response.data);
            axios.post(`${baseURL}Rocks/getbyid?rockId=${Id}`).then((response) => {
                setRock(response.data);
                axios.post(`${baseURL}rocks/getrocksdominantformations?rockId=${Id}`).then((response) =>{
                    setDominantFormations(response.data);
                    setLoading(false);
                })
            })
        });
    }

    const form = () => {
        return(
            <form onSubmit={handleSubmit} className="form">
                <label>Numer drogi</label>
                <input 
                    required
                    type="number" 
                    min="1"
                    value={number}
                    onChange={(e) => setNumber(e.target.value)}
                />
                <label>Nazwa</label>
                <input 
                    required
                    type="text" 
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <label>Trudność</label>
                <input 
                    type="textr" 
                    value={grade}
                    onChange={(e) => setGrade(e.target.value)}
                />
                <label>Opis</label>
                <input 
                    type="text" 
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
                <label>Autor</label>
                <input 
                    type="text" 
                    value={author}
                    onChange={(e) => setAuthor(e.target.value)}
                />
                <label>Rok wytyczenia</label>
                <input 
                    type="number" 
                    value={year}
                    onChange={(e) => setYear(e.target.value)}
                />
            </form>
        )
    }

    const closeModal = () => {
        getRoutes();
        clearVariables();
    }

    const clearVariables = () => {
        setNumber(0);
        setName("");
        setDescription("");
        setGrade("");
        setAuthor("");
        setYear(0);
    }

    if(isLoading)
        return (<Spinner/>)

    return (
        <div className='container'>
            <h2>{rock.name}</h2>
            
            <div className="rockImage">
                <Image src="/img/migdalowka.jpg" fluid/>
            </div>
            <div className="infoSection">
                <Tabs
                    defaultActiveKey="routes"
                    id="uncontrolled-tab-example"
                    className="mb-3"
                    >
                    <Tab eventKey="routes" title="Drogi">
                        <ul className="routeList">
                            { routes.map(function(element, index){
                                        return(<Route key = {index} 
                                                    routeId={element.routeId}
                                                    number={element.number}
                                                    name = {element.name}
                                                    grade = {element.difficulty}
                                                    author = {element.author}
                                                    year = {element.year}
                                                    info = {element.description}
                                                    formTemplate = {form}
                                                    onEdit = {handleEdit}
                                                    onDelete = {handleDelete}
                                                    onHide = {closeModal}/>
                                                    )
                                        })}
                        </ul>
                    </Tab>
                    {rock.description ? <Tab eventKey="description" title="Opis">
                                            <p className="description">
                                                {rock.description}
                                            </p>
                                        </Tab> : "" }
                    <Tab eventKey="info" title="Informacje">
                        <RockInformation rock={rock} dominantFormations={dominantFormations}/>
                    </Tab>
                    <Tab eventKey="map" title="Mapa">
                        <MapComponent name={rock.name} position={[rock.latitude, rock.longitude]}/>
                    </Tab>
                    {role == "Admin" ?
                 <Tab eventKey="add" title="Dodaj">
                    {form()}
                    <Button variant="outline-success"onClick={handleSubmit} className="mr-btm-3">Dodaj</Button>
                    {error ? <Alert variant="danger">Podczas dodawania drogi wystąpił błąd.</Alert> : "" }     
                    {success ? <Alert variant="success">Pomyślnie dodano drogę!</Alert> : "" }     

                </Tab>
             : ""}
                </Tabs>
            </div>



        </div>
    );
}

export default Element;
