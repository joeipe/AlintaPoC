import React, { useState, useEffect } from 'react'

function PersonForm (props) {
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
            <small className='text-danger'>
              {props.errors.firstName}
            </small>
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
            <small className='text-danger'>
              {props.errors.lastName}
            </small>
          )}
        </div>
      </div>
      <div className='form-group row'>
        <label htmlFor='doB' className='col-sm-2 col-form-label required'>
          DoB
        </label>
        <div className='col-sm-10'>
          <input
            id='doB'
            type='text'
            name='doB'
            onChange={props.onChange}
            className='form-control'
            value={props.person.doB || ''}
            required
          />
          {props.errors.doB && (
            <small className='text-danger'>
              {props.errors.doB}
            </small>
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
