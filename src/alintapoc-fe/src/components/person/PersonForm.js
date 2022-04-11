import React from 'react'
import moment from 'moment'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'

function PersonForm (props) {
  function formatDateToString (dateString) {
    if (dateString !== '' && dateString !== 'Invalid date') {
      return moment(dateString, 'DD/MM/YYYY').toDate();
    }
    return null
  }

  return (
    <form>
      <div className='form-group row required'>
        <label htmlFor='firstName' className='col-sm-2 col-form-label control-label'>
          First Name
        </label>
        <div className='col-sm-10'>
          <input
            id='firstName'
            type='text'
            name='firstName'
            onChange={props.onChange}
            className='form-control'
            value={props.person.firstName || ''}
            required
          />
          {props.errors.firstName && (
            <small className='text-danger'>{props.errors.firstName}</small>
          )}
        </div>
      </div>
      <div className='form-group row required'>
        <label htmlFor='lastName' className='col-sm-2 col-form-label control-label'>
          Last Name
        </label>
        <div className='col-sm-10'>
          <input
            id='lastName'
            type='text'
            name='lastName'
            onChange={props.onChange}
            className='form-control'
            value={props.person.lastName || ''}
            required
          />
          {props.errors.lastName && (
            <small className='text-danger'>{props.errors.lastName}</small>
          )}
        </div>
      </div>
      <div className='form-group row required'>
        <label htmlFor='doB' className='col-sm-2 col-form-label control-label'>
          DoB
        </label>
        <div className='col-sm-10'>
          <DatePicker
            selected={formatDateToString(props.person.doB)}
            dateFormat='dd/MM/yyyy'
            onChange={props.onDateChange}
          />
          {props.errors.doB && (
            <small className='text-danger'>{props.errors.doB}</small>
          )}
        </div>
      </div>
      <div>
        <input
          type='submit'
          value='Submit'
          className='btn btn-primary'
          onClick={props.onSubmit}
        />
        &nbsp;
        <input
          type='submit'
          value='Delete'
          className='btn btn-primary'
          onClick={props.onDelete}
          disabled={props.person.id === 0}
        />
      </div>
    </form>
  )
}

export default PersonForm
