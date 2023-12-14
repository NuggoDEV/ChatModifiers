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
      "ExamplePlugin.Images.Icon.png", // Assembly path to the icon, will be used in the gameplay setup menu and the settings menu,
      "example", // Command keyword, ChatModifiers will know to execute this modifiers function when the keyword is used
      ExampleFunction, // Action linking the ExampleFunction below
      new ChatModifiers.API.ArgumentInfo[] { // Arguments listed in this array will be positioned the same within the object[] args in the example function.
        new ChatModifiers.API.ArgumentInfo("example", typeof(int)) // This ArgumentInfo will activate when any integer is typed after "!example". Any Type should work with this.
      }, 

      Areas.Game, // The area that the command will execute in. Areas are as such: None = 0, Menu = 1, Game = 2, Both = 3
      new System.Collections.Generic.Dictionary<string, object> { { "TestKey", "TestValue" } } // The config, where the key is the name of the setting, and the value is the default value of the setting
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

### Additional Notes

To change your CustomModifier settings
(You will need to cast it to whatever Type you are using, this may require extra work)

``` customModifier ``` will just be your instance of CustomModifier
```cs
    [UIValue("TestSettingValue")]
    public string TestSettingValue
    {
        get
        {
            if (customModifier != null)
            {
                return customModifier.ModifierSettings.AdditionalSettings["KeyTest"].ToString();
            }
            return null;
        }
        set
        {
            if (customModifier != null)
            {
                customModifier.ModifierSettings.AdditionalSettings["KeyTest"] = (object)value;
                customModifier.SaveSettings();
            }
        }
    }
```

There is an extra optional field for ```ViewController``` in order to show your settings within a list of other CustomModifiers, simply define the ViewController and it will appear within the settings menu for ChatModifiers, FlowCoordinators will be handled for you.


<table>
  <tr>
    <td align="center">
      <img src="https://github.com/RileyPrince/ChatModifiers/assets/90689870/e8420369-8947-498e-9cb8-862810100766" alt="Image 1">
    </td>
    <td align="center">
      <img src="https://github.com/RileyPrince/ChatModifiers/assets/90689870/79cc32bf-a88d-42bf-9567-333582b36c7f" alt="Image 2">
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://github.com/RileyPrince/ChatModifiers/assets/90689870/c8acfa86-8128-42d7-9773-1de71d0f7e35" alt="Image 3">
    </td>
    <td align="center">
      <img src="https://github.com/RileyPrince/ChatModifiers/assets/90689870/8c59ae6d-3659-42f0-824d-f9786af2189e" alt="Image 4">
    </td>
  </tr>
</table>
