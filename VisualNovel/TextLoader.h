#pragma once
#include <vector>
#include <string>
#include <fstream>
#include <sstream>
#include <iostream>
#include <ctype.h>
#include <algorithm>

class Panel;

class TextLoader {
public:
	TextLoader();
	~TextLoader();

	std::vector<Panel*> m_PanelList;
	std::vector<std::string> LoadText(std::string _filepath);
};

