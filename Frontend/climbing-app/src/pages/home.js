import '../App.css';

import LastActivityComponent from '../components/lastAcitivityComponent';

import Image from 'react-bootstrap/Image'
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';



export default function Home() {
    return (
        <>
       <div className="mainImage">
            <Image src="img/me.jpg" fluid/>
        </div>
        <div className = "container">
            <Form className="d-flex mainSearchBar">
                <Form.Control
                type="search"
                placeholder=" np. Kaszanka dla Kierownika"
                className="me-2"
                aria-label="Search"
                />
            <Button variant="outline-success">Szukaj</Button>
            </Form>
            <div className="lastActivites">
            <h3>Ostatnio pokonane</h3>    
            <LastActivityComponent  name = "Kaszanka dla kierownika"
                                    rock = "Biblioteka"
                                    grade = "VI.2"
                                    login = "SpanikowanyKojot" />

            <LastActivityComponent  name = "Maskotek"
                                    rock = "Plaskula"
                                    grade = "III"
                                    login = "ZadymiaAŻ" />
                                    
            <LastActivityComponent  name = "Diritessima Rozwalistej "
                                    rock = "Rozwalista Turnia"
                                    grade = "IV"
                                    login = "PapaTata" />

            </div>
        </div>
</>     
    )
}