#pragma once
#include <string>
#include <list>
#include <vector>
#include "SpritePosition.h"

class DialogueLine {

	public:
		//Standard Constructor
		//Creates a new Instance of a Button
		DialogueLine();
		//Standard Deconstrucor
		~DialogueLine();
		//The Name Displayed on top of the Textbox
		std::string Name;
		//The Text displayed inside of the Textbox
		std::string Text;
		//The Sprite Positions of the sprites shown in The dialogue
		std::vector<SpritePosition> m_SpritesShown;
};