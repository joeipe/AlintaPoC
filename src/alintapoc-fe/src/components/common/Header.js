import React from 'react'
import { NavLink } from 'react-router-dom'

function Header () {
  return (
    <nav className='navbar navbar-expand-md navbar-dark fixed-top bg-dark'>
      <NavLink className='navbar-brand' to='/'>
        Alinta-PoC
      </NavLink>
      <button
        className='navbar-toggler'
        type='button'
        data-toggle='collapse'
        data-target='#navbarCollapse'
        aria-controls='navbarCollapse'
        aria-expanded='false'
        aria-label='Toggle navigation'
      >
        <span className='navbar-toggler-icon'></span>
      </button>
      <div className='collapse navbar-collapse' id='navbarCollapse'>
        <ul className='navbar-nav mr-auto'>
          <li className='nav-item'>
            <NavLink className='nav-link' to='/'>
              Home
            </NavLink>
          </li>
          <li className='nav-item'>
            <NavLink className='nav-link' to='/person'>
              Person
            </NavLink>
          </li>
          <li className='nav-item'>
            <NavLink className='nav-link' to='/personapollo'>
              PersonApollo
            </NavLink>
          </li>
          <li className='nav-item'>
            <NavLink className='nav-link' to='/about'>
              About
            </NavLink>
          </li>
        </ul>
      </div>
    </nav>
  )
}

export default Header
