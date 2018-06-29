#pragma once
#include "Menu.h"

class MainMenu : public Menu {
public:
	MainMenu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, std::string _filePath);
	~MainMenu();

	void StartGame();
	void LoadGame();
	void OpenGallery();
	void OpenSettings();
	void Quit();
};

