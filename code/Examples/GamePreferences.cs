using Sandbox.Audio;
using System.Text.Json.Serialization;

public enum Difficulty
{
	Easy,
	Normal,
	Hard
}

public class GamePreferences : EasySave<GamePreferences>
{
	public bool restartLevelOnFail { get; set; } = false;
	public bool restartLevelOnCivKill { get; set; } = false;
	public bool useOriginalClothing { get; set; } = false;

	[JsonPropertyName("difficulty")]
	public Difficulty difficulty { get; set; }
	//public Difficulty difficulty { get; set; } = Difficulty.Normal;

	protected override void SetDefaultValues()
	{
		difficulty = Difficulty.Normal;
	}

	public void UseOriginalClothing(bool isUsing)
	{
		useOriginalClothing = isUsing;
		Save();
	}

	protected override UITab OnBuildUI()
	{
		var tab = new UITab("Game", 1);

		var groupToggles = tab.AddGroup("Restarts");

		groupToggles.AddToggle("Restart Level On Fail", () => restartLevelOnFail, (value) => restartLevelOnFail = value);
		groupToggles.AddToggle("Restart Level On Civilian Kill", () => restartLevelOnCivKill, (value) => restartLevelOnCivKill = value);

		var groupVolumes = tab.AddGroup("Clothing");

		groupVolumes.AddToggle("Use Original Clothing", () => useOriginalClothing, (value) => useOriginalClothing = value);

		var groupGamePlay = tab.AddGroup("Gameplay");

		groupGamePlay.AddDropDown("Difficulty", () => difficulty, (value) => difficulty = value);

		return tab;
	}
}
