import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { GetAllCandidates, DeleteCandidate, UpdateCandidateSkill, RemoveCandidateSkill } from "../../services/candidate/CandidateService";
 
export function CandidateList(){
    const [candidates, setCandidates] = useState([]);

    useEffect(() => {
        async function loadCandidates(){
            const data = await GetAllCandidates();
            setCandidates(data);
        }
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
                alert("Veština uspešno dodata!");
                const updatedData = await GetAllCandidates();
                setCandidates(updatedData || []);
            } else {
                alert("Greška: Veština možda ne postoji u bazi ili je kandidat već ima.");
            }
        }
    };

    const handleRemove = async (id) => {
        const deleteSkill = window.prompt("Skill for remove:");
        
        if (deleteSkill && deleteSkill.trim() !== "") {
            const success = await RemoveCandidateSkill(id, deleteSkill);
            
            if (success) {
                alert("Veština obrisana!");
                const updatedData = await GetAllCandidates();
                setCandidates(updatedData || []);
            } else {
                alert("error");
            }
        }
    };
 
    return(
        <div>
            <h3>Job Candidates</h3>
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
                    {candidates.length > 0 ? (
                        candidates.map((candidate) =>(
                            <tr key={candidate.id}>
                                <td>{candidate.fullName}</td>
                                <td>{new Date(candidate.birthDate).toLocaleDateString()}</td>
                                <td>{candidate.contactNumber}</td>
                                <td>{candidate.email}</td>
                                <td>{candidate.skills && candidate.skills.length > 0 ? candidate.skills.join(", ") : "No skilss"}</td>
                                <td>
                                    <button onClick={() => handleDelete(candidate.id)}>Delete</button>
                                </td>
                                <td>
                                    <button onClick={() => handleUpdate(candidate.id)}>Update skills</button>
                                </td>
                                <td>
                                    <button onClick={() => handleRemove(candidate.id)}>Remove skills</button>
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