#include "OptionsMenu.h"

OptionsMenu::OptionsMenu(SDL_Renderer * _renderer, ImageLoader * _imageLoader, std::string _filePath) : Menu(_renderer, _imageLoader, _filePath) {

	LoadMenu(_filePath);
	CreateMenu();
}

OptionsMenu::~OptionsMenu() {

}