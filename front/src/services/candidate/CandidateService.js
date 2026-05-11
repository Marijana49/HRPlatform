const API_URL = import.meta.env.VITE_API_URL;

export async function GetAllCandidates(){
    try{
        const res = await fetch(`${API_URL}api/candidate/candidates`, {
            method: `GET`,
        });
        if(!res.ok){
            throw new Error('Failed to fetch candidates!');
        }
        return await res.json();
    } catch (error){
        console.log(error);
    }
}

export async function DeleteCandidate(id){
    try{
        const res = await fetch(`${API_URL}api/candidate/deleteCandidate`, {
            method: `POST`,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Id: id })
        });
        return res.ok;
    } catch(error){
        console.log(error);
    }
}

export async function UpdateCandidateSkill(id, name){
     try{
        const res = await fetch(`${API_URL}api/candidate/updateSkill?candidateId=${id}&skillName=${encodeURIComponent(name)}`, {
            method: `PATCH`,
            headers: { 'Content-Type': 'application/json' }
        });
        return res.ok;
    } catch(error){
        console.log(error);
    }
}

export async function RemoveCandidateSkill(id, name){
    try{
        const res = await fetch(`${API_URL}api/candidate/removeSkill?candidateId=${id}&skillName=${encodeURIComponent(name)}`, {
            method: `PATCH`,
            headers: { 'Content-Type': 'application/json' }
        });
        return res.ok;
    } catch(error){
        console.log(error);
    }
}

export async function AddNewCandidate(newCandidate){
    try{
        const res = await fetch(`${API_URL}api/candidate/addCandidate`, {
            method: `POST`,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newCandidate)
        });
       return await res.json();
    } catch (error){
         console.log(error);
    }
}

export async function SearchCandidate(searchData){
    try{
        const parameters = new URLSearchParams();
        if (searchData.name) parameters.append('name', searchData.name);
        if (searchData.skills && searchData.skills.length > 0) {
            searchData.skills.forEach(skill => parameters.append('skills', skill));
        }

        const res = await fetch(`${API_URL}api/candidate/searchCandidate?${parameters.toString()}`, {
            method: `GET`,
            headers: { 'Accept': 'application/json' }
        });

       if (!res.ok) return [];
       const data = await res.json();
       return Array.isArray(data) ? data : [];
        return data || [];
    } catch(error){
        console.log(error);
        return [];
    }
}