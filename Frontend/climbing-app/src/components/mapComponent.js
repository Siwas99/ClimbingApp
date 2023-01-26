import 'bootstrap/dist/css/bootstrap.min.css';
import '../App.css'
import 'leaflet/dist/leaflet.css'
import L from 'leaflet'

import { MapContainer, TileLayer, Popup, Marker } from 'react-leaflet'

const icon = L.icon({
    iconUrl: "./img/pointer.png",
    iconSize: [38, 38]
});
// const position = [51.505, -0.09]

export default function MapComponent(props) {

    return(
        <div className="mapContainer">
            <MapContainer center={props.position} zoom={16} scrollWheelZoom={false} style={{width: '100%', height: '100%' }}>
                <TileLayer
                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                url="https://api.maptiler.com/maps/basic/256/{z}/{x}/{y}.png?key=2x4SENTXDNsTVV9OJlsR"
                />
                <Marker position={props.position} icon={icon}>
                    <Popup>
                       {props.name}
                    </Popup>
                </Marker>
            </MapContainer>
        </div>
    )
}