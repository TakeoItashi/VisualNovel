#include "TextBox.h"

#include <vector>

TextBox::TextBox(SDL_Renderer* _renderer) {
	m_renderer = _renderer;
	//TODO richtige implementierung von color
	m_Color = SDL_Color{ 0, 0, 0 };
	//TODO TextBox Größe und Position aus den Settings holen
	Width = 750;
	Height = 150;
	m_boxBackground = new Texture(_renderer);
}

TextBox::~TextBox() {
}

void TextBox::Render(std::string _text, int _speed) {
	if (m_textTexture != NULL) {

		m_textTexture->Free();
	} else {
		m_textTexture = new Texture(m_renderer);
	}

	std::vector<Texture*> TextCharsTexture;

	//TODO TextBox Position an Window Größe anpassen
	m_boxBackground->Render(25, 425, Height, Width);

	////TODO Rework der Text Darstellung für Animation: Create new Texture after DeltaTime has passed. New Texture uses one Char more than before

	int TextWidth, LineWidth;
	std::string result, text, line, word;
	std::vector<std::string> lines;
	line = "";
	text = _text;
	TTF_SizeText(m_font, _text.c_str(), &TextWidth, nullptr);

	while (_text.size() > 0) {

		//Findet den Index vom ende des nächsten Wortes
		int nextSpace = _text.find(' ');

		//Itteriert den Satz Wort um Wort
		if (nextSpace == _text.npos) {

			nextSpace = _text.size();
			_text = _text.substr(0, nextSpace);
			_text = "";
		} else {
			word = _text.substr(0, nextSpace + 1);
			_text.erase(0, nextSpace + 1);
		}

		//Testet die neue Zeilen länge
		std::string temp = line + word;
		TTF_SizeText(m_font, temp.c_str(), &LineWidth, nullptr);

		if (LineWidth <= (Width - 10)) {
			line += word;
			TTF_SizeText(m_font, _text.c_str(), &TextWidth, nullptr);
		} else {
			lines.push_back(line);
			line = word;
			TTF_SizeText(m_font, _text.c_str(), &TextWidth, nullptr);
		}
	}

	for (int i = 0; i < lines.size(); i++) {

		SDL_Surface* textSurface = TTF_RenderText_Blended(m_font, lines[i].c_str(), m_Color);
		m_textTexture->CreateFromSurface(textSurface);
		TTF_SizeText(m_font, lines[i].c_str(), &m_textTexture->Width, &m_textTexture->Height);
		m_textTexture->Render(30, 425 + (i*(m_textTexture->Height - 5)), m_textTexture->Height, m_textTexture->Width);
		SDL_FreeSurface(textSurface);
	}
}

void TextBox::loadFont(std::string _path) {
	//TODO use FontSize from Settings
	m_font = TTF_OpenFont("OpenSans-Regular.ttf", 28);
}

//TODO Use Configured Background Settings
void TextBox::ApplySettings(std::string _settings) {
	SDL_Surface* backgroundSurface = SDL_CreateRGBSurface(0, Width, Height, 32, 0, 0, 0, 0);

	SDL_FillRect(backgroundSurface, NULL, SDL_MapRGB(backgroundSurface->format, 0, 0, 255));
	m_boxBackground->CreateFromSurface(backgroundSurface);

	//TODO checke Settings auf Alpha für TextBox
	m_boxBackground->SetBlendMode(SDL_BLENDMODE_BLEND);
	m_boxBackground->SetAlpha((255 * 0.5));
}
