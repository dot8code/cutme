import axios from 'axios';
import React from 'react'
import { useParams, useNavigate } from 'react-router-dom'

function Redirect() {
    let {shortcut} = useParams();
    const navigate = useNavigate()

    React.useEffect(() => {
        let url = 'https://localhost:7078/api/redirect/'+shortcut;
        axios.get(url)
        .catch(
            (err) => navigate("/")
        )
        .then(respone => {
            window.location.href = respone.data
        })
    })

  return Redirect
}

export default Redirect