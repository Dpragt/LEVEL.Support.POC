import { useState, useEffect } from 'react'
import type { Melding, MeldingInput } from '../types/Melding'

interface MeldingFormProps {
  melding?: Melding | null
  onSubmit: (input: MeldingInput) => void
  onCancel: () => void
}

const categorieOpties = ['Bug', 'Feature', 'Vraag', 'Overig']
const prioriteitOpties = ['Laag', 'Normaal', 'Hoog', 'Kritiek']
const applicatieOpties = ['Taxatie', 'Object', 'Subject', 'Kadastraal', 'Woz', 'Belasting', 'Aanslag']

function MeldingForm({ melding, onSubmit, onCancel }: MeldingFormProps) {
  const [titel, setTitel] = useState('')
  const [beschrijving, setBeschrijving] = useState('')
  const [applicatie, setApplicatie] = useState<string>('')
  const [categorie, setCategorie] = useState<string>('')
  const [prioriteit, setPrioriteit] = useState<string>('Normaal')
  const [isAfgehandeld, setIsAfgehandeld] = useState(false)

  useEffect(() => {
    if (melding) {
      setTitel(melding.titel)
      setBeschrijving(melding.beschrijving)
      setApplicatie(melding.applicatie ?? '')
      setCategorie(melding.categorie ?? '')
      setPrioriteit(melding.prioriteit ?? 'Normaal')
      setIsAfgehandeld(melding.isAfgehandeld)
    }
  }, [melding])

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    onSubmit({
      titel,
      beschrijving,
      applicatie: applicatie || null,
      categorie: categorie || null,
      prioriteit: prioriteit || null,
      isAfgehandeld,
    })
  }

  const isEdit = !!melding

  return (
    <div className="melding-form-overlay">
      <form className="melding-form card" onSubmit={handleSubmit}>
        <h2 className="section-title">{isEdit ? 'Melding bewerken' : 'Nieuwe melding'}</h2>

        <div className="form-field">
          <label htmlFor="titel">Titel *</label>
          <input
            id="titel"
            type="text"
            value={titel}
            onChange={(e) => setTitel(e.target.value)}
            required
            placeholder="Geef een titel op"
          />
        </div>

        <div className="form-field">
          <label htmlFor="applicatie">Applicatie</label>
          <select id="applicatie" value={applicatie} onChange={(e) => setApplicatie(e.target.value)}>
            <option value="">— Geen —</option>
            {applicatieOpties.map((a) => (
              <option key={a} value={a}>{a}</option>
            ))}
          </select>
        </div>

        <div className="form-field">
          <label htmlFor="beschrijving">Beschrijving</label>
          <textarea
            id="beschrijving"
            value={beschrijving}
            onChange={(e) => setBeschrijving(e.target.value)}
            placeholder="Beschrijf de melding"
            rows={4}
          />
        </div>

        <div className="form-row">
          <div className="form-field">
            <label htmlFor="categorie">Categorie</label>
            <select id="categorie" value={categorie} onChange={(e) => setCategorie(e.target.value)}>
              <option value="">— Geen —</option>
              {categorieOpties.map((c) => (
                <option key={c} value={c}>{c}</option>
              ))}
            </select>
          </div>

          <div className="form-field">
            <label htmlFor="prioriteit">Prioriteit</label>
            <select id="prioriteit" value={prioriteit} onChange={(e) => setPrioriteit(e.target.value)}>
              {prioriteitOpties.map((p) => (
                <option key={p} value={p}>{p}</option>
              ))}
            </select>
          </div>
        </div>

        {isEdit && (
          <div className="form-field form-checkbox">
            <label>
              <input
                type="checkbox"
                checked={isAfgehandeld}
                onChange={(e) => setIsAfgehandeld(e.target.checked)}
              />
              Afgehandeld
            </label>
          </div>
        )}

        <div className="form-actions">
          <button type="button" className="melding-btn melding-btn-cancel" onClick={onCancel}>
            Annuleren
          </button>
          <button type="submit" className="refresh-button" disabled={!titel.trim()}>
            {isEdit ? 'Opslaan' : 'Aanmaken'}
          </button>
        </div>
      </form>
    </div>
  )
}

export default MeldingForm
