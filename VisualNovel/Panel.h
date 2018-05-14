#pragma once
#include <string>
#include <list>

class Panel {

	public:
		Panel(int ArraySize);
		~Panel();

		std::list<std::string> DialogueLines[];

		void ShowNextLine();
};