#pragma once
#include <string>
#include <vector>
#include "Texture.h"
#include "TextBox.h"
#include "DialogueLine.h"
#include "SpritePosition.h"
#include "ImageLoader.h"
class Panel {

	public:
		Panel(SDL_Renderer* _renderer, TextBox* _textBoxReference, ImageLoader* _textLoaderReference);
		Panel(SDL_Renderer* _renderer, std::vector<Texture*> _Images, TextBox* _text_textBoxReference, ImageLoader* _ImageLoaderReference);
		~Panel();

		Texture* m_BackgroundImage = nullptr;
		SDL_Renderer* m_Renderer = nullptr;
		TextBox* m_TextBox = nullptr;
		ImageLoader* m_ImageLoader;
		std::string m_PanelName;
		std::vector<DialogueLine*> m_DialogueLines;
		std::vector<SpritePosition> m_SpriteIndexList;
		std::vector<Texture*> m_SpriteList;
		/**
		Displays the Line corresponding to the index that is handed over
		@param _lineIndex: The Index of the Line in this Panel
		*/
		void ShowLine(int _lineIndex);
		void LoadImages();
};