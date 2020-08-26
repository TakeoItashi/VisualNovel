#include "Menu.h"
#include "Condition.h"
#include "Panel.h"
#include "Settings.h"
#include "TextLoader.h"
#include "Button.h"
#include "DataValue.h"
#include "MenuItem.h"
#include "SpritePosition.h"
#include "DialogLine.h"
#include "DataValueType.h"
#include "TextBox.h"
#include "OptionsMenu.h"
#include "MainMenu.h"
#include "ImageLoader.h"
#include "Save.h"
#include "ConditionAction.h"
#include "Game.h"
#include "SplitDecision.h"
#include "Branch.h"

Game* Game::m_gamePointer = nullptr;					//required for singleton
MainMenu* Game::m_MainMenu;								//
OptionsMenu* Game::m_OptionsMenu;						//
Settings* Game::m_GameSettings;							//
ImageLoader* Game::m_ImageLoader;						//
TextBox* Game::m_TextBox;								//
SDL_Window* Game::m_Window;								//
SDL_Renderer* Game::m_Renderer;							// Defining the static members is required since it is in a static context
SDL_Event* Game::m_EventHandler;						//
TextLoader* Game::m_textLoader;							//
Save* Game::m_save;										//
std::map<std::string, Panel*> Game::m_PanelMap;					//
std::map<std::string, int> Game::m_panelNameDictionary;	//
std::vector<std::string> Game::m_keywords;				//
int Game::m_CurrentLine;								//
std::string Game::m_CurrentPanelKey;								//
Menu* Game::m_CurrentMenu;								//
bool Game::m_GameIsRunning;								//
bool Game::m_IsDecisionPending;							//

Game::Game() {

	m_MainMenu = nullptr;
	m_OptionsMenu = nullptr;
	m_GameSettings = nullptr;
	m_ImageLoader = nullptr;
	m_TextBox = nullptr;
	m_Window = nullptr;
	m_Renderer = nullptr;
	m_EventHandler = nullptr;
	m_textLoader = nullptr;
	m_CurrentMenu = nullptr;
	m_GameIsRunning = false;
	m_IsDecisionPending = false;
}

Game::~Game() {

	SDL_DestroyRenderer(m_Renderer);
	SDL_DestroyWindow(m_Window);
	delete m_ImageLoader;
	delete m_textLoader;
	m_Renderer = nullptr;
	m_Window = nullptr;
	m_ImageLoader = nullptr;
	m_textLoader = nullptr;
	IMG_Quit();
	SDL_Quit();
	//TODO: delete all the lists and maps
}

void Game::Init(Settings* _initialSettings, SDL_Event* _eventHandler) {

	m_GameSettings = _initialSettings;
	m_EventHandler = _eventHandler;

	SDL_Init(SDL_INIT_VIDEO);
	IMG_Init(IMG_INIT_PNG);
	TTF_Init();

	_initialSettings->LoadSettings();

	m_Window = SDL_CreateWindow("Visual Novel", 100, 50, _initialSettings->m_WindowWidth, _initialSettings->m_WindowHeight, SDL_WINDOW_SHOWN);
	m_Renderer = SDL_CreateRenderer(m_Window, -1, SDL_RENDERER_ACCELERATED);

	SDL_SetRenderDrawColor(m_Renderer, 0xFF, 0xFF, 0xFF, 0xFF);

	m_ImageLoader = new ImageLoader(m_Renderer);
	m_ImageLoader->LoadTextures();

	m_TextBox = new TextBox(m_Renderer, m_GameSettings);
	m_TextBox->ApplySettings(_initialSettings);
	_initialSettings->m_Font = m_TextBox->GetFont();

	m_textLoader = new TextLoader();
	m_keywords = m_textLoader->LoadText("Storyboard.txt");

	m_MainMenu = new MainMenu(m_Renderer, m_ImageLoader, m_GameSettings, "MainMenu.txt");
	m_CurrentMenu = m_MainMenu;
	m_CurrentMenu->Render();

	//m_OptionsMenu = new OptionsMenu(m_Renderer, m_ImageLoader, "OptionsMenu.txt");

	LoadStoryBoard();
	m_GameIsRunning = false;
}

bool Game::NewGame(Button* _butt) {

	m_CurrentPanelKey = m_PanelMap.begin()->second->m_PanelName;
	m_CurrentLine = 0;

	////Test for save game serialization. must be removed
	m_save = new Save();
	m_save->m_currentLine = m_CurrentLine;
	m_save->m_currentPanel = m_CurrentPanelKey;
	DataValue* testValue = new DataValue();
	testValue->m_name = "StoryTrigger1";
	testValue->SetValue(true);
	m_save->m_values.insert({ testValue->m_name, testValue });

	testValue = new DataValue();
	testValue->m_name = "StoryVariable1";
	testValue->SetValue(3);
	m_save->m_values.insert({ testValue->m_name, testValue });

	testValue = new DataValue();
	testValue->m_name = "TestDecimal1";
	testValue->SetValue(6.9f);
	m_save->m_values.insert({ testValue->m_name, testValue });

	m_save->Serialize();
	m_GameIsRunning = true;

	m_CurrentMenu = nullptr;

	std::map<std::string, Panel*>::iterator iterator = m_PanelMap.begin();

	while (iterator != m_PanelMap.end()) {

		iterator->second->LoadImages();
		iterator++;
	}

	Render();
	SDL_RenderPresent(m_Renderer);
	return true;
}

bool Game::Update(SDL_Event* _eventhandler) {

	if (m_CurrentMenu != nullptr) {

		ShowMenu(m_CurrentMenu);
		return false;
	}

	if (m_GameIsRunning) {

		if (!m_IsDecisionPending) {

			//TODO Update neu strukturieren:
			if (m_EventHandler->type == SDL_MOUSEBUTTONUP ||
				m_EventHandler->type == SDL_KEYUP) {

				//TODO: If the PanelCondition is null because of an Error, relying on it being null here could create Problems
				if (m_PanelMap[m_CurrentPanelKey]->m_PanelCondition == nullptr ||
					m_PanelMap[m_CurrentPanelKey]->m_PanelCondition->isMet(m_save->m_values)) {

					m_CurrentLine++;

					Branch* currentBranch = m_PanelMap[m_CurrentPanelKey]->GetCurrentBranch();
					if (m_CurrentLine >= currentBranch->m_shownItems.size()) {
						switch (currentBranch->m_continueType) {
							case ContinueType::BranchContinue:
								ChangeBranch(currentBranch->m_continueKey.c_str());
								break;
							case ContinueType::PanelContinue:
								ChangePanel(currentBranch->m_continueKey.c_str());
								break;
						}
					}

					Render();

					if (m_IsDecisionPending) {
						return false;
					}

					//if (m_CurrentLine >= m_PanelMap[m_CurrentPanelKey]->m_DialogueLines.size()) {
					//
					//	if (m_CurrentPanelKey != m_PanelMap.end()->first) {
					//
					//		m_CurrentMenu = m_MainMenu;
					//		m_GameIsRunning = false;
					//		return false;
					//	} else {
					//
					//		//m_CurrentPanelKey++;
					//		m_CurrentLine = 0;
					//	}
					//}
				} else {
					if (m_PanelMap[m_CurrentPanelKey]->m_PanelCondition != nullptr) {

						m_CurrentPanelKey = m_PanelMap[m_CurrentPanelKey]->m_PanelCondition->m_AlternativePanelKey;
					}
					//else {
					//	//TODO Fehlermeldung
					//}
				}
			}
		} else {
			SplitDecision* split = (SplitDecision*)m_PanelMap[m_CurrentPanelKey]->GetCurrentBranch()->m_shownItems[m_CurrentLine];
			for (int i = 0; i < split->m_buttons.size(); i++) {

				bool cancel = false;
				cancel = split->m_buttons[i]->HandleEvent(m_EventHandler);
				if (cancel) {
					return true;
				}
			}
			m_PanelMap[m_CurrentPanelKey]->RenderCurrentSplit(m_CurrentLine);
		}
	}
}

bool Game::Render() {

	SDL_RenderClear(m_Renderer);
	Panel* m_currentPanel = nullptr;
	if (m_PanelMap.find(m_CurrentPanelKey) != m_PanelMap.end()) {
		m_currentPanel = m_PanelMap[m_CurrentPanelKey];
	} else {
		ResetGameToMainMenu();
		return true;
	}
	Branch* currentBranch = m_currentPanel->GetCurrentBranch();
	switch (currentBranch->m_shownItems[m_CurrentLine]->m_type) {

		case ShownItemType::Line:
			m_PanelMap[m_CurrentPanelKey]->ShowLine(m_CurrentLine);
			break;
		case ShownItemType::Decision:
			m_PanelMap[m_CurrentPanelKey]->ShowSplit(m_CurrentLine);
			m_IsDecisionPending = true;
			break;
	}
	SDL_RenderPresent(m_Renderer);
	return false;
}

bool Game::LoadGame(Button* _buttonCallback) {

	if (m_save == nullptr) {

		m_save = new Save();
	}
	std::string test;
	m_save->Deserialize();
	return false;

}

void Game::ShowMenu(Menu* _menuInstance) {

	SDL_RenderClear(m_Renderer);
	_menuInstance->Render();

	for (int i = 0; i < _menuInstance->m_MenuItems.size(); i++) {

		bool cancel = false;
		cancel = _menuInstance->m_MenuItems[i].Button->HandleEvent(m_EventHandler);
		if (cancel) {
			return;
		}
	}
}

bool Game::ChangeBranch(const char* _BranchKey) {
	m_PanelMap[m_CurrentPanelKey]->m_currentBranchKey = _BranchKey;
	m_IsDecisionPending = false;
	m_CurrentLine = 0;
	Render();
	return true;
}

bool Game::ChangePanel(const char* _panelKey) {
	m_CurrentPanelKey = _panelKey;
	m_IsDecisionPending = false;
	m_CurrentLine = 0;

	Render();
	return true;
}

void Game::ResetGameToMainMenu() {
	m_CurrentMenu = m_MainMenu;
	m_GameIsRunning = false;
	m_CurrentPanelKey = m_PanelMap.begin()->second->m_PanelName;
	std::map<std::string, Panel*>::iterator it;

	for (it = m_PanelMap.begin(); it != m_PanelMap.end(); it++) {
		it->second->m_currentBranchKey = it->second->m_EntryBranchKey;
	}
	m_CurrentLine = 0;
}

bool Game::LoadCustomMethod(Button* _buttonCallback) {
	return false;

}

bool Game::ChangeSettings(Settings* NewSettings) {
	return false;

}

bool Game::Gallery(Button* _buttonCallback) {
	return false;

}

bool Game::OpenOptions(Button* _buttonCallback) {

	m_CurrentMenu = m_OptionsMenu;
	m_CurrentMenu->Render();
	return false;

}

bool Game::Quit(Button* _buttonCallback) {

	SDL_FlushEvents(0, UINT32_MAX);
	SDL_Event* quitEvent = new SDL_Event();
	quitEvent->type = SDL_QUIT;
	//TODO nachfaregn, ob man schließen will. vlt noch zum Menü leiten
	SDL_PushEvent(quitEvent);
	return false;
}

bool Game::ChangeResolution(Button* _buttonCallback) {

	SDL_RenderClear(m_Renderer);

	for (int i = 0; i < m_OptionsMenu->m_MenuItems.size(); i++) {

		m_OptionsMenu->m_MenuItems[i].Button->HandleEvent(m_EventHandler);

		SDL_RenderPresent(m_Renderer);
	}
	return false;

}

bool Game::ToggleFullscreen(Button* _buttonCallback) {
	return false;

}

bool Game::OpenMainMenu(Button* _buttonCallback) {

	m_CurrentMenu = m_MainMenu;
	m_CurrentMenu->Render();
	return false;

}

void Game::RenderCurrentMenu() {

	SDL_RenderClear(m_Renderer);

	m_CurrentMenu->Render();

	for (int i = 0; i < m_CurrentMenu->m_MenuItems.size(); i++) {

		m_CurrentMenu->m_MenuItems[i].Button->HandleEvent(m_EventHandler);

	}
	SDL_RenderPresent(m_Renderer);
}

void Game::LoadStoryBoard() {

	int lineKey;

	//TODO while schleifen benutzen
	for (int i = 0; i < m_keywords.size(); i++) {

		if (m_keywords[i] == "Panel" && m_keywords[i + 1] == "{") {

			Panel* newPanel = new Panel(m_TextBox, m_ImageLoader, m_GameSettings);
			i = i + 2;
			for (i; i < m_keywords.size(); i) {

				if (m_keywords[i] == ";") {

					i++;
					continue;
				}
				if (m_keywords[i] == "Name:") {

					newPanel->m_PanelName = m_keywords[i + 1];
					i = i + 2;
					continue;
				}
				if (m_keywords[i] == "BGIndex:") {

					int index = std::stoi(m_keywords[i + 1]);
					newPanel->m_BackgroundImageIndex = index;
					SpritePosition spritePosition;
					spritePosition.Index = index;
					newPanel->m_SpriteIndexList.push_back(spritePosition);
					i = i + 2;
					continue;
				}
				if (m_keywords[i] == "Condition" && m_keywords[i + 1] == "{") {

					Condition* newCondition = new Condition;
					i = i + 2;
					for (i; i < m_keywords.size(); i) {

						if (m_keywords[i] == "DataValue" && m_keywords[i + 1] == "{") {

							DataValue* newDataValue;
							i = i + 2;
							DataValueType newType;
							ConditionAction newAction;
							std::string newName;
							std::variant<int, std::string, float, bool> newValue = new void*;

							for (i; i < m_keywords.size(); i) {

								if (m_keywords[i] == ";") {

									i++;
									continue;
								}
								if (m_keywords[i] == "Name:") {

									newName = m_keywords[i + 1];
									i = i + 2;
									continue;
								}
								if (m_keywords[i] == "Type:") {

									if (m_keywords[i + 1] == "trigger") {

										newType = DataValueType::trigger;
										i = i + 2;
									} else if (m_keywords[i + 1] == "variable") {

										newType = DataValueType::variable;
										i = i + 2;
									} else if (m_keywords[i + 1] == "decimal") {

										newType = DataValueType::decimal;
										i = i + 2;
									} else {
										//TODO Fehlermeldung
									}
									continue;
								}
								if (m_keywords[i] == "Value:") {

									switch (newType) {
										case DataValueType::trigger:
											if (m_keywords[i + 1] == "true") {

												newValue = true;
											} else {

												newValue = false;
											}
											i = i + 2;
											break;
										case DataValueType::variable:
											newValue = atoi(m_keywords[i + 1].c_str());
											i = i + 2;
											break;
										case DataValueType::decimal:
											newValue = (float)atof(m_keywords[i + 1].c_str());
											i = i + 2;
											break;
										case DataValueType::text:
											newValue = m_keywords[i + 1];
											i = i + 2;
											break;
									}
									if (m_keywords[i] == "}") {
										i++;
										break;
									}
								}
								if (m_keywords[i] == "Action:") {

									if (m_keywords[i + 1] == "isSmaller") {

										newAction = ConditionAction::isSmaller;
										i = i + 2;
									} else if (m_keywords[i + 1] == "isEqual") {

										newAction = ConditionAction::isEqual;
										i = i + 2;
									} else if (m_keywords[i + 1] == "isBigger") {

										newAction = ConditionAction::isBigger;
										i = i + 2;
									} else {
										//TODO Fehlermeldung
									}
									continue;
								}
								if (m_keywords[i] == "}") {
									newDataValue = new DataValue(newName, newType, newValue, newAction);
									newCondition->m_ConditionValues[newDataValue->m_name] = newDataValue;
									i++;
									break;
								}
							}
						}

						if (m_keywords[i] == "Alternative" && m_keywords[i + 1] == "{") {
							i = i + 2;
							newCondition->m_AlternativePanelKey = m_keywords[i + 1];
							i = i + 4;
						}

						if (m_keywords[i] == "}") {
							i++;
							break;
						}
					}
					newPanel->m_PanelCondition = newCondition;
					continue;
				}
				if (m_keywords[i] == "Sprites" && m_keywords[i + 1] == "{") {

					i = i + 2;
					for (i; i < m_keywords.size(); i) {

						if (m_keywords[i] == ";") {

							i++;
							continue;
						}
						//TODO Einen identifier (Index oder Name) für die Sprites definieren, den man für die SpritePositions benutzen kann. Dieser muss dann hier geparst werden

						if (m_keywords[i] == "Sprite:") {
							SpritePosition spritePosition;
							spritePosition.Index = std::stoi(m_keywords[i + 1]);
							newPanel->m_SpriteIndexList.push_back(spritePosition);
							i = i + 2;
						}
						if (m_keywords[i] == "}") {
							i++;
							break;
						}
					}
				}
				if (m_keywords[i] == "EntryBranch:") {
					newPanel->m_EntryBranchKey = m_keywords[i + 1];
					i = i + 3;
				}
				if (m_keywords[i] == "Branches" && m_keywords[i + 1] == "{") {
					i += 2;
					for (i; i < m_keywords.size(); i) {
						if (m_keywords[i] == "Branch" && m_keywords[i + 1] == "{") {
							i += 2;
							Branch* newBranch = new Branch();
							lineKey = 0;
							for (i; i < m_keywords.size(); i) {

								if (m_keywords[i] == "Name:") {
									newBranch->m_Name = m_keywords[i + 1];
									i += 3;
									continue;
								}

								if (m_keywords[i] == "Text:") {

									DialogLine* newLine = new DialogLine();
									newLine->m_type = ShownItemType::Line;
									newLine->Name = m_keywords[i + 1];
									newLine->Text = m_keywords[i + 3];		//+3 weil sich zwischen dem Namen und dem Text ein Komma befindet
									i = i + 4;
									if (m_keywords[i] == ";") {
										newBranch->m_shownItems.insert(std::pair(lineKey, newLine));
										lineKey++;
										i++;
										continue;
									} else if (m_keywords[i] == ",") {
										i++;
										SpritePosition spritePosition;
										spritePosition.Index = std::stoi(m_keywords[i]);
										spritePosition.PosX = -1;
										spritePosition.PosY = -1;
										i++;
										while (m_keywords[i] != ";") {

											if (m_keywords[i] == ",") {

												newLine->m_SpritesShown.push_back(spritePosition);
												i++;
												spritePosition = { -1, -1, -1 };
												continue;
											}
											if (m_keywords[i] == "(") {
												i++;
												spritePosition.PosY = std::stoi(m_keywords[i]);
												i = i + 2;											//um 2 Positionen verschieben, weil sich zwischen den Koordinaten ein Komma befindet
												spritePosition.PosX = std::stoi(m_keywords[i]);
												i++;
												continue;
											}
											if (m_keywords[i] == ")") {
												i++;
												continue;
											}
											spritePosition.Index = std::stoi(m_keywords[i]);
											i++;
										}
										newLine->m_SpritesShown.push_back(spritePosition);
										newBranch->m_shownItems.insert(std::pair(lineKey, newLine));
										lineKey++;
										i++;
										spritePosition = { -1, -1, -1 };
									}
									continue;
								}

								if (m_keywords[i] == "Continue" && m_keywords[i + 1] == "{") {
									i += 3;
									std::map<std::string, ContinueType> enumMap = { {"Branch", ContinueType::BranchContinue}, {"Split", ContinueType::SplitContinue},{"Panel", ContinueType::PanelContinue} };
									auto iterator = enumMap.find(m_keywords[i]);
									if (iterator != enumMap.end()) {
										newBranch->m_continueType = iterator->second;
									} else {
										//TODO Error
									}
									switch (newBranch->m_continueType) {
										case ContinueType::BranchContinue:
										case ContinueType::PanelContinue:
											i += 3;
											newBranch->m_continueKey = m_keywords[i];
											i += 3;
											break;
										case ContinueType::SplitContinue:
											i += 2;
											if (m_keywords[i] == "Split" && m_keywords[i + 1] == "{") {
												i += 2;
												SplitDecision* newDecision = new SplitDecision(m_Renderer, m_GameSettings, m_ImageLoader);
												newDecision->m_type = ShownItemType::Decision;
												for (i; i < m_keywords.size(); i) {
													if (m_keywords[i] == "Option" && m_keywords[i + 1] == "{") {
														i += 2;
														SplitOption* newOption = new SplitOption();
														for (i; i < m_keywords.size(); i) {
															if (m_keywords[i] == "Name:") {
																newOption->m_name = m_keywords[i + 1];
																i += 3;
																continue;
															}
															if (m_keywords[i] == "Text:") {
																newOption->m_shownText = m_keywords[i + 1];
																i += 3;
																continue;
															}
															if (m_keywords[i] == "Sprite:") {
																newOption->SpriteIndex = stoi(m_keywords[i + 1]);
																i += 3;
																continue;
															}
															if (m_keywords[i] == "Continue" && m_keywords[i + 1] == "{") {
																i += 3;
																iterator = enumMap.find(m_keywords[i]);
																if (iterator != enumMap.end()) {
																	newOption->m_type = iterator->second;
																} else {
																	//TODO Error
																}
																switch (newOption->m_type) {
																	case ContinueType::BranchContinue:
																	case ContinueType::PanelContinue:
																		i += 3;
																		newOption->m_continueKey = m_keywords[i];
																		i += 3;
																		continue;
																}
															}
															if (m_keywords[i] == "}") {
																newDecision->m_options.push_back(newOption);
																i++;
																break;
															}
														}

													}
													if (m_keywords[i] == "}") {
														i += 2;
														newBranch->m_shownItems.insert(std::pair(lineKey, newDecision));
														lineKey++;
														break;
													}
												}
											}
									}
								}

								if (m_keywords[i] == "}") {
									i++;
									newPanel->m_Branches.insert(std::pair<std::string, Branch*>(newBranch->m_Name, newBranch));
									break;
								}
							}
						}
						if (m_keywords[i] == "}") {
							i++;		//TODO Animation
							break;
						}
					}
					if (m_keywords[i] == "Animation_Placeholder") {
						i += 2;		//TODO Animation
						continue;
					}
					if (m_keywords[i] == "}") {
						i++;
						break;
					}
				}
				if (m_keywords[i] == "}") {
					i++;
					break;
				}
			}
			newPanel->m_currentBranchKey = newPanel->m_EntryBranchKey;
			m_PanelMap.insert(std::pair(newPanel->m_PanelName, newPanel));
			m_panelNameDictionary.insert(std::pair(newPanel->m_PanelName, (m_PanelMap.size() - 1)));
		}
	}
}