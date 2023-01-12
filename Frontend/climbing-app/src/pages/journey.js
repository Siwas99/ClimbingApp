import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Spinner from '../components/loadingComponent';
import HistoryElement from '../components/historyElement';


import axios from "axios";
import {useState, useEffect} from 'react';
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
    const [logs, setLogs] = useState(null);
    const [stats, setStats] = useState(null);
    const [isLoading, setLoading] = useState(true);

    const [date, setDate] = useState("");
    const [grade, setGrade] = useState(0);
    const [note, setNote] = useState("");
    const [style, setStyle] = useState(0);

    useEffect(() => {
        getExpeditionLogs();
    }, []);

    const getExpeditionLogs = async () => {
        await axios.post(`${baseURL}expeditionlogs/getexpeditionlogsbylogin?login=${auth().login}`)
        .then((response) => {
            setLogs(response.data);
            axios.post(`${baseURL}expeditionlogs/getuserstats?login=${auth().login}`)
            .then((response) => {
                setStats(response.data);
                setLoading(false);
            })
        }).catch((response)=>{        
        });
    }

    const handleDelete = (id) => {
        axios.post(`${baseURL}expeditionlogs/delete?id=${id}`)
        .then((response) => {
            if(response.data === true){
                // onHide();
            }
        })
    }

    const handleEdit = (id) => {
        const sendedDate = ()=> {
            return date === "" ? "0001-01-01" : date;
        }
        axios.post(`${baseURL}expeditionlogs/update`, {
            date: sendedDate(),
            valuation: grade,
            climbStyleId: style,
            comment: note,
            expeditionLogId: id
        })
        .then((response) => {
            if(response.data === true){
                // onHide();
            }
        })
    }

    const form = () => {
        return(
            <form className="form">
                <label>Data</label>
                <input 
                    type="date" 
                    value={date}
                    required
                    onChange={(e) => setDate(e.target.value)}
                />
                <label>Ocena</label>
                <input 
                    type="number" 
                    placeholder='5'
                    value={grade}
                    min="1"
                    max = "5"
                    required
                    onChange={(e) => setGrade(e.target.value)}
                />
                <label>Styl przejścia</label>
                <select value={style} onChange={e => setStyle(e.target.value)}>
                    <option value="1">On Sight</option>
                    <option value="2">Flash</option>
                    <option value="3">Red Point</option>
                    <option value="4">Top Rope (wędka)</option>
                </select>
                <label>Uwagi</label>
                <input 
                    type="tetxt" 
                    placeholder='np. ciężka końcówka'
                    value={note}
                    onChange={(e) => setNote(e.target.value)}
                />
            </form>
        )
    }

    if(isLoading)
        return(<Spinner/>)

    return (
        <div className='container'>
            <h2>Dziennik wpinaczkowy</h2>
            <hr/>

            <h4>Statystyki:</h4>
            <div className="profileStats">
                <div className="statsContainer statsEqualed">
                    Pokonane drogi: <span>{stats.beatenRoutes}</span>
                </div>
                <div className="statsContainer statsEqualed">
                    Najwyższa trudność: <span>{stats.hardestRoute}</span>
                </div>
                <div className="statsContainer statsEqualed">
                    Ilość On Sight'ów: <span>{stats.onSights}</span>
                </div>
                <div className="statsContainer statsEqualed">
                    Ilość Flash'y: <span>{stats.flashes}</span>
                </div>
            </div>
            <div className="manageProfile">
                <h4>Historia</h4>
                <ul>
                { logs.length > 0 ?
                    logs.map(function(element, index){
                        return <HistoryElement 
                        isHistory={true} 
                        key={index} 
                        element={element} 
                        getFunction={getExpeditionLogs}
                        handleDelete={handleDelete}
                        handleEdit={handleEdit}
                        formTemplate={form}/>
                    }) :
                    "Brak zapisanych przejść"
                }
                </ul>

            </div>

        </div>
    );
}