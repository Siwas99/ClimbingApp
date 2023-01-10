import '../App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import HistoryEditModal from './historyEditModal';
import React from 'react';

import Button from 'react-bootstrap/Button';


export default function HistoryElement(props){
    const [modalShow, setModalShow] = React.useState(false);


    const ExpeditionLog = {
        routeName: "Gzylowy filarek",
        routeGrade: "V",
        rockName: "Gzylowe Skały",
        areaName: "Dolina Kobylańska",
        regionName: "Jura Południowa",
        style: "OS",
        valuation: 4,
        comment: "Ciekawa końcówka",
        date: "13.04.2022"
    }


    return(
        <div className="historyRow">
            <li className="historyEntry">
                <div className="historyLine">{ExpeditionLog.routeName} - {ExpeditionLog.routeGrade}</div>
                <div className="historyLine">{ExpeditionLog.rockName} 
                    <span className="separator"> | </span> {ExpeditionLog.areaName} 
                    <span className="separator"> | </span> {ExpeditionLog.regionName}
                </div>
                {props.isHisotry ?
                    <>
                    <div className="historyLine">{ExpeditionLog.date} 
                        <span className="separator"> | </span> {ExpeditionLog.valuation} 
                        <span className="separator"> | </span> {ExpeditionLog.style} 
                        {/* <span className="separator">|</span> {ExpeditionLog.co} */}
                    </div>
                    <div className="historyLine">{ExpeditionLog.comment}</div>
                    </> :
                    ""
                }
            </li>
            <div className="historyEdit">
            {/* PUT SOME SVG EDIT ICON HERE */}
                <Button variant="outline-success" onClick={() => setModalShow(true)}>Ed</Button>
            </div>
            <HistoryEditModal show={modalShow} onHide={() => setModalShow(false)} name={ExpeditionLog.routeName}/>
        </div>
    )
}