import '../App.css'

import Toast from 'react-bootstrap/Toast';
import ToastContainer from 'react-bootstrap/ToastContainer';

import { useEffect, useState } from 'react';

function ToastComponent(props) {
	const [showToast, setToast] = useState(true);

	
	return (
			<ToastContainer className="p-3" position="bottom-center">
				<Toast bg={props.isSucceded ? "success" : "danger"} show = {showToast} onClose = {() => {
					setToast(false);
					console.log("test")}}>
					<Toast.Header>
						<img src="holder.js/20x20?text=%20" className="rounded me-2" alt="" />
						<strong className="me-auto">{props.isSucceded ? "Operacje wykonano pomyślnie" : "Podczas wykonywania operacji wystąpił błąd"}</strong>
						<small></small>
					</Toast.Header>
				</Toast>
			</ToastContainer>
	);
}

export default ToastComponent;