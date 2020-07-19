#pragma once
#include <string>
#include <variant>
#include <stdexcept>
#include "ConditionAction.h"

enum DataValueType;

#ifdef api_export 
#define api_export_DataValue __declspec(dllexport)
#else
#define api_export_DataValue __declspec(dllimport)
#endif

using ValueVariant = std::variant<int, std::string, float, bool>;

class DataValue {
	public:
		DataValue();
		DataValue(std::string _name, DataValueType _type, ValueVariant _value);
		~DataValue();

		std::string m_name;

		void SetValue(bool _value);
		void SetValue(int _value);
		void SetValue(float _value);
		void SetValue(std::string _value);

		//TODO strings hinzufügen
		bool GetBool();
		int GetInt();
		float GetFloat();
		std::string GetString();
		DataValueType GetType();
		ConditionAction GetAction();
private:
		DataValueType m_Type;
		ValueVariant m_Value;
		ConditionAction m_Action;
};

//extern "C" api_export_DataValue void* __cdecl CreateDataValue_int(const char* _name, int _value);
//extern "C" api_export_DataValue void* __cdecl CreateDataValue_float(const char* _name, float _value);
//extern "C" api_export_DataValue void* __cdecl CreateDataValue_bool(const char* _name, bool _value);
//extern "C" api_export_DataValue void* __cdecl CreateDataValue_string(const char* _name, const char* _value);
//extern "C" api_export_DataValue bool __cdecl ReadDataValue_bool(void* _ptr);
//extern "C" api_export_DataValue void __cdecl SetDataValue_bool(void* _ptr, bool _value);
//extern "C" api_export_DataValue void __cdecl FreeDataValue(void* _ptr);