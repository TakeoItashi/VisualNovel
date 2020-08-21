#include "OptionsMenu.h"
#include "Settings.h"

OptionsMenu::OptionsMenu(SDL_Renderer * _renderer, ImageLoader * _imageLoader, Settings* _settings, std::string _filePath) : Menu(_renderer, _imageLoader, _settings, _filePath) {

	LoadMenu(_filePath);
	CreateMenu(_settings);
}

OptionsMenu::~OptionsMenu() {

}