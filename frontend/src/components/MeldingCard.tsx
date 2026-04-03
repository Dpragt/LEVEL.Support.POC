import type { Melding } from '../types/Melding'

interface MeldingCardProps {
  melding: Melding
  onClick: (id: number) => void
  onDelete: (id: number) => void
  onToggleAfgehandeld: (melding: Melding) => void
}

const prioriteitKleur: Record<string, string> = {
  Laag: '#38a169',
  Normaal: '#3182ce',
  Hoog: '#dd6b20',
  Kritiek: '#e53e3e',
}

function MeldingCard({ melding, onClick, onDelete, onToggleAfgehandeld }: MeldingCardProps) {
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString(undefined, {
      weekday: 'short',
      month: 'short',
      day: 'numeric',
    })
  }

  return (
    <article
      className={`weather-card melding-card ${melding.isAfgehandeld ? 'melding-afgehandeld' : ''}`}
      aria-label={`Melding: ${melding.titel}`}
    >
      <div className="melding-card-header">
        <h3 className="weather-date">
          <time dateTime={melding.aangemaaktOp}>{formatDate(melding.aangemaaktOp)}</time>
        </h3>
        {melding.prioriteit && (
          <span
            className="melding-prioriteit"
            style={{ backgroundColor: prioriteitKleur[melding.prioriteit] ?? '#718096' }}
          >
            {melding.prioriteit}
          </span>
        )}
      </div>

      {melding.applicatie && <span className="melding-applicatie">{melding.applicatie}</span>}

      <p className="weather-summary melding-titel">{melding.titel}</p>

      <div className="melding-meta">
        {melding.categorie && <span className="melding-categorie">{melding.categorie}</span>}
        {melding.oplossingen && melding.oplossingen.length > 0 && (
          <span className="melding-oplossing-badge">
            💡 {melding.oplossingen.length}
          </span>
        )}
        <span className={`melding-status ${melding.isAfgehandeld ? 'status-afgehandeld' : 'status-open'}`}>
          {melding.isAfgehandeld ? 'Afgehandeld' : 'Open'}
        </span>
      </div>

      <div className="melding-card-actions">
        <button
          className="melding-btn melding-btn-detail"
          onClick={() => onClick(melding.id)}
          type="button"
          aria-label={`Details bekijken van ${melding.titel}`}
        >
          Details
        </button>
        <button
          className="melding-btn melding-btn-toggle"
          onClick={(e) => { e.stopPropagation(); onToggleAfgehandeld(melding) }}
          type="button"
          aria-label={melding.isAfgehandeld ? 'Markeer als open' : 'Markeer als afgehandeld'}
        >
          {melding.isAfgehandeld ? 'Heropen' : '✓ Afronden'}
        </button>
        <button
          className="melding-btn melding-btn-delete"
          onClick={(e) => { e.stopPropagation(); onDelete(melding.id) }}
          type="button"
          aria-label={`Verwijder ${melding.titel}`}
        >
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" aria-hidden="true">
            <polyline points="3 6 5 6 21 6" />
            <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" />
          </svg>
        </button>
      </div>
    </article>
  )
}

export default MeldingCard
