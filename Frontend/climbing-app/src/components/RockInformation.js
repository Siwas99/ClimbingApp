import '../App.css';

import InformationRowComponent from './informationRowComponent';

function BoolenToHuman(bool){
    return bool ? "Tak" : "Nie";
}

function PopularityToHuman(popularity){
    switch(popularity){
        case 3:
            return "Bardzo poularna";
        case 2:
            return "Popularna";
        case 1:
            return "Mało popularna"
    }
}

function ExposureToHuman(exposure){
    switch(exposure){
        case "North":
            return "Słońce nie dociera";
        case "East":
            return "Słonecznie przed południem";
        case "South":
            return "Słonecznie przez większość dnia";
        case "West":
            return "Słonecznie po południu";
    }
}

export default function RockInformation(props){

    if(props.isRoute)
        return(
            <>
                <InformationRowComponent info="Numer i nazwa drogi" value = {`${props.route.number}. ${props.route.name}`}/>
                <InformationRowComponent info="Trudność drogi" value = {`${props.route.difficulty}`}/>
                {props.route.description ? <InformationRowComponent info="Opis drogi" value = {props.route.description}/> : ""}
                <InformationRowComponent info="Autor" value = {props.route.author}/>
                <InformationRowComponent info="Rok wytyczenia" value = {props.route.year}/>
            </>
        )    

    return(
        <>
        <InformationRowComponent info="Wysokość ściany" value = {props.rock.height}/>
        <InformationRowComponent info="Odległość od parkingu" value = {`${props.rock.distance} minut`}/>
        <InformationRowComponent info="Popularność" value = {PopularityToHuman(props.rock.popularity)}/>
        <InformationRowComponent info="Rekomendowana" value = {BoolenToHuman(props.rock.isRecommended)}/>
        <InformationRowComponent info="Wystawa ściany" value = {ExposureToHuman(props.rock.rockFaceExposure.name)}/>
        <InformationRowComponent info="Zacieniona od drzew" value = {BoolenToHuman(props.rock.isShadedFromTrees)}/>
        <InformationRowComponent info="Krucha" value = {BoolenToHuman(props.rock.isLose)}/>
        </>
    )

}