import '../App.css';
import Spinner from '../components/loadingComponent';
import { Link } from 'react-router-dom';

export default function SearchResult(props) {
const linkStyle = {
    width: '100%',
    color: "black"
}
    return (
       <div className='resultContainer'>
        <div className="resultHeader">
            <h3>{props.name}</h3>
        </div>

        <div className="resultBody">
            {props.item.map(function(element, index){
                return(
                    <Link to = {`/${props.type}/${Object.values(element)[0]}`} style = {linkStyle} key={index}>
                        <div className="resultRow">
                            {element.name}
                        </div>
                    </Link>
                )
            })}
            
        </div>
       </div>
    )
}