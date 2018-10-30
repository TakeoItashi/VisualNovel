#pragma once
#include <vector>
#include <map>

class DataValue;

class Condition {

public:

	std::map<std::string, DataValue> m_RequiredValues;

	bool isMet(std::vector<DataValue*> _matchingData);
};