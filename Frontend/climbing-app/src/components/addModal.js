import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import { Alert } from 'react-bootstrap';
import { useState } from 'react';
import { useAuthUser } from 'react-auth-kit';
import axios from 'axios';

const baseURL = "https://localhost:7191/api/";
export default function AddModal(props) {
    const auth = useAuthUser();
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [success, setSuccess] = useState(false);
    const [error, setError] = useState(false);

    const isEdit = () => {
        return props.type === "editRegion" ? true : false
    }

    const handleSubmit = () =>{
        const region = {
            name: name,
            description: description,
            regionId: props.id
        };
        const url = `${baseURL}regions/${isEdit() ? "update" : "insert"}`
        axios({
            method: 'POST',
            url: url,
            data: JSON.stringify(region),
            headers: {'Content-Type': 'application/json'},
            dataType : "json"
        }).then(response => {
        if(response)//probably response should be converted from to bool
            setSuccess(true);
        else
            setError(true);
        }).catch(response => {
        setError(true);
        });
    }

    const handleDelete = ()=>{
        axios.post(`${baseURL}regions/delete?regionId=${props.id}`).then(response => {
        if(response)//probably response should be converted from to bool
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
        props.onHide();
    }

    return (
        <Modal
        {...props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
        >
        <Modal.Header>
            <Modal.Title id="contained-modal-title-vcenter">
            {isEdit() ? "Edytuj" : "Dodaj"}  region
            </Modal.Title>
        </Modal.Header>
        <Modal.Body>

                {error ? <Alert variant="danger">Wystąpił nieznany błąd, spróbuj ponownie.</Alert> : "" }     
                {success ? <Alert variant="success">Operacja wykonana pomyślnie!</Alert> : "" }  
   
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
            <div className="buttonHolder">
                <Button variant="outline-success"onClick={handleSubmit} >
                    {isEdit() ? "Edytuj" : "Dodaj"}
                </Button>
                {isEdit() ?<Button variant="outline-danger"onClick={handleDelete} >
                    Usuń
                </Button> : "" }
            </div>

            
        </Modal.Body>
        <Modal.Footer>
            <Button variant="outline-danger"onClick={closeModal}>Zamknij</Button>
        </Modal.Footer>
        </Modal>
  );
}