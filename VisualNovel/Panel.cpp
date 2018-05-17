#include "Panel.h"

Panel::Panel(SDL_Renderer* _renderer, std::vector<Texture*> _imagePaths) {

	//m_BackgroundImage->LoadMedia(_imagePaths[0], _renderer);
	m_Renderer = _renderer;

	//Background Image is always the first in the list
	m_BackgroundImage = _imagePaths[0];
	_imagePaths.erase(_imagePaths.begin());
	m_SpriteList = _imagePaths;
}

Panel::~Panel() {

	m_currentLine = 0;
	m_BackgroundImage = NULL;

	for (int i = 0; i < m_SpriteList.size(); i++) {

		m_SpriteList[i]->Free();
	}

	m_DialogueLines.shrink_to_fit();
	m_SpriteList.shrink_to_fit();
	m_Renderer = NULL;
}

void Panel::ShowLine(int _lineIndex) {

	//Sprites rendern
	m_BackgroundImage->Render(0, 0, 800, 600, m_Renderer);

	//TODO Magic Numbers :D
	int widthRatio = 600 / 2;
	int HeightRatio = ((800 / 4) * 3)-100;
	m_SpriteList[0]->Render(50, 50, widthRatio, HeightRatio, m_Renderer);
		//currentLine parsen um Sprite Positionen rauszufinden

	//Text anzeigen
}
