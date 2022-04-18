import axios from 'axios';
import React, { useState } from 'react'

function Home() {
    const [fullUrl, setFullUrl] = useState("");
    const [shortcut, setShortcut] = useState("");

    const handleSubmit = (event) => 
    {
        event.preventDefault();

        let redirectInformation = {
            shortcut: shortcut,
            fullUrl: fullUrl
        }

        axios.post('https://localhost:7078/api/redirect', redirectInformation)
        .catch(function (error)
        {
            if(error.response)
            {
                console.log(error.response.data);
                console.log(error.response.status);
                console.log(error.response.headers);
            }
            else if (error.request) 
            {
                console.log(error.request);
            } 
            else 
            {
                console.log('Error', error.message);
            }
        })
        .then(response => alert("Done"))
    }

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Full Url:
                <input type="text" 
                    name="fullUrl" 
                    value={fullUrl} 
                    onChange={(e) => setFullUrl(e.target.value)} 
                />
            </label>
            <label>
                Shortcut:
                <input type="text" 
                    name="shortcut" 
                    value={shortcut} 
                    onChange={(e) => setShortcut(e.target.value)}
                />
            </label>
            <input type="submit" value="Send" />
        </form>
    )
}

export default Home