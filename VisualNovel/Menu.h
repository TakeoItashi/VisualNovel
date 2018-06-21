#pragma once
#include <vector>
#include <string>
#include "MenuItem.h"
#include "Texture.h"
#include "ImageLoader.h"
#include "TextLoader.h"

class Menu{

public:
	Menu(ImageLoader* _imageLoader, std::string _filePath);
	~Menu();

	std::string m_Name;
	std::vector<MenuItem> m_MenuItems;
	std::vector<SpritePosition> m_SpriteIndices;
	Texture* m_BackgroundImage;
	ImageLoader* m_ImageLoader;
private:

	void LoadMenu(std::string _filepath);
	void CreateMenu();
	void AutoWidth(Button* _button);

	TextLoader* m_textLoader;
	std::vector<std::string> m_keywords;
};