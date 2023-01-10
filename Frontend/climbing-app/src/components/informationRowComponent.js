import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/components.css'

export default function informationRowComponent({info, value}) {
    return(
        <div className='informationRow'>
            <div className='infoName'>
                {info}
            </div>
            
            <div className='infoValue'>
                {value}
            </div>
        </div>
    )
}