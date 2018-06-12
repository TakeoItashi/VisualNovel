#pragma once
#include <string>
#include <list>
#include <vector>
#include "SpritePosition.h"
class DialogueLine {

	public:
		DialogueLine();
		DialogueLine(std::string _name, std::string _text, std::list<int> _spriteIndicies);
		~DialogueLine();

		std::string Name;
		std::string Text;
		std::vector<SpritePosition> m_SpritesShown;
};