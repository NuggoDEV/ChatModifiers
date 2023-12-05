
using CatCore.Models.Twitch.IRC;
using System;

namespace ChatModifiers.API
{
    public class CustomModifier
    {
        public string Name { get; set; } = "Default";
        public string CommandKeyword { get; set; } = "default";
        public Action<TwitchMessage, string> Function { get; set; }

        public CustomModifier(string name, string commandKeyword, Action<TwitchMessage, string> function)
        {
            Name = name;
            CommandKeyword = commandKeyword;
            Function = function;
        }
    }
}
