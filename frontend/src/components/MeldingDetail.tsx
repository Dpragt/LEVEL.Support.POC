import type { Melding } from '../types/Melding'

interface MeldingDetailProps {
  melding: Melding
  onBack: () => void
  onEdit: (melding: Melding) => void
  onDelete: (id: number) => void
  onToggleAfgehandeld: (melding: Melding) => void
}

const prioriteitKleur: Record<string, string> = {
  Laag: '#38a169',
  Normaal: '#3182ce',
  Hoog: '#dd6b20',
  Kritiek: '#e53e3e',
}

function MeldingDetail({ melding, onBack, onEdit, onDelete, onToggleAfgehandeld }: MeldingDetailProps) {
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

        <div className="melding-detail-field">
          <span className="melding-detail-label">Beschrijving</span>
          <p className="melding-detail-beschrijving">{melding.beschrijving || 'Geen beschrijving'}</p>
        </div>

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
