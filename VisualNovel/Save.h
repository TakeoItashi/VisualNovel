#pragma once
#include <vector>

class Save {
public:

	Save();

	int m_currentLine;
	int m_currentPanel;
	std::vector<bool> m_triggers;	//TODO eigene Value Klasse mit ID zum erstellen.
	std::vector<int> m_variables;	//	   Wert wird als void pointer abgespeichert um alle Datentypen annehmen zu können
	std::vector<float> m_decimals;	//	   Datentyp des Wertes wird in einem Enum dargestellt

	void Serialize(std::string _path = "savegame.sg");
	void Deserialize(std::string _path = "savegame.sg");
};