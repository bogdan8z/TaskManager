const API_BASE = import.meta.env.VITE_API_BASE || 'https://localhost:7240'
async function request(path, options = {}) {
  const url = `${API_BASE}${path}`
  const res = await fetch(url, options)
  if (!res.ok) {
    const text = await res.text()
    throw new Error(text || res.statusText)
  }
  return res.json().catch(() => null)
}

export function login(email, password) {
  return request('/api/auth/login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  })
}

export function register(email, password) {
  return request('/api/auth/register', {
    method: 'POST',
    headers: { 
      'Content-Type': 'application/json'      
    },
    body: JSON.stringify({ email, password }),
  })
}

export function getTasks(token) {
  return request('/tasks', {
    headers: { Authorization: `Bearer ${token}` },
  })
}

export function createTask(token, title) {
  return request('/tasks', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json', Authorization: `Bearer ${token}` },
    body: JSON.stringify({ title }),
  })
}
