#pragma once
#include "Menu.h"

class MainMenu : public Menu {
public:
	MainMenu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, std::string _filePath);
	~MainMenu();

	void StartGame();
	void LoadGame();
	void Gallery();
	void OpenOptions();
	void Quit();
	void AddFunctions(Button* _button, int _type) override;
};

