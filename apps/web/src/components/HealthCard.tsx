import { useEffect, useState } from 'react'

type HealthState =
  | { kind: 'idle' | 'loading' }
  | { kind: 'ok'; message: string }
  | { kind: 'error'; message: string }

const healthUrl = '/health'

function HealthCard() {
  const [state, setState] = useState<HealthState>({ kind: 'idle' })
    const [checkedAt, setCheckedAt] = useState<string>('')
    
    async function checkHealth() {
      setState({ kind: 'loading' })
      try {
        const res = await fetch(healthUrl, { method: 'GET' })
        const text = await res.text()
        if (!res.ok) throw new Error(`HTTP ${res.status} - ${text}`)
        setState({ kind: 'ok', message: text || 'Healthy' })
      } catch (e) {
        const msg = e instanceof Error ? e.message : 'Unknown error'
        setState({ kind: 'error', message: msg })
      } finally {
        setCheckedAt(new Date().toLocaleString())
      }
    }
  
    useEffect(() => {
      checkHealth()
    }, [])
  
    const badge =
      state.kind === 'ok' ? '🟢' : state.kind === 'error' ? '🔴' : '🟡'
  
    return (
      <div style={{ maxWidth: 720, margin: '0 auto', padding: 24 }}>
        <h1>Fintech-lite</h1>
  
        <section style={{ padding: 16, border: '1px solid #333', borderRadius: 8 }}>
          <h2>
            API Health {badge}
          </h2>
  
          <p>
            <strong>URL:</strong> {healthUrl}
          </p>
  
          {state.kind === 'loading' && <p>Checking...</p>}
          {state.kind === 'ok' && <p><strong>Status:</strong> {state.message}</p>}
          {state.kind === 'error' && (
            <p style={{ color: '#ff6b6b' }}>
              <strong>Error:</strong> {state.message}
            </p>
          )}
  
          <p style={{ opacity: 0.7 }}>Last checked: {checkedAt || '-'}</p>
  
          <button onClick={() => checkHealth()}>Re-check</button>
        </section>
      </div>
    )
}

export default HealthCard