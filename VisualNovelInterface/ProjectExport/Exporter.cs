using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualNovelInterface.Models;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.ProjectExport
{
	public class Exporter
	{
		private string m_exportPath;
		private TextBuilder m_textBuilder;
		private string m_gameName;

		public Exporter(string _exportPath = null, string _gameName = null) {
			if (_exportPath == null) {
				m_exportPath = Path.Combine(Directory.GetCurrentDirectory(), "Output");
			} else {
				m_exportPath = _exportPath;
			}
			m_gameName = _gameName + ".exe";
			m_textBuilder = new TextBuilder();
		}

		public bool Export(Project _project, SpriteExporter _spriteExporter, SpriteImage _mainMenuBGImage) {
			bool success = false;
			try {
				if (!Directory.Exists(m_exportPath)) {
					Directory.CreateDirectory(m_exportPath);
				}
				//Text Files


				//Gib den SpriteImages einen unique Key und füge diesen Key beim Export für jede Benutzung einem Dictionary zu
				//Wenn Das Sprite scho einmal benutzt wurde, wird es trotzdem nurt einmal vermerkt.

				//ImageImports
				using (StreamWriter writer = new StreamWriter(Path.Combine(m_exportPath, "ImageImports.txt"), false)) {
					for (int i = 0; i < _spriteExporter.SpriteCount; ++i) {
						writer.WriteLine(Path.GetFileName(_spriteExporter.SpritesArray[i].Image));
					}
					writer.Close();
				}

				//(Button)Sprite Imports
				using (StreamWriter writer = new StreamWriter(Path.Combine(m_exportPath, "SpriteImports.txt"), false)) {
					for (int i = 0; i < _spriteExporter.ButtonSpriteCount; ++i) {
						writer.WriteLine(Path.GetFileName(_spriteExporter.ButtonSpritesArray[i].Image));
					}
					writer.Close();
				}

				//Storyboard
				using (IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(Path.Combine(m_exportPath, "Storyboard.txt"), false))) {
					m_textBuilder.ExportStory(writer, _project, _spriteExporter);
					writer.Close();
				}

				//Options Datei
				using (StreamWriter writer = new StreamWriter(Path.Combine(m_exportPath, "options.txt"), false)) {
					SettingsViewModel svm = _project.ProjectSettingsViewModel;
					FontManagerViewModel fmvm = _project.FontManagerViewModel;
					writer.WriteLine($"WindowWidth: {svm.WindowWidth};");
					writer.WriteLine($"WindowHeight: {svm.WindowHeight};");
					writer.WriteLine($"TextBoxRed: {svm.TextBoxRed};");
					writer.WriteLine($"TextBoxGreen: {svm.TextBoxGreen};");
					writer.WriteLine($"TextBoxBlue: {svm.TextBoxBlue};");
					writer.WriteLine($"TextBoxAlpha: {svm.TextBoxAlpha};");
					writer.WriteLine($"Font: {fmvm.CurrentUsedFont.Font.Source}.ttf;");
					writer.Write($"FontSize: {fmvm.FontSize};"); //Last entry has to not contain new Line or Game loading will fail
					writer.Close();
				}

				//Menu Files
				using (IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(Path.Combine(m_exportPath, "MainMenu.txt"), false))) {
					m_textBuilder.ExportMainMenu(writer, _mainMenuBGImage, _spriteExporter);
					writer.Close();
				}

				//SaveMenu
				using (IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(Path.Combine(m_exportPath, "SaveMenu.txt"), false))) {

					var base64EncodedBytes = Convert.FromBase64String("TWVudSB7CgoJTmFtZTogIlNhdmUgTWVudSI7CgoJQkdJbmRleDogMgoKCUl0ZW1zIHsKCQlCdXR0b24gewoJCQlUZXh0OiAiR28gQmFjayI7CgkJCUJ1dHRvbjogNjAwLCA1MDAsIDEwMCwgMjAwLCAwOwoJCQlUeXBlOiAxMTsKCQl9CgkJQnV0dG9uIHsKCQkJVGV4dDogIlRlbXAiOwoJCQlCdXR0b246IDUwLCAxNzAsIDUwLCAxMDAsIDA7CgkJCVR5cGU6IDc7CgkJfQoJCUJ1dHRvbiB7CgkJCVRleHQ6ICJUZW1wIjsKCQkJQnV0dG9uOiA1MCwgMjMwLCA1MCwgMTAwLCAwOwoJCQlUeXBlOiA3OwoJCX0KCQlCdXR0b24gewoJCQlUZXh0OiAiVGVtcCI7CgkJCUJ1dHRvbjogNTAsIDI5MCwgNTAsIDEwMCwgMDsKCQkJVHlwZTogNzsKCQl9CgoJCUJ1dHRvbiB7CgkJCVRleHQ6ICJUZW1wIjsKCQkJQnV0dG9uOiAyMDAsIDE3MCwgNTAsIDEwMCwgMDsKCQkJVHlwZTogNzsKCQl9CgkJQnV0dG9uIHsKCQkJVGV4dDogIlRlbXAiOwoJCQlCdXR0b246IDIwMCwgMjMwLCA1MCwgMTAwLCAwOwoJCQlUeXBlOiA3OwoJCX0KCQlCdXR0b24gewoJCQlUZXh0OiAiVGVtcCI7CgkJCUJ1dHRvbjogMjAwLCAyOTAsIDUwLCAxMDAsIDA7CgkJCVR5cGU6IDc7CgkJfQoJfQp9");
					string exportString = Encoding.UTF8.GetString(base64EncodedBytes);
					writer.Write(exportString);
					writer.Close();
				}
				//LoadMenu
				using (IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(Path.Combine(m_exportPath, "LoadMenu.txt"), false))) {
					var base64EncodedBytes = Convert.FromBase64String("TWVudSB7CgoJTmFtZTogIkxvYWQgTWVudSI7CgoJQkdJbmRleDogMgoKCUl0ZW1zIHsKCQlCdXR0b24gewoJCQlUZXh0OiAiR28gQmFjayI7CgkJCUJ1dHRvbjogNjAwLCA1MDAsIDEwMCwgMjAwLCAwOwoJCQlUeXBlOiAxMTsKCQl9CgkJQnV0dG9uIHsKCQkJVGV4dDogIlRlbXAiOwoJCQlCdXR0b246IDUwLCAxNzAsIDUwLCAxMDAsIDA7CgkJCVR5cGU6IDEyOwoJCX0KCQlCdXR0b24gewoJCQlUZXh0OiAiVGVtcCI7CgkJCUJ1dHRvbjogNTAsIDIzMCwgNTAsIDEwMCwgMDsKCQkJVHlwZTogMTI7CgkJfQoJCUJ1dHRvbiB7CgkJCVRleHQ6ICJUZW1wIjsKCQkJQnV0dG9uOiA1MCwgMjkwLCA1MCwgMTAwLCAwOwoJCQlUeXBlOiAxMjsKCQl9CgoJCUJ1dHRvbiB7CgkJCVRleHQ6ICJUZW1wIjsKCQkJQnV0dG9uOiAyMDAsIDE3MCwgNTAsIDEwMCwgMDsKCQkJVHlwZTogMTI7CgkJfQoJCUJ1dHRvbiB7CgkJCVRleHQ6ICJUZW1wIjsKCQkJQnV0dG9uOiAyMDAsIDIzMCwgNTAsIDEwMCwgMDsKCQkJVHlwZTogMTI7CgkJfQoJCUJ1dHRvbiB7CgkJCVRleHQ6ICJUZW1wIjsKCQkJQnV0dG9uOiAyMDAsIDI5MCwgNTAsIDEwMCwgMDsKCQkJVHlwZTogMTI7CgkJfQoJfQp9");
					string exportString = Encoding.UTF8.GetString(base64EncodedBytes);
					writer.Write(exportString);
					writer.Close();
				}
				//PauseMenu
				using (IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(Path.Combine(m_exportPath, "PauseMenu.txt"), false))) {
					var base64EncodedBytes = Convert.FromBase64String("TWVudSB7CgoJTmFtZTogIlBhdXNlIE1lbnUiOwoKCUJHSW5kZXg6IDIKCglJdGVtcyB7CgkJQnV0dG9uIHsKCQkJVGV4dDogIkJhY2sgdG8gR2FtZSI7CgkJCUJ1dHRvbjogNTAsIDUwLCAxMDAsIDIwMCwgMDsKCQkJVHlwZTogMTA7CgkJfQoJCUJ1dHRvbiB7CgkJCVRleHQ6ICJTYXZlIjsKCQkJQnV0dG9uOiA1MCwgMTEwLCA1MCwgMTAwLCAwOwoJCQlUeXBlOiAxNDsKCQl9CgkJQnV0dG9uIHsKCQkJVGV4dDogIkxvYWQiOwoJCQlCdXR0b246IDUwLCAxNzAsIDUwLCAxMDAsIDA7CgkJCVR5cGU6IDE1OwoJCX0KCQlCdXR0b24gewoJCQlUZXh0OiAiQmFjayB0byBNYWluIE1lbnUiOwoJCQlCdXR0b246IDUwLCAyMzAsIDUwLCAxMDAsIDA7CgkJCVR5cGU6IDEzOwoJCX0KCX0KfQ==");
					string exportString = Encoding.UTF8.GetString(base64EncodedBytes);
					writer.Write(exportString);
					writer.Close();
				}
				//Variables
				using (IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(Path.Combine(m_exportPath, "Variables.txt"), false))) {
					m_textBuilder.ExportVariables(writer, _project.VariableManagerViewModel);
					writer.Close();
				}

				//Copy Files
				string filename;
				string destFile;
				//Image Files
				for (int i = 0; i < _spriteExporter.SpriteCount; ++i) {

					filename = Path.GetFileName(_spriteExporter.SpritesArray[i].Image);
					destFile = Path.Combine(m_exportPath, filename);
					File.Copy(_spriteExporter.SpritesArray[i].Image, destFile, true);
				}
				//Button Image Files
				for (int i = 0; i < _spriteExporter.ButtonSpriteCount; ++i) {

					filename = Path.GetFileName(_spriteExporter.ButtonSpritesArray[i].Image);
					destFile = Path.Combine(m_exportPath, filename);
					File.Copy(_spriteExporter.ButtonSpritesArray[i].Image, destFile, true);
				}
				//TTF Font Files
				string path = _project.FontManagerViewModel.CurrentUsedFont.Font.BaseUri.OriginalString;
				filename = _project.FontManagerViewModel.CurrentUsedFont.Font.Source;
				destFile = Path.Combine(m_exportPath, filename + ".ttf");
				File.Copy(path, destFile, true);

				//bin Files
				path = Directory.GetCurrentDirectory() + @"\bin";
				DirectoryCopy(path, m_exportPath, false);

				return success = true;
			} catch (Exception ex) {
				MessageBox.Show("Export failed! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return success;
			}
		}

		//https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories?redirectedfrom=MSDN
		private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs) {
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists) {
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory(destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files) {
				try {
					string temppath;
					if (file.Extension != ".exe") {
						temppath = Path.Combine(destDirName, file.Name);
					} else {
						temppath = Path.Combine(destDirName, m_gameName);
					}
					file.CopyTo(temppath, true);
				} catch (Exception) {

				}
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs) {
				foreach (DirectoryInfo subdir in dirs) {
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}
	}
}