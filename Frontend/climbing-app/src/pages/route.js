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
    const [modalShow, setModalShow] = React.useState(false);


    React.useEffect(() => {
        axios.post(`${baseURL}routes/getroutebyid?routeId=${Id}`).then((response) => {
            setRoute(response.data);
            setLoading(false);
            });
    }, []);
    
    const addToWishlist = async () => {
//         const usersName = JSON.stringify({ login: 'John Doe' });
//         const customConfig = {
//             headers: {
//             'Content-Type': 'application/json'
//             }
//         };
// const result = await axios.post(`${baseURL}wishlist/insert`, usersName, customConfig);
    const login = auth().login;

        // axios.post(`${baseURL}wishlist/insert`, {
        //     login : login,
        //     route : route
        // }).then((response)=>{
        //     alert("Dodano do planowanych");
        // })


        axios({
            method: 'POST',
            url: `${baseURL}wishlist/insert?login=${login}`,
            data: JSON.stringify(route),
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        });


        // axios.put(`${baseURL}wishlist/insert`, {route}, {
        // withCredentials: true,
        // headers: {'Access-Control-Allow-Origin': '*', 'Content-Type': 'application/json'
        // }});


        // fetch(`${baseURL}wishlist/insert`,{
        //     method: "POST",
        //     headers:{"Content-Type": "application/json"},
        //     body: JSON.stringify({
        //         route: route
        //     })
        // }).then(() =>{

        //     alert("działa");
        // });
        
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
                        <RockInformation rock={route.rock} />
                    </Tab>
                    <Tab eventKey="comments" title="Komentarze">

                    </Tab>

                    <Tab eventKey="map" title="Mapa">
                        <MapComponent name="Biblioteka" position={[12.345, 34.123]}/>
                    </Tab>
                </Tabs>
                <br/>
            </div>

            <Button variant="outline-success" onClick={() => setModalShow(true)}>Dodaj wpis do dziennika</Button>
            <Button variant="outline-success" onClick={addToWishlist} style={{margin: "2% 0"}}>Dodaj do planowanych</Button>
            
            <AddRouteToJourneyModal show={modalShow} onHide={() => setModalShow(false)} route={route}/>
        </div>
    );
}

export default Element;
