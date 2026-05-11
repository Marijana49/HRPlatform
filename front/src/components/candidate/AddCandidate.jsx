import { useState } from "react"
import { AddNewCandidate } from "../../services/candidate/CandidateService";
import { Link } from "react-router-dom";

export function AddCandidate(){
    const [formData, setFormData] = useState({
        FullName: "",
        BirthDate: "",
        ContactNumber: "",
        Email: "",
        Skills: ""
    });

    const initialState = {
        FullName: "",
        BirthDate: "",
        ContactNumber: "",
        Email: "",
        Skills: ""
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,      
            [name]: value     
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const sendCandidate = {
            FullName: formData.FullName,
            BirthDate: formData.BirthDate,
            ContactNumber: formData.ContactNumber,
            Email: formData.Email,
            Skills: formData.Skills ? formData.Skills.split(",").map(s => s.trim()) : []
        };

        const res = await AddNewCandidate(sendCandidate)

        if(res){
            console.log(res.message);
            alert("New candidate added!");
            setFormData(initialState);
        } else{
            console.log("Problem while adding candidate")
        }
    };

    return(
        <>
            <h1>Add new candidate</h1>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="FullName"
                    placeholder="Full Name"
                    value={formData.FullName}
                    onChange={handleChange}
                />
                 <input
                    type="date"
                    name="BirthDate"
                    placeholder="Birth date"
                    value={formData.BirthDate}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="ContactNumber"
                    placeholder="Contact number"
                    value={formData.ContactNumber}
                    onChange={handleChange}
                />
                <input
                    type="email"
                    name="Email"
                    placeholder="Email"
                    value={formData.Email}
                    onChange={handleChange}
                />
                <textarea
                    name="Skills"
                    placeholder="Skills"
                    value={formData.Skills}
                    onChange={handleChange}
                />
                <button type="submit">Add new candidate</button>
            </form>
        </>
    )
}