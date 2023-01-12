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

            </Routes>
          
          
          <Footer login = "admin" />
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;
