import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import Spinner from 'react-bootstrap/Spinner';

function LoadingComponent() {
  return (
    <div className="spinnerContainer">
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    </div>
  );
}

export default LoadingComponent;