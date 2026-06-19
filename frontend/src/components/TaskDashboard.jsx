import React, { useEffect, useState } from 'react'
import { getTasks, createTask } from '../api'

export default function TaskDashboard({ token, onLogout }) {
  const [tasks, setTasks] = useState([])
  const [title, setTitle] = useState('')
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)

  async function load() {
    setLoading(true)
    setError(null)
    try {
      const data = await getTasks(token)
      setTasks(data || [])
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => { load() }, [])

  async function addTask(e) {
    e.preventDefault()
    if (!title) return
    try {
      await createTask(token, title)
      setTitle('')
      await load()
    } catch (err) {
      setError(err.message)
    }
  }

  return (
    <div className="card">
      <div className="toolbar">
        <h2>Your Tasks</h2>
        <button onClick={onLogout}>Logout</button>
      </div>

      <form onSubmit={addTask} className="add-form">
        <input placeholder="New task title" value={title} onChange={(e) => setTitle(e.target.value)} />
        <button type="submit">Add</button>
      </form>

      {loading ? (
        <div>Loading...</div>
      ) : error ? (
        <div className="error">{error}</div>
      ) : (
        <ul className="tasks">
          {tasks.map((t) => (
            <li key={t.id}>{t.title}</li>
          ))}
        </ul>
      )}
    </div>
  )
}
