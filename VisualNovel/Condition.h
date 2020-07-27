#pragma once
#include <vector>
#include <map>
#include "DataValueType.h"
#include "DataValue.h"
#include "ConditionAction.h"

class DataValue;
using ValueVariant = std::variant<int, std::string, float, bool>;

class Condition {

public:

	std::map<std::string, DataValue*> m_ConditionValues;

	std::string m_AlternativePanelKey;

	bool isMet(std::map<std::string, DataValue*> _matchingData);
	bool EvaluateValue(ConditionAction _evaluationAction, ValueVariant _givenvalue, ValueVariant _conditionValue);
};