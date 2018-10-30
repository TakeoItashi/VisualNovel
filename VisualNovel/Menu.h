#pragma once
#include <vector>
#include <string>
#include <functional>
#include <SDL_ttf.h>

class ImageLaoder;
class Texture;
class Button;
class TextLoader;
class ImageLoader;
class MenuItem;
class SpritePosition;

class Menu{
	//TODO Menu von Panel erben lassen?
public:
	Menu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, std::string _filePath);
	~Menu();

	std::string m_Name;
	std::vector<MenuItem> m_MenuItems;
	std::vector<SpritePosition> m_SpriteIndices;
	SDL_Renderer* m_Renderer;
	Texture* m_BackgroundImage;
	ImageLoader* m_ImageLoader;
	std::vector<Texture*> m_Sprites;
	void CreateMenu();
	void LoadMenu(std::string _filepath);
	void AutoWidth(Button* _button);
	void Render();
	void loadFont(std::string _path = "");
	virtual void AddFunctions(Button* _button, int _type);

private:

	TTF_Font* m_font;
	SDL_Color m_Color;
	TextLoader* m_textLoader;
	std::vector<std::string> m_keywords;
	std::vector<Texture*> m_Texture;
};