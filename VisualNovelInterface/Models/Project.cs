using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.Models.Serialization;
using VisualNovelInterface.MVVM;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.Models
{
	public class Project : BaseObject
	{
		private ObservableCollection<Panel> m_panels;
		private VariableManagerViewModel m_variableManagerViewModel;
		private FontManagerViewModel m_fontManagerViewModel;
		private SettingsViewModel m_projectSettingsViewModel;

		private ObservableCollection<SpriteImage> m_globalSprites;
		private ObservableCollection<SpriteImage> m_globalButtonSprites;

		private Panel m_selectedPanel;

		public ObservableCollection<Panel> Panels {
			get => m_panels;
			set => SetProperty(ref m_panels, value);
		}

		public VariableManagerViewModel VariableManagerViewModel {
			get => m_variableManagerViewModel;
			set => SetProperty(ref m_variableManagerViewModel, value);
		}

		public Panel SelectedPanel {
			get => m_selectedPanel;
			set => SetProperty(ref m_selectedPanel, value);
		}
		public FontManagerViewModel FontManagerViewModel {
			get => m_fontManagerViewModel;
			set => SetProperty(ref m_fontManagerViewModel, value);
		}
		public SettingsViewModel ProjectSettingsViewModel {
			get => m_projectSettingsViewModel;
			set => SetProperty(ref m_projectSettingsViewModel, value);
		}

		public ObservableCollection<SpriteImage> GlobalSprites {
			get => m_globalSprites;
			set => SetProperty(ref m_globalSprites, value);
		}

		public ObservableCollection<SpriteImage> GlobalButtonSprites {
			get => m_globalButtonSprites;
			set => SetProperty(ref m_globalButtonSprites, value);
		}

		public Project() {
			Panels = new ObservableCollection<Panel>();
			VariableManagerViewModel = new VariableManagerViewModel();
			FontManagerViewModel = new FontManagerViewModel();
			ProjectSettingsViewModel = new SettingsViewModel();
			GlobalSprites = new ObservableCollection<SpriteImage>();
			GlobalButtonSprites = new ObservableCollection<SpriteImage>();
			OnPropertyChanged(nameof(Panels));
		}

		public static void Serialize(Project _project, string path = null) {
			if (path == null) {
				path = Directory.GetCurrentDirectory();
			}
			SerializableProject serProject = _project.BuildSerializableProject();
			string exportstring = JsonConvert.SerializeObject(serProject);
			File.WriteAllText(path, exportstring);
		}

		public static void Deserialize(out Project _project, string path, SpriteViewModel.SpriteMoveEventHandler _spriteMoveEvent, Option.ButtonSpriteChange _buttonSpriteChangeEvent, Branch.SetEntryBranchEvent _entryBranchEvent) {

			_project = new Project();
			string exportstring = File.ReadAllText(path);
			SerializableProject serProject = JsonConvert.DeserializeObject<SerializableProject>(exportstring);
			_project.ApplySerielizableProject(serProject, _spriteMoveEvent, _buttonSpriteChangeEvent, _entryBranchEvent);
		}

		public SerializableProject BuildSerializableProject() {

			SerializableProject newProject = new SerializableProject();

			newProject.ButtonSprites = new SerializableSprite[GlobalButtonSprites.Count];

			//Global Button Sprites
			for (int i = 0; i < GlobalButtonSprites.Count; ++i) {

				newProject.ButtonSprites[i] = new SerializableSprite(GlobalButtonSprites[i].Name, GlobalButtonSprites[i].Image, GlobalButtonSprites[i].Id);
			}

			newProject.Sprites = new SerializableSprite[GlobalSprites.Count];

			//Global Sprites
			for (int i = 0; i < GlobalSprites.Count; ++i) {

				newProject.Sprites[i] = new SerializableSprite(GlobalSprites[i].Name, GlobalSprites[i].Image, GlobalSprites[i].Id);
			}

			newProject.VariableManager = new SerializableVariableManager() {
				Variables = new SerializableDataValue[VariableManagerViewModel.Variables.Count]
			};

			for (int i = 0; i < VariableManagerViewModel.Variables.Count; ++i) {
				newProject.VariableManager.Variables[i] = new SerializableDataValue() {
					Name = VariableManagerViewModel.Variables[i].Name,
					Type = VariableManagerViewModel.Variables[i].ValueType,
					Value = VariableManagerViewModel.Variables[i].Value
				};
			}

			newProject.FontManager = new SerializableFontManager() {
				CurrentUsedFont = FontManagerViewModel.CurrentUsedFont.Font,
				FontSize = FontManagerViewModel.FontSize,
				Fonts = new System.Windows.Media.FontFamily[FontManagerViewModel.Fonts.Count]
			};

			//Fonts
			for (int i = 0; i < FontManagerViewModel.Fonts.Count; ++i) {

				newProject.FontManager.Fonts[i] = FontManagerViewModel.Fonts[i].Font;
			}

			newProject.Settings = new SerializableSettings() {
				WindowHeight = ProjectSettingsViewModel.WindowHeight,
				WindowWidth = ProjectSettingsViewModel.WindowWidth,
				TextBoxRed = ProjectSettingsViewModel.TextBoxRed,
				TextBoxBlue = ProjectSettingsViewModel.TextBoxBlue,
				TextBoxGreen = ProjectSettingsViewModel.TextBoxGreen,
				TextBoxAlpha = ProjectSettingsViewModel.TextBoxAlpha
			};

			newProject.Panels = new SerializablePanel[Panels.Count];

			SerializableDataValue[] dataValues = null;
			SerializableBranch[] branches;
			SerializableOption[] options = null;
			SerializableDialogLine[] dialogLines;
			SerializableSpriteViewModel[] spriteVms;
			for (int i = 0; i < Panels.Count; ++i) {

				if (Panels[i].Condition != null) {
					dataValues = new SerializableDataValue[Panels[i].Condition.DataValues.Count];
					for (int j = 0; j < Panels[i].Condition.DataValues.Count; ++j) {
						dataValues[i] = new SerializableDataValue {
							Name = Panels[i].Condition.DataValues[j].Name,
							Type = Panels[i].Condition.DataValues[j].ValueType,
							Value = Panels[i].Condition.DataValues[j].Value
						};
					}
				}

				branches = new SerializableBranch[Panels[i].Branches.Count];

				for (int j = 0; j < Panels[i].Branches.Count; ++j) {

					if (Panels[i].Branches[j].Continue.Type == Enums.ContinueTypeEnum.Split) {
						options = new SerializableOption[Panels[i].Branches[j].Continue.Split.Options.Count];

						for (int k = 0; k < Panels[i].Branches[j].Continue.Split.Options.Count; ++k) {
							options[k] = new SerializableOption() {
								Name = Panels[i].Branches[j].Continue.Split.Options[k].Name,
								Height = Panels[i].Branches[j].Continue.Split.Options[k].Height,
								Width = Panels[i].Branches[j].Continue.Split.Options[k].Width,
								PosX = Panels[i].Branches[j].Continue.Split.Options[k].PosX,
								PosY = Panels[i].Branches[j].Continue.Split.Options[k].PosY,
								ShownText = Panels[i].Branches[j].Continue.Split.Options[k].ShownText,
								ButtonSpriteId = Panels[i].Branches[j].Continue.Split.Options[k].ButtonSprite.Id,
								Continue = new SerializableContinue() {
									ContinueKey = Panels[i].Branches[j].Continue.Split.Options[k].Continue.ContinueKey,
									Type = Panels[i].Branches[j].Continue.Split.Options[k].Continue.Type,
									Split = null
								}
							};
						}
					}

					dialogLines = new SerializableDialogLine[Panels[i].Branches[j].ShownItems.Count];

					for (int k = 0; k < Panels[i].Branches[j].ShownItems.Count; ++k) {

						DialogLine currentLine = (DialogLine)Panels[i].Branches[j].ShownItems[k];

						spriteVms = new SerializableSpriteViewModel[currentLine.Sprites.Count];

						for (int l = 0; l < currentLine.Sprites.Count; ++l) {
							spriteVms[l] = new SerializableSpriteViewModel() {
								Height = currentLine.Sprites[l].Height,
								Width = currentLine.Sprites[l].Width,
								PosX = currentLine.Sprites[l].PosX,
								PosY = currentLine.Sprites[l].PosY,
								ImageId = currentLine.Sprites[l].SpriteImage.Id,
							};
						}

						dialogLines[k] = new SerializableDialogLine() {
							CharacterName = currentLine.CharacterName,
							TextShown = currentLine.TextShown,
							UsedSprites = spriteVms
						};
					}

					branches[j] = new SerializableBranch() {
						Name = Panels[i].Branches[j].Name,
						Continue = new SerializableContinue() {
							ContinueKey = Panels[i].Branches[j].Continue.ContinueKey,
							Type = Panels[i].Branches[j].Continue.Type,
							Split = Panels[i].Branches[j].Continue.Split == null ? null : new SerializableSplit() {
								Name = Panels[i].Branches[j].Continue.Split.Name,
								Options = options,
							},
						},
						Items = dialogLines,
					};
				}

				newProject.Panels[i] = new SerializablePanel() {
					PanelName = Panels[i].PanelName,
					Condition = Panels[i].Condition == null ? null : new SerializableCondition() {
						AlternativePanelKey = Panels[i].Condition.AlternativePanelKey,
						DataValues = dataValues,
					},
					BackgroundId = Panels[i].BackgroundImage.Id,
					EntryBranch = Panels[i].EntryBranchKey,
					Branches = branches,
				};
			}

			return newProject;
		}

		public void ApplySerielizableProject(SerializableProject _project, SpriteViewModel.SpriteMoveEventHandler _spriteMoveEvent, Option.ButtonSpriteChange _buttonSpriteChangeEvent, Branch.SetEntryBranchEvent _entryBranchEvent) {

			Dictionary<Guid, SpriteImage> m_SpritesDict = new Dictionary<Guid, SpriteImage>();
			Dictionary<Guid, SpriteImage> m_ButtonSpritesDict = new Dictionary<Guid, SpriteImage>();

			//Global Button Sprites
			SpriteImage newSprite;
			for (int i = 0; i < _project.ButtonSprites.Length; ++i) {

				byte[] bytes = Convert.FromBase64String(_project.ButtonSprites[i].Base64Image);
				string fileName = Directory.GetCurrentDirectory();
				using (FileStream imageFile = new FileStream(fileName + @"\Resources\" + _project.ButtonSprites[i].Name + Path.GetExtension(_project.ButtonSprites[i].Path), FileMode.Create)) {
					imageFile.Write(bytes, 0, bytes.Length);
					imageFile.Flush();
					imageFile.Close();
					fileName = imageFile.Name;
				}

				newSprite = new SpriteImage(fileName, _project.ButtonSprites[i].Name, _project.ButtonSprites[i].Id);
				m_ButtonSpritesDict.Add(newSprite.Id, newSprite);
				GlobalButtonSprites.Add(newSprite);
			}


			//Global Sprites
			for (int i = 0; i < _project.Sprites.Length; ++i) {

				byte[] bytes = Convert.FromBase64String(_project.Sprites[i].Base64Image);
				string fileName = Directory.GetCurrentDirectory();
				using (FileStream imageFile = new FileStream(fileName + @"\Resources\" + _project.Sprites[i].Name + Path.GetExtension(_project.Sprites[i].Path), FileMode.Create)) {
					imageFile.Write(bytes, 0, bytes.Length);
					imageFile.Flush();
					fileName = imageFile.Name;
				}

				newSprite = new SpriteImage(_project.Sprites[i].Path, _project.Sprites[i].Name, _project.Sprites[i].Id);
				m_SpritesDict.Add(newSprite.Id, newSprite);
				GlobalSprites.Add(newSprite);
			}

			VariableManagerViewModel = new VariableManagerViewModel() {
				Variables = new ObservableCollection<DataValue>(),
			};

			for (int i = 0; i < _project.VariableManager.Variables.Length; ++i) {
				VariableManagerViewModel.Variables.Add(
					new DataValue(_project.VariableManager.Variables[i].Name,
								  new Tuple<Enums.DataValueTypeEnum, object>(_project.VariableManager.Variables[i].Type, _project.VariableManager.Variables[i].Value)));
			}
			ObservableCollection<ProjectFont> FontsList = new ObservableCollection<ProjectFont>();
			for (int i = 0; i < _project.FontManager.Fonts.Length; ++i) {
				FontsList.Add(new ProjectFont() { Font = _project.FontManager.Fonts[i], IsUsed = false });
			}

			FontManagerViewModel = new FontManagerViewModel() {
				Fonts = FontsList,
				FontSize = _project.FontManager.FontSize,
				CurrentUsedFont = FontsList.Single(x => x.Font.Source == _project.FontManager.CurrentUsedFont.Source)
			};

			ProjectSettingsViewModel = new SettingsViewModel(ProjectSettingsViewModel.WindowWidth,
															 ProjectSettingsViewModel.WindowHeight,
															 ProjectSettingsViewModel.TextBoxRed,
															 ProjectSettingsViewModel.TextBoxGreen,
															 ProjectSettingsViewModel.TextBoxBlue,
															 ProjectSettingsViewModel.TextBoxAlpha);


			Panel newPanel;
			ObservableCollection<DataValue> dataValues;
			DataValue newDataValue;
			ObservableCollection<Branch> branches = new ObservableCollection<Branch>();
			Branch newBranch;
			ObservableCollection<ShownItem> newDialogLines;
			DialogLine newLine;
			Continue newContinue;
			Split newSplit;
			ObservableCollection<Option> newOptions;
			Option newOption;
			for (int i = 0; i < _project.Panels.Length; ++i) {
				newPanel = new Panel(_project.Panels[i].PanelName, null);
				newPanel.Branches.Clear();
				dataValues = new ObservableCollection<DataValue>();

				if (_project.Panels[i].Condition != null) {
					for (int j = 0; j < _project.Panels[i].Condition.DataValues.Length; j++) {

						newDataValue = new DataValue(_project.Panels[i].Condition.DataValues[j].Name,
													 new Tuple<Enums.DataValueTypeEnum, object>(_project.Panels[i].Condition.DataValues[j].Type,
																								_project.Panels[i].Condition.DataValues[j].Value));
						dataValues.Add(newDataValue);
					}
				}
				if (_project.Panels[i].Condition != null) {
					newPanel.Condition = new Condition() {
						AlternativePanelKey = _project.Panels[i].Condition.AlternativePanelKey,
						DataValues = dataValues
					};
				}
				newPanel.BackgroundImage = m_SpritesDict[_project.Panels[i].BackgroundId];
				newPanel.PanelName = _project.Panels[i].PanelName;


				for (int j = 0; j < _project.Panels[i].Branches.Length; ++j) {
					newBranch = new Branch(_project.Panels[i].Branches[j].Name);
					newBranch.ShownItems.Clear();
					newContinue = new Continue();
					newContinue.Type = _project.Panels[i].Branches[j].Continue.Type;

					if (newContinue.Type == Enums.ContinueTypeEnum.Split) {

						newOptions = new ObservableCollection<Option>();

						for (int k = 0; k < _project.Panels[i].Branches[j].Continue.Split.Options.Length; ++k) {
							newOption = new Option(_project.Panels[i].Branches[j].Continue.Split.Options[k].Name,
								_project.Panels[i].Branches[j].Continue.Split.Options[k].ShownText,
								m_ButtonSpritesDict[_project.Panels[i].Branches[j].Continue.Split.Options[k].ButtonSpriteId],
								new Continue(_project.Panels[i].Branches[j].Continue.Split.Options[k].Continue.Type, _project.Panels[i].Branches[j].Continue.Split.Options[k].Continue.ContinueKey));
							newOption.OnButtonSpriteChange += _buttonSpriteChangeEvent;
						}

						newSplit = new Split(_project.Panels[i].Branches[j].Continue.Split.Name, newOptions);
						newContinue.Split = newSplit;
					} else {
						newContinue.ContinueKey = _project.Panels[i].Branches[j].Continue.ContinueKey;
					}
					newBranch.Continue = newContinue;
					newDialogLines = new ObservableCollection<ShownItem>();

					for (int k = 0; k < _project.Panels[i].Branches[j].Items.Length; ++k) {

						newLine = new DialogLine();
						SerializableDialogLine currentLine = _project.Panels[i].Branches[j].Items[k];

						newLine.CharacterName = currentLine.CharacterName;
						newLine.TextShown = currentLine.TextShown;
						newLine.Sprites = new ObservableCollection<SpriteViewModel>();

						for (int l = 0; l < currentLine.UsedSprites.Length; ++l) {
							SpriteViewModel newSpriteVM = new SpriteViewModel(m_SpritesDict[currentLine.UsedSprites[l].ImageId],
																			  currentLine.UsedSprites[l].PosX,
																			  currentLine.UsedSprites[l].PosY,
																			  currentLine.UsedSprites[l].Height,
																			  currentLine.UsedSprites[l].Width);
							newSpriteVM.OnSpriteMoveEvent += _spriteMoveEvent;
							newLine.Sprites.Add(newSpriteVM);
						}
						newBranch.SetEntryBranchEventHandler += _entryBranchEvent;
						newBranch.ShownItems.Add(newLine);
					}

					newPanel.Branches.Add(newBranch);
				}
				Panels.Add(newPanel);
			}
		}
	}
}