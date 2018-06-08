#include "Game.h"

Game::Game(Settings* _initialSettings) {

}

Game::~Game() {

	SDL_DestroyTexture(m_Texture);
	SDL_DestroyRenderer(m_Renderer);
	SDL_DestroyWindow(m_Window);
	delete m_ImageLoader;
	delete m_textLoader;
	m_Texture = nullptr;
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
	m_ScreenSurface = SDL_GetWindowSurface(m_Window);

	//TODO Game Loop

	m_ImageLoader = new ImageLoader(m_Renderer);
	m_ImageLoader->LoadTextures();

	m_TextBox = new TextBox(m_Renderer);
	m_TextBox->ApplySettings("");
	m_TextBox->loadFont();

	std::vector<int> testIndicies;

	testIndicies.push_back(0);
	//testIndicies.push_back(1);

	m_textLoader = new TextLoader();
	m_keywords = m_textLoader->LoadText();
	Load();
}

void Game::NewGame() {


}

void Game::Update() {

	
}

void Game::Render() {

	SDL_RenderClear(m_Renderer);
	m_PanelList[0]->ShowLine(0);
	m_TextBox->Render("This is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a TestThis is a Test");
}

void Game::Load() {

	for (int i = 0; i < m_keywords.size(); i++) {

		if (m_keywords[i] == "Panel" && m_keywords[i+1] == "{") {

			Panel* newPanel = new Panel(m_Renderer);
			i = i + 2;
			for (i; i < m_keywords.size(); i++) {

				if (m_keywords[i] == "Name:") {

					newPanel->m_PanelName = m_keywords[i + 1];
				}
			}

			m_PanelList.push_back(newPanel);
		}
	}
}

void Game::ChangeSettings(Settings* NewSettings) {

}