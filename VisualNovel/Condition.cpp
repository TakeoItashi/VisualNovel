#include "Condition.h"

bool Condition::isMet(std::map<std::string, DataValue*> _matchingData) {

	bool ConditionMet = false;
	std::map<std::string, DataValue*>::iterator search;
	//TODO eine bessere Lösung für die isMet schleife finden: die Länge sollte von den Required Values abhängig sein und nicht von der Matching Data
	for (search = m_RequiredValues.begin(); search != m_RequiredValues.end(); search++) {

		std::map<std::string, DataValue*>::iterator matchingItem = _matchingData.find(search->first);
		//search = m_RequiredValues.find(_matchingData[i]->m_name);
		DataValue Value = *m_RequiredValues[matchingItem->first];
		DataValueType type = Value.GetType();

		if (type == DataValueType::trigger) {

			bool requiredvalue = m_RequiredValues[matchingItem->first]->GetBool();
			bool givenValue = _matchingData[matchingItem->first]->GetBool();

			ConditionMet = requiredvalue == givenValue;
		}
		else if (type == DataValueType::variable) {

			int requiredvalue = m_RequiredValues[matchingItem->first]->GetInt();
			int givenValue = _matchingData[matchingItem->first]->GetInt();

			ConditionMet = requiredvalue == givenValue;
		}
		else if (type == DataValueType::decimal) {

			float requiredvalue = m_RequiredValues[matchingItem->first]->GetFloat();
			float givenValue = _matchingData[matchingItem->first]->GetFloat();

			ConditionMet = requiredvalue == givenValue;
		}
		else if (type == DataValueType::text) {

			std::string requiredvalue = m_RequiredValues[matchingItem->first]->GetString();
			std::string givenValue =_matchingData[matchingItem->first]->GetString();

			ConditionMet = requiredvalue == givenValue;
		}
	}

	return ConditionMet;
}

bool Condition::EvaluateValue(ConditionAction _evaluationAction, DataValue _value)
{
	return false;
}
