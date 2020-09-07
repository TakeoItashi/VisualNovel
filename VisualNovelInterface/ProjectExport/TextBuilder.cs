using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using VisualNovelInterface.Models;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.ProjectExport
{
	public class TextBuilder
	{
		public TextBuilder() {

		}

		public void ExportStory(IndentedTextWriter _writer, Project _Project, SpriteExporter _spriteExporter) {

			_writer.Indent = 0;
			//Panels
			for (int i = 0; i < _Project.Panels.Count; ++i) {
				//-Panel
				Panel currentPanel = _Project.Panels[i];
				_writer.WriteLine("Panel {");
				_writer.WriteLine();
				++_writer.Indent;
				//Name
				_writer.WriteLine($"Name: \"{currentPanel.PanelName}\";");
				_writer.WriteLine();
				//BG
				_writer.WriteLine($"BGIndex: {_spriteExporter.GetSpriteIndex(currentPanel.BackgroundImage)};");
				_writer.WriteLine();
				//Condition
				if (currentPanel.Condition != null) {
					_writer.WriteLine("Condition {");
					_writer.WriteLine();
					++_writer.Indent;
					//DataValues
					for (int j = 0; j < currentPanel.Condition.DataValues.Count; ++j) {
						//-DataValue
						DataValue currentDataValue = currentPanel.Condition.DataValues[j];
						_writer.WriteLine("DataValue {");
						_writer.WriteLine();
						++_writer.Indent;
						//Name
						_writer.WriteLine($"Name: \"{currentDataValue.Name}\";");
						//Type
						_writer.WriteLine($"Type: \"{currentDataValue.ValueType}\";");
						//Value
						_writer.WriteLine($"Value: \"{currentDataValue.Value}\";");
						//Action
						//_writer.WriteLine($"Action: \"{currentDataValue.Action}\";");
						--_writer.Indent;
						_writer.WriteLine("}");
					}
					//Alternative
					_writer.WriteLine("Alternative {");
					++_writer.Indent;
					//Name
					_writer.WriteLine($"Name: \"{currentPanel.Condition.AlternativePanelKey}\";");
					--_writer.Indent;
					_writer.WriteLine("}");
					--_writer.Indent;
					_writer.WriteLine("}");
				}//Condition end
				_writer.WriteLine("Sprites {");
				_writer.WriteLine();
				++_writer.Indent;
				//Sprites
				if (currentPanel.SpriteImages != null) {

					foreach (KeyValuePair<Guid, SpriteImage> pair in currentPanel.SpriteImages) {

						_writer.WriteLine($"Sprite: {_spriteExporter.GetSpriteIndex(pair.Value)};");
					}
				}
				--_writer.Indent;
				_writer.WriteLine("}");
				_writer.WriteLine();
				//EntryBranch Name
				_writer.WriteLine($"EntryBranch: \"{currentPanel.EntryBranchKey}\";");
				_writer.WriteLine();
				_writer.WriteLine("Branches {");
				_writer.WriteLine();
				++_writer.Indent;

				//Branches
				for (int j = 0; j < currentPanel.Branches.Count; ++j) {

					//-Branch
					Branch currentBranch = currentPanel.Branches[j];
					_writer.WriteLine("Branch {");
					++_writer.Indent;

					//Name
					_writer.WriteLine($"Name: \"{currentBranch.Name}\";");
					//Texts
					for (int k = 0; k < currentBranch.ShownItems.Count; ++k) {

						//-Text
						DialogLine currentText = (DialogLine)currentBranch.ShownItems[k];
						//Character Name
						//Said text
						_writer.Write($"Text: \"{currentText.CharacterName}\", \"{currentText.TextShown}\"");
						if (currentText.Sprites.Count > 0) {
							for (int l = 0; l < currentText.Sprites.Count; ++l) {
								SpriteViewModel currentSprite = currentText.Sprites[l];
								//SpritePosition
								if (currentSprite.IsAutoPositioned) {

									//SpriteIndex
									_writer.Write($", {_spriteExporter.GetSpriteIndex(currentSprite.SpriteImage)}");
								} else {
									//SpriteIndex, XPos, YPos
									_writer.Write($", {_spriteExporter.GetSpriteIndex(currentSprite.SpriteImage)}({(int)currentSprite.PosX}, {(int)currentSprite.PosX})");
								}
							}
							_writer.WriteLine(";");
						} else {
							_writer.WriteLine(";");
						}
					}//Texts loop end

					//Continue
					_writer.WriteLine();

					_writer.WriteLine("Continue {");
					++_writer.Indent;
					//ContinueType
					_writer.WriteLine($"Type: {currentBranch.Continue.Type};");
					if (currentBranch.Continue.Type == Models.Enums.ContinueTypeEnum.Split) {

						//Split
						_writer.WriteLine("Split {");
						++_writer.Indent;

						//Options
						for (int l = 0; l < currentBranch.Continue.Split.Options.Count; ++l) {

							Option currentOption = currentBranch.Continue.Split.Options[l];
							//-Option
							_writer.WriteLine("Option {");
							++_writer.Indent;
							//Name
							_writer.WriteLine($"Name: {currentOption.Name};");
							//Text
							_writer.WriteLine($"Text: \"{currentOption.ShownText}\";");
							//ButtonSprite
							_writer.WriteLine($"Sprite: {_spriteExporter.GetButtonSpriteIndex(currentOption.ButtonSprite)};");
							//Continue
							_writer.WriteLine("Continue {");
							++_writer.Indent;
							//Type
							_writer.WriteLine($"Type: {currentOption.Continue.Type};");
							//Key
							_writer.WriteLine($"Name: \"{currentOption.Continue.ContinueKey}\";");
							--_writer.Indent;
							_writer.WriteLine("}");    //Continue Closing Bracket
							--_writer.Indent;
							_writer.WriteLine("}");    //Option Closing Bracket
						}
						--_writer.Indent;
						_writer.WriteLine("}");
					} else {
						//Continue Key
						_writer.WriteLine($"Name: \"{currentBranch.Continue.ContinueKey}\";");
					}
					--_writer.Indent;
					_writer.WriteLine("}");    //Continue closing Bracket
					--_writer.Indent;
					_writer.WriteLine("}");    //Current Branch closing Bracket
				}//Branches loop end
				--_writer.Indent;
				_writer.WriteLine("}");    //Branch Collection closiung Braces

				_writer.WriteLine("Animation_Placeholder {");
				++_writer.Indent;
				--_writer.Indent;
				_writer.WriteLine("}");    //AnimationPlaceholder closing Braces
				--_writer.Indent;
				_writer.WriteLine("}");    //currentPanel closing Braces
			}
		}

		public void ExportMainMenu(IndentedTextWriter _writer, SpriteImage _backgroundImage, SpriteExporter _spriteExporter) {
			_writer.Indent = 0;

			_writer.WriteLine("Menu {");
			_writer.WriteLine();
			++_writer.Indent;
			_writer.WriteLine("Name: \"Main Menu\";");
			_writer.WriteLine();
			_writer.WriteLine($"BGIndex: {_spriteExporter.GetSpriteIndex(_backgroundImage)};");
			_writer.WriteLine();
			_writer.WriteLine("Items {");
			++_writer.Indent;
			_writer.WriteLine("Button {");
			++_writer.Indent;
			_writer.WriteLine("Text: \"Start Game\";");
			_writer.WriteLine("Button: 50, 50, 50, 100, 0;");
			_writer.WriteLine("Type: 0;");
			--_writer.Indent;
			_writer.WriteLine("}");
			_writer.WriteLine("Button {");
			++_writer.Indent;
			_writer.WriteLine("Text: \"Load Game\";");
			_writer.WriteLine("Button: 50, 110, 50, 100, 0;");
			_writer.WriteLine("Type: 1;");
			--_writer.Indent;
			_writer.WriteLine("}");
			_writer.WriteLine("Button {");
			++_writer.Indent;
			_writer.WriteLine("Text: \"Quit Game\";");
			_writer.WriteLine("Button: 50, 170, 50, 100, 0;");
			_writer.WriteLine("Type: 4;");
			--_writer.Indent;
			_writer.WriteLine("}");
			--_writer.Indent;
			_writer.WriteLine("}");
			--_writer.Indent;
			_writer.WriteLine("}");
		}
	}
}