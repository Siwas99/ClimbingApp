import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Spinner from '../components/loadingComponent';
import HistoryElement from '../components/historyElement';


import axios from "axios";
import React from 'react';
import {Link} from 'react-router-dom'


import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useParams } from "react-router-dom";
import {useAuthUser} from 'react-auth-kit'




const baseURL = "https://localhost:7191/api/";

const linkStyle = {
    width: '100%',
    color: "black"
}

export default function Journey() {
    const auth = useAuthUser();
    const { Id } = useParams()
    const [logs, setLogs] = React.useState(null);
    const [isLoading, setLoading] = React.useState(true);

    React.useEffect(() => {
            axios.post(`${baseURL}expoeditionlogs/getexpeditionlogbylogin`, {
                login: auth().login
            }).then((response) => {
                setLogs(response.data);
                setLoading(false);
            });
        }, []);


//UNCOMMENT WHEN DOWNLOAND USER DATA WILL BE COMPLETED
    // if(isLoading)
    //     return(<Spinner/>)

    const user = {
        name: "Łukasz",
        surname: "Pierdzibąk",
        email: "piedziluki@gmail.com",
        login: "pierdzioszek"
    }
    const userStats = {
        beated: 23,
        hardest: "VI.4",
        onSights: 6,
        flashes: 12
    }
    return (
        <div className='container'>
            <h2>Dziennik wpinaczkowy</h2>
            <hr/>

            <h4>Statystyki:</h4>
            <div className="profileStats">
                <div className="statsContainer statsEqualed">
                    Pokonane drogi: <span>{userStats.beated}</span>
                </div>
                <div className="statsContainer statsEqualed">
                    Najwyższa trudność: <span>{userStats.hardest}</span>
                </div>
                <div className="statsContainer statsEqualed">
                    Ilość On Sight'ów: <span>{userStats.onSights}</span>
                </div>
                <div className="statsContainer statsEqualed">
                    Ilość Flash'y: <span>{userStats.flashes}</span>
                </div>
            </div>
            <div className="manageProfile">
                <h4>Historia</h4>
                <ul>
                    <HistoryElement isHistory={true}/>
                    <HistoryElement isHistory={true}/>
                    <HistoryElement isHistory={true}/>
                    <HistoryElement isHistory={true}/>
                </ul>

            </div>

        </div>
    );
}