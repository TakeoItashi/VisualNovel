#include "VariableAction.h"
#include "DataValue.h"

VariableAction::VariableAction() {
}

VariableAction::~VariableAction() {
}

void VariableAction::ExecuteOperation(DataValue* _value) {

	ValueVariant newValue;
	bool boolVar;
	int intVar;
	float floatVar;
	std::string stringVar;

	switch (m_VariableType) {
		case DataValueType::trigger:
			boolVar = _value->GetBool();
			break;
		case DataValueType::variable:
			intVar = _value->GetInt();
			break;
		case DataValueType::decimal:
			floatVar = _value->GetFloat();
			break;
		case DataValueType::text:
			stringVar = _value->GetString();
			break;
	}
	
	switch (m_Operation) {
		case VariableOperation::add:
			if (m_VariableType == DataValueType::variable) {

				intVar = intVar + std::get<int>(m_Value);
				_value->SetValue(intVar);
			} else if (m_VariableType == DataValueType::decimal) {

				floatVar = floatVar + std::get<float>(m_Value);
				_value->SetValue(floatVar);
			}
			break;
		case VariableOperation::subtract:
			if (m_VariableType == DataValueType::variable) {

				intVar = intVar - std::get<int>(m_Value);
				_value->SetValue(intVar);
			} else if (m_VariableType == DataValueType::decimal) {

				floatVar = floatVar - std::get<float>(m_Value);
				_value->SetValue(floatVar);
			}
			break;
		case VariableOperation::set:
			_value->SetValue(m_Value);
			break;
	}
}
