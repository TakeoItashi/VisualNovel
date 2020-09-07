#include "Game.h"
#include "Save.h"
#include "MenuItem.h"
#include "MenuItemType.h"
#include "Texture.h"
#include "ImageLoader.h"
#include "TextLoader.h"
#include "SpritePosition.h"
#include "Menu.h"
#include "Settings.h"
#include "MenuText.h"

#define LastButton m_MenuItems[m_MenuItems.size()-1]->Button

Menu::Menu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, Settings* _settings, std::string _filePath) {

	m_ImageLoader = _imageLoader;
	m_Renderer = _renderer;
	m_Color = SDL_Color{ 0, 0, 0 };
	m_settings = _settings;
}

Menu::~Menu() {

	m_textLoader = new TextLoader();
}

bool ButtonPress() {
	return true;
}

void Menu::LoadMenu(std::string _filepath) {

	m_keywords = m_textLoader->LoadText(_filepath);
	auto test = 0;
	//Last keyword is an empty string for some reason
	m_keywords.erase(m_keywords.end()-1);
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
					i = i + 3;
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
						if (m_keywords[i] == "Button" && m_keywords[i + 1] == "{") {

							MenuItem* menuItem;
							menuItem = new MenuItem();
							i += 2;
							for (i; i < m_keywords.size() - 1;) {
								// Check for the end of Line
								if (m_keywords[i] == ";") {

									i++;
									continue;
								}
								// Parse the Text displayed on the Button
								if (m_keywords[i] == "Text:") {

									menuItem->ItemName = m_keywords[i + 1];
									i += 2;
									continue;
								}
								// Parse the design of the button
								if (m_keywords[i] == "Button:") {

									menuItem->Button = new Button(m_Renderer);

									if (m_keywords[i + 1] == ";") {		//No Parameter = generic Button = Draw Texture from Color
										i++;
										break;
										//TODO generische Buttons; optionale Texturen
									// 1 Button Parameter = parameter is the texture index; generic Button of Position 50 on X Axis and 100 on y Axis
									} else if (m_keywords[i + 2] == ";") {

										AutoPositionButton(menuItem->Button);
										//TODO: Size Button
										menuItem->Button->TextureIndex = std::stoi(m_keywords[i + 1]);
										i += 2;
										continue;
										// 2 Button Parameters = first parameter is the position on x & y Axis; second parameter is the texture index 
									} else if (m_keywords[i + 4] == ";") {

										AutoPositionButton(menuItem->Button);
										menuItem->Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem->Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem->Button->TextureIndex = std::stoi(m_keywords[i + 3]);
										i += 4;
										continue;
										// 3 Button Parameters = first parameter is the Position on x Axis
										//						 second Parameter is the Position on y Axis
										//                       third parameter is the texture index 
									} else if (m_keywords[i + 6] == ";") {

										AutoPositionButton(menuItem->Button);
										menuItem->Button->Height = std::stoi(m_keywords[i + 1]);
										menuItem->Button->Width = std::stoi(m_keywords[i + 3]);
										menuItem->Button->TextureIndex = std::stoi(m_keywords[i + 5]);
										i += 6;
										continue;
										// 4 Button Parameters = first parameter is the Position on x Axis
										//						 second Parameter is the Position on y Axis
										//                       third parameter is the width and height
										//                       fourth parameter is the texture index 
									} else if (m_keywords[i + 8] == ";") {

										menuItem->Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem->Button->PosY = std::stoi(m_keywords[i + 3]);
										menuItem->Button->Height = std::stoi(m_keywords[i + 5]);
										menuItem->Button->Width = std::stoi(m_keywords[i + 5]);
										menuItem->Button->TextureIndex = std::stoi(m_keywords[i + 7]);
										i += 8;
										continue;
										// 4 Button Parameters = first parameter is the Position on x Axis
										//						 second Parameter is the Position on y Axis
										//                       third parameter is the height
										//                       fourth parameter is the width
										//                       fifth parameter is the texture index 
									} else if (m_keywords[i + 10] == ";") {

										menuItem->Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem->Button->PosY = std::stoi(m_keywords[i + 3]);
										menuItem->Button->Height = std::stoi(m_keywords[i + 5]);
										menuItem->Button->Width = std::stoi(m_keywords[i + 7]);
										menuItem->Button->TextureIndex = std::stoi(m_keywords[i + 9]);
										i += 10;
										continue;
									} else {
										//TODO Parser Exceptions
										return;
									}
								}
								// Check for
								if (m_keywords[i] == "Type:") {

									AddFunctions(menuItem->Button, atoi(m_keywords[i + 1].c_str()));
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
						if (m_keywords[i] == "Text" && m_keywords[i + 1] == "{") {
							MenuText* menuText;
							i += 2;
							//TODO: Parse Texts
						}
						// Check for the end of the Items
						if (m_keywords[i] == "}") {
							i++;
							continue;
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
	}
}

void Menu::CreateMenu(Settings* _settings) {

	m_BackgroundImage = m_ImageLoader->GetTexture(m_SpriteIndices[0].Index);
	m_font = _settings->m_Font;
	std::vector<int> m_indices;
	for (int i = 0; i < m_MenuItems.size(); i++) {

		Texture* m_textTexture = new Texture(m_Renderer);

		m_MenuItems[i]->Button->SetSprite(m_ImageLoader->GetSprite(m_MenuItems[i]->Button->TextureIndex));
		SDL_Surface* textSurface = TTF_RenderText_Blended(m_font, m_MenuItems[i]->ItemName.c_str(), m_Color);
		m_textTexture->CreateFromSurface(textSurface);

		m_MenuItems[i]->Button->m_textTexture = m_textTexture;
		TTF_SizeText(m_font, m_MenuItems[i]->ItemName.c_str(), &m_MenuItems[i]->Button->Width, &m_MenuItems[i]->Button->Height);	//TODO Breite des Textes an Button oder vice versa anpassen
		m_textTexture->PosX = m_MenuItems[i]->Button->PosX;
		m_textTexture->PosY = m_MenuItems[i]->Button->PosY;
		SDL_FreeSurface(textSurface);
	}

	m_Sprites = m_ImageLoader->GetTextures(m_indices);
}

void Menu::AutoPositionButton(Button* _button, int _previousElementIndex) {

	if (m_MenuItems.size() == 0) {

		_button->PosX = 50;
		_button->PosY = 50;
	} else {
		_button->PosX = LastButton->PosX;
		_button->PosY = LastButton->PosY + LastButton->Height + 10;
	}
}

void Menu::Render() {

	SDL_RenderClear(m_Renderer);
	m_BackgroundImage->Render(0, 0, m_settings->m_WindowHeight, m_settings->m_WindowWidth);
	for (int i = 0; i < m_MenuItems.size(); i++) {

		m_MenuItems[i]->Button->Render();
		m_MenuItems[i]->Button->m_textTexture->Render();
	}
	SDL_RenderPresent(m_Renderer);
}

void Menu::AddFunctions(Button* _button, int _type) {

	switch (_type) {
		case MenuItemType::StartGame:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->NewGame, _button);
			break;
		case MenuItemType::LoadGame:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->LoadGame, _button);
			break;
		case MenuItemType::Gallery:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->Gallery, _button);
			break;
		case MenuItemType::Options:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->OpenOptions, _button);
			break;
		case MenuItemType::QuitGame:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->Quit, _button);
			break;
		case MenuItemType::Resolution:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->ChangeResolution, _button);
			break;
		case MenuItemType::Fullscreen:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->ToggleFullscreen, _button);
			break;
		case MenuItemType::Back:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->OpenMainMenu, _button);
			break;
		case MenuItemType::Custom:
			_button->m_delegateFunction = std::bind(&Game::GetInstance()->LoadCustomMethod, _button);
			//TODO implement custom Main menu Buttons
			break;
	}
}