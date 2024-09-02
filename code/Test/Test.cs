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
		//var tab = new UITab("A");		
		
	}

	[Button("Test Toggle Music")]
	void Button_ToggleMusic()
	{
		AudioPreferences.instance.ToggleMusic();
	}

	public void SaveExample()
	{
		GameSave.instance.hasMetTerry = true;
		GameSave.instance.Save();
	}
}