import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import { getAllPeople } from '../../api/dataApiService'
import PersonList from './PersonList'

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
      <PersonList people={people} />
    </div>
  )
}

export default PersonPage
