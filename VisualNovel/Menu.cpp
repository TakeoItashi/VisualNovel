#include "Menu.h"
#include "Game.h"
#include "Save.h"
#include <functional>
#define LastButton m_MenuItems[m_MenuItems.size()-1].Button

Menu::Menu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, std::string _filePath) {

	m_ImageLoader = _imageLoader;
	m_Renderer = _renderer;
	m_Color = SDL_Color{ 0, 0, 0 };
}

Menu::~Menu() {

	m_textLoader = new TextLoader();
}

bool ButtonPress() {
	return true;
}

void Menu::LoadMenu(std::string _filepath) {

	m_keywords = m_textLoader->LoadText("MainMenu.txt");
	auto test = 0;
	//TODO while schleifen benutzen
	for (int i = 0; i < m_keywords.size() - 1; i++) {

		if (m_keywords[i] == "Menu" && m_keywords[i + 1] == "{") {
			i = i + 2;
			for (i; i < m_keywords.size(); i) {

				// Check for end of Line
				if (m_keywords[i] == ";") {

					i++;
					continue;
				}
				// Parse the Name
				if (m_keywords[i] == "Name:") {

					// The Name is the following keyword 
					m_Name = m_keywords[i + 1];
					i = i + 2;
					continue;
				}
				// Parse the BG Image Index
				if (m_keywords[i] == "BGIndex:") {

					// Create a Sprite Position for the Background. Only the index is used, because the BG Image takes up the whole screen
					SpritePosition spritePosition;
					spritePosition.Index = std::stoi(m_keywords[i + 1]);
					m_SpriteIndices.push_back(spritePosition);
					i = i + 2;
					continue;
				}
				// Parse the Menu Items
				if (m_keywords[i] == "Items" && m_keywords[i + 1] == "{") {
					i = i + 2;
					// Iterate over all Buttons
					for (i; i < m_keywords.size() - 1; i) {
						// Check for end of Line
						if (m_keywords[i] == ";") {

							i++;
							continue;
						}
						// Parse the Button
						if (m_keywords[i] == "Item" && m_keywords[i + 1] == "{") {

							MenuItem menuItem;
							i += 2;
							for (i; i < m_keywords.size() - 1;) {
								// Check for the end of Line
								if (m_keywords[i] == ";") {

									i++;
									continue;
								}
								// Parse the Text displayed on the Button
								if (m_keywords[i] == "Text:") {

									menuItem.ItemName = m_keywords[i + 1];
									i += 2;
									continue;
								}
								// Parse the design of the button
								if (m_keywords[i] == "Button:") {

									std::function<void(Button*)> f;
									menuItem.Button = new Button(m_Renderer, f);

									if (m_keywords[i + 1] == ";") {		//No Parameter = generic Button
										i++;
										break;
										//TODO generische Buttons; optionale Texturen
									// 1 Button Parameter = parameter is the texture index; generic Button of Position 50 on X Axis and 100 on y Axis
									} else if (m_keywords[i + 2] == ";") {

										AutoWidth(menuItem.Button);
										menuItem.Button->PosX = 50;
										menuItem.Button->PosY = 100;
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 1]);
										i += 2;
										continue;
										// 2 Button Parameters = first parameter is the position on x & y Axis; second parameter is the texture index 
									} else if (m_keywords[i + 4] == ";") {

										AutoWidth(menuItem.Button);
										menuItem.Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem.Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 3]);
										i += 4;
										continue;
										// 3 Button Parameters = first parameter is the Position on x Axis
										//						 second Parameter is the Position on y Axis
										//                       third parameter is the texture index 
									} else if (m_keywords[i + 6] == ";") {

										AutoWidth(menuItem.Button);
										menuItem.Button->Height = std::stoi(m_keywords[i + 1]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 3]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 5]);
										i += 6;
										continue;
										// 4 Button Parameters = first parameter is the Position on x Axis
										//						 second Parameter is the Position on y Axis
										//                       third parameter is the width and height
										//                       fourth parameter is the texture index 
									} else if (m_keywords[i + 8] == ";") {

										menuItem.Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem.Button->PosY = std::stoi(m_keywords[i + 3]);
										menuItem.Button->Height = std::stoi(m_keywords[i + 5]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 5]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 7]);
										i += 8;
										continue;
										// 4 Button Parameters = first parameter is the Position on x Axis
										//						 second Parameter is the Position on y Axis
										//                       third parameter is the height
										//                       fourth parameter is the width
										//                       fifth parameter is the texture index 
									} else if (m_keywords[i + 10] == ";") {

										menuItem.Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem.Button->PosY = std::stoi(m_keywords[i + 3]);
										menuItem.Button->Height = std::stoi(m_keywords[i + 5]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 7]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 9]);
										i += 10;
										continue;
									} else {
										//TODO Parser Exceptions
										return;
									}
								}
								// Check for
								if (m_keywords[i] == "Type:") {



									AddFunctions(menuItem.Button, atoi(m_keywords[i + 1].c_str()));
									i += 2;
									continue;
								}
								// Check for end of Iteration
								if (m_keywords[i] == "}") {
									i++;
									m_MenuItems.push_back(menuItem);
									break;
								}
							}
						}
						// Check for the end of the Items
						if (m_keywords[i] == "}") {
							i++;
							continue;
						}
					}
				}
				// Check for the End of the File
				if (m_keywords[i] == "}") {
					i++;
					break;
				}
			}
		}
	}
	loadFont("");
}

void Menu::CreateMenu() {

	m_BackgroundImage = m_ImageLoader->GetTextures(m_SpriteIndices[0].Index);
	std::vector<int> m_indices;
	for (int i = 0; i < m_MenuItems.size(); i++) {

		Texture* m_textTexture = new Texture(m_Renderer);

		m_MenuItems[i].Button->SetTexture(m_ImageLoader->GetTextures(m_MenuItems[i].Button->TextureIndex));
		SDL_Surface* textSurface = TTF_RenderText_Blended(m_font, m_MenuItems[i].ItemName.c_str(), m_Color);
		m_textTexture->CreateFromSurface(textSurface);

		m_MenuItems[i].Button->m_textTexture = m_textTexture;
		TTF_SizeText(m_font, m_MenuItems[i].ItemName.c_str(), &m_textTexture->Width, &m_textTexture->Height);	//TODO Breite des Textes an Button oder vice versa anpassen
		m_textTexture->PosX = m_MenuItems[i].Button->PosX;
		m_textTexture->PosY = m_MenuItems[i].Button->PosY;
		SDL_FreeSurface(textSurface);
	}

	m_Sprites = m_ImageLoader->GetTextures(m_indices);

	SDL_RenderClear(m_Renderer);
	Render();
	SDL_RenderPresent(m_Renderer);
}

void Menu::AutoWidth(Button* _button) {

	if (m_MenuItems.size() == 0) {

		_button->PosX = 50;
		_button->PosY = 50;
	} else {
		_button->PosX = LastButton->PosX;
		_button->PosY = LastButton->PosY + LastButton->Height + 10;
	}
}

void Menu::Render() {
	m_BackgroundImage->Render(0, 0, 600, 800);
	for (int i = 0; i < m_MenuItems.size(); i++) {

		m_MenuItems[i].Button->Render();
		m_MenuItems[i].Button->m_textTexture->Render();
	}
	SDL_RenderPresent(m_Renderer);
}

void Menu::loadFont(std::string _path) {
	//TODO allow other fonts
	m_font = TTF_OpenFont("OpenSans-Regular.ttf", 28);
}

void Menu::AddFunctions(Button* _button, int _type) {

	if (_type == MenuItemType::StartGame) {

		_button->m_delegateFunction = std::bind(&Game::GetInstance()->NewGame, _button);
		//_button->m_delegateFunction = []() {Game::GetInstance()->NewGame(_button)};
	} else if (_type == MenuItemType::LoadGame) {

		_button->m_delegateFunction = std::bind(&Game::GetInstance()->LoadGame, _button);
	} else if (_type == MenuItemType::Gallery) {

		_button->m_delegateFunction = std::bind(&Game::GetInstance()->Gallery, _button);
		//_button->m_delegateFunction = std::bind(&Game::Gallery, _button);
	} else if (_type == MenuItemType::Options) {

		_button->m_delegateFunction = std::bind(&Game::GetInstance()->Gallery, _button);
	} else if (_type == MenuItemType::QuitGame) {

		_button->m_delegateFunction = std::bind(&Game::GetInstance()->Quit, _button);
	} else if (_type == MenuItemType::Custom) {

		_button->m_delegateFunction = std::bind(&Game::GetInstance()->LoadCustomMethod, _button);
		//TODO implement custom Main menu Buttons
	}
}
