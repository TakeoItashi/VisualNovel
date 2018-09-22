#pragma once
#include "Menu.h"
#include "MenuItemType.h"

class MainMenu : public Menu {
public:
	MainMenu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, std::string _filePath);
	~MainMenu();
};