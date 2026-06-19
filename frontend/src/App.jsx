import React, { useState } from 'react'
import Login from './components/Login'
import TaskDashboard from './components/TaskDashboard'

export default function App() {
  const [token, setToken] = useState(null)

  return (
    <div className="app-container">
      <h1>Task Manager Dashboard</h1>
      {!token ? (
        <Login onLogin={setToken} />
      ) : (
        <TaskDashboard token={token} onLogout={() => setToken(null)} />
      )}
    </div>
  )
}
