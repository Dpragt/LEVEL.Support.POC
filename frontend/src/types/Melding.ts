export interface Oplossing {
  id: number
  meldingId: number
  beschrijving: string
  bron: string
  aangemaaktOp: string
}

export interface OplosssingInput {
  beschrijving: string
  bron: string
}

export interface GekoppeldeMeldingView {
  id: number
  meldingId: number
  gekoppeldeMeldingId: number
  reden: string | null
  aangemaaktOp: string
  gekoppeldeTitel: string
  gekoppeldeApplicatie: string | null
  gekoppeldeId: number
}

export interface Melding {
  id: number
  titel: string
  beschrijving: string
  samenvatting: string | null
  applicatie: string | null
  categorie: string | null
  prioriteit: string | null
  isAfgehandeld: boolean
  aangemaaktOp: string
  oplossingen: Oplossing[]
  gekoppeldeMeldingen: GekoppeldeMeldingView[]
}

export interface MeldingInput {
  titel: string
  beschrijving: string
  applicatie: string | null
  categorie: string | null
  prioriteit: string | null
  isAfgehandeld: boolean
}
