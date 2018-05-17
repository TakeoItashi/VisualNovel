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

		void ShowLine(int _lineIndex);
};