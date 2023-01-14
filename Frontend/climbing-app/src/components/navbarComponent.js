import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/components.css'


import {Link, useNavigate} from 'react-router-dom';
import {useState} from 'react';
import {useIsAuthenticated, useSignOut} from 'react-auth-kit';

import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import Offcanvas from 'react-bootstrap/Offcanvas';

  const setOffcanvasClasses = function() {
    if(window.screen.width > 765)
      return "";
    else 
      return "offcanvas-body-custom";
  };

  const closeOffcanvas = function() {
    console.log('raz raz1111!11!oneone');
    let offcanvasElementList = [].slice.call(document.querySelectorAll('.offcanvas'));
	console.log(offcanvasElementList);
    let offcanvasList = offcanvasElementList.map(function (offcanvasEl) {
		console.log(offcanvasEl);
      return Offcanvas(offcanvasEl)
    })
  }

function NavbarComponent() {
  const [searchPhrase, setPhrase] = useState("");
	const [menuOpen, setMenuOpen] = useState(false);

  const toggleMenu = () => {
    setMenuOpen(!menuOpen)
  }

  const handleClose = () => setMenuOpen(false)

  const isAuthenticated = useIsAuthenticated()
  const signOut = useSignOut();
  const navigate = useNavigate();

  const logout = () => {
    signOut();
    navigate("/");
    window.location.reload()
  }
  return (
	  <>
      <Navbar key="md" bg="dark" variant="dark" expand="md" className="navigationBar mb-3">
        <Container fluid>
          <Navbar.Brand><Link to="/"><h1>ClimbApp</h1></Link></Navbar.Brand>
          <Navbar.Toggle aria-controls={`offcanvasNavbar-expand-md`} onClick={toggleMenu}/>
          <Navbar.Offcanvas
            id={`offcanvasNavbar-expand-md`}
            aria-labelledby={`offcanvasNavbarLabel-expand-md`}
            placement="end"
			show={menuOpen}
      		onHide={handleClose}
          >
            <Offcanvas.Header closeButton>
              <Offcanvas.Title id={`offcanvasNavbarLabel-expand-md`}>
                <Link to="/" onClick={toggleMenu} >ClimbApp</Link>
              </Offcanvas.Title>
            </Offcanvas.Header>
            <Offcanvas.Body className={setOffcanvasClasses()}>
              <Nav className="justify-content-end flex-grow-1 pe-3 offcanvasNavigation">
                <div className="d-flex searchContainer">
					<Form.Control className="navbarSearch" type="text" placeholder="np. Kaszanka dla kierownika" onChange={(e) => {setPhrase(e.target.value)}} />
					<Button variant="outline-success" onClick={() => {
                                                        navigate(`/searchResults/${searchPhrase}`);
														toggleMenu()
                                                        }}>Szukaj
					</Button>
                </div>
                <Link to="regions" onClick={toggleMenu}>Przeglądaj</Link>
                <Link to="journey" onClick={toggleMenu}>Dziennik</Link>
                <Link to="wishlist" onClick={toggleMenu}>Planowanie</Link>
                {isAuthenticated() ?
                <Link to='profile' onClick={toggleMenu}>Profil</Link> :
                <Link to='login' onClick={toggleMenu}>Logowanie</Link>
              }
              </Nav>
            </Offcanvas.Body>
              {isAuthenticated() ?
                <Button variant="outline-danger" onClick={logout} className="logoutButton">Wyloguj się</Button> :
                ""
              }
          </Navbar.Offcanvas>
        </Container>
      </Navbar>
	  </>
  );
}

export default NavbarComponent;