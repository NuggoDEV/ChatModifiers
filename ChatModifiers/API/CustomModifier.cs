using CatCore.Models.Twitch.IRC;
using System;
using System.Collections.Generic;

namespace ChatModifiers.API
{
    public class ArgumentInfo
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public ArgumentInfo(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }

    public class CustomModifier
    {
        public string Name { get; set; } = "Default";
        public string CommandKeyword { get; set; } = "default";
        public Action<TwitchMessage, object[]> Function { get; set; }
        public ArgumentInfo[] Arguments { get; set; }

        public CustomModifier(string name, string commandKeyword, Action<TwitchMessage, object[]> function, ArgumentInfo[] arguments)
        {
            Name = name;
            CommandKeyword = commandKeyword;
            Function = function;
            Arguments = arguments;
        }
    }
}