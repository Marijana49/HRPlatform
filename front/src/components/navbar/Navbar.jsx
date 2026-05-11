import { Link } from "react-router-dom"

export function Navbar(){
    return(
        <nav className="navbar">
            <Link to="/">Candidates</Link>
            <Link to="/candidate/addCandidate">Add Candidate</Link>
            <Link to="/skill/skills">Skills</Link>
            <Link to="/skill/addSkill">Add Skill</Link>
        </nav>
    )
}