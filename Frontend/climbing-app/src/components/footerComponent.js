import { EditIcon } from './icons/editIcon';
import { Link } from 'react-router-dom';

export default function footer(props){
    return (
        <div className = "footer">
            <div className = "footerNavSection">

                <Link to="/About">O nas</Link>
                    <span className="footerSeparator">|</span>
                <Link to="/Contact">Kontakt</Link>
                    <span className="footerSeparator">|</span>
                <Link to="/climberwithclass">Wspinacz z klasą</Link>
                    <span className="footerSeparator">|</span>
                <Link to="/help">Pomoc</Link>

            </div>
            {/* <div className = "footerSocialSection">
              <a href = "https://www.facebook.com"><EditIcon/></a>
              <a href = "https://www.instagram.com" ><EditIcon/> </a>
              <a href = "https://www.linkedin.com/in/dawid-wo%C5%BAnica-206909249/ "><EditIcon/></a>
              <a href = "https://github.com/Siwas99"><EditIcon/></a>


            </div> */}
           <div className= "footerInfoSection">
                Strona wykonana na potrzeby pracy inżynierskiej.
                Autorem pracy jest Dawid Woźnica 2022 &copy;
            </div>
            
        </div>
    )
}