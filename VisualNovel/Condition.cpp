#include "DataValue.h"
#include "Condition.h"

bool Condition::isMet(std::vector<DataValue*> _matchingData) {
	
	bool ConditionMet = false;

	for (int i = 0; i < _matchingData.size(); i++) {

		if (m_RequiredValues.count(_matchingData[i]->m_Name) > 0) {

			ConditionMet = true;
		} else {

			ConditionMet = false;
		}
	}

	return ConditionMet;
}
