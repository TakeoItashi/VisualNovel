#pragma once
#include "Menu.h"

class MainMenu : public Menu {
public:
	MainMenu(ImageLoader* _imageLoader, std::string _filePath);
	~MainMenu();
};

