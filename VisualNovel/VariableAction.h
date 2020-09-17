#pragma once
#include <string>
#include <variant>
#include "DataValueType.h"
#include "VariableOperation.h"

using ValueVariant = std::variant<int, std::string, float, bool>;
class DataValue;

class VariableAction {
public:
	VariableAction();
	~VariableAction();
	std::string m_VariableKey;
	DataValueType m_VariableType;
	VariableOperation m_Operation;
	ValueVariant m_Value;

	void ExecuteOperation(DataValue* _value);
};