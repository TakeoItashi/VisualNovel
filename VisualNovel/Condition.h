#pragma once
#include "DataValue.h"
#include <map>
#include <vector>

class Condition {

public:

	std::map<std::string, DataValue> m_RequiredValues;

	bool isMet(std::vector<DataValue> _matchingData);
};