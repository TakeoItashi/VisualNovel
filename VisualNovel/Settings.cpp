#include "Textloader.h"
#include "Settings.h"

Settings::Settings(TextLoader* _textLoaderReference) {

	m_TextLoader = _textLoaderReference;
}

Settings::~Settings() {
}

void Settings::LoadSettings() {

	std::vector<std::string> keywords;
	keywords = m_TextLoader->LoadText("options.txt");

	if (keywords.size() <= 0) {

		//TODO Fehlermeldung falls die Optionen leer sind
	}
	int i = 0;
	while (i < keywords.size()) {

		if (keywords[i] == "WindowWidth:") {			//C++ does not allow for switch statements with strings

			m_WindowWidth = std::stoi(keywords[i + 1]);
			i += 3;
		} else if (keywords[i] == "WindowHeight:") {

			m_WindowHeight = std::stoi(keywords[i + 1]);
			i += 3;
		} else if (keywords[i] == "TextBoxRed:") {

			m_TextBoxRed = std::stoi(keywords[i + 1]);
			i += 3;
		} else if (keywords[i] == "TextBoxGreen:") {

			m_TextBoxGreen = std::stoi(keywords[i + 1]);
			i += 3;
		} else if (keywords[i] == "TextBoxBlue:") {

			m_TextBoxBlue = std::stoi(keywords[i + 1]);
			i += 3;
		} else if (keywords[i] == "TextBoxAlpha:") {

			m_TextBoxAlpha = std::stoi(keywords[i + 1]);
			i += 3;
		} else if (keywords[i] == "Font:") {
			LoadFont(keywords[i + 1], std::stoi(keywords[i + 4]));
			i += 6;
		}
	}
}

void Settings::LoadFont(std::string _path, int _fontSize) {
	//TODO use FontSize from Settings
	m_Font = TTF_OpenFont(_path.c_str(), _fontSize);
}
