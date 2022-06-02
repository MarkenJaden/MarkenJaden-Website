using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MarkenJaden_Website.Integrations.Discord.Services;

namespace MarkenJaden_Website.Integrations.Discord
{
    public class DiscordMain
    {
        private DiscordSocketClient _client;

        public async Task MainAsync(WebApplication app)
        {
            var token = await File.ReadAllLinesAsync("Sensitive-data");

            await using var services = ConfigureServices();

            _client = services.GetRequiredService<DiscordSocketClient>();
            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, token[0]);
            await _client.StartAsync();

            services.GetRequiredService<ControlService>().Register();
            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected!");
                if (app.Environment.IsDevelopment()) app.RunAsync();
                else app.RunAsync("http://localhost:5104/");
                return Task.CompletedTask;
            };
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
                .AddSingleton<ControlService>()
                .AddSingleton<HttpClient>()
                .BuildServiceProvider();
        }
    }
}
