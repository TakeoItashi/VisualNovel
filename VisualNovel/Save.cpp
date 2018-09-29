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

		int triggerSize = m_triggers.size();
		//Serialize the count of all triggers for deserialization
		SDL_RWwrite(file, &triggerSize, sizeof(int), 1);
		//Serialize the triggers(bools)
		for (int i = 0; i <= triggerSize - 1; i++) {

			SDL_RWwrite(file, &m_triggers[i], sizeof(bool), 1);
		}

		int variableSize = m_variables.size();
		//Serialize the count of all variables for deserialization
		SDL_RWwrite(file, &variableSize, sizeof(int), 1);
		//Serialize the variables(ints)
		for (int i = 0; i <= variableSize - 1; i++) {

			SDL_RWwrite(file, &m_variables[i], sizeof(int), 1);
		}

		int decimalSize = m_decimals.size();
		//Serialize the count of all decimals for deserialization
		SDL_RWwrite(file, &decimalSize, sizeof(int), 1);
		//Serialize the decimals(floats)
		for (int i = 0; i <= decimalSize - 1; i++) {

			SDL_RWwrite(file, &m_decimals[i], sizeof(float), 1);
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


		int triggerSize;
		//Deserialize the number of all triggers
		SDL_RWread(file, &triggerSize, sizeof(int), 1);
		std::vector<bool> newTriggerList;
		//deserialize all the triggers into a new list
		for (int i = 0; i <= triggerSize - 1; ++i) {

			bool newValue;
			SDL_RWread(file, &newValue, sizeof(bool), 1);
			newTriggerList.push_back(newValue);
		}
		//assign all the trigger values to the public list
		m_triggers = newTriggerList;

		int variableSize;
		//Deserialize the number of all variables
		SDL_RWread(file, &variableSize, sizeof(int), 1);
		std::vector<int> newIntList;
		//deserialize all the variables into a new list
		for (int i = 0; i <= variableSize - 1; ++i) {

			int newValue;
			SDL_RWread(file, &newValue, sizeof(int), 1);
			newIntList.push_back(newValue);
		}
		//assign all the variables values to the public list
		m_variables = newIntList;

		int decimalSize;
		//Deserialize the number of all decimals
		SDL_RWread(file, &decimalSize, sizeof(int), 1);
		std::vector<float> newDecimalList;
		//deserialize all the decimals into a new list
		for (int i = 0; i <= decimalSize - 1; ++i) {

			float newValue;
			SDL_RWread(file, &newValue, sizeof(float), 1);
			newDecimalList.push_back(newValue);
		}
		//assign all the decimals values to the public list
		m_decimals = newDecimalList;
		SDL_RWclose(file);
	}
}