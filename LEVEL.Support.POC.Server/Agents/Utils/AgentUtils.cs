namespace LEVEL.Support.POC.Server.Agents.Utils;

public static class AgentUtils
{
    public static string SanitizeAgentResponseJson(string json)
    {

        // Strip markdown code fences als het model die toevoegt
        if (json.StartsWith("```"))
        {
            var startIndex = json.IndexOf('{');
            var endIndex = json.LastIndexOf('}');
            if (startIndex >= 0 && endIndex >= 0)
                json = json[startIndex..(endIndex + 1)];
        }

        return json;
    }
}