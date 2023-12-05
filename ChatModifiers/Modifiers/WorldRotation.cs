using ChatModifiers.API;
using CatCore.Models.Twitch.IRC;

namespace ChatModifiers.Modifiers
{
    internal class WorldRotation
    {
        internal static CustomModifier modifier = new CustomModifier("World Rotation", "rotate", (TwitchMessage message, string chatMessage) => Rotate(message, chatMessage));
        
        public static void Rotate(TwitchMessage message, string chatMessage)
        {
            string[] splitChatMessage = chatMessage.Split(' ');

            switch (splitChatMessage[1].ToLower())
            {
                case "left":
                    Plugin.Log.Info("Rotating Left!");
                    break;
                case "right":
                    Plugin.Log.Info("Rotating Right!");
                    break;
                case "start":
                    Plugin.Log.Info("Rotating To Start!");
                    break;
                case "stop":
                    Plugin.Log.Info("Stopping Rotation!");
                    break;
            }
        }

        
    }
}
