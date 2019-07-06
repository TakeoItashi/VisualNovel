#pragma once
#include <vector>
#include <map>
#include "DataValueType.h"
#include "DataValue.h"

class DataValue;

class Condition {

public:

	std::map<std::string, DataValue*> m_RequiredValues;

	bool isMet(std::map<std::string, DataValue*> _matchingData);
};