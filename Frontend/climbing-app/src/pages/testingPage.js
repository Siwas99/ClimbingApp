import '../App.css'

import { useState } from 'react';
import axios from 'axios';
import ToastComponent from '../components/ToastComponent';

const baseURL = "https://localhost:7191/api/";


export default function TestingPage(){
    const [selectedImage, setSelectedImage] = useState(null);

    let rock = {
        name: "name",
        description: "description",
        height: 2,
        distance: 2,
        popularity: 2,
        rockFaceExposure: "North",
        isShadedFromTrees: false,
        isRecommended: false,
        isLoose: false,
        positionLatitude: 12,
        positionLogitude: 12,
        slabs: false,
        vertical: false,
        overhang: false,
        roof: false,
        image: selectedImage
    }

    const createFormData = () => {
         const formData = new FormData();

        for (let key in rock) {
            formData.append(key, rock[key]);

        }
            return formData;
    }

    const uploadImage = async() => {
        try {
            const formData = createFormData();
    
            const response = await axios({
                method: "post",
                url: `${baseURL}app/imagetest`,
                data: formData,
                headers: { "Content-Type": "multipart/form-data" },
            });
        } catch(error) {
            console.log(error)
        }
    }

    const updateImage = async () => {
        try {
            const formData = createFormData();
    
            const response = await axios({
                method: "post",
                url: `${baseURL}app/imagechange`,
                data: formData,
                headers: { "Content-Type": "multipart/form-data" },
            });
        } catch(error) {
            console.log(error)
        }
    }
    return(
        <div className = "container">
            <h2>Dodawnie zdjęć </h2>
            {selectedImage && (
                    <div>
                    <img alt="not fount" width={"250px"} src={URL.createObjectURL(selectedImage)} />
                    <br />
                    <button onClick={()=>setSelectedImage(null)}>Remove</button>
                    <button onClick={uploadImage}>Upload</button>
                    <button onClick={updateImage}>Update</button>
                    </div>
                )}
                <br />
                
                <br /> 
                <input
                    type="file"
                    name="myImage"
                    onChange={(event) => {
                    setSelectedImage(event.target.files[0]);
                    }}
                />
                <ToastComponent isSucceded/>
                <ToastComponent/>
        </div>
    )
}
