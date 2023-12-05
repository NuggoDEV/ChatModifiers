
using CatCore.Models.Twitch.IRC;
using System;

namespace ChatModifiers.API
{
    public class CustomModifier
    {
        public string Name { get; set; } = "Default";
        public Action<TwitchMessage, string> Function { get; set; }

        public CustomModifier(string name, string commandKeyword, Action<TwitchMessage, string> function)
        {
            Name = name;
            Function = function;
        }
    }
}
