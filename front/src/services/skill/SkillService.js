const API_URL = import.meta.env.VITE_API_URL;

export async function AddNewSkill(newSkill){
    try{
        const res = await fetch(`${API_URL}api/skill/addSkill`, {
            method: `POST`,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newSkill)
        });
       return await res.json();
    } catch (error){
         console.log(error);
    }
}

export async function GetAllSkills(){
    try{
        const res = await fetch(`${API_URL}api/skill/skills`, {
            method: `GET`,
        });
        if(!res.ok){
            throw new Error('Failed to fetch skills!');
        }
        return await res.json();
    } catch (error){
        console.log(error);
    }
}