#include "Game.h"
#include "MenuItemType.h"
#include "ImageLoader.h"
#include "MainMenu.h"

MainMenu::MainMenu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, Settings* _settings, std::string _filePath) : Menu(_renderer, _imageLoader, _settings, _filePath) {

	LoadMenu(_filePath);
	CreateMenu(_settings);
}

MainMenu::~MainMenu() {

}