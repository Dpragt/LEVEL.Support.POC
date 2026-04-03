import { useState } from 'react'

interface OplossingFormProps {
  onSubmit: (beschrijving: string, bron: string) => void
  onCancel: () => void
}

const bronOpties = ['Handmatig', 'AI-suggestie']

function OplossingForm({ onSubmit, onCancel }: OplossingFormProps) {
  const [beschrijving, setBeschrijving] = useState('')
  const [bron, setBron] = useState('Handmatig')

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    onSubmit(beschrijving, bron)
  }

  return (
    <form className="oplossing-form" onSubmit={handleSubmit}>
      <div className="form-field">
        <label htmlFor="oplossing-beschrijving">Oplossing *</label>
        <textarea
          id="oplossing-beschrijving"
          value={beschrijving}
          onChange={(e) => setBeschrijving(e.target.value)}
          placeholder="Beschrijf de oplossing"
          rows={3}
          required
        />
      </div>
      <div className="form-row">
        <div className="form-field">
          <label htmlFor="oplossing-bron">Bron</label>
          <select id="oplossing-bron" value={bron} onChange={(e) => setBron(e.target.value)}>
            {bronOpties.map((b) => (
              <option key={b} value={b}>{b}</option>
            ))}
          </select>
        </div>
        <div className="form-actions" style={{ alignSelf: 'flex-end' }}>
          <button type="button" className="melding-btn melding-btn-cancel" onClick={onCancel}>
            Annuleren
          </button>
          <button type="submit" className="refresh-button" disabled={!beschrijving.trim()}>
            Toevoegen
          </button>
        </div>
      </div>
    </form>
  )
}

export default OplossingForm
