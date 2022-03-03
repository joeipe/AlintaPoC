import React from 'react'
import { Link } from 'react-router-dom'

function PersonList (props) {
  return (
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
        {props.people?.map(person => {
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
  )
}

export default PersonList
