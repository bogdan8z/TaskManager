import React, { useState } from 'react'
import { login, register } from '../api'

export default function Login({ onLogin }) {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState(null)
  const [isRegister, setIsRegister] = useState(false)

  async function submit(e) {
    e.preventDefault()
    setError(null)
    try {
      const fn = isRegister ? register : login
      const token = await fn(email, password)
      if (typeof token === 'string') onLogin(token)
      else if (token?.token) onLogin(token.token)
      else throw new Error('Invalid token response')
    } catch (err) {
      setError(err?.message || String(err))
    }
  }

  return (
    <form className="card" onSubmit={submit}>
      <h2>{isRegister ? 'Register' : 'Login'}</h2>
      <label>
        Email
        <input value={email} onChange={(e) => setEmail(e.target.value)} />
      </label>
      <label>
        Password
        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
      </label>
      <div style={{display: 'flex', gap: 8}}>
        <button type="submit">{isRegister ? 'Register' : 'Login'}</button>
        <button type="button" onClick={() => setIsRegister(!isRegister)} style={{background:'#6b7280'}}>
          {isRegister ? 'Have an account? Login' : 'No account? Register'}
        </button>
      </div>
      {error && <div className="error">{error}</div>}
    </form>
  )
}
