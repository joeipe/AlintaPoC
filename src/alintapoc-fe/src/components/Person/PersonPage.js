import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import { getAllPeople } from '../../api/dataApiService'

function PersonPage () {
  const [people, setPeople] = useState([])

  useEffect(() => {
    getAllPeople().then(data => setPeople(data))
  }, [])

  return (
    <div className='jumbotron'>
      <h1>People</h1>
      <Link to='/person/0' className='btn btn-lg btn-primary'>
        &spades; Add
      </Link>
      <table className='table'>
        <thead>
          <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>DoB</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {people?.map(person => {
            return (
              <tr key={person.id}>
                <td>{person.firstName}</td>
                <td>{person.lastName}</td>
                <td>{person.doB}</td>
                <td>
                  <Link to={`/person/${person.id}`}>Edit</Link>
                </td>
              </tr>
            )
          })}
        </tbody>
      </table>
    </div>
  )
}

export default PersonPage
