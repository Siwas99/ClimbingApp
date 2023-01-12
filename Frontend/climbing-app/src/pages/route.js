import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from "axios";
import React from 'react';

import Image from 'react-bootstrap/Image'
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import Button from 'react-bootstrap/Button';


import RockInformation from '../components/RockInformation';
import MapComponent from '../components/mapComponent';
import Spinner from '../components/loadingComponent';
import AddRouteToJourneyModal from '../components/addRoutetoJourneyModal';

import { useParams } from "react-router-dom";
import {useAuthUser} from 'react-auth-kit'



const baseURL = "https://localhost:7191/api/";
axios.defaults.withCredentials = true

function Element(props) {
    const auth = useAuthUser();
    const { Id } = useParams();
    const [isLoading, setLoading] = React.useState(true);
    const [route, setRoute] = React.useState(null);
    const [dominantFormations, setDominantFormations] = React.useState(null);
    const [modalShow, setModalShow] = React.useState(false);
    const [wishlist, setWishlist] = React.useState(false);
    const [journey, setJourney] = React.useState(false);


    React.useEffect(() => {
        getRoute();
    }, []);
    
    const getRoute = async () => {
        await axios.post(`${baseURL}routes/getroutebyid?routeId=${Id}`).then((response) => {
            setRoute(response.data);
            checkJourney(response.data.routeId);
            checkWishlist(response.data.routeId);
            axios.post(`${baseURL}rocks/getrocksdominantformations?rockId=${response.data.rock.rockId}`).then((response) =>{
                setDominantFormations(response.data);
                setLoading(false);
            });
        });
    }

    const addToWishlist = async () => {
        axios({
            method: 'POST',
            url: `${baseURL}wishlist/insert?login=${auth().login}`,
            data: JSON.stringify(route),
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        }).then(response => {
            console.log(response.data);
            if(response.data === true)
                setWishlist(true);
        });
    }

    const deleteFromWishlist = async () => {
        await axios.post(`${baseURL}wishlist/deletebyloginandroute`,{
            login: auth().login,
            routeId: route.routeId
        }).then(response => {
            if(response.data  === true){
                setWishlist(false);
                console.log(`deletewishlist is ${wishlist}`);

            }
        });
    }

    const deleteFromJourney = async () => {
        await axios.post(`${baseURL}expeditionlogs/deletebyloginandroute`,{
            login: auth().login,
            routeId: route.routeId
        }).then(response => {
            if(response.data === true){
                setJourney(false);
            }
        });
    }

    const checkWishlist = async (routeId) => {
        await axios.post(`${baseURL}wishlist/checkifwishlistexists`,{
            login: auth().login,
            routeId: routeId
        }).then(response => {
            if(response.data  === true)
                setWishlist(true);
        });
    }

    const checkJourney = async (routeId) => {
        await axios.post(`${baseURL}expeditionlogs/checkifexpeditionlogexists`,{
            login: auth().login,
            routeId: routeId
        }).then(response => {
            if(response.data  === true)
                setJourney(true);
        });
    }

    const closeModal = (isAdded) => {
        setModalShow(false);
        setJourney(isAdded);
    }

    if(isLoading)
        return (<Spinner/>)

    return (
        <div className='container'>
            <h2>{route.name}</h2>
            
            <div className="rockImage">
                <Image src="/img/migdalowka.jpg" fluid/>
            </div>
            <div className="infoSection">
                <Tabs
                    defaultActiveKey="info"
                    id="uncontrolled-tab-example"
                    className="mb-3"
                    >
                    <Tab eventKey="info" title="Informacje">
                        {route.rock.description ? route.rock.description : ""}
                        <br/>
                        <br/>
                        
                        <RockInformation route={route} isRoute = {true} />
                        <br/>
                        <RockInformation rock={route.rock} dominantFormations={dominantFormations} />
                    </Tab>
                    <Tab eventKey="map" title="Mapa">
                        <MapComponent name="Biblioteka" position={[12.345, 34.123]}/>
                    </Tab>
                </Tabs>
                <br/>
            </div>
            {
                journey ?
                <Button variant="outline-danger" onClick={deleteFromJourney}>Usuń wpis z dziennika</Button> :
                <Button variant="outline-success" onClick={() => setModalShow(true)}>Dodaj wpis do dziennika</Button>
            }
            { 
                wishlist ?
                <Button variant="outline-danger" onClick={deleteFromWishlist} style={{margin: "2% 0"}}>Usuń z planowanych</Button> :
                <Button variant="outline-success" onClick={addToWishlist} style={{margin: "2% 0"}}>Dodaj do planowanych</Button>
                
            }
            <AddRouteToJourneyModal show={modalShow} onHide={closeModal} route={route} />
        </div>
    );
}

export default Element;
