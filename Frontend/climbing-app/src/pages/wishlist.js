import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Spinner from '../components/loadingComponent';
import HistoryElement from '../components/historyElement';


import axios from "axios";
import React from 'react';
import {Link} from 'react-router-dom'


import { Alert } from 'react-bootstrap';
import {useAuthUser} from 'react-auth-kit';





const baseURL = "https://localhost:7191/api/";

const linkStyle = {
    width: '100%',
    color: "black"
}

export default function Journey() {
    const auth = useAuthUser();
    const [wishlists, setWishlists] = React.useState(null);
    const [isLoading, setLoading] = React.useState(true);
    const [error, setError] = React.useState(false);

    React.useEffect(() => {
            getWishlists();
        }, []);

    const getWishlists = async () => {
        await axios.post(`${baseURL}wishlist/getwishlistbylogin?login=${auth().login}`)
        .then((response) => {
            setWishlists(response.data);
            setLoading(false);
        });
    } 

    const handleDelete = (id) => {
        axios.post(`${baseURL}wishlist/delete?id=${id}`)
        .then((response) => {
            getWishlists();
            // if(!response.data )
            //     setError(true);
        }).catch(() => {
            setError(true);
        });
    }

    if(isLoading)
        return(<Spinner/>)

    return (
        <div className='container'>
            <h2>Planowane drogi</h2>
            <hr/>
            <div className="manageProfile">
                <ul>
                     { wishlists.length > 0 ?
                    wishlists.map(function(element, index){
                        return <HistoryElement 
                        key={index} 
                        element={element} 
                        getFunction={getWishlists}
                        isHistory={false}
                        handleDelete={handleDelete}
                        />
                    }) :
                    "Brak zaplanowanych przejść"
                }
                </ul>

            </div>
            {error ? <Alert variant="danger"> Podczas usuwania drogi wystąpił błąd</Alert> : ""}
        </div>
    );
}