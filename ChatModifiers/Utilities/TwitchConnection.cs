using CatCore;
using CatCore.Models.Twitch.IRC;
using CatCore.Services.Twitch.Interfaces;

namespace ChatModifiers.Utilities
{
    internal class TwitchConnection
    {
        internal static void Initialize()
        {
            CatCoreInstance instance = CatCoreInstance.Create();
            ITwitchService service = instance.RunTwitchServices();

            service.OnTextMessageReceived += Service_OnTextMessageReceived;
        }

        private static void Service_OnTextMessageReceived(ITwitchService service, TwitchMessage message)
        {
            string chatMessage = message.Message.ToLower();

            if (!chatMessage.StartsWith("!"))
                return;
        }
    }
}
