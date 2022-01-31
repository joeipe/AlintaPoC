import React, { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom'

function PersonModifyPage (props) {
  const { id } = useParams()

  return (
    <div className='jumbotron'>
      <h1>Todo Person</h1>
      <Link to='/person' className='btn btn-lg btn-primary'>
        &laquo; Back
      </Link>
    </div>
  )
}

export default PersonModifyPage
