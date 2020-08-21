#include "Texture.h"
#include "TextBox.h"
#include "DialogLine.h"
#include "SpritePosition.h"
#include "ImageLoader.h"
#include "Condition.h"
#include "Panel.h"
#include "Button.h"
#include "SplitDecision.h"
#include "Branch.h"

#define CurrentSprite currentLine->m_SpritesShown[i]

Panel::Panel(TextBox* _textBoxReference, ImageLoader* _imageLoaderReference) {

	m_TextBox = _textBoxReference;
	m_ImageLoader = _imageLoaderReference;
}

Panel::~Panel() {

	m_BackgroundImage = NULL;

	for (int i = 0; i < m_SpriteList.size(); i++) {

		m_SpriteList[i]->Free();
	}

	m_Branches.clear();
	m_SpriteList.shrink_to_fit();
}

void Panel::ShowLine(int _lineIndex) {

	m_BackgroundImage->Render(0, 0, 600, 800);

	int aviableSpriteSpace;

	//Find out Default Sprite offset
	DialogLine* currentLine = (DialogLine*)m_Branches[m_currentBranchKey]->m_shownItems[_lineIndex];
	if (currentLine->m_SpritesShown.size() > 0) {

		//Hintergrund Breite herausfinden
		int BackgroundWidth = 800;
		int Padding = 20;
		//Padding addieren
		int AviableWidth = BackgroundWidth;// -Padding;
		//Bilder um (BreiteHintergrund / (Anzahl Sprites + 1)) - (BreiteSprite/2) verschieben
		aviableSpriteSpace = (AviableWidth / (currentLine->m_SpritesShown.size() + 1));
	}

	//Sprites anzeigen
	if (currentLine->m_SpritesShown.size() != 0) {
		//TODO Magic Numbers entfernen
		int widthRatio = 600 / 2;
		int HeightRatio = ((800 / 4) * 3) - 100;
		int SpritePosX = 0;
		int SpritePosY = 0;		//TODO Padding für die Sprites überarbeiten
		if (m_SpriteList.size() > 0) {
			for (int i = 0; i < currentLine->m_SpritesShown.size(); i++) {

				if (CurrentSprite.PosX < 0) {

					SpritePosX = (aviableSpriteSpace * (i + 1)) - (widthRatio / 2);		//TODO richtige Textur Width benutzen
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

	m_TextBox->Render((*currentLine));
}

void Panel::ShowSplit(int _lineIndex) {
	m_BackgroundImage->Render(0, 0, 600, 800);

	SplitDecision* currentSplit = (SplitDecision*)m_Branches[m_currentBranchKey]->m_shownItems[_lineIndex];
	currentSplit->CreateButtons();
	currentSplit->RenderOptions();
}

void Panel::LoadImages() {

	std::vector<int> indices;

	for (int i = 0; i < m_SpriteIndexList.size(); i++) {

		indices.push_back(m_SpriteIndexList[i].Index);
	}
	std::sort(indices.begin(), indices.end());
	m_SpriteList = m_ImageLoader->GetTextures(indices);
	m_BackgroundImage = m_SpriteList[m_BackgroundImageIndex]; //Background Sprite should be first in the m_SpriteIndexList, so it should be first here too
}

void Panel::RenderCurrentSplit(int _lineIndex) {

	m_BackgroundImage->Render(0, 0, 600, 800);

	SplitDecision* currentSplit = (SplitDecision*)m_Branches[m_currentBranchKey]->m_shownItems[_lineIndex];
	currentSplit->RenderOptions();
}

Branch* Panel::GetCurrentBranch() {

	return m_Branches[m_currentBranchKey];
}
