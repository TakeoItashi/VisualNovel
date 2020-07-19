#pragma once
#include <vector>
#include <map>
#include "DataValueType.h"
#include "DataValue.h"
#include "ConditionAction.h"

class DataValue;

class Condition {

public:

	std::map<std::string, DataValue*> m_RequiredValues;

	bool isMet(std::map<std::string, DataValue*> _matchingData);
	bool EvaluateValue(ConditionAction _evaluationAction, DataValue _value);
};