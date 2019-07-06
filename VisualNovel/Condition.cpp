#include "Condition.h"

bool Condition::isMet(std::map<std::string, DataValue*> _matchingData) {

	bool ConditionMet = false;
	std::map<std::string, DataValue*>::iterator search;
	//TODO eine bessere Lösung für die isMet schleife finden: die Länge sollte von den Required Values abhängig sein und nicht von der Matching Data
	for (search = m_RequiredValues.begin(); search != m_RequiredValues.end(); search++) {

		search = m_RequiredValues.find(_matchingData[i]->m_name);
		DataValue test = *m_RequiredValues[_matchingData[i]->m_name];
		DataValueType type = test.GetType();

		if (type == DataValueType::trigger) {

			bool requiredvalue = (*(bool*)m_RequiredValues[_matchingData[i]->m_name]->GetPointer());
			bool givenValue = (*(bool*)_matchingData[i]->GetPointer());

			ConditionMet = requiredvalue == givenValue;
		}
		else if (type == DataValueType::variable) {

			int requiredvalue = (*(int*)m_RequiredValues[_matchingData[i]->m_name]->GetPointer());
			int givenValue = (*(int*)_matchingData[i]->GetPointer());

			ConditionMet = requiredvalue == givenValue;
		}
		else if (type == DataValueType::decimal) {

			float requiredvalue = (*(float*)m_RequiredValues[_matchingData[i]->m_name]->GetPointer());
			float givenValue = (*(float*)_matchingData[i]->GetPointer());

			ConditionMet = requiredvalue == givenValue;
		}
	}

	return ConditionMet;
}
