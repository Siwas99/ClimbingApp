import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import { Alert } from 'react-bootstrap';
import { useState } from 'react';
import { useAuthUser } from 'react-auth-kit';
import axios from 'axios';

const baseURL = "https://localhost:7191/api/";
export default function FormComponent(props) {
    const auth = useAuthUser();
    const {onEdit, onDelete, formTemplate , id, ...rest} = props;
    
    const closeModal = () => {
        props.onHide();
    }

    const handleDelete = () => {
        onDelete(props.id);
    }

    const handleEdit = () => {
        onEdit(props.id);
    }

    return (
        <Modal
        {...rest}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
        >
        <Modal.Header>
            <Modal.Title id="contained-modal-title-vcenter">
                Edytuj {props.element} {props.name ? `- ${props.name}` : ''}
            </Modal.Title>
        </Modal.Header>
        <Modal.Body>

            {/* {error ? <Alert variant="danger">Wystąpił nieznany błąd, spróbuj ponownie.</Alert> : "" }     
            {success ? <Alert variant="success">Operacja wykonana pomyślnie!</Alert> : "" }   */}
            <div>
           {formTemplate()}
            </div>
            
            {
                !props.isAdd ?
            <div className="buttonHolder">
                <Button variant="outline-success"onClick={handleEdit} > Edytuj </Button>
                <Button variant="outline-danger" onClick={handleDelete} > Usuń </Button> 
            </div>
            :
            <div className="buttonHolder">
                <Button variant="outline-success"onClick={handleEdit} > Dodaj </Button>
            </div>
            }

            
        </Modal.Body>
        <Modal.Footer>
            <Button variant="outline-danger"onClick={closeModal}>Zamknij</Button>
        </Modal.Footer>
        </Modal>
  );
}