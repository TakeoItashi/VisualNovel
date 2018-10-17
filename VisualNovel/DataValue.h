#pragma once
#include <string>
#include "DataValueType.h"

class DataValue {
	public:
		std::string m_Name;
		DataValueType m_Type;
		void* m_Value;
};