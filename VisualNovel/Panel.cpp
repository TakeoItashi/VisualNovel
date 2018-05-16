#include "Panel.h"

Panel::Panel(SDL_Renderer* _renderer, std::vector<std::string> _imagePaths) {

	m_BackgroundImage->LoadMedia(_imagePaths[0], _renderer);
	m_Renderer = _renderer;
}

Panel::~Panel() {

	m_currentLine = 0;
	m_BackgroundImage = NULL;
	m_DialogueLines.shrink_to_fit();
	m_SpriteList.shrink_to_fit();
	m_Renderer = NULL;
}

void Panel::ShowLine(int _lineIndex) {

	//Sprites rendern
	m_BackgroundImage->Render(0, 0, m_Renderer);

	//Text anzeigen
}
