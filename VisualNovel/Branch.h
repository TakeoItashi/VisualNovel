#pragma once
#include <string>
#include <map>
#include "ShownItem.h"
#include "ContinueType.h"

class ShownItem;
enum ContinueType;

class Branch {
public:
	std::string m_Name;
	ContinueType m_continueType;
	std::map<int, ShownItem*> m_shownItems;
	std::string m_continueKey;
};