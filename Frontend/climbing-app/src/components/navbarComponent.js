import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/components.css'

import {Link, useNavigate} from 'react-router-dom';
import React from 'react';
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

    let offcanvasElementList = [].slice.call(document.querySelectorAll('.offcanvas'));
    let offcanvasList = offcanvasElementList.map(function (offcanvasEl) {
      return Offcanvas(offcanvasEl)
    })
  }

function NavbarComponent() {
  

  const isAuthenticated = useIsAuthenticated()
  const signOut = useSignOut();
  const navigate = useNavigate();

  const logout = () => {
    signOut();
    navigate("/");
    window.location.reload()
  }
  return (
      <Navbar key="md" bg="dark" variant="dark" expand="md" className="navigationBar mb-3">
        <Container fluid>
          <Navbar.Brand><Link to="/"><h1>ClimbApp</h1></Link></Navbar.Brand>
          <Navbar.Toggle aria-controls={`offcanvasNavbar-expand-md`} />
          <Navbar.Offcanvas
            id={`offcanvasNavbar-expand-md`}
            aria-labelledby={`offcanvasNavbarLabel-expand-md`}
            placement="end"
          >
            <Offcanvas.Header closeButton>
              <Offcanvas.Title id={`offcanvasNavbarLabel-expand-md`}>
                <Link to="/" onClick={() => closeOffcanvas()}>ClimbApp</Link>
              </Offcanvas.Title>
            </Offcanvas.Header>
            <Offcanvas.Body className={setOffcanvasClasses()}>
              <Nav className="justify-content-end flex-grow-1 pe-3 offcanvasNavigation">
                <Form className="d-flex">
                <Form.Control
                  type="search"
                  placeholder=" np. Kaszanka dla Kierownika"
                  className="me-2"
                  aria-label="Search"
                />
                <Button variant="outline-success">Szukaj</Button>
              </Form>
                <Link to="regions" onClick={() => closeOffcanvas()}>Przeglądaj drogi</Link>
                <Link to="journey" onClick={() => closeOffcanvas()}>Dziennik</Link>
                <Link to="wishlist" onClick={() => closeOffcanvas()}>Planowanie</Link>
                {isAuthenticated() ?
                <Link to='profile' onClick={() => closeOffcanvas()}>Profil</Link> :
                <Link to='login' onClick={() => closeOffcanvas()}>Logowanie</Link>
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
  );
}

export default NavbarComponent;