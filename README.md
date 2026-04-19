# LEVEL.Support.POC

AI-gedreven supportmodule die binnenkomende meldingen automatisch classificeert (categorie, prioriteit, applicatie, samenvatting) en duplicaten detecteert.
Gebouwd met .NET 10, ASP.NET Core Minimal API, Microsoft.Extensions.AI (OpenAI), Entity Framework Core en een React/TypeScript frontend.
Georkestreerd via .NET Aspire.

---

## 🚀 How to run

### Vereisten

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) (voor de frontend)
- Een [OpenAI API key](https://platform.openai.com/api-keys)

### 1. OpenAI API key instellen

```json
{
  "OpenAI": {
    "Model": "gpt-4o-mini",
    "ApiKey": "<jouw-api-key>"
  }
}
```

Het model staat standaard op `gpt-4o-mini` in `appsettings.json`.
De ApiKey moet je wijzigen in `appsettings.json` of via user secrets:

```bash
cd LEVEL.Support.POC.Server
dotnet user-secrets set "OpenAI:ApiKey" "<jouw-api-key>"
```

### 2. Starten via Aspire

```bash
cd LEVEL.Support.POC.AppHost
dotnet run
```

De Aspire AppHost start automatisch de backend (`LEVEL.Support.POC.Server`) én de frontend (`frontend`). Open het Aspire dashboard om de endpoints te bekijken.
