#pragma once
#include <string>
#include <vector>
#include "ShownItem.h"

class SpritePosition;

class DialogLine : public ShownItem {

	public:
		//Standard Constructor
		//Creates a new Instance of a Button
		DialogLine();
		//Standard Deconstrucor
		~DialogLine();
		//The Name Displayed on top of the Textbox
		std::string Name;
		//The Text displayed inside of the Textbox
		std::string Text;
		//The Sprite Positions of the sprites shown in The dialogue
		std::vector<SpritePosition> m_SpritesShown;
};