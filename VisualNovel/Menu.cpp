#include "Menu.h"
#define LastButton m_MenuItems[m_MenuItems.size()-1].Button

Menu::Menu(ImageLoader* _imageLoader, std::string _filePath) {

	m_ImageLoader = _imageLoader;
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

									menuItem.Button = new Button();

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
										break;
									} else if (m_keywords[i + 4] == ";") {

										AutoWidth(menuItem.Button);
										menuItem.Button->Height = std::stoi(m_keywords[i + 1]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 1]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 3]);
										i += 4;
										break;
									} else if (m_keywords[i + 6] == ";") {

										AutoWidth(menuItem.Button);
										menuItem.Button->Height = std::stoi(m_keywords[i + 1]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 3]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 5]);
										i += 6;
										break;
									} else if (m_keywords[i + 8] == ";") {

										menuItem.Button->xPos = std::stoi(m_keywords[i + 1]);
										menuItem.Button->yPos = std::stoi(m_keywords[i + 1]);
										menuItem.Button->Height = std::stoi(m_keywords[i + 3]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 5]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 7]);
										i += 8;
										break;
									} else if (m_keywords[i + 10] == ";") {

										menuItem.Button->xPos = std::stoi(m_keywords[i + 1]);
										menuItem.Button->yPos = std::stoi(m_keywords[i + 3]);
										menuItem.Button->Height = std::stoi(m_keywords[i + 5]);
										menuItem.Button->Width = std::stoi(m_keywords[i + 7]);
										menuItem.Button->TextureIndex = std::stoi(m_keywords[i + 9]);
										i += 10;
										break;
									} else {
										//TODO Parser Exceptions
										return;
									}
								}
								if (m_keywords[i] == "}") {
									m_MenuItems.push_back(menuItem);
									i++;
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
				test = 0;	
}

void Menu::CreateMenu() {
}

void Menu::AutoWidth(Button* _button) {

	if (m_MenuItems.size() == 0) {

		_button->xPos = 50;
		_button->yPos = 50;
	} else {
		_button->xPos = LastButton->xPos + LastButton->Height + 10;
		_button->yPos = LastButton->yPos;
	}
}