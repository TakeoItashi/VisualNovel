#include "Condition.h"

bool Condition::isMet(std::map<std::string, DataValue*> _matchingData) {

	bool ConditionMet = false;
	std::map<std::string, DataValue*>::iterator search;
	search = m_ConditionValues.begin();
	//TODO eine bessere Lösung für die isMet schleife finden: die Länge sollte von den Required Values abhängig sein und nicht von der Matching Data
	for (search; search != m_ConditionValues.end(); search++) {

		std::map<std::string, DataValue*>::iterator matchingItem = _matchingData.find(search->first);
		//search = m_ConditionValues.find(_matchingData[i]->m_name);
		DataValue Value = *m_ConditionValues[matchingItem->first];
		DataValueType type = Value.GetType();
		ConditionAction action = Value.GetAction();
		ValueVariant conditionalValue, givenValue;

		switch (type) {
			case DataValueType::trigger:
				conditionalValue = m_ConditionValues[matchingItem->first]->GetBool();
				givenValue = _matchingData[matchingItem->first]->GetBool();

				ConditionMet = EvaluateValue(action, conditionalValue, givenValue);
				break;
			case DataValueType::variable:
				conditionalValue = m_ConditionValues[matchingItem->first]->GetInt();
				givenValue = _matchingData[matchingItem->first]->GetInt();

				ConditionMet = EvaluateValue(action, conditionalValue, givenValue);
				break;
			case DataValueType::decimal:
				conditionalValue = m_ConditionValues[matchingItem->first]->GetFloat();
				givenValue = _matchingData[matchingItem->first]->GetFloat();

				ConditionMet = EvaluateValue(action, conditionalValue, givenValue);
				break;
			case DataValueType::text:
				conditionalValue = m_ConditionValues[matchingItem->first]->GetString();
				givenValue = _matchingData[matchingItem->first]->GetString();

				ConditionMet = EvaluateValue(action, conditionalValue, givenValue);
				break;
		}
	}

	return ConditionMet;
}

bool Condition::EvaluateValue(ConditionAction _evaluationAction, ValueVariant _givenvalue, ValueVariant _conditionValue) {
	switch (_evaluationAction) {
		case ConditionAction::isSmaller:
			return _conditionValue > _givenvalue;
		case ConditionAction::isEqual:
			return _conditionValue == _givenvalue;
		case ConditionAction::isBigger:
			return _conditionValue < _givenvalue;
	}
}
