﻿using HMUI;
using System;
using System.Collections.Generic;

namespace ChatModifiers.API
{
    public class CustomModifier
    {
        public string Name { get; set; } = "Default";
        public string Description { get; set; } = "Default Description";
        public string Author { get; set; } = "Default Author";
        public string PathToIcon { get; set; } = "Default Path";
        public string CommandKeyword { get; set; } = "default";
        public Action<MessageInfo, object[]> Function { get; set; } = null;
        public ArgumentInfo[] Arguments { get; set; } = null;
        public Areas ActiveAreas { get; set; } = Areas.None;
        public Dictionary<string, object> Settings { get; set; } = new Dictionary<string, object>();
        public ViewController SettingsViewController { get; set; } = null;
        public bool Register()
        {
            return RegistrationManager.RegisterModifier(this);
        }
        public bool Unregister()
        {
            return RegistrationManager.UnregisterModifier(this);
        }
        public CustomModifier(string name, string description, string author, string pathToIcon, string commandKeyword, Action<MessageInfo, object[]> function, ArgumentInfo[] arguments, Areas areas, Dictionary<string, object> settings, ViewController viewController = null)
        {
            Name = name;
            Description = description;
            Author = author;
            PathToIcon = pathToIcon;
            CommandKeyword = commandKeyword;
            Function = function;
            Arguments = arguments;
            ActiveAreas = areas;
            Settings = settings;
            SettingsViewController = viewController;
        }
    }
}