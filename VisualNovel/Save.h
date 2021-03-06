#pragma once
#include <vector>
#include <SDL.h>
#include <iostream>
#include "map"

class DataValue;

class Save {
public:

	Save();

	int m_currentLine;
	std::string m_shownText;
	std::string BranchKey;
	std::string m_currentPanel;
	//TODO: unordered map benutzen
	std::map<std::string, DataValue*> m_values;
	std::vector<bool> m_triggers;	//TODO eigene Value Klasse mit ID zum erstellen.
	std::vector<int> m_variables;	//	   Wert wird als void pointer abgespeichert um alle Datentypen annehmen zu k�nnen
	std::vector<float> m_decimals;	//	   Datentyp des Wertes wird in einem Enum dargestellt

	void Serialize(std::string _path = "savegame.sg");
	void Deserialize(std::string _path = "savegame.sg");

	static std::string* GetSaveGameText(std::string _path = "savegame.sg");
	static void WriteStringToFile(std::string _string, SDL_RWops* _targetFile);
	static std::string ReadStringFromFile(SDL_RWops* _sourceFile);
};