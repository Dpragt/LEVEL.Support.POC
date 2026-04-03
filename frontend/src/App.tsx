import { useState, useEffect, useCallback } from 'react'
import aspireLogo from '/Aspire.png'
import './App.css'
import type { Melding, MeldingInput } from './types/Melding'
import MeldingCard from './components/MeldingCard'
import MeldingDetail from './components/MeldingDetail'
import MeldingForm from './components/MeldingForm'

type View = 'list' | 'detail' | 'create' | 'edit'

function App() {
  const [meldingen, setMeldingen] = useState<Melding[]>([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [filterAfgehandeld, setFilterAfgehandeld] = useState(false)
  const [view, setView] = useState<View>('list')
  const [selectedMelding, setSelectedMelding] = useState<Melding | null>(null)

  const fetchMeldingen = useCallback(async () => {
    setLoading(true)
    setError(null)

    try {
      const url = filterAfgehandeld ? '/api/meldingen/afgehandeld' : '/api/meldingen'
      const response = await fetch(url)

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`)
      }

      const data: Melding[] = await response.json()
      setMeldingen(data)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Kan meldingen niet ophalen')
      console.error('Error fetching meldingen:', err)
    } finally {
      setLoading(false)
    }
  }, [filterAfgehandeld])

  useEffect(() => {
    fetchMeldingen()
  }, [fetchMeldingen])

  const fetchMeldingById = async (id: number) => {
    try {
      const response = await fetch(`/api/meldingen/${id}`)
      if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`)
      const data: Melding = await response.json()
      setSelectedMelding(data)
      setView('detail')
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Kan melding niet ophalen')
    }
  }

  const handleCreate = async (input: MeldingInput) => {
    try {
      const response = await fetch('/api/meldingen', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(input),
      })
      if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`)
      setView('list')
      await fetchMeldingen()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Kan melding niet aanmaken')
    }
  }

  const handleUpdate = async (input: MeldingInput) => {
    if (!selectedMelding) return
    try {
      const response = await fetch(`/api/meldingen/${selectedMelding.id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(input),
      })
      if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`)
      setView('list')
      setSelectedMelding(null)
      await fetchMeldingen()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Kan melding niet bijwerken')
    }
  }

  const handleDelete = async (id: number) => {
    if (!confirm('Weet je zeker dat je deze melding wilt verwijderen?')) return
    try {
      const response = await fetch(`/api/meldingen/${id}`, { method: 'DELETE' })
      if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`)
      if (view === 'detail') {
        setView('list')
        setSelectedMelding(null)
      }
      await fetchMeldingen()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Kan melding niet verwijderen')
    }
  }

  const handleToggleAfgehandeld = async (melding: Melding) => {
    try {
      const response = await fetch(`/api/meldingen/${melding.id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...melding, isAfgehandeld: !melding.isAfgehandeld }),
      })
      if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`)
      if (view === 'detail' && selectedMelding?.id === melding.id) {
        setSelectedMelding({ ...melding, isAfgehandeld: !melding.isAfgehandeld })
      }
      await fetchMeldingen()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Kan status niet wijzigen')
    }
  }

  return (
    <div className="app-container">
      <header className="app-header">
        <a
          href="https://aspire.dev"
          target="_blank"
          rel="noopener noreferrer"
          aria-label="Visit Aspire website (opens in new tab)"
          className="logo-link"
        >
          <img src={aspireLogo} className="logo" alt="Aspire logo" />
        </a>
        <h1 className="app-title">LEVEL Support</h1>
        <p className="app-subtitle">Meldingen beheer</p>
      </header>

      <main className="main-content">
        {view === 'detail' && selectedMelding && (
          <section className="weather-section">
            <MeldingDetail
              melding={selectedMelding}
              onBack={() => { setView('list'); setSelectedMelding(null) }}
              onEdit={(m) => { setSelectedMelding(m); setView('edit') }}
              onDelete={handleDelete}
              onToggleAfgehandeld={handleToggleAfgehandeld}
            />
          </section>
        )}

        {(view === 'list') && (
          <section className="weather-section" aria-labelledby="meldingen-heading">
            <div className="card">
              <div className="section-header">
                <h2 id="meldingen-heading" className="section-title">Meldingen</h2>
                <div className="header-actions">
                  <fieldset className="toggle-switch" aria-label="Filter meldingen">
                    <legend className="visually-hidden">Filter</legend>
                    <button
                      className={`toggle-option ${!filterAfgehandeld ? 'active' : ''}`}
                      onClick={() => setFilterAfgehandeld(false)}
                      aria-pressed={!filterAfgehandeld}
                      type="button"
                    >
                      Alle
                    </button>
                    <button
                      className={`toggle-option ${filterAfgehandeld ? 'active' : ''}`}
                      onClick={() => setFilterAfgehandeld(true)}
                      aria-pressed={filterAfgehandeld}
                      type="button"
                    >
                      Afgehandeld
                    </button>
                  </fieldset>
                  <button
                    className="refresh-button"
                    onClick={() => setView('create')}
                    type="button"
                    aria-label="Nieuwe melding aanmaken"
                  >
                    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" aria-hidden="true">
                      <line x1="12" y1="5" x2="12" y2="19" />
                      <line x1="5" y1="12" x2="19" y2="12" />
                    </svg>
                    <span>Nieuwe melding</span>
                  </button>
                  <button
                    className="refresh-button"
                    onClick={fetchMeldingen}
                    disabled={loading}
                    aria-label={loading ? 'Meldingen laden' : 'Meldingen vernieuwen'}
                    type="button"
                  >
                    <svg
                      className={`refresh-icon ${loading ? 'spinning' : ''}`}
                      width="20"
                      height="20"
                      viewBox="0 0 24 24"
                      fill="none"
                      stroke="currentColor"
                      strokeWidth="2"
                      aria-hidden="true"
                      focusable="false"
                    >
                      <path d="M21.5 2v6h-6M2.5 22v-6h6M2 11.5a10 10 0 0 1 18.8-4.3M22 12.5a10 10 0 0 1-18.8 4.2" />
                    </svg>
                    <span>{loading ? 'Laden...' : 'Vernieuwen'}</span>
                  </button>
                </div>
              </div>

              {error && (
                <div className="error-message" role="alert" aria-live="polite">
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" aria-hidden="true">
                    <circle cx="12" cy="12" r="10" />
                    <line x1="12" y1="8" x2="12" y2="12" />
                    <line x1="12" y1="16" x2="12.01" y2="16" />
                  </svg>
                  <span>{error}</span>
                </div>
              )}

              {loading && meldingen.length === 0 && (
                <div className="loading-skeleton" role="status" aria-live="polite" aria-label="Meldingen laden">
                  {[...Array(5)].map((_, i) => (
                    <div key={i} className="skeleton-row" aria-hidden="true" />
                  ))}
                  <span className="visually-hidden">Meldingen worden geladen...</span>
                </div>
              )}

              {!loading && meldingen.length === 0 && !error && (
                <div className="melding-empty">
                  <p>Geen meldingen gevonden.</p>
                  <button className="refresh-button" onClick={() => setView('create')} type="button">
                    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" aria-hidden="true">
                      <line x1="12" y1="5" x2="12" y2="19" />
                      <line x1="5" y1="12" x2="19" y2="12" />
                    </svg>
                    <span>Eerste melding aanmaken</span>
                  </button>
                </div>
              )}

              {meldingen.length > 0 && (
                <div className="weather-grid">
                  {meldingen.map((melding) => (
                    <MeldingCard
                      key={melding.id}
                      melding={melding}
                      onClick={fetchMeldingById}
                      onDelete={handleDelete}
                      onToggleAfgehandeld={handleToggleAfgehandeld}
                    />
                  ))}
                </div>
              )}
            </div>
          </section>
        )}

        {view === 'create' && (
          <MeldingForm onSubmit={handleCreate} onCancel={() => setView('list')} />
        )}

        {view === 'edit' && selectedMelding && (
          <MeldingForm
            melding={selectedMelding}
            onSubmit={handleUpdate}
            onCancel={() => { setView('detail') }}
          />
        )}
      </main>

      <footer className="app-footer">
        <nav aria-label="Footer navigation">
          <a href="https://aspire.dev" target="_blank" rel="noopener noreferrer">
            Learn more about Aspire<span className="visually-hidden"> (opens in new tab)</span>
          </a>
          <a
            href="https://github.com/dotnet/aspire"
            target="_blank"
            rel="noopener noreferrer"
            className="github-link"
            aria-label="View Aspire on GitHub (opens in new tab)"
          >
            <img src="/github.svg" alt="" width="24" height="24" aria-hidden="true" />
            <span className="visually-hidden">GitHub</span>
          </a>
        </nav>
      </footer>
    </div>
  )
}

export default App
