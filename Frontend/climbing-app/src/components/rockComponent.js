import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import { Link} from 'react-router-dom'
import { useState } from 'react';
import { useAuthUser } from 'react-auth-kit';

import { EditIcon } from './icons/editIcon';
import FormComponent from '../components/fromComponent';


const linkStyle = {
    textDecoration: "none",
    color: 'black'
};



export default function Rock(props){   
    const auth = useAuthUser();
    const role = auth() !== null ? auth().role : "";
    const [modalShow, setModalShow] = useState(false);


    const handleEditIcon = () => {
        setModalShow(true);
    }

    const onHide = () => {
        props.onHide();
        setModalShow(false);
    }

    return(
            <li>
                <div className = "mainInfo">
                    <Link to={props.isRock ? `/rocks/${props.Id}`: `/areas/${props.Id}`} style={linkStyle}>
                        <span>{props.element.name}</span> 
                    </Link>
                    { role ==="Admin"? <EditIcon function={handleEditIcon}/> : ""} 
                </div>

                <Link to={props.isRock ? `/rocks/${props.Id}`: `/areas/${props.Id}`} style={linkStyle}>
                    <div className = "numberOfRoutes">
                        <div className = "number" style={{borderTop: "2px solid #f5a623"}}>{props.numberOfRoutes.veryEasyRoutes}</div>
                        <div className = "number" style={{borderTop: "2px solid #05bb01"}}>{props.numberOfRoutes.easyRoutes}</div>
                        <div className = "number" style={{borderTop: "2px solid #066ce9"}}>{props.numberOfRoutes.mediumRoutes}</div>
                        <div className = "number" style={{borderTop: "2px solid #f02e2e"}}>{props.numberOfRoutes.hardRoutes}</div>
                        <div className = "number" style={{borderTop: "2px solid #a833d3"}}>{props.numberOfRoutes.veryHardRoutes}</div>
                        <div className = "number" style={{borderTop: "2px solid #5d5d5d"}}>{props.numberOfRoutes.projects}</div>
                    </div>
                </Link>
                <FormComponent show={modalShow} id={props.Id} 
                onHide={() => onHide()} 
                type="rock" name={props.element.name} 
                element="skałę"  
                onEdit = {props.onEdit} 
                onDelete = {props.onDelete}
                formTemplate = {props.formTemplate}/>
            </li>
    )
}