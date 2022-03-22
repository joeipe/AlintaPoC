import React from 'react'
import { Route, Routes } from 'react-router-dom'
import './App.css'
import Header from '../common/Header'
import HomePage from '../HomePage'
import AboutPage from '../AboutPage'
import PersonPage from '../person/PersonPage'
import PersonModifyPage from '../person/PersonModifyPage'
import NotFoundPage from '../NotFoundPage'
import PersonApolloPage from '../person/PersonApolloPage'
import PersonModifyApolloPage from '../person/PersonModifyApolloPage'

function App () {
  return (
    <>
      <Header />
      <main role='main' className='container'>
        <Routes>
          <Route path='*' element={<NotFoundPage />} />
          <Route path='/' element={<HomePage />} />
          <Route path='/about' element={<AboutPage />} />
          <Route path='/person' element={<PersonPage />} />
          <Route path='/person/:id' element={<PersonModifyPage />} />
          <Route path='/personapollo' element={<PersonApolloPage />} />
          <Route path='/personapollo/:id' element={<PersonModifyApolloPage />} />
        </Routes>
      </main>
    </>
  )
}

export default App
