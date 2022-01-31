import React from 'react'
import { Route, Routes } from 'react-router-dom'
import { NavLink } from 'react-router-dom'
import './App.css'
import HomePage from '../HomePage'
import AboutPage from '../AboutPage'
import PersonPage from '../Person/PersonPage'
import PersonModifyPage from '../Person/PersonModifyPage'
import NotFoundPage from '../NotFoundPage'

function App () {
  return (
    <>
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
              <NavLink className='nav-link' to='/about'>
                About
              </NavLink>
            </li>
          </ul>
        </div>
      </nav>
      <main role='main' className='container'>
        <Routes>
          <Route path='/' exact={true} element={<HomePage />} />
          <Route path='/about' element={<AboutPage />} />
          <Route path='/person' element={<PersonPage />} />
          <Route path='/person/:id' element={<PersonModifyPage />} />
          <Route element={<NotFoundPage />} />
        </Routes>
      </main>
    </>
  )
}

export default App
