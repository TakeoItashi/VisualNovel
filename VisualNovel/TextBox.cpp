#include "TextBox.h"

#include <vector>

TextBox::TextBox(SDL_Renderer* _renderer)
{
	m_renderer = _renderer;
	//TODO richtige implementierung von color
	m_Color = SDL_Color{0, 0, 0};
	//TODO TextBox Größe und Position aus den Settings holen
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

	//TODO TextBox Position an Window Größe anpassen
	m_boxBackground->Render(25, 425, Height, Width);

	for (int i = 0; i < _text.size(); i++) {
	
		SDL_Surface* textSurface = TTF_RenderText_Solid(m_font, &_text[i], m_Color);
		Texture* newCharTexture = new Texture(m_renderer);
		newCharTexture->CreateFromSurface(textSurface);
		newCharTexture->Height = 100;
		newCharTexture->Width = 100;
		TextCharsTexture.push_back(newCharTexture);
		SDL_FreeSurface(textSurface);
	}
	
	for (int i = 0; i < _text.size(); i++) {
	
		TextCharsTexture[i]->Render(25 + (i*TextCharsTexture[i]->Width), 425, TextCharsTexture[i]->Height, TextCharsTexture[i]->Width);
	}

	//SDL_Surface* textSurface = TTF_RenderText_Solid(m_font, _text.c_str(), m_Color);
	//m_textTexture->CreateFromSurface(textSurface);
	//m_textTexture->Height = Height;
	//m_textTexture->Width = Width;
	//m_textTexture->Render(25, 425, Width, Height);
	//SDL_FreeSurface(textSurface);
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

	//TODO checke Settings auf Alpha für TextBox
	m_boxBackground->SetBlendMode(SDL_BLENDMODE_BLEND);
	m_boxBackground->SetAlpha((255 * 0.5));
}
