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
			   _speed: The Speed at which the Text is supposed to appear
		*/
		void Render(std::string _text, int _speed = 0);
		/**
		Loads the specified Font for the TextBox
		@param _path: The filepath of the Font
		*/
		void loadFont(std::string _path = "");
		/**
		Loads the Settings for the Textbox and spplies them
		@param _settings: The Settings for the TextBox
		*/
		void ApplySettings(std::string _settings);
	private:
		SDL_Renderer* m_renderer;
		Texture* m_boxBackground;
		Texture* m_textTexture;
		TTF_Font* m_font;
		//TODO Color is defined in Settings
		SDL_Color m_Color;
};