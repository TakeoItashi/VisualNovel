using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.Models;

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

				//ImageImports
				using (StreamWriter writer = new StreamWriter(Path.Combine(m_exportPath, "ImageImports.txt"), false)) {
					for (int i = 0; i < _spriteExporter.SpriteCount; ++i) {
						writer.WriteLine(_spriteExporter.Sprites[i].Image);
					}
					writer.Close();
				}

				//(Button)Sprite Imports
				using (StreamWriter writer = new StreamWriter(Path.Combine(m_exportPath, "SpriteImports.txt"), false)) {
					for (int i = 0; i < _spriteExporter.ButtonSpriteCount; ++i) {
						writer.WriteLine(_spriteExporter.ButtonSprites[i].Image);
					}
					writer.Close();
				}

				//Storyboard
				using (IndentedTextWriter writer = new IndentedTextWriter(new StreamWriter(Path.Combine(m_exportPath, "Storyboard.txt"), false))) {
					m_textBuilder.Export(writer, _project, _spriteExporter);
					writer.Close();
				}

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
				string path = Path.Combine(_project.FontManagerViewModel.CurrentUsedFont.BaseUri.AbsolutePath, _project.FontManagerViewModel.CurrentUsedFont.Source);
				filename = _project.FontManagerViewModel.CurrentUsedFont.Source;
				destFile = Path.Combine(m_exportPath, filename);
				File.Copy(path, destFile, true);
				return success = true;
			} catch (Exception ex) {
				return success;
			}
		}
	}
}
