using Sandbox.Audio;
using System;

public enum VOIPMode
{
	PushToTalk,
	Open
}

public class AudioPreferences : EasySave<AudioPreferences>
{
	public bool muteMusic { get; set; }

	public float gameVolume { get; set; } = 1.0f;
	public float musicVolume { get; set; } = 0.35f;
	public float uiVolume { get; set; } = 0.8f;

	public VOIPMode voipMode { get; set; } = VOIPMode.PushToTalk;
	public int voipIndex { get; set; } = 0;

	public void ApplyVolumesToMixers()
	{
		var mixerGame = Mixer.FindMixerByName("Game");
		var mixerMusic = Mixer.FindMixerByName("Music");
		var mixerUI = Mixer.FindMixerByName("UI");

		//Log.Info($"mixerGame: {mixerGame}, mixerMusic: {mixerMusic}, mixerUI: {mixerUI}");
		if (mixerGame != null)
		{
			mixerGame.Volume = gameVolume;
		}
		if (mixerMusic != null)
		{
			mixerMusic.Volume = muteMusic ? 0.0f : musicVolume;
		}
		if (mixerUI != null)
		{
			mixerUI.Volume = uiVolume;
		}
	}

	public void MuteMusic(bool mute)
	{
		muteMusic = mute;
		ApplyVolumesToMixers();
		Save();
	}

	public void ToggleMusic()
	{
		muteMusic = !muteMusic;
		ApplyVolumesToMixers();
		Save();
	}

	protected override UITab OnBuildUI()
	{
		// Tab Name - UI
		var tab = new UITab("Audio", 2);

		// Add Group Toggles
		var groupToggles = tab.AddGroup("Toggles");

		// Add Toggle 'Mute Music'
		groupToggles.AddToggle("Mute Music", () => muteMusic, (value) => muteMusic = value);
		groupToggles.AddToggle("Mute Music", () => muteMusic, (value) => muteMusic = value);
		groupToggles.AddToggle("Mute Music", () => muteMusic, (value) => muteMusic = value);
		groupToggles.AddToggle("Mute Music", () => muteMusic, (value) => muteMusic = value);

		// Add Group Volumes
		var groupVolumes = tab.AddGroup("Volumes");

		// Add Slider 'Game Volume'
		groupVolumes.AddSlider("Game Volume", () => gameVolume, (value) => gameVolume = value);
		// Add Slider 'Music Volume'
		groupVolumes.AddSlider("Music Volume", () => musicVolume, (value) => musicVolume = value);
		// Add Slider 'UI Volume'
		groupVolumes.AddSlider("UI Volume", () => uiVolume, (value) => uiVolume = value);

		// Add Group VOIP
		var groupVOIP = tab.AddGroup("VOIP");

		// Add Drop Down Selection 'VOIP'
		groupVOIP.AddSlider("UI Volume", () => uiVolume, (value) => uiVolume = value);
		groupVOIP.AddDropDown("VOIP Mode", () => voipMode, (value) => voipMode = value);
		groupVOIP.AddToggle("Mute Music", () => muteMusic, (value) => muteMusic = value);

		/*List<string> voipOptions = new List<string>();
		voipOptions.Add("Push To Talk");
		voipOptions.Add("Open");
		groupVolumes.AddDropDown("VOIP Mode", voipOptions, () => voipIndex, (value) => voipIndex = value);*/

		return tab;
	}
}