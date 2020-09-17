#pragma once
#include <string>
#include "ContinueType.h"
#include "VariableAction.h"

class SplitOption {
public:
	std::string m_name;
	std::string m_shownText;
	int SpriteIndex;
	ContinueType m_type;
	std::string m_continueKey;
	std::vector<VariableAction*> m_VariableActions;
};
