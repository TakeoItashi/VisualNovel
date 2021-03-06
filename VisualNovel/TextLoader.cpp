#include "Panel.h"
#include "TextLoader.h"

TextLoader::TextLoader() {
}


TextLoader::~TextLoader() {
}

std::vector<std::string> TextLoader::LoadText(std::string _filepath) {

	std::ifstream fin;
	fin.open(_filepath, std::ios::in);

	char currentCharacter;
	std::string currentKeyword;
	bool quote = false;
	std::vector<std::string> keywords;

	if (fin.good()) {

		while (!fin.eof()) {

			fin.get(currentCharacter);

			//Check for quotes
			//If the parser finds a quote, all whitespaces are replaced by underscores
			//TODO Anfuehrungszeichen erlauben
			if (currentCharacter == '"') {

				quote = !quote;
				//currentKeyword += currentCharacter;
				while (quote) {

					fin.get(currentCharacter);
					if (currentCharacter == ' ') {

						currentCharacter = '_';
					} else if (currentCharacter == '"') {

						//currentKeyword += currentCharacter;
						std::replace(currentKeyword.begin(), currentKeyword.end(), '_', ' ');
						keywords.push_back(currentKeyword);
						currentKeyword.clear();
						quote = !quote;
						break;
					}
					currentKeyword += currentCharacter;
				}
				continue;
			}

			// Check for Comments. All Comments are disregarded by the parser
			if (currentCharacter == '#') {
			
				fin.get(currentCharacter);
				if (currentCharacter == '#') {

					while (currentCharacter != '\n') {
						if (!fin.good()) {
							goto endloop;
						}
						fin.get(currentCharacter);
					}
				}
			}

			// Semicolons, commas and brackets are turned into their own keywords
			if (currentCharacter == ';' || currentCharacter == ',' || currentCharacter == '(' || currentCharacter == ')') {

				if (currentKeyword.size() > 0) {

					keywords.push_back(currentKeyword);
					currentKeyword.clear();
				}
				currentKeyword += currentCharacter;
				keywords.push_back(currentKeyword);
				currentKeyword.clear();
				continue;
			}

			if (!isspace(currentCharacter)) {

				currentKeyword += currentCharacter;
			} else {

				if (currentKeyword.size() > 0) {

					keywords.push_back(currentKeyword);
					currentKeyword.clear();
				}
			}
		}
	}

	keywords.push_back(currentKeyword);
	endloop:
	currentKeyword.clear();

	if (keywords.back() == "}}") {
		keywords.pop_back();
		keywords.push_back("}");
		keywords.push_back("}");
	}
	fin.close();
	return keywords;
}
