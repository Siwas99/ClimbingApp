import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import BrowseRow from '../components/browseRowComponent';
import Spinner from '../components/loadingComponent';
import AddModal from '../components/addModal';


import axios from "axios";
import {useState, useEffect} from 'react';
import { useAuthUser } from 'react-auth-kit';
import Button from 'react-bootstrap/Button';
import RockComponent from '../components/rockComponent';
import ToastComponent from '../components/ToastComponent';



const baseURL = "https://localhost:7191/api/";


function Regions() {
  const auth = useAuthUser();
  const role = auth() !== null ? auth().role : "";

  const [regions, setRegions] = useState(null);
  const [isLoading, setLoading] = useState(true);
  const [modalShow, setModalShow] = useState(false);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState(false);

  useEffect(() => {
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

	const handleSubmit = (id) =>{
        const region = {
            name: name,
            description: description,
            regionId: 0
        };
		if(id !== undefined){
			region.regionId = id;
		}
        const url = `${baseURL}regions/${id !== undefined ? "update" : "insert"}`
        axios({
            method: 'POST',
            url: url,
            data: JSON.stringify(region),
            headers: {'Content-Type': 'application/json'},
        }).then(response => {
        if(response){
				  setModalShow(false);
		}
        else
            setError(true);
        }).catch(response => {
          setError(true);
        });
    }

    const handleDelete = (id)=>{
        axios.post(`${baseURL}regions/delete?regionId=${id}`).then(response => {
        if(response)
            setSuccess(true);
        else
            setError(true);
        }).catch(response => {
        setError(true);
        });
    }

    const closeModal = () => {
        setDescription("");
        setName("");
        setSuccess(false);
        setError(false);
        getRegions();
    }

	const form = () => {
		return(
			 <form className="form">
                                      <label>Nazwa</label>
                    <input 
                        type="text" 
                        value={name}
                        required
                        onChange={(e) => setName(e.target.value)}
                    />
                    <label>Opis</label>
                    <input 
                        type="text" 
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </form>
		)
	}

  if(isLoading)
    return (<Spinner />)
  return (
    <div className='container'>
      <h2>Regiony</h2>
      <ul className="browseElementRow">
        {regions.map(function(element, index){
          return(<RockComponent key = {index} Id={element.obj.regionId}
							url = "/regions"
                            isRock={false} element = {element.obj}
                            numberOfRoutes = {element.numberOfRoutes}
                            formTemplate = {form}
                            onEdit = {handleSubmit} onDelete = {handleDelete} onHide={closeModal}/>)
          // return <BrowseRow key={index} id={element.obj.regionId} name={element.obj.name} description={element.obj.description} onHide={hideModal}/>
        })}
      </ul>
      <hr/>
      {role == "Admin" ?
        <>
          <Button variant='outline-success' onClick={() => setModalShow(true)}>Dodaj region</Button>
          <AddModal show={modalShow} onHide={hideModal}/>
        </> : ""  
      }
      {error ? <ToastComponent /> : "" }
    </div>
  );
}

export default Regions;
