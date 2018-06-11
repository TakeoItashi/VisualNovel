#pragma once
#include <string>
#include <list>
class DialogueLine {

	public:
		DialogueLine();
		DialogueLine(std::string _name, std::string _text, std::list<int> _spriteIndicies);
		~DialogueLine();

		std::string Name;
		std::string Text;
		std::list<int> SpriteIndicies;
};