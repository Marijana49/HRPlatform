import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { GetAllCandidates, DeleteCandidate, UpdateCandidateSkill, RemoveCandidateSkill, SearchCandidate } from "../../services/candidate/CandidateService";
 
export function CandidateList(){
    const [candidates, setCandidates] = useState([]);
    const [searchName, setSearchName] = useState("");
    const [searchSkills, setSearchSkills] = useState("");
    
    async function loadCandidates(){
        const data = await GetAllCandidates();
        setCandidates(data);
    }

    useEffect(() => {
        loadCandidates();
    }, []);

    const handleDelete = async (id) => {
        if (window.confirm("Delete this candidate?")) {
            const success = await DeleteCandidate(id);
            if (success) {
                setCandidates(candidates.filter(c => c.id !== id));
            } else {
                alert("Error.");
            }
        }
    };

    const handleUpdate = async (id) => {
        const newSkill = window.prompt("New skill:");
        
        if (newSkill && newSkill.trim() !== "") {
            const success = await UpdateCandidateSkill(id, newSkill);
            
            if (success) {
                alert("Skill added!");
                const updatedData = await GetAllCandidates();
                setCandidates(updatedData || []);
            } else {
                alert("Error: Skill maybe doesn't exist in databese or candidate already has.");
            }
        }
    };

    const handleRemove = async (id) => {
        const deleteSkill = window.prompt("Skill for remove:");
        
        if (deleteSkill && deleteSkill.trim() !== "") {
            const success = await RemoveCandidateSkill(id, deleteSkill);
            
            if (success) {
                alert("Skill removed!");
                const updatedData = await GetAllCandidates();
                setCandidates(updatedData || []);
            } else {
                alert("error");
            }
        }
    };

    const handleSearch = async () => {
        const skillsArray = searchSkills 
        ? searchSkills.split(',').map(s => s.trim()).filter(s => s !== "") 
        : [];

        const filteredData = await SearchCandidate({ name: searchName, skills: skillsArray });
        setCandidates(filteredData || []);
    }

    const handleReset = async () => {
        setSearchName("");
        setSearchSkills("");
        await loadCandidates();
    };
 
    return(
        <div>
            <h3>Job Candidates</h3>
            <div className="search-container">
                <input
                    type="text"
                    name="skill"
                    placeholder="Search candidate name"
                    value={searchName}
                    onChange={(e) => setSearchName(e.target.value)}
                />
                <input
                type="text"
                name="name"
                placeholder="Search skill(s)"
                value={searchSkills}
                onChange={(e) => setSearchSkills(e.target.value)}
            />
            <button onClick={handleSearch}>Search</button>
            <button onClick={handleReset}>Reset search</button>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>Full Name</th>
                        <th>Birth Date</th>
                        <th>Contact Number</th>
                        <th>Email</th>
                        <th>Skills</th>
                    </tr>
                </thead>
                <tbody>
                    {candidates.length > 0 && candidates? (
                        candidates.map((candidate, index) =>(
                            <tr key={candidate.id || index}>
                                <td>{candidate.fullName}</td>
                                <td>{new Date(candidate.birthDate).toLocaleDateString()}</td>
                                <td>{candidate.contactNumber}</td>
                                <td>{candidate.email}</td>
                                <td>{candidate.skills && candidate.skills.length > 0 ? candidate.skills.join(", ") : "No skilss"}</td>
                                <td>
                                    <button onClick={() => handleDelete(candidate.id)}>Delete</button>
                                </td>
                                <td>
                                    <button onClick={() => handleUpdate(candidate.id)}>Update skill</button>
                                </td>
                                <td>
                                    <button onClick={() => handleRemove(candidate.id)}>Remove skill</button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td>No candidates</td>
                        </tr>   
                    )}
                </tbody>
            </table>
        </div>
    )
}