import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import BrowseRow from '../components/browseRowComponent';
import Spinner from '../components/loadingComponent';
import AddModal from '../components/addModal';


import axios from "axios";
import React from 'react';
import { useAuthUser } from 'react-auth-kit';
import Button from 'react-bootstrap/Button';



const baseURL = "https://localhost:7191/api/";


function Regions() {
  const auth = useAuthUser();
  const [regions, setRegions] = React.useState(null);
  const [isLoading, setLoading] = React.useState(true);
  const [modalShow, setModalShow] = React.useState(false);

  React.useEffect(() => {
        getRegions();
    }, []);

    const getRegions = async () => {
      await axios.get(`${baseURL}regions/getregions`).then((response) => {
              setRegions(response.data);
              setLoading(false);
            });
    }

    const hideModal = () => {
      setModalShow(false);
      getRegions();
    }
  if(isLoading)
    return (<Spinner />)
  return (
    <div className='container'>
      <h2>Regiony</h2>
      <ul className="browseElementRow">
        {regions.map(function(element, index){
          return <BrowseRow key={index} id={element.regionId} name={element.name} description={element.description} onHide={hideModal}/>
        })}
      </ul>
      <hr/>
      { auth().role == "Admin" ?
        <>
          <Button variant='outline-success' onClick={() => setModalShow(true)}>Dodaj rejon</Button>
          <AddModal show={modalShow} onHide={hideModal}/>
        </> : ""  
      }
    </div>
  );
}

export default Regions;
