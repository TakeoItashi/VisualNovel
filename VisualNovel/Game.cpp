#include "Game.h"

Game::Game(Settings* _initialSettings) {

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

void Game::Init() {

	SDL_Init(SDL_INIT_VIDEO);
	IMG_Init(IMG_INIT_PNG);
	TTF_Init();

	m_Window = SDL_CreateWindow("Visual Novel", 100, 50, 800, 600, SDL_WINDOW_SHOWN);
	m_Renderer = SDL_CreateRenderer(m_Window, -1, SDL_RENDERER_ACCELERATED);

	SDL_SetRenderDrawColor(m_Renderer, 0xFF, 0xFF, 0xFF, 0xFF);

	//TODO Game Loop? In der Init?

	m_ImageLoader = new ImageLoader(m_Renderer);
	m_ImageLoader->LoadTextures();

	m_TextBox = new TextBox(m_Renderer);
	m_TextBox->ApplySettings("");
	m_TextBox->loadFont();

	std::vector<int> testIndicies;

	testIndicies.push_back(0);
	//testIndicies.push_back(1);

	m_textLoader = new TextLoader();
	m_keywords = m_textLoader->LoadText("Storyboard.txt");

	m_MainMenu = new MainMenu(m_ImageLoader, "MainMenu.txt");
	Load();
}

void Game::NewGame() {


}

void Game::Update() {

	m_CurrentLine++;

	if (m_CurrentLine >= m_PanelList[m_CurrentPanel]->m_DialogueLines.size()) {

		m_CurrentPanel++;
		m_CurrentLine = 0;
	}
}

void Game::Render() {

	SDL_RenderClear(m_Renderer);

	m_PanelList[m_CurrentPanel]->ShowLine(m_CurrentLine);
}

void Game::Load() {

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
				if (m_keywords[i] == "Sprites" && m_keywords[i + 1] == "{") {
					i = i + 2;
					for (i; i < m_keywords.size(); i) {

						if (m_keywords[i] == ";") {

							i++;
							continue;
						}

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

void Game::ChangeSettings(Settings* NewSettings) {

}