#include "Panel.h"

#define CurrentSprite m_DialogueLines[_lineIndex]->m_SpritesShown[i]

Panel::Panel(TextBox* _textBoxReference, ImageLoader* _imageLoaderReference) {

	m_TextBox = _textBoxReference;
	m_ImageLoader = _imageLoaderReference;
}

Panel::~Panel() {

	m_BackgroundImage = NULL;

	for (int i = 0; i < m_SpriteList.size(); i++) {

		m_SpriteList[i]->Free();
	}

	m_DialogueLines.shrink_to_fit();
	m_SpriteList.shrink_to_fit();
}

void Panel::ShowLine(int _lineIndex) {

	m_BackgroundImage->Render(0, 0, 600, 800);

	int aviableSpriteSpace;

	//Find out Default Sprite offset
	if (m_DialogueLines[_lineIndex]->m_SpritesShown.size() > 0) {

		//Hintergrund Breite herausfinden
		int BackgroundWidth = 800;
		int Padding = 20;
		//Padding addieren
		int AviableWidth = BackgroundWidth;// -Padding;
		//Bilder um (BreiteHinter / (Anzahl Sprites + 1)) - (BreiteSprite/2) verschieben
		aviableSpriteSpace = (AviableWidth / (m_DialogueLines[_lineIndex]->m_SpritesShown.size() + 1));
	}

	//Sprites anzeigen
	if (m_DialogueLines[_lineIndex]->m_SpritesShown.size() != 0) {
		//TODO Magic Numbers entfernen
		int widthRatio = 600 / 2;
		int HeightRatio = ((800 / 4) * 3) - 100;
		int SpritePosX = 0;
		int SpritePosY = 0;		//TODO Padding für die Sprites überarbeiten
		if (m_SpriteList.size() > 0) {
			for (int i = 0; i < m_DialogueLines[_lineIndex]->m_SpritesShown.size(); i++) {

				if (CurrentSprite.PosX < 0) {

					int test = aviableSpriteSpace * (i + 1);
					int test2 = (widthRatio / 2);
					SpritePosX = (aviableSpriteSpace * (i+1)) - (widthRatio / 2);		//TODO richtige Textur Width benutzen
				} else {

					SpritePosX = CurrentSprite.PosX;
				}
				if (CurrentSprite.PosY < 0) {

					SpritePosY = 50;
				} else {

					SpritePosY = CurrentSprite.PosY;
				}
				m_SpriteList[CurrentSprite.Index]->Render(SpritePosX, SpritePosY, HeightRatio, widthRatio);
			}
		}
		//TODO currentLine parsen um Sprite Positionen rauszufinden

		//TODO Text anzeigen
	}

	m_TextBox->Render((*m_DialogueLines[_lineIndex]));
}

void Panel::LoadImages() {

	m_SpriteList = m_ImageLoader->GetTextures(m_SpriteIndexList);
	m_BackgroundImage = m_SpriteList[m_SpriteIndexList[0].Index];
}