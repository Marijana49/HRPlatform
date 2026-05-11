import { useEffect, useState } from "react"
import { GetAllSkills } from "../../services/skill/SkillService";

export function SkillList(){
    const [skills, setSkills] = useState([]);
    
        useEffect(() => {
            async function loadSkills(){
                const data = await GetAllSkills();
                setSkills(data);
            }
            loadSkills();
        }, []);

    return(
        <div>
            <h3>Skills</h3>
            <table>
                <thead>
                    <tr>
                        <th>Skill Name</th>
                    </tr>
                </thead>
                <tbody>
                    {skills.length > 0 ? (
                        skills.map((skill) =>(
                            <tr key={skill.id}>
                                <td>{skill.name}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td>No skils</td>
                        </tr>   
                    )}
                </tbody>
            </table>
        </div>
    )
}