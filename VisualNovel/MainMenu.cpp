#include "MainMenu.h"
#include "MenuItemType.h"
#include "Game.h"

MainMenu::MainMenu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, std::string _filePath) : Menu(_renderer, _imageLoader, _filePath) {

	LoadMenu(_filePath);
	CreateMenu();
}

MainMenu::~MainMenu() {

}