#include "DataValue.h"
#include "DataValueType.h"
#include "Save.h"

Save::Save() {
	m_currentLine = 0;
}

void Save::Serialize(std::string _path) {

	SDL_RWops* file = SDL_RWFromFile(_path.c_str(), "w+b");
	if (file == nullptr) {

		//TODO Error Handling
	}
	else if (file != nullptr) {

		//Serialize the currentLine
		SDL_RWwrite(file, &m_currentLine, sizeof(int), 1);
		//Serialize the current panel key
		Save::WriteStringToFile(m_currentPanel, file);
		//Serialize the current branch key
		Save::WriteStringToFile(BranchKey, file);

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
		//Deserialize the current panel key
		m_currentPanel = ReadStringFromFile(file);
		//Deserialize the current branch key
		BranchKey = ReadStringFromFile(file);

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


//Returns the necessary infos of the save file so that its possible to show the last shown line. If no file at the specified path exists then a nullptr is returned
std::string* Save::GetSaveGameText(std::string _path) {
	SDL_RWops* file = SDL_RWFromFile(_path.c_str(), "r+b");
	int currentline = 0;
	std::string panelKey;
	std::string branchKey;

	//Go through the file and read the current line, the panel key and the branch key
	if (file != nullptr) {
		//Read currentline, panelKey and branchKey out of the file in this order
		SDL_RWread(file, &currentline, sizeof(int), 1);
		panelKey = Save::ReadStringFromFile(file);
		branchKey = Save::ReadStringFromFile(file);
		SDL_RWclose(file);
		return new std::string[3]{ std::to_string(currentline), panelKey, branchKey };
	}
	else {
		return nullptr;
	}

	//TODO make this method functional
}

void Save::WriteStringToFile(std::string _string, SDL_RWops* _targetFile) {
	//Converting the std::string into a const char array and getting its size
	const char* charArray = _string.c_str();
	int size = strlen(charArray);

	//Writing the size of the const char array as an int into the file
	SDL_RWwrite(_targetFile, &size, sizeof(int), 1);

	//Iterating over the const char array and writing each character into the file seperately
	for (int i = 0; i < size; i++) {
		char cur = charArray[i];
		SDL_RWwrite(_targetFile, &cur, 1, 1);
	}

	//TODO add error handling for when the file cannot be written to
}


std::string Save::ReadStringFromFile(SDL_RWops* _sourceFile) {
	//Reading the String size that is saved in the file
	int size = 0;
	SDL_RWread(_sourceFile, &size, sizeof(int), 1);
	char* charArray = (char*)malloc(size + 1);
	for (int i = 0; i < size; i++) {
		SDL_RWread(_sourceFile, &charArray[i], 1, 1);
	}

	//Adding an escape character at the end of the string since there was an error before were there were
	//remaining characters at the end of the char pointer even though its size was specified with malloc()
	charArray[size] = '\0';

	std::string result = charArray;
	return result;

	//TODO free memory used by charArray
	//TODO add error handling
}