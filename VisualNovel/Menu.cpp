#include "Menu.h"
#include "Game.h"
#define LastButton m_MenuItems[m_MenuItems.size()-1].Button

Menu::Menu(SDL_Renderer* _renderer, ImageLoader* _imageLoader, std::string _filePath) {

	m_ImageLoader = _imageLoader;
	m_Renderer = _renderer;
	m_Color = SDL_Color{ 0, 0, 0 };
	LoadMenu(_filePath);
	CreateMenu();
}

Menu::~Menu() {

	m_textLoader = new TextLoader();
}

void Menu::LoadMenu(std::string _filepath) {

	m_keywords = m_textLoader->LoadText("MainMenu.txt");
	auto test = 0;
	//TODO while schleifen benutzen
	for (int i = 0; i < m_keywords.size()-1; i++) {

		if (m_keywords[i] == "Menu" && m_keywords[i + 1] == "{") {
			i = i + 2;
			for (i; i < m_keywords.size(); i) {

				if (m_keywords[i] == ";") {

					i++;
					continue;
				}
				if (m_keywords[i] == "Name:") {

					m_Name = m_keywords[i + 1];
					i = i + 2;
					continue;
				}
				if (m_keywords[i] == "BGIndex:") {

					SpritePosition spritePosition;
					spritePosition.Index = std::stoi(m_keywords[i + 1]);
					m_SpriteIndices.push_back(spritePosition);
					i = i + 2;
					continue;
				}
				if (m_keywords[i] == "Items" && m_keywords[i + 1] == "{") {
					i = i + 2;
					for (i; i < m_keywords.size()-1; i) {

						if (m_keywords[i] == ";") {

							i++;
							continue;
						}

						if (m_keywords[i] == "Item" && m_keywords[i + 1] == "{") {

							MenuItem menuItem;
							i+=2;
							for (i; i < m_keywords.size()-1;) {

								if (m_keywords[i] == ";") {

									i++;
									continue;
								}
								if (m_keywords[i] == "Text:") {

									menuItem.ItemName = m_keywords[i + 1];
									i += 2;
									continue;
								}
								if (m_keywords[i] == "Button:") {

									menuItem.Button = new Button(m_Renderer);

									if (m_keywords[i + 1] == ";") {		//Kein Parameter = generischer Button
										i++;
										break;
										//TODO generische Buttons; optionale Texturen
									} else if (m_keywords[i + 2] == ";") {

										AutoWidth(menuItem.Button);
										menuItem.Button->Height = 50;
										menuItem.Button->Height = 100;
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 1]);
										i += 2;
										continue;
									} else if (m_keywords[i + 4] == ";") {

										AutoWidth(menuItem.Button);
										menuItem.Button->Height = std::stoi(m_keywords[i + 1]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 1]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 3]);
										i += 4;
										continue;
									} else if (m_keywords[i + 6] == ";") {

										AutoWidth(menuItem.Button);
										menuItem.Button->Height = std::stoi(m_keywords[i + 1]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 3]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 5]);
										i += 6;
										continue;
									} else if (m_keywords[i + 8] == ";") {

										menuItem.Button->PosX = std::stoi(m_keywords[i + 1]);
										menuItem.Button->PosY = std::stoi(m_keywords[i + 1]);
										menuItem.Button->Height = std::stoi(m_keywords[i + 3]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 5]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 7]);
										i += 8;
										continue;
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
								if (m_keywords[i] == "}") {
									i++;
									m_MenuItems.push_back(menuItem);
									break;
								}
							}
						}
						if (m_keywords[i] == "}") {
							i++;
							continue;
						}
					}
				}
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

	Render();
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
	m_BackgroundImage->Render(-1,-1,600,800);
	for (int i = 0; i < m_MenuItems.size(); i++) {

		m_MenuItems[i].Button->Render();
		m_MenuItems[i].Button->m_textTexture->Render();
	}
	SDL_RenderPresent(m_Renderer);
}

void Menu::loadFont(std::string _path) {

	m_font = TTF_OpenFont("OpenSans-Regular.ttf", 28);
}
