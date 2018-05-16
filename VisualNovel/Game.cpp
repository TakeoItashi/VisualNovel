#include "Game.h"

Game::Game(Settings* _initialSettings) {

	m_PanelList->push_back(new Panel(m_Renderer, new std::vector<std::string>("")));
}

Game::~Game() {
	SDL_DestroyTexture(m_Texture);
	m_Texture = NULL;

	SDL_DestroyRenderer(m_Renderer);
	SDL_DestroyWindow(m_Window);
	m_Renderer = NULL;
	m_Window = NULL;

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
}

void Game::NewGame() {

	m_Texture = loadTexture("wallpaper.jpg");
}

void Game::Update() {

	
}

void Game::Render() {

	SDL_RenderClear(m_Renderer);

}

void Game::Load() {

}

//TODO Remove, implemented in Texture
SDL_Texture* Game::loadTexture(std::string path) {

	SDL_Texture* newTexture = NULL;
	SDL_Surface* loadedSurface = IMG_Load(path.c_str());
	newTexture = SDL_CreateTextureFromSurface(m_Renderer, loadedSurface);
	SDL_FreeSurface(loadedSurface);

	return newTexture;
}

void Game::ChangeSettings(Settings* NewSettings) {

}