#pragma once
#include <fstream>
#include <sstream>
#include <iostream>
#include <string>
#include <vector>
#include "Panel.h"
#include <string>
#include <ctype.h>

class TextLoader {
public:
	TextLoader();
	~TextLoader();

	std::vector<Panel*> m_PanelList;

	std::vector<std::string> LoadText(std::string _filepath);
};

