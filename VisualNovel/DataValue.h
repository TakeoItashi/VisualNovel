#pragma once
#include <string>

enum DataValueType;

class DataValue {
	public:
		DataValue();
		DataValue(std::string _name, DataValueType _type, void* _value);
		~DataValue();

		std::string m_name;

		void SetValue(bool _value);
		void SetValue(int _value);
		void SetValue(float _value);

		//TODO strings hinzuf�gen
		bool GetBool();
		int GetInt();
		float GetFloat();
		void* GetPointer();
		DataValueType GetType();
private:
		DataValueType m_Type;
		void* m_Value;
};