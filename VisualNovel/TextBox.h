#pragma once
#include "Texture.h"
#include <string>
#include <SDL_ttf.h>
class TextBox {

	public:
		TextBox(SDL_Renderer* _renderer);
		~TextBox();
		
		int Width;
		int Height;

		/**
		Renders this Textbox with it's according text
		@param _text: The Text that is supposed to be displayed inside the Textbox 
		*/
		void Render(std::string _text);
		void loadFont();
		void ApplyBackgroundSettings();
	private:
		SDL_Renderer * m_renderer;
		Texture* m_boxBackground;
		Texture* m_textTexture;
		TTF_Font* m_font;
		//TODO Color is defined in Settings
		SDL_Color m_Color;
};