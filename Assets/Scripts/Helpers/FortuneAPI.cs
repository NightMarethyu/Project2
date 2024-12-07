using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class FortuneAPI : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GetFortune()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://fortune-cookie4.p.rapidapi.com/slack"),
            Headers =
            {
                { "x-rapidapi-key", "7e406e347amsh4e357ddd571bc7bp1bcf71jsn6eb0e86f66e1" },
                { "x-rapidapi-host", "fortune-cookie4.p.rapidapi.com" },
            },
        };

        try
        {
            Debug.Log("Sending API Request");
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
        catch (Exception ex)
        {
            Debug.LogError($"API request failed: {ex.Message}");
            return null;
        }
    }

    [Serializable]
    public class FortuneResponse
    {
        public string response_type;
        public string text;
    }

    public string ExtractFortune(string jsonResponse)
    {
        var fortune = JsonUtility.FromJson<FortuneResponse>(jsonResponse);
        return fortune?.text ?? "No fortune found.";
    }

    public string RemoveFortuneReads(string fortuneText)
    {
        string fortunePrefix = "🥠 y";
        return fortuneText.Replace(fortunePrefix, "Y");
    }
}
