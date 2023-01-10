import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import Rock from '../components/rockComponent.js'
import Spinner from '../components/loadingComponent';


import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import { Button } from 'react-bootstrap';
import { Alert } from 'react-bootstrap';

import axios from "axios";
import React from 'react';
import { useParams } from 'react-router-dom';
import { useAuthUser } from 'react-auth-kit';


const baseURL = "https://localhost:7191/api/";

function Areas() {
    const auth = useAuthUser();

    //DIPLAY
    const {Id} = useParams()
    const [areas, setAreas] = React.useState(null);
    const [region, setRegion] = React.useState(null);
    const [isLoading, setLoading] = React.useState(true);

    //ADD AREA
    const [name, setName] = React.useState("");
    const [description, setDescription] = React.useState("");
    const [success, setSuccess] = React.useState(false);
    const [error, setError] = React.useState(false);

    React.useEffect(() => {
            getAreas();
        }, []);

        const getAreas = () => {
            axios.post(`${baseURL}areas/getareaswithnumberofroutesbyregionid?regionId=${Id}`, {
                withCredentials: true
            }).then((response) => {
                setAreas(response.data);
                axios.post(`${baseURL}regions/getregionbyid?regionId=${Id}`).then((response) => {
                    setRegion(response.data);
                    setLoading(false);
                })
            });
        }

    const newArea = {
        name: name,
        description: description
    }


    
    const handleSubmit = () =>{
        axios({
            method: 'PUT',
            url: `${baseURL}areas/insert?regionId=${Id}`,
            data: JSON.stringify(newArea),
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        }).then(response => {
            if(response) {
                setSuccess(true);
                getAreas();
            }
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });
    }

    const handleEdit = (id) =>{
        axios({
            method: 'POST',
            url: `${baseURL}areas/update?regionId=${Id}`,
            data: {
                areaId : id,
                name: name,
                description: description
            },
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        }).then(response => {
            if(response) {
                setSuccess(true);
                getAreas();
            }
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });
    }

    const handleDelete = (id) =>{
        axios.post(`${baseURL}areas/delete?areaId=${id}`).then(response => {
            if(response) {
                setSuccess(true);
                getAreas();
            }
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });
    }

    const form = () => {
        return(
            <>
            <form onSubmit={handleSubmit} className="form">
                <label>Nazwa</label>
                <input 
                    type="text" 
                    value={name}
                    required
                    onChange={(e) => setName(e.target.value)}
                />
                <label>Opis</label>
                <input 
                    type="text" 
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
            </form>
            </>
        )
    }

    const closeModal = () => {
        getAreas();
        clearVariables();
    }

    const clearVariables = () => {
        setName("");
        setDescription("");
    }

    if(isLoading)
        return (<Spinner/>)

    return (
        <div className='container'>
        <h2> {region.name}</h2>
        <Tabs  
                defaultActiveKey="rocks"
                id="uncontrolled-tab-example"
                className="mb-3">
                <Tab eventKey="rocks" title="Rejony">
                    <ul>
                        {areas.map(function(element, index){
                            return(<Rock key = {index} Id={element.area.areaId} 
                            element={element.area} numberOfRoutes = {element.numberOfRoutes} formTemplate = {form}
                            onEdit = {handleEdit} onDelete = {handleDelete} onHide={closeModal}/>)
                        }
                        )}
                    </ul>
                </Tab>
                <Tab eventKey="description" title="Opis">
                    <p className="description">
                        {region.description}
                    </p>
                </Tab>


                {auth().role == "Admin" ?
                 <Tab eventKey="add" title="Dodaj">
                    {form()}
                    <Button variant="outline-success" onClick={handleSubmit} className="mr-btm-3">Dodaj</Button>
                    {error ? <Alert variant="danger">Podczas dodawania rejonu wystąpił błąd.</Alert> : "" }     
                    {success ? <Alert variant="success">Pomyślnie dodano rejon!</Alert> : "" }     

                </Tab>
             : ""}


        </Tabs>
        
        </div>
    );
}

export default Areas;
