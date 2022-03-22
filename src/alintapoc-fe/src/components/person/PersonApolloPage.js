import React from 'react'
import { gql, useQuery } from '@apollo/client'
import { Link } from 'react-router-dom'
import PersonList from './PersonList'

const PERSON_ATTRIBUTES = gql`
  fragment PersonInfo on Person {
    id
    firstName
    lastName
    doB
  }
`

const PEOPLE = gql`
  query getAllPeople {
    getAllPeople {
      ...PersonInfo
    }
  }
  ${PERSON_ATTRIBUTES}
`

function PersonApolloPage () {
  const { loading, error, data } = useQuery(PEOPLE)
  if (loading) return <p>Loading...</p>
  if (error) return <p>Error while loading</p>
  var people = data.getAllPeople

  return (
    <div className='jumbotron'>
      <h1>People Apollo</h1>
      <Link to='/personapollo/0' className='btn btn-lg btn-primary'>
        &spades; Add Apollo
      </Link>
      <PersonList people={people} />
    </div>
  )
}

export default PersonApolloPage
