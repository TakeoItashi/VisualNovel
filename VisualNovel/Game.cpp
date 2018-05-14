#include "Game.h"

Game::Game(Settings* _initialSettings) {

}

Game::~Game() {

}

void Game::Init() {
	SDL_Init(SDL_INIT_VIDEO);
	IMG_Init(IMG_INIT_PNG);
	m_Window = SDL_CreateWindow("Visual Novel", 100, 50, 600, 800, SDL_WINDOW_SHOWN);
	m_ScreenSurface = SDL_GetWindowSurface(m_Window);
}

void Game::NewGame() {
	m_Background = IMG_Load("C:\\Users\\root\\Desktop\\Projekte\\VisualNovel\\VisualNovel\\wallpaper.jpg");
	m_ScreenSurface = m_Background;
}

void Game::Load() {

}

void Game::ChangeSettings(Settings* NewSettings) {

}
