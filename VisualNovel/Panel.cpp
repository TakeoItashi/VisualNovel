#include "Panel.h"

Panel::Panel(SDL_Renderer* _renderer, TextBox* _textBoxReference, ImageLoader* _imageLoaderReference) {

	m_Renderer = _renderer;
	m_TextBox = _textBoxReference;
	m_ImageLoader = _imageLoaderReference;
}

Panel::Panel(SDL_Renderer* _renderer, std::vector<Texture*> _imagePaths, TextBox* _textBoxReference, ImageLoader* _ImageLoaderReference) {

	m_Renderer = _renderer;
	//Background Image is always the first in the list
	m_TextBox = _textBoxReference;
}

Panel::~Panel() {

	m_BackgroundImage = NULL;

	for (int i = 0; i < m_SpriteList.size(); i++) {

		m_SpriteList[i]->Free();
	}

	m_DialogueLines.shrink_to_fit();
	m_SpriteList.shrink_to_fit();
	m_Renderer = NULL;
}

void Panel::ShowLine(int _lineIndex) {

	//m_DialogueLines[_lineIndex]->SpriteIndicies[0];

	//Sprites rendern
	m_BackgroundImage->Render(0, 0, 600, 800);

	//TODO Magic Numbers entfernen :D
	int widthRatio = 600 / 2;
	int HeightRatio = ((800 / 4) * 3)-100;
	if (m_SpriteList.size() > 0) {
		for (int i = 0; i < m_SpriteList.size(); i++) {

			//TODO alle Sprites an der richtigen Stelle rendern
			m_SpriteList[i]->Render(50, 50, HeightRatio, widthRatio);
		}
	}
	//TODO currentLine parsen um Sprite Positionen rauszufinden

	//TODO Text anzeigen
	m_TextBox->Render(m_DialogueLines[_lineIndex]->Text);
}

void Panel::LoadImages() {

	m_SpriteList = m_ImageLoader->GetTextures(m_SpriteIndexList);
	m_BackgroundImage = m_SpriteList[m_SpriteIndexList[0].Index];
}
