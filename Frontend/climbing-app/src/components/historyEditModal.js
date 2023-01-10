import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';



export default function HistoryEditModal(props) {
  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered
    >
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          Edytuj wpis
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <h6>{props.name}</h6>
         <Form.Group className="mb-3">
          <Form.Label>Data</Form.Label>
          <Form.Control placeholder="Od 1 do 5" type="date" />
        </Form.Group>
         <Form.Group className="mb-3">
          <Form.Label>Ocena</Form.Label>
          <Form.Control placeholder="Od 1 do 5" type="number" min="1" max='5' />
        </Form.Group>
        <Form.Group className="mb-3">
          <Form.Label>Styl przejścia</Form.Label>
          <Form.Select >
            <option>On Sight</option>
            <option>Flash</option>
            <option>Red point</option>
            <option>Rotkreis</option>
            <option>All free</option>
            <option>Pre protection</option>
            <option>Top rope</option>
          </Form.Select>
        </Form.Group>
        <Form.Group className="mb-3">
          <Form.Label>Uwagi</Form.Label>
          <Form.Control placeholder="np. Ciężka końcówka" />
        </Form.Group>
        <div className="buttonHolder">
          <Button variant="outline-danger"onClick={props.onHide}>Usuń</Button>
          <Button variant="outline-success"onClick={props.onHide}>Edytuj</Button>
        </div>

        
      </Modal.Body>
      <Modal.Footer>
        <Button variant="outline-success"onClick={props.onHide}>Zamknij</Button>
      </Modal.Footer>
    </Modal>
  );
}