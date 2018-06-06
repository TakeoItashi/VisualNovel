#pragma once
#include <string>
#include <vector>
#include "Texture.h"

class Panel {

	public:
		Panel(SDL_Renderer* _renderer, std::vector<Texture*> _Images);
		~Panel();

		int m_currentLine;
		Texture* m_BackgroundImage;
		std::vector<std::string> m_DialogueLines;
		std::vector<Texture*> m_SpriteList;
		SDL_Renderer* m_Renderer;
		/**
		Displays the Line corresponding to the index that is handed over
		@param _lineIndex: The Index of the Line in this Panel
		*/
		void ShowLine(int _lineIndex);
};