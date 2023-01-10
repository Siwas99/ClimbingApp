import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Alert from 'react-bootstrap/Alert';
import { useState } from 'react';
import axios from 'axios';
import {useAuthUser} from 'react-auth-kit'



const baseURL = "https://localhost:7191/api/";

export default function AddRouteToJourneyModal(props) {
  const auth = useAuthUser();
  const [date, setDate] = useState("");
  const [grade, setGrade] = useState("");
  const [note, setNote] = useState("");
  const [styles, setStyles] = useState(["On Sight", "Flash", "Red Point", "Top Rope"]);
  const [style, setStyle] = useState("");
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState(false);

  const handleSubmit = () =>{
    axios.put(`${baseURL}expeditionlogs/insert`,{
      route: props.route,
      login: auth().login,
      date: date,
      valuation: grade,
      climbstyle: style,
      comment: note
    }).then(response => {
      if(response)//probably response should be converted from to bool
        setSuccess(true);
      else
        setError(true);
    }).catch(response => {
      setError(true);
    });
  }
  const Styles = styles.map(Add => Add)
  const handleAddrTypeChange = (e) => setStyle(styles[e.target.value]);

  return (
    
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered
    >
      <Modal.Header closeButton>
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
                <select name="style" onChange={e => handleAddrTypeChange(e)}>
                {
                  Styles.map((address, key) => <option key={key} value={key}>{address}</option>)
                }
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
        <Button variant="outline-success"onClick={props.onHide}>Zamknij</Button>
      </Modal.Footer>
    </Modal>
  );
}