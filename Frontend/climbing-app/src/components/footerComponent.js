import { FaFacebook, FaInstagram, FaLinkedin, FaGithub } from 'react-icons/fa';

export default function footer(props){
    return (
        <div className = "footer">
            <div className = "footerNavSection">

                <a href="#">O nas</a>
                    <span className="footerSeparator">|</span>
                <a href="#">Kontakt</a>
                    <span className="footerSeparator">|</span>
                <a href="#">Wspinacz z klasą</a>
                    <span className="footerSeparator">|</span>
                <a href="#">Pomoc</a>

            </div>
            <div className = "footerSocialSection">
              <a href = "https://www.facebook.com"><FaFacebook/></a>
              <a href = "https://www.instagram.com" ><FaInstagram /> </a>
              <a href = "https://www.linkedin.com/in/dawid-wo%C5%BAnica-206909249/ "><FaLinkedin /></a>
              <a href = "https://github.com/Siwas99"><FaGithub /></a>


            </div>
           <div className= "footerInfoSection">
                Strona wykonana na potrzeby pracy inżynierskiej.
                Autorem pracy jest Dawid Woźnica 2022 &copy;
            </div>
            
        </div>
    )
}