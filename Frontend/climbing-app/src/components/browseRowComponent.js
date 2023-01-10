import {Link} from 'react-router-dom'
import { useAuthUser } from 'react-auth-kit';
import AddModal from '../components/addModal';
import { useState } from 'react';

import { EditIcon } from './icons/editIcon';

export default function BrowseRowComponent(props) {
    const auth = useAuthUser(); 
  const [modalShow, setModalShow] = useState(false);

    const linkStyle = {
        textDecoration: "none",
        color: 'black'
    };
   const role = auth().role;

   const handleEditIcon = () => {
        setModalShow(true);
   }

    return(
        <>
            <li className="browseElementRowName browseElementRow"> 
                <Link to={`/regions/${props.id}`} style={linkStyle}>
                    <span style={{marginRight: "2%"}}>{props.name}</span>
                </Link>
                {role ==="Admin"? <EditIcon function={handleEditIcon}/> : ""}
             </li>
            <Link to={`/regions/${props.id}`} style={linkStyle}>
                <li className="browseElementRow">
                    {props.description}
                </li>
            </Link>
            <AddModal show={modalShow} id={props.id} onHide={() => props.onHide()} type="editRegion"/>

        </>
  );
}