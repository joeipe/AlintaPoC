import React, { useState, useEffect } from 'react'
import { useParams, useNavigate, Link } from 'react-router-dom'
import PersonForm from './PersonForm'
import * as dataApiService from '../../api/dataApiService'

function PersonModifyPage () {
  const { id } = useParams()
  let navigate = useNavigate()
  const [errors, setErrors] = useState({});
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
      getPerson()
    } else {
      setActionName('Add')
      setPerson({
        id: 0,
        firstName: '',
        lastName: '',
        doB: ''
      })
    }
  }, [id])

  function handleChange ({ target }) {
    setPerson({
      ...person,
      [target.name]: target.value
    })
  }

  function formIsValid() {
    const _errors = {};

    if (!person.firstName) _errors.firstName = "First Name is required";
    if (!person.lastName) _errors.lastName = "Last Name is required";
    if (!person.doB) _errors.doB = "DoB is required";

    setErrors(_errors);

    return Object.keys(_errors).length === 0;
  }

  function handleBack () {
    navigate('/person')
  }

  function handleDelete () {
    dataApiService.deletePerson(id).then(() => {
      handleBack()
    })
  }

  function handleSubmit () {
    if (!formIsValid()) return;

    if (actionName === 'Edit') {
      editPerson()
    } else {
      addPerson()
    }
  }

  function editPerson () {
    dataApiService.updatePerson(person).then(() => {
      handleBack()
    })
  }

  function addPerson () {
    dataApiService.addPerson(person).then(() => {
      handleBack()
    })
  }

  function getPerson () {
    dataApiService.getPersonById(id).then(data => setPerson(data))
  }

  return (
    <div className='jumbotron'>
      <h1>{actionName} Person</h1>
      <Link to='/person' className='btn btn-lg btn-primary'>
        &laquo; Back
      </Link>
      <PersonForm person={person} errors={errors} onChange={handleChange} onSubmit={handleSubmit} onDelete={handleDelete} />
    </div>
  )
}

export default PersonModifyPage
