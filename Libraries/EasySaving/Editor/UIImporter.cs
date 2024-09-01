global using Editor;
global using System.Collections.Generic;
global using System.Linq;
global using Sandbox;
using System;
using System.IO;
using Sandbox.Internal;

public static class UIImporter
{
	[Event("localaddons.changed")]
	public static void OnLibrarysChanged()
	{
		Log.Info($"OnLibrarysChanged()");
		if (ProjectCookie == null)
		{
			return;
		}
		var hasOfferedLibraryImport = ProjectCookie.Get("easysaving.hasOfferedLibraryImport", false);
		if (hasOfferedLibraryImport)
		{
			ProjectCookie.Set("easysaving.hasOfferedLibraryImport", true);
			OpenMyMenu();
		}
	}

	public static void OpenMyMenu()
	{
		var confirm = new PopupWindow(
			"Import UI from Library?",
			"UI doesn't work in libraries, so if you want the options screens that comes with this library you need to import it into your project",
			"No",
			new Dictionary<string, Action>()
			{
				{ "Yes", () => ImportDisabledLibraryFiles() }
			}
		);

		confirm.Show();
	}

	[Menu("Editor", "Library/Easy Saving/Import Disabled Library Files")]
	public static void ImportDisabledLibraryFiles()
	{
		string libraryFolder = new System.IO.DirectoryInfo(Editor.FileSystem.Content.GetFullPath("/")).FullName.Replace("Assets", "");
		string projectFolder = new System.IO.DirectoryInfo(Editor.FileSystem.ProjectSettings.GetFullPath("/")).FullName.Replace("ProjectSettings", "");

		string source = $"{libraryFolder}code";
		string target = $"{projectFolder}code";

		string[] files = Directory.GetFiles(source, "*.DISABLED", SearchOption.AllDirectories);

		Log.Info($"Moving .DISABLED files from source: {source}");

		foreach (string file in files)
		{
			string relativePath = Path.GetRelativePath(source, file);
			string targetPath = Path.Combine(target, relativePath);

			targetPath = RemoveDisabledExtension(targetPath);

			Directory.CreateDirectory(Path.GetDirectoryName(targetPath));

			if (File.Exists(targetPath))
			{
				if ((File.GetAttributes(targetPath) & FileAttributes.ReadOnly) != 0)
				{
					File.SetAttributes(targetPath, FileAttributes.Normal);
				}
				File.Delete(targetPath);
			}
			File.Copy(file, targetPath);

			Log.Info($"Moved: {file} -> {targetPath}");
		}

		Log.Info("All files have been moved successfully.");
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