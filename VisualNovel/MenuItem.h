#pragma once
#include <string>
#include "Button.h"
struct MenuItem {
public:

	Button* Button = nullptr;	//TODO Deallokierung 
	std::string ItemName;
};