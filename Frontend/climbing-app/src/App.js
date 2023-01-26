import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import NavbarComponent from  './components/navbarComponent'
import Footer from './components/footerComponent';
import Home from './pages/home';
import Regions from './pages/Regions';
import Areas from './pages/areas';
import Rocks from './pages/rocks';
import RoutesList from './pages/Routes';
import RoutesPage from './pages/route';
import Login from './pages/login';
import Register from './pages/register';
import Profile from './pages/profile';
import Journey from './pages/journey';
import Wishlist from './pages/wishlist';
import SearchResultPage from './pages/searchResultPage';
import AdminPanel from './pages/adminPanel';
import NotFound from './pages/NotFound';
import TestingPage from './pages/testingPage';
import About from './pages/about';
import ClimberWithClass from './pages/climberWithClass';
import Help from './pages/help';
import Contact from './pages/contact';

import { BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import { RequireAuth, AuthProvider } from 'react-auth-kit';



function App() {
  return (
    <AuthProvider 
    authType = {'cookie'}
                  authName={'_auth'}
                  cookieDomain={window.location.hostname}
                  cookieSecure={false}>
    
      <Router>
        <div className='App'>
          <NavbarComponent />
            <Routes>
              
				{/* DELETE ROUTE BELOW AFTER ALL */}

				<Route path="test" element = {<TestingPage/>}/>



				<Route path="*" element = {<NotFound/>}/>
				<Route path="/" element = {<Home/>}/>
				<Route path="regions" element = {<Regions/>}/>
				<Route path="regions/:Id" element = {<Areas/>}/>
				<Route path="areas/:Id" element = {<Rocks/>}/>
				<Route path="rocks/:Id" element = {<RoutesList/>}/>
				<Route path="routes/:Id" element = {<RoutesPage/>}/>
				<Route path="login" element = {<Login/>}/>
				<Route path="register" element = {<Register/>}/>
				<Route path="/searchResults" element = {<SearchResultPage/>}/>
				<Route path="/searchResults/:phrase" element = {<SearchResultPage/>}/>
				<Route path="/adminPanel" element = {<AdminPanel/>}/>
				<Route path={'/profile'} element={<RequireAuth loginPath='/login'>
														<Profile/>
													</RequireAuth>
													}/>
				<Route path="journey" element = {<RequireAuth loginPath="/login">
														<Journey/>
												</RequireAuth>}/>
				<Route path="wishlist" element = {<RequireAuth loginPath="/login">
														<Wishlist/>
													</RequireAuth>}/>
				<Route path="/contact" element ={<Contact />} />
				<Route path="/climberwithclass" element ={<ClimberWithClass />} />
				<Route path="/Help" element ={<Help />} />
				<Route path="/about" element ={<About />} />

            </Routes>
          
          
          <Footer login = "admin" />
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;
