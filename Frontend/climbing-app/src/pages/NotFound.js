import '../App.css';

import { Link } from 'react-router-dom';

export default function NotFound() {
    return(
        <div className="container">
            <h2>Ups, zaszedłeś za daleko...</h2>
            <p style={{height:"100%", margin:"auto"}}>
                <br/>
                Nie znaleziono strony o podanym adresie
                <br/>
                Powrót do <Link to="/" style = {{color: "green", textDecoration:"underline"}}>strony głównej</Link>
            </p>
        </div>
    )
}