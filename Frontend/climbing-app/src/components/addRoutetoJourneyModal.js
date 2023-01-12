import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Alert from 'react-bootstrap/Alert';
import { useState, useEffect } from 'react';
import axios from 'axios';
import {useAuthUser} from 'react-auth-kit'



const baseURL = "https://localhost:7191/api/";

export default function AddRouteToJourneyModal(props) {
  const auth = useAuthUser();
  const [date, setDate] = useState("");
  const [grade, setGrade] = useState(5);
  const [note, setNote] = useState("");
  const [style, setStyle] = useState(1);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState(false);

  useEffect(() => {
    getDate();
  }, [])

  const handleSubmit = () =>{
    axios.put(`${baseURL}expeditionlogs/insert?login=${auth().login}`,{
      routeId: props.route.routeId,
      date: date,
      valuation: grade,
      climbstyleId: style,
      comment: note
    }).then(response => {
      if(response.data === true){
        setSuccess(true);
      }
      else
        setError(true);
    }).catch(response => {
      setError(true);
    });
  }

  const getDate = () => {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy +'-'+mm+'-'+dd;
    setDate(today);
  }

  const closeModal = () => {
    props.onHide(success);
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
          Dodaj wpis do dziennika
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {error ? <Alert variant="danger">Podczas dodawania wpisu do dziennika wystąpił błąd.</Alert> : "" }     
        {success ? <Alert variant="success">Pomyślnie dodano wpis do dziennika!</Alert> : "" }     

        <h6>{props.route.name}</h6>
         <form onSubmit={handleSubmit} className="form">
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
        <div className="buttonHolder">
          <Button variant="outline-success"onClick={handleSubmit}>Dodaj</Button>
        </div>

        
      </Modal.Body>
      <Modal.Footer>
        <Button variant="outline-success"onClick={closeModal}>Zamknij</Button>
      </Modal.Footer>
    </Modal>
  );
}