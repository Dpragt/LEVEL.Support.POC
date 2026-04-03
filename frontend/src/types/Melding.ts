export interface Melding {
  id: number
  titel: string
  beschrijving: string
  applicatie: string | null
  categorie: string | null
  prioriteit: string | null
  isAfgehandeld: boolean
  aangemaaktOp: string
}

export interface MeldingInput {
  titel: string
  beschrijving: string
  applicatie: string | null
  categorie: string | null
  prioriteit: string | null
  isAfgehandeld: boolean
}
