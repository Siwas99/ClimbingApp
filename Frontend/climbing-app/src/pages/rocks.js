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
	const role = auth() !== null ? auth().role : "";

	const {Id} = useParams();
	const [rocks, setRocks] = useState(null);
	const [area, setArea] = useState(null);
	const [isLoading, setLoading] = useState(true);
	const [wasChanges, setWasChanged] = useState(false);

	const [success, setSuccess] = useState(false);
	const [error, setError] = useState(false);


	const [name, setName] = useState("");
	const [description, setDescription] = useState(" ");
	const [height, setHeight] = useState(0);
	const [distance, setDistance] = useState(0);
	const [popularity, setPopularity] = useState(2);
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
	const [selectedImage, setSelectedImage] = useState(null);


	useEffect(() => {
		getRocks();
		}, []);

	let rock = {
		name: name,
		description: description,
		height: height,
		distance: distance,
		popularity: popularity,
		rockFaceExposure: exposure,
		isShadedFromTrees: shaded,
		isRecommended: recommended,
		isLoose: loose,
		latitude: latitude,
		longitude: longitude,
		slabs: slab,
		vertical: vertical,
		overhang: overhang,
		roof: roof,
		changes: wasChanges,
		rockId: 0,
		image: selectedImage
	}

	const createFormData = () => {
		const formData = new FormData();
		for (let key in rock) {
			formData.append(key, rock[key]);
		}
		return formData;
    }

  const handleSubmit = () => {
       	const formData = createFormData();
		axios({
			method: "put",
			url: `${baseURL}rocks/insert?areaId=${Id}`,
			data: formData,
			headers: { "Content-Type": "multipart/form-data" },
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
		rock.rockId = rockId;
       	const formData = createFormData();

        axios({
            method: 'POST',
            url: `${baseURL}rocks/update?areaId=${Id}`,
            data: formData,
			headers: { "Content-Type": "multipart/form-data" },
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

    const handleCheckboxChange = () =>{
        setWasChanged(true);
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
                <input type="checkbox" value={slab} onChange={() => {
                    handleCheckboxChange();
                    setSlab(slab? false: true)
                }}/>
                <label>Piony</label>
                <input type="checkbox" value={vertical} onChange={() => {
                    handleCheckboxChange();
                    setVertical(vertical? false: true)
                }}/>
                <label>Przewieszenia</label>
                <input type="checkbox" value={overhang} onChange={() => {
                    handleCheckboxChange();
                    setOverhang(overhang? false: true)
                }}/>
                <label>Dachy</label>
                <input type="checkbox" value={roof} onChange={() => {
                    handleCheckboxChange();
                    setRoof(roof ? false: true)
                }}/>
                
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
                    onChange={() => {
                    handleCheckboxChange();
                    setShaded(shaded? false: true)
                }}
                />
                <label>Rekomendowana</label>
                <input 
                    type="checkbox"
                    value ={recommended}
                    onChange={() => {
                    handleCheckboxChange();
                    setRecommended(recommended? false: true)
                }}
                />
                <label>Krucha</label>
                <input 
                    type="checkbox"
                    value={loose}
                    onChange={() => {
                    handleCheckboxChange();
                    setLoose(loose? false: true)
                }}
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
					accept='image/jpeg, image/jpg'
                    onChange={(e) => setLogitude(e.target.value)}
                />
				<label>Zdjęcie</label>
				 <input
                    type="file"
                    name="myImage"
                    onChange={(event) => {
                    setSelectedImage(event.target.files[0]);
                    }}
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
        setWasChanged(false);
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
                    {rocks !== null ?
                        rocks.map(function(element, index){
                        return(<Rock key = {index}
                                    Id={element.obj.rockId}
                                    url = "/rocks" 
                                    element = {element.obj}
                                    numberOfRoutes = {element.numberOfRoutes}
                                    formTemplate = {form}
                                    onEdit = {handleEdit} 
                                    onDelete = {handleDelete} 
                                    onHide={closeModal}
                         />)
                        })
                        :
                        "Brak skał  "
                    }
                </ul>
            </Tab>
            <Tab eventKey="description" title="Opis">
                <p className="description">
                    {area.description ? area.description : "Brak informacji"}
                </p>
            </Tab>
              {role == "Admin" ?
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
