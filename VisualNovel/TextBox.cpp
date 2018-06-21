#include "TextBox.h"
#include <algorithm>
#include <vector>

TextBox::TextBox(SDL_Renderer* _renderer) {
	m_renderer = _renderer;
	//TODO richtige implementierung von color
	m_Color = SDL_Color{ 0, 0, 0 };
	//TODO TextBox Größe und Position aus den Settings holen
	Width = 750;
	Height = 150;
	//TODO Box Backgound lädt eine textur aus einem Ordner und erstellt selbst eine Textur, wenn keine Textur vordefiniert wurde
	m_boxBackground = new Texture(_renderer);
}

TextBox::~TextBox() {
}

void TextBox::Render(DialogueLine _line, int _speed) {

	if (m_textTexture != NULL) {

		m_textTexture->Free();
	} else {
		m_textTexture = new Texture(m_renderer);
	}

	//TODO ganz lange Wörter abarbeiten

	std::vector<Texture*> TextCharsTexture;

	//TODO TextBox Position an Window Größe anpassen
	m_boxBackground->Render(25, 425, Height, Width);

	//TODO Rework der Text Darstellung für Animation: Create new Texture after DeltaTime has passed. New Texture uses one Char more than before

	int TextWidth, LineWidth;
	std::string result, text, line, word;
	std::vector<std::string> lines;
	line = "";
	text = _line.Text;
	TTF_SizeText(m_font, _line.Text.c_str(), &TextWidth, nullptr);
	int nextSpace;

	std::replace(_line.Text.begin(), _line.Text.end(), '_', ' ');
	std::replace(_line.Name.begin(), _line.Name.end(), '_', ' ');
	_line.Name.append(":");

	while (_line.Text.size() > 0) {

		//Findet den Index vom ende des nächsten Wortes
		nextSpace = _line.Text.find(' ');

		//Itteriert den Satz Wort um Wort
		if (nextSpace == _line.Text.npos) {

			nextSpace = _line.Text.size();
			word = _line.Text.substr(0, nextSpace);
			_line.Text = "";
		} else {
			word = _line.Text.substr(0, nextSpace + 1);
			_line.Text.erase(0, nextSpace + 1);
		}

		//Testet die neue Zeilen länge
		std::string temp = line + word;
		TTF_SizeText(m_font, temp.c_str(), &LineWidth, nullptr);

		if (LineWidth <= (Width - 10)) {
			line += word;
			TTF_SizeText(m_font, _line.Text.c_str(), &TextWidth, nullptr);
		} else {
			lines.push_back(line);
			line = word;
			TTF_SizeText(m_font, _line.Text.c_str(), &TextWidth, nullptr);
		}
	}

	lines.push_back(line);
	int lineStart = 425;	//TODO an Fenstergröße anpassen
	SDL_Surface* textSurface = TTF_RenderText_Blended(m_font, _line.Name.c_str(), m_Color);
	m_textTexture->CreateFromSurface(textSurface);
	TTF_SizeText(m_font, _line.Name.c_str(), &m_textTexture->Width, &m_textTexture->Height);
	m_textTexture->Render(30, lineStart, m_textTexture->Height, m_textTexture->Width);
	lineStart += m_textTexture->Height - 5;
	SDL_FreeSurface(textSurface);

	for (int i = 0; i < lines.size(); i++) {

		textSurface = TTF_RenderText_Blended(m_font, lines[i].c_str(), m_Color);
		m_textTexture->CreateFromSurface(textSurface);
		TTF_SizeText(m_font, lines[i].c_str(), &m_textTexture->Width, &m_textTexture->Height);
		m_textTexture->Render(35, lineStart + (i*(m_textTexture->Height - 5)), m_textTexture->Height, m_textTexture->Width);
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
