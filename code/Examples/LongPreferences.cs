using Sandbox.Audio;
using System.Text.Json.Serialization;

public enum Cycle
{
	Low,
	Normal,
	High
}

public class LongPreferences : EasySave<LongPreferences>
{
	public bool toggle { get; set; }

	public float slider { get; set; }

	public Cycle cycler {  get; set; }

	protected override UITab OnBuildUI()
	{
		var tab = new UITab("Long", 3);

		var groupToggles = tab.AddGroup("Test Toggle");

		groupToggles.AddToggle("Toggle", () => toggle, (value) => toggle = value);

		var groupVolumes = tab.AddGroup("Test Slider");

		groupVolumes.AddSlider("Slider", () => slider, (value) => slider = value);

		var groupGamePlay = tab.AddGroup("Test Cycler");

		groupGamePlay.AddCycler("Cycler", () => cycler, (value) => cycler = value);

		var groupMisc = tab.AddGroup("Misc");

		groupMisc.AddToggle("Toggle", () => toggle, (value) => toggle = value);
		groupMisc.AddSlider("Slider", () => slider, (value) => slider = value);
		groupMisc.AddCycler("Cycler", () => cycler, (value) => cycler = value);
		groupMisc.AddToggle("Toggle 2", () => toggle, (value) => toggle = value);
		groupMisc.AddSlider("Slider 2", () => slider, (value) => slider = value);
		groupMisc.AddCycler("Cycler 2", () => cycler, (value) => cycler = value);

		return tab;
	}
}
