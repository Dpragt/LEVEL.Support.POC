# LEVEL.Support.POC

> **Proof of Concept** — AI-gestuurd support meldingensysteem voor lokale belasting- en waarderingsapplicaties.

## 🎯 Wat doet dit project?

Dit project is een POC voor een intelligent support systeem dat binnenkomende meldingen automatisch verwerkt met behulp van AI. Denk aan: een gebruiker maakt een melding aan ("WOZ-waarde klopt niet") en het systeem bepaalt zelfstandig de categorie, prioriteit, applicatie en samenvatting — en herkent of er al vergelijkbare meldingen bestaan.

### De kern: AI Agents + Orchestrator

Wanneer een nieuwe melding binnenkomt, doorloopt deze het volgende pad:

```
Gebruiker maakt melding aan
        ↓
  MeldingOrchestrator
   ├── 1. ClassificationAgent    → bepaalt categorie, prioriteit, applicatie & samenvatting
   ├── 2. DuplicateDetectionAgent → herkent mogelijke duplicaten
   ├── 3. Opslaan in database
   └── 4. Koppelen van duplicaten
        ↓
  Verrijkte melding terug naar gebruiker
```

### Agents

| Agent | Wat doet het? |
|---|---|
| **ClassificationAgent** | Classificeert meldingen op basis van titel en beschrijving. Bepaalt de **applicatie** (Taxatie, Object, Woz, Belasting, etc.), **categorie** (Bug, Feature, Vraag, Overig), **prioriteit** (Laag t/m Kritiek) en een korte **samenvatting**. |
| **DuplicateDetectionAgent** | Vergelijkt nieuwe meldingen met bestaande meldingen en identificeert mogelijke duplicaten inclusief motivering. |

### Services

| Service | Wat doet het? |
|---|---|
| **RetrievalService** | Haalt kandidaat-meldingen op uit de database voor de duplicate detectie agent. |

## 🏗️ Projectstructuur

De solution bestaat uit de volgende projecten:

| Project | Beschrijving |
|---|---|
| **LEVEL.Support.POC.Server** | ASP.NET Core Minimal API backend (.NET 10) met AI agents, orchestrator en REST endpoints. |
| **frontend** | React/TypeScript frontend voor het beheren van meldingen. |
| **LEVEL.Support.POC.AppHost** | .NET Aspire App Host voor het orkestreren van de volledige applicatie. |
| **LEVEL.Support.POC.MCP.Classifcation** | MCP Server voor classificatie (⚠️ *work in progress*, zie onder). |

## ⚙️ Technologie

- **.NET 10** met C# 14
- **ASP.NET Core Minimal API** — REST endpoints
- **.NET Aspire** — Orkestratie en service defaults
- **Microsoft.Extensions.AI** — AI abstractielaag met OpenAI
- **Entity Framework Core** — In-memory database (POC)
- **React + TypeScript** — Frontend (Vite)

## 🚀 Opstarten

### Vereisten

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) (voor de frontend)
- Een OpenAI API key

### Configuratie

Stel de OpenAI API key in via user secrets:

```bash
cd LEVEL.Support.POC.Server
dotnet user-secrets set "OpenAI:ApiKey" "<jouw-api-key>"
```

Optioneel kan je het model instellen (standaard `gpt-4o-mini`):

```bash
dotnet user-secrets set "OpenAI:Model" "gpt-4o-mini"
```

### Draaien via Aspire

```bash
cd LEVEL.Support.POC.AppHost
dotnet run
```

Dit start zowel de backend als de frontend.

## 🔮 MCP Server (Work in Progress)

> ⚠️ **Let op:** het project `LEVEL.Support.POC.MCP.Classifcation` is nog in ontwikkeling en bevat momenteel alleen placeholder-tooling (random number generator). De intentie is om de classificatie-functionaliteit als [MCP (Model Context Protocol)](https://modelcontextprotocol.io/) server beschikbaar te maken, zodat AI-assistenten zoals GitHub Copilot de tools direct kunnen aanroepen.

### Geplande MCP-tools

- **Classificatie** — Meldingen classificeren op categorie, prioriteit en applicatie
- **Duplicate detectie** — Controleren op bestaande vergelijkbare meldingen

### Lokaal testen (huidige staat)

Configureer de MCP server in VS Code (`.vscode/mcp.json`) of Visual Studio (`.mcp.json`):

```json
{
  "servers": {
    "LEVEL.Support.POC.MCP.Classifcation": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "LEVEL.Support.POC.MCP.Classifcation"
      ]
    }
  }
}
```

Meer informatie over MCP:

- [MCP Specificatie](https://spec.modelcontextprotocol.io/)
- [MCP C# SDK](https://modelcontextprotocol.github.io/csharp-sdk)
- [MCP servers in Visual Studio](https://learn.microsoft.com/visualstudio/ide/mcp-servers)
- [MCP servers in VS Code](https://code.visualstudio.com/docs/copilot/chat/mcp-servers)
