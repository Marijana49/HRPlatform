import { useState } from "react"
import { AddNewSkill } from "../../services/skill/SkillService";

export function AddSkill(){
    const [Name, setName] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        const sendSkill = {
            Name: Name,
        };

        const res = await AddNewSkill(sendSkill)

        if(res){
            console.log(res.message);
            alert("New skill added!");
        } else{
            console.log("Problem while adding skill")
        }
    };

    return(
        <>
            <h1>Add new skill</h1>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="Name"
                    placeholder="Skill Name"
                    value={Name}
                    onChange={(e) => setName(e.target.value)}
                />
                <button type="submit">Add new skill</button>
            </form>
        </>
    )
}