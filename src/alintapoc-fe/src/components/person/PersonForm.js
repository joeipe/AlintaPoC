import React from 'react'
import moment from 'moment'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'

function PersonForm (props) {
  function formatDateToString (dateString) {
    var dateObject = new Date()
    if (dateString !== '') {
      dateObject = moment(dateString, 'DD/MM/YYYY').toDate();
    }
    return dateObject
  }

  return (
    <form>
      <div className='form-group row'>
        <label htmlFor='firstName' className='col-sm-2 col-form-label required'>
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
      <div className='form-group row'>
        <label htmlFor='lastName' className='col-sm-2 col-form-label required'>
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
      <div className='form-group row'>
        <label htmlFor='doB' className='col-sm-2 col-form-label required'>
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
        />
      </div>
    </form>
  )
}

export default PersonForm
