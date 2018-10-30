#pragma once
#include <string>
#include <SDL.h>
#include "Menu.h"

class SDL_Renderer;
class ImageLoader;

class MainMenu : public Menu {
public:
	MainMenu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, std::string _filePath);
	~MainMenu();
};