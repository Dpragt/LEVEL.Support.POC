import { useState, useEffect } from 'react'
import type { Melding, Oplossing, GekoppeldeMeldingView } from '../types/Melding'
import OplossingForm from './OplossingForm'

interface MeldingDetailProps {
  melding: Melding
  onBack: () => void
  onEdit: (melding: Melding) => void
  onDelete: (id: number) => void
  onToggleAfgehandeld: (melding: Melding) => void
  onNavigate: (id: number) => void
}

const prioriteitKleur: Record<string, string> = {
  Laag: '#38a169',
  Normaal: '#3182ce',
  Hoog: '#dd6b20',
  Kritiek: '#e53e3e',
}

function MeldingDetail({ melding, onBack, onEdit, onDelete, onToggleAfgehandeld, onNavigate }: MeldingDetailProps) {
  const [showOplossingForm, setShowOplossingForm] = useState(false)
  const [oplossingen, setOplossingen] = useState<Oplossing[]>(melding.oplossingen ?? [])
  const [koppelingen, setKoppelingen] = useState<GekoppeldeMeldingView[]>([])

  useEffect(() => {
    setOplossingen(melding.oplossingen ?? [])
    fetchKoppelingen()
  }, [melding.id])

  const fetchKoppelingen = async () => {
    try {
      const response = await fetch(`/api/meldingen/${melding.id}/koppelingen`)
      if (response.ok) {
        const data: GekoppeldeMeldingView[] = await response.json()
        setKoppelingen(data)
      }
    } catch {
      // silently fail
    }
  }

  const handleAddOplossing = async (beschrijving: string, bron: string) => {
    try {
      const response = await fetch(`/api/meldingen/${melding.id}/oplossingen`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ beschrijving, bron }),
      })
      if (response.ok) {
        const created: Oplossing = await response.json()
        setOplossingen([created, ...oplossingen])
        setShowOplossingForm(false)
      }
    } catch {
      // silently fail
    }
  }

  const handleDeleteOplossing = async (id: number) => {
    try {
      const response = await fetch(`/api/meldingen/${melding.id}/oplossingen/${id}`, {
        method: 'DELETE',
      })
      if (response.ok) {
        setOplossingen(oplossingen.filter((o) => o.id !== id))
      }
    } catch {
      // silently fail
    }
  }

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString(undefined, {
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    })
  }

  const formatDateShort = (dateString: string) => {
    return new Date(dateString).toLocaleDateString(undefined, {
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    })
  }

  return (
    <div className="melding-detail">
      <button className="melding-btn melding-btn-back" onClick={onBack} type="button">
        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" aria-hidden="true">
          <line x1="19" y1="12" x2="5" y2="12" />
          <polyline points="12 19 5 12 12 5" />
        </svg>
        Terug naar overzicht
      </button>

      <div className="melding-detail-card card">
        <div className="melding-detail-header">
          <div>
            <h2 className="section-title">{melding.titel}</h2>
            <p className="melding-detail-date">
              Aangemaakt op <time dateTime={melding.aangemaaktOp}>{formatDate(melding.aangemaaktOp)}</time>
            </p>
          </div>
          <div className="melding-detail-badges">
            {melding.prioriteit && (
              <span
                className="melding-prioriteit"
                style={{ backgroundColor: prioriteitKleur[melding.prioriteit] ?? '#718096' }}
              >
                {melding.prioriteit}
              </span>
            )}
            <span className={`melding-status ${melding.isAfgehandeld ? 'status-afgehandeld' : 'status-open'}`}>
              {melding.isAfgehandeld ? 'Afgehandeld' : 'Open'}
            </span>
          </div>
        </div>

        {melding.applicatie && (
          <div className="melding-detail-field">
            <span className="melding-detail-label">Applicatie</span>
            <span className="melding-applicatie">{melding.applicatie}</span>
          </div>
        )}

        {melding.categorie && (
          <div className="melding-detail-field">
            <span className="melding-detail-label">Categorie</span>
            <span className="melding-categorie">{melding.categorie}</span>
          </div>
        )}

        {melding.samenvatting && (
          <div className="melding-detail-field">
            <span className="melding-detail-label">Samenvatting</span>
            <p className="melding-detail-samenvatting">{melding.samenvatting}</p>
          </div>
        )}

        <div className="melding-detail-field">
          <span className="melding-detail-label">Beschrijving</span>
          <p className="melding-detail-beschrijving">{melding.beschrijving || 'Geen beschrijving'}</p>
        </div>

        {/* Oplossingen sectie */}
        <div className="melding-detail-section">
          <div className="melding-detail-section-header">
            <span className="melding-detail-label">
              Oplossingen ({oplossingen.length})
            </span>
            {!showOplossingForm && (
              <button
                className="melding-btn melding-btn-add"
                onClick={() => setShowOplossingForm(true)}
                type="button"
              >
                + Oplossing toevoegen
              </button>
            )}
          </div>

          {showOplossingForm && (
            <OplossingForm
              onSubmit={handleAddOplossing}
              onCancel={() => setShowOplossingForm(false)}
            />
          )}

          {oplossingen.length > 0 && (
            <div className="oplossingen-list">
              {oplossingen.map((oplossing) => (
                <div key={oplossing.id} className="oplossing-item">
                  <div className="oplossing-header">
                    <span className={`oplossing-bron ${oplossing.bron === 'AI-suggestie' ? 'bron-ai' : 'bron-handmatig'}`}>
                      {oplossing.bron === 'AI-suggestie' ? '🤖 ' : '👤 '}
                      {oplossing.bron}
                    </span>
                    <span className="oplossing-datum">{formatDateShort(oplossing.aangemaaktOp)}</span>
                    <button
                      className="melding-btn melding-btn-delete"
                      onClick={() => handleDeleteOplossing(oplossing.id)}
                      type="button"
                      aria-label="Oplossing verwijderen"
                    >
                      <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" aria-hidden="true">
                        <line x1="18" y1="6" x2="6" y2="18" />
                        <line x1="6" y1="6" x2="18" y2="18" />
                      </svg>
                    </button>
                  </div>
                  <p className="oplossing-beschrijving">{oplossing.beschrijving}</p>
                </div>
              ))}
            </div>
          )}

          {oplossingen.length === 0 && !showOplossingForm && (
            <p className="melding-detail-empty">Nog geen oplossingen toegevoegd.</p>
          )}
        </div>

        {/* Gekoppelde meldingen sectie */}
        {koppelingen.length > 0 && (
          <div className="melding-detail-section">
            <span className="melding-detail-label">
              Gekoppelde meldingen ({koppelingen.length})
            </span>
            <div className="koppelingen-list">
              {koppelingen.map((koppeling) => (
                <button
                  key={koppeling.id}
                  className="koppeling-item"
                  onClick={() => onNavigate(koppeling.gekoppeldeId)}
                  type="button"
                >
                  <div className="koppeling-info">
                    {koppeling.gekoppeldeApplicatie && (
                      <span className="melding-applicatie">{koppeling.gekoppeldeApplicatie}</span>
                    )}
                    <span className="koppeling-titel">{koppeling.gekoppeldeTitel}</span>
                  </div>
                  {koppeling.reden && (
                    <p className="koppeling-reden">{koppeling.reden}</p>
                  )}
                </button>
              ))}
            </div>
          </div>
        )}

        <div className="melding-detail-actions">
          <button
            className="melding-btn melding-btn-toggle"
            onClick={() => onToggleAfgehandeld(melding)}
            type="button"
          >
            {melding.isAfgehandeld ? 'Heropen' : '✓ Markeer als afgehandeld'}
          </button>
          <button
            className="melding-btn melding-btn-edit"
            onClick={() => onEdit(melding)}
            type="button"
          >
            Bewerken
          </button>
          <button
            className="melding-btn melding-btn-delete"
            onClick={() => onDelete(melding.id)}
            type="button"
          >
            Verwijderen
          </button>
        </div>
      </div>
    </div>
  )
}

export default MeldingDetail
