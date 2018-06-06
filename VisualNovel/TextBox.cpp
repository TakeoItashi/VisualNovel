#include "TextBox.h"

#include <vector>

TextBox::TextBox(SDL_Renderer* _renderer)
{
	m_renderer = _renderer;
	//TODO richtige implementierung von color
	m_Color = SDL_Color{0, 0, 0};
	//TODO TextBox Gr��e und Position aus den Settings holen
	Width = 750;
	Height = 150;
	m_boxBackground = new Texture(_renderer);
}

TextBox::~TextBox()
{
}

void TextBox::Render(std::string _text, int _speed)
{
	if (m_textTexture != NULL) {

		m_textTexture->Free();
	}
	else {
		m_textTexture = new Texture(m_renderer);
	}

	std::vector<Texture*> TextCharsTexture;

	//TODO TextBox Position an Window Gr��e anpassen
	m_boxBackground->Render(25, 425, Height, Width);

	////TODO Rework der Text Darstellung f�r Animation: Create new Texture after DeltaTime has passed. New Texture uses one Char more than before
	//
	//for (int i = 0; i < _text.size(); i++) {
	//
	//	//const char** currentChar = new char(_text[i]);
	//	std::string parString;
	//	parString += _text[i];
	//	const char* currentChar = parString.c_str();
	//	SDL_Surface* textSurface = TTF_RenderText_Blended(m_font, currentChar, m_Color);		//TODO Gr��en Verh�ltnisse f�r alle chars heruasfinden mit TTF_Glyph Metric
	//	Texture* newCharTexture = new Texture(m_renderer);
	//	newCharTexture->CreateFromSurface(textSurface);
	//	newCharTexture->Height = 40;
	//	newCharTexture->Width = 20;
	//	TextCharsTexture.push_back(newCharTexture);
	//	SDL_FreeSurface(textSurface);
	//}
	//
	//for (int i = 0; i < _text.size(); i++) {
	//
	//	TextCharsTexture[i]->Render(25 + (i*TextCharsTexture[i]->Width), 425, TextCharsTexture[i]->Width, TextCharsTexture[i]->Height);
	//}

	SDL_Surface* textSurface = TTF_RenderText_Blended(m_font, _text.c_str(), m_Color);
	m_textTexture->CreateFromSurface(textSurface);
	m_textTexture->Height = Height;
	m_textTexture->Width = Width;
	m_textTexture->Render(25, 425, Width, Height);
	SDL_FreeSurface(textSurface);
}

void TextBox::loadFont()
{
	//TODO use FontSize from Settings
	m_font = TTF_OpenFont("OpenSans-Regular.ttf", 28);
}

void TextBox::ApplyBackgroundSettings()
{
	SDL_Surface* backgroundSurface = SDL_CreateRGBSurface(0, Width, Height, 32, 0, 0, 0, 0);
	
	SDL_FillRect(backgroundSurface, NULL, SDL_MapRGB(backgroundSurface->format, 0, 0, 255));
	m_boxBackground->CreateFromSurface(backgroundSurface);

	//TODO checke Settings auf Alpha f�r TextBox
	m_boxBackground->SetBlendMode(SDL_BLENDMODE_BLEND);
	m_boxBackground->SetAlpha((255 * 0.5));
}
