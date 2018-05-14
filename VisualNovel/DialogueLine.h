#pragma once
#include <string>

class DialogueLine {

	public:
		DialogueLine(std::string _name, std::string _text);
		~DialogueLine();

		std::string Name;
		std::string Text;
};