#include "Menu.h"
#include "Condition.h"
#include "Panel.h"
#include "Save.h"
#include "Settings.h"
#include "TextLoader.h"
#include "Button.h"
#include "DataValue.h"
#include "MenuItem.h"
#include "SpritePosition.h"
#include "DialogueLine.h"
#include "DataValueType.h"
#include "TextBox.h"
#include "OptionsMenu.h"
#include "MainMenu.h"
#include "ImageLoader.h"
#include "Game.h"

Game* Game::m_gamePointer = nullptr;			//required for singleton
MainMenu* Game::m_MainMenu;						//
OptionsMenu* Game::m_OptionsMenu;				//
Settings* Game::m_GameSettings;					//
ImageLoader* Game::m_ImageLoader;				//
TextBox* Game::m_TextBox;						//
SDL_Window* Game::m_Window;						//
SDL_Renderer* Game::m_Renderer;					// Defining the static members is required since it is in a static context
SDL_Event* Game::m_EventHandler;				//
TextLoader* Game::m_textLoader;					//
Save* Game::m_save;
std::vector<Panel*> Game::m_PanelList;			//
std::vector<std::string> Game::m_keywords;		//
int Game::m_CurrentLine;						//
int Game::m_CurrentPanel;						//
Menu* Game::m_CurrentMenu;
bool Game::m_GameIsRunning;



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
	m_GameIsRunning = nullptr;
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
}

void Game::Init(Settings* _initialSettings, SDL_Event* _eventHandler) {

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

	m_TextBox = new TextBox(m_Renderer);
	m_TextBox->ApplySettings(_initialSettings);
	m_TextBox->loadFont();

	m_textLoader = new TextLoader();
	m_keywords = m_textLoader->LoadText("Storyboard.txt");

	m_MainMenu = new MainMenu(m_Renderer, m_ImageLoader, "MainMenu.txt");
	m_CurrentMenu = m_MainMenu;
	m_CurrentMenu->Render();

	m_OptionsMenu = new OptionsMenu(m_Renderer, m_ImageLoader, "OptionsMenu.txt");

	LoadStoryBoard();
	m_GameIsRunning = false;
}

void Game::NewGame(Button* _butt) {

	////Test for save game serialization. must be removed
	m_save = new Save();
	m_save->m_currentLine = 8;
	m_save->m_currentPanel = 15;
	DataValue* testValue = new DataValue();
	testValue->m_name = "StoryTrigger1";
	testValue->SetValue(true);
	m_save->m_values.push_back(testValue);

	testValue = new DataValue();
	testValue->m_name = "StoryVariable1";
	testValue->SetValue(3);
	m_save->m_values.push_back(testValue);

	testValue = new DataValue();
	testValue->m_name = "TestDecimal1";
	testValue->SetValue(6.9f);
	m_save->m_values.push_back(testValue);

	m_save->Serialize();
	m_GameIsRunning = true;
}

void Game::Update(SDL_Event* _eventhandler, bool* _quitCondition) {

	if (m_CurrentMenu != nullptr) {

		for (int i = 0; i < m_CurrentMenu->m_MenuItems.size(); i++) {

			m_CurrentMenu->m_MenuItems[i].Button->HandleEvent(m_EventHandler);
		}
	}

	while (SDL_PollEvent(m_EventHandler) != 0) {

		if (m_EventHandler->type == SDL_QUIT) {

			*_quitCondition = true;
			return;
		}

		if (m_GameIsRunning) {

			if (m_EventHandler->type == SDL_MOUSEBUTTONUP || m_EventHandler->type == SDL_MOUSEMOTION || true) {

				//TODO Update neu strukturieren:
				if (m_EventHandler->type == SDL_MOUSEBUTTONUP || m_EventHandler->type == SDL_KEYUP|| true) {

					if (m_PanelList[m_CurrentPanel]->m_PanelCondition->isMet(m_save->m_values)) {

						if (m_CurrentPanel >= m_PanelList.size()) {

							_eventhandler->quit;
						}

						Render();
						SDL_RenderPresent(m_Renderer);
						m_CurrentLine++;
						SDL_ShowSimpleMessageBox(SDL_MESSAGEBOX_INFORMATION, "Next Line triggered.", "Next Line triggered.", NULL);

						if (m_CurrentLine >= m_PanelList[m_CurrentPanel]->m_DialogueLines.size()) {

							m_CurrentPanel++;
							m_CurrentLine = 0;
						}
					}
					else {
						m_CurrentPanel++;
						m_CurrentLine = 0;
					}
				}
			}
		}
	}
}

void Game::Render() {

	SDL_RenderClear(m_Renderer);

	m_PanelList[m_CurrentPanel]->ShowLine(m_CurrentLine);
}

void Game::LoadGame(Button* _buttonCallback) {

	if (m_save == nullptr) {

		m_save = new Save();
	}
	std::string test;
	m_save->Deserialize();
}

void Game::LoadStoryBoard() {

	//TODO while schleifen benutzen
	for (int i = 0; i < m_keywords.size(); i++) {

		if (m_keywords[i] == "Panel" && m_keywords[i + 1] == "{") {

			Panel* newPanel = new Panel(m_TextBox, m_ImageLoader);
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

					SpritePosition spritePosition;
					spritePosition.Index = std::stoi(m_keywords[i + 1]);
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
							std::string newName;
							void* newValue = new void*;

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
									}
									else if (m_keywords[i + 1] == "variable") {

										newType = DataValueType::variable;
										i = i + 2;
									}
									else if (m_keywords[i + 1] == "decimal") {

										newType = DataValueType::decimal;
										i = i + 2;
									}
									else {
										//TODO Fehlermeldung
									}
									continue;
								}
								if (m_keywords[i] == "Value:") {

									switch (newType) {
									case DataValueType::trigger:
										if (m_keywords[i + 1] == "true") {

											(*(bool*)newValue) = true;
										}
										else {

											(*(bool*)newValue) = false;
										}
										i = i + 2;
										break;
									case DataValueType::variable:
										(*(int*)newValue) = atoi(m_keywords[i + 1].c_str());
										i = i + 2;
										break;
									case DataValueType::decimal:
										(*(float*)newValue) = (float)atof(m_keywords[i + 1].c_str());
										i = i + 2;
										break;
									}
									if (m_keywords[i] == "}") {
										i++;
										break;
									}
								}
								if (m_keywords[i] == "}") {
									newDataValue = new DataValue(newName, newType, newValue);
									newCondition->m_RequiredValues[newDataValue->m_name] = newDataValue;
									i++;
									break;
								}
							}
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
				if (m_keywords[i] == "Texts" && m_keywords[i + 1] == "{") {		//TODO Anführungszeichen entfernen
					i = i + 2;
					DialogueLine* newLine = new DialogueLine();
					for (i; i < m_keywords.size(); i) {

						if (m_keywords[i] == "Text:") {

							newLine = new DialogueLine();
							newLine->Name = m_keywords[i + 1];
							newLine->Text = m_keywords[i + 3];		//+3 weil sich zwischen dem Namen und dem Text ein Komma befindet
							i = i + 4;
							continue;
						}
						if (m_keywords[i] == ";") {

							newPanel->m_DialogueLines.push_back(newLine);
							i++;
							continue;
						}
						else if (m_keywords[i] == ",") {
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
							newPanel->m_DialogueLines.push_back(newLine);
							i++;
							spritePosition = { -1, -1, -1 };
						}
						if (m_keywords[i] == "}") {
							i++;
							break;
						}
					}
				}
				if (m_keywords[i] == "Animation_Placeholder") {
					i++;		//TODO Animation
					break;
				}
				if (m_keywords[i] == "}") {
					i++;
					break;
				}
			}
			m_PanelList.push_back(newPanel);
		}
	}

	for (int i = 0; i < m_PanelList.size(); i++) {

		m_PanelList[i]->LoadImages();
	}
}


void Game::LoadCustomMethod(Button * _buttonCallback) {

}

void Game::ChangeSettings(Settings * NewSettings) {

}

void Game::Gallery(Button * _buttonCallback) {

}

void Game::OpenOptions(Button * _buttonCallback) {

	m_CurrentMenu = m_OptionsMenu;
	m_CurrentMenu->Render();
}

void Game::Quit(Button * _buttonCallback) {

	SDL_FlushEvents(0, UINT32_MAX);
	SDL_Event* quitEvent = new SDL_Event();
	quitEvent->type = SDL_QUIT;
	//TODO nachfaregn, ob man schließen will. vlt noch zum Menü leiten
	SDL_PushEvent(quitEvent);
	return;
}

void Game::ChangeResolution(Button * _buttonCallback) {

	SDL_RenderClear(m_Renderer);

	for (int i = 0; i < m_OptionsMenu->m_MenuItems.size(); i++) {

		m_OptionsMenu->m_MenuItems[i].Button->HandleEvent(m_EventHandler);

		SDL_RenderPresent(m_Renderer);
	}
}

void Game::ToggleFullscreen(Button * _buttonCallback) {
}

void Game::OpenMainMenu(Button * _buttonCallback) {

	m_CurrentMenu = m_MainMenu;
	m_CurrentMenu->Render();
}

void Game::RenderCurrentMenu() {

	SDL_RenderClear(m_Renderer);

	m_CurrentMenu->Render();

	for (int i = 0; i < m_CurrentMenu->m_MenuItems.size(); i++) {

		m_CurrentMenu->m_MenuItems[i].Button->HandleEvent(m_EventHandler);

	}
	SDL_RenderPresent(m_Renderer);
}