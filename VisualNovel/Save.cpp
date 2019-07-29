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
	}
	else if (file != nullptr) {

		//Serialize the currentLine
		SDL_RWwrite(file, &m_currentLine, sizeof(int), 1);
		//Serialize the current Panel
		SDL_RWwrite(file, &m_currentPanel, sizeof(int), 1);

		int valueSize = m_values.size();

		SDL_RWwrite(file, &valueSize, sizeof(int), 1);


		//for (int i = 0; i <= valueSize-1; i++) {
		//TODO: Auto entfernen
		for (auto value = m_values.cbegin(); value != m_values.end(); value++) {

			SDL_RWwrite(file, &m_values[value->first]->m_name, sizeof(std::string), 1);

			unsigned long long size = 0;

			DataValueType ValueReference;
			switch (m_values[value->first]->GetType()) {

			case DataValueType::trigger: {
				ValueReference = trigger;
				SDL_RWwrite(file, &ValueReference, sizeof(int), 1);
				bool boolValue = m_values[value->first]->GetBool();
				SDL_RWwrite(file, &boolValue, sizeof(bool), 1);
			}break;
			case DataValueType::variable: {

				ValueReference = variable;
				SDL_RWwrite(file, &ValueReference, sizeof(int), 1);
				int intValue = m_values[value->first]->GetInt();
				SDL_RWwrite(file, &intValue, sizeof(int), 1);
			}break;
			case DataValueType::decimal: {

				ValueReference = decimal;
				SDL_RWwrite(file, &ValueReference, sizeof(int), 1);
				float floatValue = m_values[value->first]->GetFloat();
				SDL_RWwrite(file, &floatValue, sizeof(float), 1);
			}break;
			case DataValueType::text: {

				ValueReference = text;
				SDL_RWwrite(file, &ValueReference, sizeof(int), 1);
				std::string stringValue = m_values[value->first]->GetString();
				SDL_RWwrite(file, stringValue.c_str(), (sizeof(char) * strlen(stringValue.c_str())), 1);
			}break;
			default:
				//TODO Fehler meldung, falls der Datentyp nicht bekannt ist
				break;
			}
		}

		SDL_RWclose(file);
	}
}

void Save::Deserialize(std::string _path) {

	SDL_RWops* file = SDL_RWFromFile(_path.c_str(), "r+b");
	if (file == nullptr) {

		//TODO Error Handling
	}
	else if (file != nullptr) {

		//Deserialize the currentline
		SDL_RWread(file, &m_currentLine, sizeof(int), 1);
		//Deserialize the currentline
		SDL_RWread(file, &m_currentPanel, sizeof(int), 1);

		int valueSize = 0;
		SDL_RWread(file, &valueSize, sizeof(int), 1);

		for (int i = 0; i <= valueSize - 1; i++) {

			unsigned long long size = 0;
			DataValue* newValue = new DataValue;

			SDL_RWread(file, &newValue->m_name, sizeof(std::string), 1);

			DataValueType valueType;
			SDL_RWread(file, &valueType, sizeof(int), 1);

			switch (valueType) {

			case DataValueType::trigger: {

				size = sizeof(bool);
				bool value;
				SDL_RWread(file, &value, size, 1);
				newValue->SetValue(value);
			}break;
			case DataValueType::variable: {

				size = sizeof(int);
				int value;
				SDL_RWread(file, &value, size, 1);
				newValue->SetValue(value);
			}break;
			case DataValueType::decimal: {

				size = sizeof(float);
				float value;
				SDL_RWread(file, &value, size, 1);
				newValue->SetValue(value);
			}break;
			case DataValueType::text:
				//TODO: string deserialisierung implementieren
				throw new std::logic_error("Not Implemented");
				//size = sizeof(float);
				//newValue->SetValue(0.0f);
				break;
			default:
				//TODO Fehler meldung, falls der Datentyp nicht bekannt ist
				break;
			}
			m_values.insert({ newValue->m_name, newValue });
		}
		int testInt;
		bool testbool;
		float testfloat;
		//TODO auto entfernen
		for (const auto& value : m_values) {

			switch (value.second->GetType()) {

			case DataValueType::trigger:
				testbool = value.second->GetBool();
				break;
			case DataValueType::variable:
				testInt = value.second->GetInt();
				break;
			case DataValueType::decimal:
				testfloat = value.second->GetFloat();
				break;
			default:
				//TODO Fehler meldung, falls der Datentyp nicht bekannt ist
				break;
			}
		}
		SDL_RWclose(file);
	}
}