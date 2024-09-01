using System;
using System.IO;

public static class Backporter
{
	[Menu("Editor", "Library Project/Backport files to library")]
	public static void ImportUIFiles()
	{
		string libraryFolder = new System.IO.DirectoryInfo(Editor.FileSystem.Content.GetFullPath("/")).FullName.Replace("Assets", "");
		string projectFolder = new System.IO.DirectoryInfo(Editor.FileSystem.ProjectSettings.GetFullPath("/")).FullName.Replace("ProjectSettings", "");

		string source = @$"{projectFolder}Libraries\EasySaving\Code";
		string target = $"{projectFolder}code";

		string[] files = Directory.GetFiles(source, "*.DISABLED", SearchOption.AllDirectories);

		Log.Info($"Moving .DISABLED files to source: {source}");

		foreach (string file in files)
		{
			var filePath = file;
			string relativePath = Path.GetRelativePath(source, file);
			string targetPath = Path.Combine(target, relativePath);

			targetPath = RemoveDisabledExtension(targetPath);

			Directory.CreateDirectory(Path.GetDirectoryName(targetPath));

			if (File.Exists(filePath))
			{
				if ((File.GetAttributes(filePath) & FileAttributes.ReadOnly) != 0)
				{
					File.SetAttributes(filePath, FileAttributes.Normal);
				}
				File.Delete(filePath);
			}
			File.Copy(targetPath, filePath);

			Log.Info($"Moved: {targetPath} -> {filePath}");
		}

		Log.Info("All files have been backported successfully.");
	}

	static string RemoveDisabledExtension(string filePath)
	{
		if (filePath.EndsWith(".DISABLED", StringComparison.OrdinalIgnoreCase))
		{
			return filePath.Substring(0, filePath.Length - ".DISABLED".Length);
		}
		return filePath;
	}
}