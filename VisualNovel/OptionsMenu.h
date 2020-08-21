#pragma once
#include <string>
#include <SDL.h>
#include "Menu.h"

class SDL_Renderer;
class ImageLoader;
class Settings;

class OptionsMenu : public Menu {
public:
	OptionsMenu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, Settings* _settings, std::string _filePath);
	~OptionsMenu();
};