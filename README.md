# ChatModifiers
> An interactive API to allow for Twitch Chat to interact with your game

[![License: GPLv3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://opensource.org/license/gpl-3-0/) 

----

## Installation
1) Download the [latest release](https://github.com/RileyPrince/ChatModifiers/releases/latest) for your game version
2) Locate your Beat Saber install folder
3) Extract the zip file to the install folder

----

## Integration

Simple integration of ChatModifiers
```cs
internal class ExampleModifier
{
    internal static ChatModifiers.API.CustomModifier Example = new ChatModifiers.API.CustomModifier(
      "Example Modifier", // Name
      "Does blah blah blah", // Description
      "ExamplePlugin.Images.Icon.png", // Assembly path to the icon, will be used in the gameplay setup menu,
      "example", // Command keyword, ChatModifiers will know to execute this modifiers function when the keyword is used
      ExampleFunction, // Action linking the ExampleFunction below
      new ChatModifiers.API.ArgumentInfo[] { // Arguments listed in this array will be positioned the same within the object[] args in the example function.
        new ChatModifiers.API.ArgumentInfo("example", typeof(int)) // This ArgumentInfo will activate when any integer is typed after "!example". Any Type should work with this.
      }, 

      Areas.Game, // The area that the command will execute in. Areas are as such: None = 0, Menu = 1, Game = 2, Both = 3
      new System.Collections.Generic.Dictionary<string, object> { { "Test", "1" } } // The config, yet to be completed.
    )

    private static void ExampleFunction(ChatModifiers.API.MessageInfo messageInfo, object[] args)
    {

    }
}

internal class Plugin
{
  // Base code for Plugin.cs

  [OnEnable]
  public void OnEnable() => ChatModifiers.API.RegistrationManager.RegisterModifier(ExampleModifier.Example);
}
```
