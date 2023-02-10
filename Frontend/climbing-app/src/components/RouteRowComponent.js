import 'bootstrap/dist/css/bootstrap.min.css';
import '../App.css'
import { useState } from 'react';
import {Link} from 'react-router-dom'
import { useAuthUser } from 'react-auth-kit';
import {EditIcon} from './icons/editIcon.jsx';
import FormComponent from '../components/fromComponent';


const linkStyle = {
    textDecoration: "none",
    color: 'black'
};


export default function Route(props){
    const auth = useAuthUser();
    const [modalShow, setModalShow] = useState(false);
    const role = auth() !== null ? auth().role : "";


    const handleEditIcon = () => {
        setModalShow(true);
    }

    const onHide = () => {
        props.onHide();
        setModalShow(false);
    }
    console.log(props.author === "");
    return(
        
            <li>
                <div className = "mainInfo">
                <Link to={`/routes/${props.routeId}`} style={linkStyle}>
                    {props.number}. <span>{props.name}</span> | {props.grade}
                </Link>  
                    {role ==="Admin"? <EditIcon function={handleEditIcon}/> : ""} 
                </div>
                <Link to={`/routes/${props.routeId}`} style={linkStyle}>
                <div className = "additionalInfo">
                    {(props.author === "" || props.author === " ") ? "" : `${props.author} -`} {props.year} {props.info ? `- ${props.info}` : ""}
                </div>
                </Link>

                <FormComponent show={modalShow} id={props.routeId} 
                onHide={() => onHide()} 
                type="area" name={props.name}
                element="rejon"  
                onEdit = {props.onEdit} 
                onDelete = {props.onDelete}
                formTemplate = {props.formTemplate}/>
            </li>
        
    )
}