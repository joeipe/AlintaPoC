import { handleResponse, handleError } from "./apiUtils";
const baseUrl = "https://localhost:44318/api/Data";

export function getAllPeople() {
    return fetch(`${baseUrl}/GetAllPeople`)
      .then(handleResponse)
      .catch(handleError);
  }
  
  export function getPersonById(id) {
    return fetch(`${baseUrl}/GetPersonById/${id}`)
      .then(handleResponse)
      .catch(handleError);
  }
  
  export function addPerson(person) {
    return fetch(`${baseUrl}/AddPerson`, {
      method: "POST",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(person),
    })
      .then(handleResponse)
      .catch(handleError);
  }
  
  export function updatePerson(person) {
    return fetch(`${baseUrl}/UpdatePerson`, {
      method: "PUT",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(person),
    })
      .then(handleResponse)
      .catch(handleError);
  }
  
  export function deletePerson(id) {
    return fetch(`${baseUrl}/DeletePerson/${id}`, { method: "DELETE" })
      .then(handleResponse)
      .catch(handleError);
  }