#pragma once
#include <string>
#include <map>
#include "ShownItem.h"

class ShownItem;

class Branch {
public:
	std::string m_Name;
	int m_startIndex;
	int m_endIndex;
	int m_continueIndex;
};