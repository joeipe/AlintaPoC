import React, { useState, useEffect } from 'react'
import _ from 'lodash'
import moment from 'moment'
import { gql, useQuery, useMutation } from '@apollo/client'
import { useParams, useNavigate, Link } from 'react-router-dom'
import PersonForm from './PersonForm'

const PERSON_BY_ID = gql`
  query getPersonById($id: ID!) {
    getPersonById(id: $id) {
      id
      firstName
      lastName
      doB
    }
  }
`

const ADD_PERSON = gql`
  mutation addPerson($person: PersonInput!) {
    addPerson(person: $person)
  }
`

const UPDATE_PERSON = gql`
  mutation updatePerson($person: PersonInput!) {
    updatePerson(person: $person)
  }
`

const DELETE_PERSON = gql`
  mutation deletePerson($id: ID!) {
    deletePerson(id: $id)
  }
`

function PersonModifyApolloPage () {
  const { id } = useParams()
  let navigate = useNavigate()
  const [errors, setErrors] = useState({})
  const [actionName, setActionName] = useState('')
  const [person, setPerson] = useState({
    id: 0,
    firstName: '',
    lastName: '',
    doB: ''
  })

  useEffect(() => {
    if (id !== '0') {
      setActionName('Edit')
    } else {
      setActionName('Add')
    }
  }, [id])

  const [addPer, { called, error }] = useMutation(ADD_PERSON)
  const [updatePer] = useMutation(UPDATE_PERSON)
  const [deletePer] = useMutation(DELETE_PERSON)
  GetPerson()

  function handleChange ({ target }) {
    setPerson({
      ...person,
      [target.name]: target.value
    })
  }

  function handleDateChange (date) {
    setPerson({ ...person, doB: moment(date).format('DD/MM/YYYY') })
  }

  function formIsValid () {
    const _errors = {}

    if (!person.firstName) _errors.firstName = 'First Name is required'
    if (!person.lastName) _errors.lastName = 'Last Name is required'
    if (!person.doB) _errors.doB = 'DoB is required'

    setErrors(_errors)

    return Object.keys(_errors).length === '0'
  }

  function handleBack () {
    navigate('/personapollo')
  }

  function handleDelete () {
    deletePer({ variables: { id: id } }).then(() => {
      handleBack()
    })
  }

  function handleSubmit () {
    //if (!formIsValid()) return

    if (actionName === 'Edit') {
      editPerson()
    } else {
      addPerson()
    }
  }

  async function editPerson () {
    await updatePer({ variables: { person: person } })
    handleBack()
  }

  async function addPerson () {
    await addPer({ variables: { person: person } })
    handleBack()
  }

  function GetPerson () {
    const { loading, error, data } = useQuery(PERSON_BY_ID, {
      variables: { id: id },
      skip: id === '0' || person.id !== 0
    })
    if (loading) return <p>Loading...</p>
    if (error) return <p>Error while loading</p>
    if (data) {
      //var picked = (({ id, firstName, lastName, doB }) => ({ id, firstName, lastName, doB }))(data.getPersonById);
      var picked = _.pick(data.getPersonById, [
        'id',
        'firstName',
        'lastName',
        'doB'
      ])
      setPerson(picked)
    }
  }

  return (
    <div className='jumbotron'>
      <h1>{actionName} Person Apollo</h1>
      <Link to='/personapollo' className='btn btn-lg btn-primary'>
        &laquo; Back
      </Link>
      <PersonForm
        person={person}
        errors={errors}
        onChange={handleChange}
        onDateChange={handleDateChange}
        onSubmit={handleSubmit}
        onDelete={handleDelete}
      />
    </div>
  )
}

export default PersonModifyApolloPage
