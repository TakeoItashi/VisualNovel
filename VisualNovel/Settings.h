#pragma once
#include <string>

class Settings {

	public:
		Settings(std::string Setting1);
		~Settings();

		//TODO richtige implementierung
		std::string TextBoxColor, TextBoxSpeed, SkipSettings, TextBoxAlpha, TextBoxTextColor,SoundSettings, ScreenSettings;
};