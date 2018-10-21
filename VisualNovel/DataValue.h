#pragma once
#include <string>
#include "DataValueType.h"

class DataValue {
	public:
		DataValue();
		~DataValue();

		std::string m_Name;

		void SetValue(bool _value);
		void SetValue(int _value);
		void SetValue(float _value);
		//TODO strings hinzufügen
		bool GetBool();
		int GetInt();
		float GetFloat();
		void* GetPointer();
		DataValueType GetType();
private:
		DataValueType m_Type;
		void* m_Value;
};