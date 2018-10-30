#include "DataValue.h"
#include "DataValueType.h"
#include "Save.h"

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

			DataValueType ValueReference;
			switch (m_values[i]->GetType()) {

				case DataValueType::trigger:
					ValueReference = trigger;
					SDL_RWwrite(file, &ValueReference, sizeof(int), 1);
					size = sizeof(bool);
					break;
				case DataValueType::variable:
					ValueReference = variable;
					SDL_RWwrite(file, &ValueReference, sizeof(int), 1);
					size = sizeof(int);
					break;
				case DataValueType::decimal:
					ValueReference = decimal;
					SDL_RWwrite(file, &ValueReference, sizeof(int), 1);
					size = sizeof(float);
					break;
				default:
					//TODO Fehler meldung, falls der Datentyp nicht bekannt ist
					break;
			}
			SDL_RWwrite(file, m_values[i]->GetPointer(), size, 1);
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
		
			DataValueType valueType;
			SDL_RWread(file, &valueType, sizeof(int), 1);
		
			switch (valueType) {
		
			case DataValueType::trigger:
				size = sizeof(bool);
				newValue->SetValue(false);
				break;
			case DataValueType::variable:
				size = sizeof(int);
				newValue->SetValue(0);
				break;
			case DataValueType::decimal:
				size = sizeof(float);
				newValue->SetValue(0.0f);
				break;
			default:
				//TODO Fehler meldung, falls der Datentyp nicht bekannt ist
				break;
			}
			SDL_RWread(file, newValue->GetPointer(), size, 1);
			m_values.push_back(newValue);
		}
		int testInt;
		bool testbool;
		float testfloat;
		for each (DataValue* value in m_values) {
			
			switch (value->GetType()) {

			case DataValueType::trigger:
				testbool = value->GetBool();
				break;
			case DataValueType::variable:
				testInt = value->GetInt();
				break;
			case DataValueType::decimal:
				testfloat = value->GetFloat();
				break;
			default:
				//TODO Fehler meldung, falls der Datentyp nicht bekannt ist
				break;
			}
		}
		SDL_RWclose(file);
	}
}