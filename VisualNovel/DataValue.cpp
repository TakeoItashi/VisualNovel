#include "DataValueType.h"
#include "DataValue.h"

DataValue::DataValue() {

	m_Value = nullptr;
}

DataValue::~DataValue() {

	if (m_Value != nullptr) {

		delete m_Value;
		m_Value = nullptr;
	}
}

void DataValue::SetValue(bool _value) {

	m_Value = new bool;
	(*(bool*)m_Value) = _value;
	m_Type = DataValueType::trigger;
}

void DataValue::SetValue(int _value) {

	m_Value = new int;
	(*(int*)m_Value) = _value;
	m_Type = DataValueType::variable;
}

void DataValue::SetValue(float _value) {

	m_Value = new float;
	(*(float*)m_Value) = _value;
	m_Type = DataValueType::decimal;
}

bool DataValue::GetBool() {

	return *(bool*)m_Value;
}

int DataValue::GetInt() {

	int* testpointer = (int*)m_Value;
	int testint = *testpointer;
	return *(int*)m_Value;
}

float DataValue::GetFloat() {

	float* testpointer = (float*)m_Value;
	float testint = *testpointer;
	return *(float*)m_Value;
}

void* DataValue::GetPointer() {

	return m_Value;
}

DataValueType DataValue::GetType() {

	return m_Type;
}
