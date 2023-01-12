import '../App.css';
import Spinner from '../components/loadingComponent';
import SearchResult from '../components/searchResult'

import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import Accordion from 'react-bootstrap/Accordion';



const baseURL = "https://localhost:7191/api/";

export default function SearchResultPage(props) {

    const phrase = useParams().phrase;
    const [result, setResult] = useState("");
    const [isLoading, setLoading] = useState(true);

    useEffect(() => {
        getResult();
    },[phrase])

    const getResult = () =>{
        axios.post(`${baseURL}app/search?phrase=${phrase}`)
        .then((response) => {
            if(response.status !== 204)
                setResult(response.data);
            setLoading(false);
        })
    } 

    if(isLoading)
        return (<Spinner/>)
        
    if(!result)
        return(
            <div className = "container">
                <h2>Wyniki wyszukiwania</h2>
                <hr></hr>
                Brak wyników wyszukiwania
            </div>
        ) 
    return(
        <div className = "container">
            <h2>Wyniki wyszukiwania</h2>
            <hr></hr>
            {result.regions.length > 0 ? 
                <SearchResult item={result.regions} type="regions" name="Regiony"/>
             : "" }
            {result.areas.length > 0 ? 
                <SearchResult item={result.areas} type="areas" name="Rejony"/>
             : "" }
            {result.rocks.length > 0 ? 
                <SearchResult item={result.rocks} type="rocks" name="Skały"/>
             : "" }
            {result.routes.length > 0 ? 
                <SearchResult item={result.routes} type="routes" name="Drogi"/>
             : "" }

        </div>
    )
}