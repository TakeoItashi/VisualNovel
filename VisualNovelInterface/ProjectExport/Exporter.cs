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

				//options
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

				//Options Datei
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