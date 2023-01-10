import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import Rock from '../components/rockComponent.js'
import Spinner from '../components/loadingComponent';


import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import { Alert, Button } from 'react-bootstrap';

import axios from "axios";
import {useState, useEffect} from 'react';
import { useParams } from 'react-router-dom';
import { useAuthUser } from 'react-auth-kit';


const baseURL = "https://localhost:7191/api/";

function Rocks() {
  const auth = useAuthUser();
  const {Id} = useParams();
  const [rocks, setRocks] = useState(null);
  const [area, setArea] = useState(null);
  const [isLoading, setLoading] = useState(true);

  const [success, setSuccess] = useState(false);
  const [error, setError] = useState(false);


  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [height, setHeight] = useState(0);
  const [distance, setDistance] = useState(0);
  const [popularity, setPopularity] = useState(0);
  const [latitude, setLatitude] = useState(0);
  const [longitude, setLogitude] = useState(0);
  const [exposure, setExposure] = useState("North");
  const [slab, setSlab] = useState(false);
  const [vertical, setVertical] = useState(false);
  const [overhang, setOverhang] = useState(false);
  const [roof, setRoof] = useState(false);
  const [shaded, setShaded] = useState(false);
  const [recommended, setRecommended] = useState(false);
  const [loose, setLoose] = useState(false);

  useEffect(() => {
       getRocks();
    }, []);


  const handleAddrTypeChangeExpo = (e) => setExposure(exposure[e.target.value]);

  
  
  const handleSubmit = () => {
    const rock = {
      name: name,
      description: description,
      height: height,
      distance: distance,
      popularity: popularity,
      rockFaceExposure: exposure,
      isShadedFromTrees: shaded,
      isRecommended: recommended,
      isLoose: loose,
      positionLatitude: latitude,
      positionLogitude: longitude,
      slabs: slab,
      vertical: vertical,
      overhang: overhang,
      roof: roof
    }
       axios({
            method: 'PUT',
            url: `${baseURL}rocks/insert?areaId=${Id}`,
            data: JSON.stringify(rock),
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        }).then(response => {
            if(response){
                setSuccess(true);
                getRocks();
            }
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });

  }

    const handleEdit = (rockId) =>{
        axios({
            method: 'POST',
            url: `${baseURL}rocks/update?areaId=${Id}`,
            data: {
                rockId: rockId,
                name: name,
                description: description,
                height: height,
                distance: distance,
                popularity: popularity,
                rockFaceExposure: exposure,
                isShadedFromTrees: shaded,
                isRecommended: recommended,
                isLoose: loose,
                positionLatitude: latitude,
                positionLogitude: longitude,
                slabs: slab,
                vertical: vertical,
                overhang: overhang,
                roof: roof,
                areaId: Id
            },
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        }).then(response => {
            if(response) {
                setSuccess(true);
                getRocks();
            }
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });
    }

    const handleDelete = (id) =>{
        axios.post(`${baseURL}rocks/delete?rockId=${id}`).then(response => {
            if(response) {
                setSuccess(true);
                getRocks();
            }
            else
                setError(true);
        }).catch(response => {
            setError(true);
        });
    }

    const getRocks = () => {
        axios.post(`${baseURL}rocks/getrockswithnumberofroutesbyregionid?areaId=${Id}`).then((response) => {
            setRocks(response.data);
            axios.post(`${baseURL}areas/getareabyid?areaId=${Id}`).then((response) => {
                setArea(response.data);
                setLoading(false);
            })
        });
    }

    const form = () => {
        return(
            <form onSubmit={handleSubmit} className="form">
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
                <label>Wysokość</label>
                <input 
                    type="number" 
                    min="1"
                    value={height}
                    required
                    onChange={(e) => setHeight(e.target.value)}
                />
                <label>Odległość</label>
                <input 
                    type="number"
                    min="1" 
                    value={distance}
                    required
                    onChange={(e) => setDistance(e.target.value)}
                />
                <label>Popularność</label>
                <input 
                    type="number"
                    min="1" 
                    max="3" 
                    value={popularity}
                    required
                    onChange={(e) => setPopularity(e.target.value)}
                />
                <label>Dominujące formacje</label>
                <label>Połogie płyty</label>
                <input type="checkbox" value={slab} onChange={() => setSlab(slab? false: true)}/>
                <label>Piony</label>
                <input type="checkbox" value={vertical} onChange={() => setVertical(vertical? false: true)}/>
                <label>Przewieszenia</label>
                <input type="checkbox" value={overhang} onChange={() => setOverhang(overhang? false: true)}/>
                <label>Dachy</label>
                <input type="checkbox" value={roof} onChange={() => setRoof(roof ? false: true)}/>
                
                <label>Wystawa ściany</label>
                <select value={exposure} onChange={(e) => setExposure(e.target.value)}>
                    <option value="Nort">Słońce nie dociera</option>
                    <option value="East">Słonecznie przed południem</option>
                    <option value="South">Słonecznie przez większą część dnia</option>
                    <option value="West">Słonecznie po południu</option>
                </select>
                <label>Zacieniona</label>
                <input 
                    type="checkbox"
                    value={shaded}
                    onChange={() => setShaded(shaded ? false: true)}
                />
                <label>Rekomendowana</label>
                <input 
                    type="checkbox"
                    value ={recommended}
                    onChange={() => setRecommended(recommended ? false: true)}
                />
                <label>Krucha</label>
                <input 
                    type="checkbox"
                    value={loose}
                    onChange={() => setLoose(loose ? false: true)}
                />
                <label>Wysokość geograficzna</label>
                    <input 
                    type="number"
                    min="1" 
                    value={latitude}
                    required
                    onChange={(e) => setLatitude(e.target.value)}
                />
                <label>Szerokość geograficzna</label>
                    <input 
                    type="number"
                    min="1" 
                    value={longitude}
                    required
                    onChange={(e) => setLogitude(e.target.value)}
                />

            </form>
        )
    }

    const closeModal = () => {
        getRocks();
        clearVariables();
    }

    const clearVariables = () => {
        setName("");
        setDescription("");
        setHeight("");
        setDistance("");
        setPopularity("");
        setExposure("North");
        setShaded(false);
        setRecommended(false);
        setLoose(false);
        setSlab(false);
        setVertical(false);
        setOverhang(false);
        setRoof(false);
    }

  
  if(isLoading)
    return (<Spinner/>)

  return (
    <div className='container'>
      <h2> {area.name}</h2>
      <Tabs  
            defaultActiveKey="rocks"
            id="uncontrolled-tab-example"
            className="mb-3">
            <Tab eventKey="rocks" title="Skały">
                <ul>
                    {rocks.map(function(element, index){
                        return(<Rock key = {index} Id={element.rock.rockId}
                            isRock={true} element = {element.rock}
                            numberOfRoutes = {element.numberOfRoutes}
                            formTemplate = {form}
                            onEdit = {handleEdit} onDelete = {handleDelete} onHide={closeModal}
                         />)
                      }
                    )}
                </ul>
            </Tab>
            <Tab eventKey="description" title="Opis">
                <p className="description">
                    {area.description}
                </p>
            </Tab>
              {auth().role == "Admin" ?
                 <Tab eventKey="add" title="Dodaj">
                    {form()}
                    <Button variant="outline-success"onClick={handleSubmit} className="mr-btm-3">Dodaj</Button>
                    {error ? <Alert variant="danger">Podczas dodawania skały wystąpił błąd.</Alert> : "" }     
                    {success ? <Alert variant="success">Pomyślnie dodano skałe!</Alert> : "" }     

                </Tab>
             : ""}

      </Tabs>
    </div>
  );
}

export default Rocks;
