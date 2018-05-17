#include "Game.h"

Game::Game(Settings* _initialSettings) {

}

Game::~Game() {

	SDL_DestroyTexture(m_Texture);
	SDL_DestroyRenderer(m_Renderer);
	SDL_DestroyWindow(m_Window);
	delete m_ImageLoader;
	m_Texture = NULL;
	m_Renderer = NULL;
	m_Window = NULL;
	m_ImageLoader = NULL;
	IMG_Quit();
	SDL_Quit();
}

void Game::Init() {

	SDL_Init(SDL_INIT_VIDEO);
	IMG_Init(IMG_INIT_PNG);

	m_Window = SDL_CreateWindow("Visual Novel", 100, 50, 800, 600, SDL_WINDOW_SHOWN);
	m_Renderer = SDL_CreateRenderer(m_Window, -1, SDL_RENDERER_ACCELERATED);
	
	SDL_SetRenderDrawColor(m_Renderer, 0xFF, 0xFF, 0xFF, 0xFF);
	m_ScreenSurface = SDL_GetWindowSurface(m_Window);

	m_ImageLoader = new ImageLoader(m_Renderer);
	m_ImageLoader->LoadTextures();
	
	std::vector<int> testIndicies;

	testIndicies.push_back(0);
	testIndicies.push_back(1);
	Panel* newPanel = new Panel(m_Renderer, m_ImageLoader->GetTextures(testIndicies));
	m_PanelList.push_back(newPanel);
}

void Game::NewGame() {


}

void Game::Update() {

	
}

void Game::Render() {

	SDL_RenderClear(m_Renderer);
	m_PanelList[0]->ShowLine(0);
}

void Game::Load() {

}

void Game::ChangeSettings(Settings* NewSettings) {

}