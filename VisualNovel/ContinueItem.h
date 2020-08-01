#pragma once
#include "ContinueType.h"
#include "SplitDecision.h"
#include <string>

class ContinueItem {
public:
	ContinueType m_type;
	std::string m_continueKey;
	SplitDecision* m_decision;
};