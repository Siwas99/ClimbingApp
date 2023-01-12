import '../App.css';
import Spinner from `../components/loadingComponent`

import { useState, useEffect } from 'react';
import axios from 'axios';

const baseURL = "https://localhost:7191/api/";

export default function AdminPanel(props) {

    // const phrase = useParams();
    // const [result, setResult] = useState("");
    // const [isLoading, setLoading] = useState(true);

    // useEffect(() => {
    //     getResult();
    // },[])

    // const getResult = () =>{
    //     axios.post(`${baseURL}app/search?phrase=${phrase}}`)
    //     .then((response) => {
    //         setResult(response.data);
    //         setLoading(false);
    //     })
    // } 

    // (isLoading)
    //     return <Spinner/>

    // return(
    //     <>
    //         <h3>Panel Administratora</h3>

    //         place here some search result
    //     </>
    // )
}