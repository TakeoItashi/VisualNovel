#pragma once
#include <string>
#include <vector>
#include "Texture.h"

class Panel {

	public:
		Panel(SDL_Renderer* _renderer);
		Panel(SDL_Renderer* _renderer, std::vector<Texture*> _Images);
		~Panel();

		int m_currentLine;
		std::string m_PanelName;
		Texture* m_BackgroundImage = nullptr;
		std::vector<std::string> m_DialogueLines;
		std::vector<Texture*> m_SpriteList;
		SDL_Renderer* m_Renderer = nullptr;
		/**
		Displays the Line corresponding to the index that is handed over
		@param _lineIndex: The Index of the Line in this Panel
		*/
		void ShowLine(int _lineIndex);
};