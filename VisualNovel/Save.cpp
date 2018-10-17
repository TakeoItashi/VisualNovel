#include "Save.h"
#include "SDL.h"

Save::Save() {
	m_currentLine = 0;
	m_currentPanel = 0;
}

void Save::Serialize(std::string _path) {

	SDL_RWops* file = SDL_RWFromFile(_path.c_str(), "w+b");
	if (file == nullptr) {

		//TODO Error Handling
	} else if (file != nullptr) {

		//Serialize the currentLine
		SDL_RWwrite(file, &m_currentLine, sizeof(int), 1);
		//Serialize the current Panel
		SDL_RWwrite(file, &m_currentPanel, sizeof(int), 1);

		int valueSize = m_values.size();

		SDL_RWwrite(file, &valueSize, sizeof(int), 1);


		for (int i = 0; i <= valueSize-1; i++) {

			SDL_RWwrite(file, &m_values[i]->m_Name, sizeof(std::string), 1);
		
			unsigned long long size = 0;

			switch (m_values[i]->m_Type) {

				case DataValueType::trigger:
					SDL_RWwrite(file, &m_values[i]->m_Type, sizeof(int), 1);
					size = sizeof(bool);
					break;
				case DataValueType::variable:
					SDL_RWwrite(file, &m_values[i]->m_Type, sizeof(int), 1);
					size = sizeof(int);
					break;
				case DataValueType::decimal:
					SDL_RWwrite(file, &m_values[i]->m_Type, sizeof(int), 1);
					size = sizeof(float);
					break;
				default:
					//TODO Fehler meldung, falls der Datentyp nicht bekannt ist
					break;
			}
			//SDL_RWwrite(file, &m_values[i], size, 1);
			SDL_RWwrite(file, &m_values[i]->m_Value, size, 1);
		}
		
		SDL_RWclose(file);
	}
}

void Save::Deserialize(std::string _path) {

	SDL_RWops* file = SDL_RWFromFile(_path.c_str(), "r+b");
	if (file == nullptr) {

		//TODO Error Handling
	} else if (file != nullptr) {
		
		//Deserialize the currentline
		SDL_RWread(file, &m_currentLine, sizeof(int), 1);
		//Deserialize the currentline
		SDL_RWread(file, &m_currentPanel, sizeof(int), 1);

		int valueSize = 0;
		SDL_RWread(file, &valueSize, sizeof(int), 1);

		for (int i = 0; i <= valueSize - 1; i++) {
		
			unsigned long long size = 0;
			DataValue* newValue = new DataValue;
		
			SDL_RWread(file, &newValue->m_Name, sizeof(std::string), 1);
		
			SDL_RWread(file, &newValue->m_Type, sizeof(int), 1);
		
			switch (newValue->m_Type) {
		
			case DataValueType::trigger:
				size = sizeof(bool);
				break;
			case DataValueType::variable:
				size = sizeof(int);
				break;
			case DataValueType::decimal:
				size = sizeof(float);
				break;
			default:
				//TODO Fehler meldung, falls der Datentyp nicht bekannt ist
				break;
			}
		
			SDL_RWread(file, &newValue->m_Value, size, 1);
			m_values.push_back(newValue);
		}

		SDL_RWclose(file);
	}
}