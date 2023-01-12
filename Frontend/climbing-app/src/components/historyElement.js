import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import FormComponent from './fromComponent';
import {TrashIcon, EditIcon} from './icons/trashIcon';

import {useState} from 'react';
import { Link } from 'react-router-dom';


const baseURL = "https://localhost:7191/api/";


export default function HistoryElement(props){

    const linkStyle = {
    width: '100%',
    color: "black"
}
    const route = props.element.route;
    const [modalShow, setModalShow] = useState(false);

    const onHide = () =>{
        setModalShow(false);
        props.getFunction();
    }

    const correctDate = () => {
        return (props.element.date.slice(0, -9));

    }
    return(
        <div className="historyRow">
            <Link to={`/routes/${route.routeId}`} style={linkStyle}>
                <li className="historyEntry">
                    <div className="historyLine">{route.name} - {route.difficulty}</div>
                    <div className="historyLine">{route.rock.name} 
                        <span className="separator"> | </span> {route.rock.area.name} 
                        <span className="separator"> | </span> {route.rock.area.region.name}
                    </div>
                    {props.isHistory ?
                        <>
                        <div className="historyLine">
                        {props.isHistory ? correctDate() : ""} 
                            <span className="separator"> | </span> {props.element.valuation} 
                            <span className="separator"> | </span> {props.element.climbStyle.name} 
                        </div>
                        <div className="historyLine">{props.element.comment}</div>
                        </> :
                        ""
                    }
                </li>
            </Link>
            <div className="historyEdit">
                {props.isHistory ?
                    <EditIcon function={() => setModalShow(true)} /> :
                    <TrashIcon function={() => props.handleDelete(props.element.wishlistId)} />
                }
            </div>
            { props.isHistory ?
                <FormComponent show={modalShow} id={props.element.expeditionLogId} 
                    onHide={() => onHide()} 
                    type="rock" name={props.element.name} 
                    element="wpis dziennika"  
                    onEdit = {props.handleEdit} 
                    onDelete = {props.handleDelete}
                    formTemplate = {props.formTemplate}
                /> 
                :
                ""
            }
        </div>
    )
}