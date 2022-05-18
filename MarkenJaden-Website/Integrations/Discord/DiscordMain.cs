using Discord;
using Discord.WebSocket;

namespace MarkenJaden_Website.Integrations.Discord
{
    public class DiscordMain
    {
        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            _client = new();

            _client.Log += Log;

            var token = await File.ReadAllLinesAsync("Sensitive-data");

            await _client.LoginAsync(TokenType.Bot, token[0]);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }        
    }
}
