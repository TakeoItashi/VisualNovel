#pragma once
#include <vector>
#include <string>
#include <algorithm>
#include <SDL_ttf.h>

class DialogLine;
class Settings;
class Texture;

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
		void Render(DialogLine _text, int _speed = 0);
		/**
		Loads the specified Font for the TextBox
		@param _path: The filepath of the Font
		*/
		//TODO lade Font aus einem bestimmten Ordner. Vielleicht anderes System mit dem man mehrere Fonts definieren kann
		void loadFont(std::string _path = "");
		/**
		Loads the Settings for the Textbox and spplies them
		@param _settings: The Settings for the TextBox
		*/
		void ApplySettings(Settings* _settings);

		TTF_Font* GetFont();
	private:
		SDL_Renderer* m_renderer;
		Texture* m_boxBackground;
		Texture* m_boxBackgroundNameCorner;
		Texture* m_textTexture;
		TTF_Font* m_font;
		//TODO Color is defined in Settings
		SDL_Color m_Color;
};