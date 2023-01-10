function lastAcitivityComponent(props) {
  return (
        <div className="lastActivitesInfo">
          <div className = "lastActivitesRow"> {props.name} </div>
          <div className = "lastActivitesRow"> {props.rock} </div>
          <div className = "lastActivitesRow"> {props.grade} </div>
          <div className = "lastActivitesRow"> {props.login} </div>
        </div>
  )
}

export default lastAcitivityComponent;