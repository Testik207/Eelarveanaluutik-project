import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [connection, setConnection] = useState('loading')
  const [refresh, setRefresh] = useState(0)

  useEffect(() => {
    const controller = new AbortController()

    async function checkApi() {
      try {
        const response = await fetch('/api/health', { signal: controller.signal })
        if (!response.ok) {
          throw new Error('API request failed')
        }

        const result = await response.json()
        if (result.status !== 'ok') {
          throw new Error('Unexpected API response')
        }

        setConnection('connected')
      } catch (error) {
        if (error.name !== 'AbortError') {
          setConnection('error')
        }
      }
    }

    checkApi()
    return () => controller.abort()
  }, [refresh])

  function retryConnection() {
    setConnection('loading')
    setRefresh((value) => value + 1)
  }

  return (
    <main className="page">
      <header>
        <p className="eyebrow">Isiklik eelarve</p>
        <h1>Eelarveanalüütik</h1>
        <p className="intro">Hoia oma tuludel ja kuludel silm peal.</p>
      </header>

      <section className="connection-card" aria-labelledby="connection-title">
        <h2 id="connection-title">Serveriühendus</h2>
        <div role="status" aria-live="polite">
          {connection === 'loading' && <p>Kontrollin ühendust...</p>}
          {connection === 'connected' && <p className="connected">Ühendus olemas</p>}
          {connection === 'error' && (
            <p className="error">Serveriga ei saa ühendust. Proovi uuesti.</p>
          )}
        </div>
        <button
          type="button"
          onClick={retryConnection}
          disabled={connection === 'loading'}
        >
          Kontrolli uuesti
        </button>
      </section>
    </main>
  )
}

export default App
