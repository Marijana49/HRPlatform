import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { CandidatePage } from './pages/candidate/CandidatePage'
import { AddCandidatePage } from './pages/candidate/AddCandidatePage'
import { SkillPage } from './pages/skill/SkillPage'
import { Navbar } from './components/navbar/Navbar'
import { AddSkillPage } from './pages/skill/AddSkillPage'
import "./App.css"

function App() {
  return (
    <>
    <BrowserRouter>
    <Navbar />
      <Routes>
        <Route path="/" element={<CandidatePage />} />
        <Route path="/candidate/addCandidate" element={<AddCandidatePage />} />
        <Route path="/skill/skills" element={<SkillPage/>} />
        <Route path="/skill/addSkill" element={<AddSkillPage/>} />
      </Routes>
    </BrowserRouter>      
    </>
  )
}

export default App
