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
		public Exporter(string _exportPath = null) {
			if (_exportPath == null) {
				m_exportPath = Path.Combine(Directory.GetCurrentDirectory(), "Output");
			} else {
				m_exportPath = _exportPath;
			}
			m_textBuilder = new TextBuilder();
		}

		public bool Export(Project _project, SpriteExporter _spriteExporter) {
			bool success = false;
			try {
				if (!Directory.Exists(m_exportPath)) {
					Directory.CreateDirectory(m_exportPath);
				}
				//Text Files


				//Gib den SpriteImages einen unique Key und füge diesen KEy beim Export für jede Benutzung einem Dictionary zu
				//Wenn Das Sprite scho einmal benutzt wurde, wird es trotzdem nurt einmal vermerkt.

				//ImageImports
				using (StreamWriter writer = new StreamWriter(Path.Combine(m_exportPath, "ImageImports.txt"), false)) {
					for (int i = 0; i < _spriteExporter.SpriteCount; ++i) {
						writer.WriteLine(Path.GetFileName(_spriteExporter.Sprites[i].Image));
					}
					writer.Close();
				}

				//(Button)Sprite Imports
				using (StreamWriter writer = new StreamWriter(Path.Combine(m_exportPath, "SpriteImports.txt"), false)) {
					for (int i = 0; i < _spriteExporter.ButtonSpriteCount; ++i) {
						writer.WriteLine(Path.GetFileName(_spriteExporter.ButtonSprites[i].Image));
					}
					writer.Close();
				}

				//Storyboard
				using (IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(Path.Combine(m_exportPath, "Storyboard.txt"), false))) {
					m_textBuilder.Export(writer, _project, _spriteExporter);
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
					writer.WriteLine($"FontSize: {fmvm.FontSize};");
					writer.Close();
				}

				//Menu Files

				//Copy Files
				string filename;
				string destFile;
				//Image Files
				for (int i = 0; i < _spriteExporter.SpriteCount; ++i) {

					filename = Path.GetFileName(_spriteExporter.Sprites[i].Image);
					destFile = Path.Combine(m_exportPath, filename);
					File.Copy(_spriteExporter.Sprites[i].Image, destFile, true);
				}
				//Button Image Files
				for (int i = 0; i < _spriteExporter.ButtonSpriteCount; ++i) {

					filename = Path.GetFileName(_spriteExporter.ButtonSprites[i].Image);
					destFile = Path.Combine(m_exportPath, filename);
					File.Copy(_spriteExporter.ButtonSprites[i].Image, destFile, true);
				}
				//TTF Font Files
				string path = Path.Combine(_project.FontManagerViewModel.CurrentUsedFont.Font.BaseUri.OriginalString, _project.FontManagerViewModel.CurrentUsedFont.Font.Source+".TTF");
				filename = _project.FontManagerViewModel.CurrentUsedFont.Font.Source;
				destFile = Path.Combine(m_exportPath, filename + ".ttf");
				File.Copy(path, destFile, true);

				//Options Datei
				return success = true;
			} catch (Exception ex) {
				MessageBox.Show("Export failed! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return success;
			}
		}
	}
}
