using Sandbox.Audio;
using System.Text.Json.Serialization;

public class GameSave : EasySave<GameSave>
{
	public bool hasMetTerry { get; set; }
	public int timesJumped { get; set; }
}
