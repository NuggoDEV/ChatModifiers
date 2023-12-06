using CatCore;
using CatCore.Models.Twitch.IRC;
using CatCore.Services.Twitch.Interfaces;
using ChatModifiers.API;
using System;
using System.Collections.Generic;

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

            /* Will later need additional checks */

            foreach (CustomModifier modifier in RegistrationManager._registeredModifiers)
            {
                if (chatMessage.StartsWith($"!{modifier.CommandKeyword.ToLower()}"))
                {
                    Plugin.Log.Info($"Executing Modifier: {modifier.Name}");

                    string[] commandParts = chatMessage.Split(' ');
                    List<object> arguments = new List<object>();

                    for (int i = 1; i < commandParts.Length; i++)
                    {
                        if (i - 1 < modifier.Arguments.Length)
                        {
                            string argString = commandParts[i];
                            Type argType = modifier.Arguments[i - 1].Type;
                            object argValue = Convert.ChangeType(argString, argType);
                            arguments.Add(argValue);
                        }
                    }

                    modifier.Function.Invoke(new MessageInfo(chatMessage, message.Sender.UserName, message.Channel.Name, DateTime.Now), arguments.ToArray());
                    break;
                }
            }
        }
    }
}
