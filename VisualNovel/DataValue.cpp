#include "DataValueType.h"
#include "DataValue.h"

DataValue::DataValue() {

	//m_Value = nullptr;
}
//TODO: statt void* variant benutzen
DataValue::DataValue(std::string _name, DataValueType _type, ValueVariant _value)
{
	m_name = _name;
	m_Type = _type;
	m_Value = _value;
}

DataValue::~DataValue() {


}

void DataValue::SetValue(bool _value) {

	m_Value = _value;
	m_Type = DataValueType::trigger;
}

void DataValue::SetValue(int _value) {

	m_Value = _value;
	m_Type = DataValueType::variable;
}

void DataValue::SetValue(float _value) {

	m_Value = _value;
	m_Type = DataValueType::decimal;
}

void DataValue::SetValue(std::string _value) {

	m_Value = _value;
	m_Type = DataValueType::text;
}

bool DataValue::GetBool() {

	//getIf gibt einen nullptr zurück, wenn der wert nicht auf float gesetzt ist
	if (bool* Ptr = std::get_if<bool>(&m_Value); Ptr) {

		return *Ptr;
	}

	throw new std::logic_error("GetBool() was called but the given value was not a bool.");
}

int DataValue::GetInt() {

	if (int* Ptr = std::get_if<int>(&m_Value); Ptr) {

		return *Ptr;
	}

	throw new std::logic_error("GetInt() was called but the given value was not an int.");
}

float DataValue::GetFloat() {

	if (float* Ptr = std::get_if<float>(&m_Value); Ptr) {

		return *Ptr;
	}

	throw new std::logic_error("GetFloat() was called but the given value was not a float.");
}

std::string DataValue::GetString() {

	if (std::string * Ptr = std::get_if<std::string>(&m_Value); Ptr) {

		return *Ptr;
	}

	throw new std::logic_error("GetString() was called but the given value was not a std::string.");
}

DataValueType DataValue::GetType() {

	return m_Type;
}
#pragma region extern Methods
extern "C" api_export_DataValue void* __cdecl CreateDataValue_int(const char* _name, int _value) {

	std::string name = _name;
	DataValue* newValue = new DataValue(name, DataValueType::variable, _value);
	return newValue;
}

extern "C" api_export_DataValue void* __cdecl CreateDataValue_float(const char* _name, float _value) {

	std::string name = _name;
	DataValue* newValue = new DataValue(name, DataValueType::decimal, _value);
	return newValue;
}
extern "C" api_export_DataValue void* __cdecl CreateDataValue_bool(const char* _name, bool _value) {

	std::string name = _name;
	DataValue* newValue = new DataValue(name, DataValueType::trigger, _value);
	return newValue;
}
extern "C" api_export_DataValue void* __cdecl CreateDataValue_string(const char* _name, const char* _value) {

	std::string name = _name;
	std::string value = _value;
	DataValue* newValue = new DataValue(name, DataValueType::text, value);
	return newValue;
}
extern "C" api_export_DataValue bool __cdecl ReadDataValue_bool(void* _ptr) {

	DataValue* ptr = (DataValue*)_ptr;
	return ptr->GetBool();
}
extern "C" api_export_DataValue void __cdecl SetDataValue_bool(void* _ptr, bool _value) {

	DataValue* ptr = (DataValue*)_ptr;
	ptr->SetValue(_value);
}
extern "C" api_export_DataValue void __cdecl FreeDataValue(void* _ptr) {

	DataValue* ptr = (DataValue*)_ptr;
	delete ptr;
}
#pragma endregion