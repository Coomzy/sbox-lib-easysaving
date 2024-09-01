using Sandbox.Audio;
using Sandbox.UI;
using System;
using System.Reflection;

public class Test : Component
{
	[Button("Test")]
	void Button_Test()
	{
		foreach (var styleSheet in StyleSheet.Loaded)
		{
			Log.Info($"styleSheet: {styleSheet.FileName}");
		}
	}

	[Button("Test Audio")]
	void Button_Test_Audio()
	{
		var inst = Game.ActiveScene.Components.Get<OptionsScreen>(true);
		if (inst != null)
		{
			inst.Enabled = true;
		}
		else
		{
			var optionsScreenGO = Game.ActiveScene.CreateObject();
			var screenPanel = optionsScreenGO.Components.Create<ScreenPanel>();
			var optionsScreen = optionsScreenGO.Components.Create<OptionsScreen>();
		}
	}

	[Button("Test Toggle Music")]
	void Button_ToggleMusic()
	{
		AudioPreferences.instance.ToggleMusic();
	}
}