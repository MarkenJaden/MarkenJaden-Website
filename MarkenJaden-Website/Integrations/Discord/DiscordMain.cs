using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MarkenJaden_Website.Integrations.Discord
{
    public class DiscordMain
    {
        private DiscordSocketClient _client;
        public static string profilePictureUrl;

        public async Task MainAsync()
        {
            _client = new();

            _client.Log += Log;

            var token = await File.ReadAllLinesAsync("Sensitive-data");

            await using var services = ConfigureServices();

            await _client.LoginAsync(TokenType.Bot, token[0]);
            await _client.StartAsync();
            
            await _client.GetGuild(961373284555956254).DownloadUsersAsync();
            profilePictureUrl = _client.GetGuild(961373284555956254).GetUser(222733101770604545).GetDisplayAvatarUrl();

            await Task.Delay(Timeout.Infinite);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<HttpClient>()
                .BuildServiceProvider();
        }
    }
}
