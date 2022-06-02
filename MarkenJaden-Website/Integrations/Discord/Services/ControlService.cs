using Discord.WebSocket;

namespace MarkenJaden_Website.Integrations.Discord.Services
{
    public class ControlService
    {
        private static DiscordSocketClient _discord;
        private readonly IServiceProvider _services;
        public ControlService(IServiceProvider services)
        {
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _services = services;
        }

        public void Register(){}

        public static string GetProfilePicture()
        {
            return _discord.GetGuild(961373284555956254).SearchUsersAsync("MarkenJaden", 1).Result.First().GetDisplayAvatarUrl();
        }
    }
}
