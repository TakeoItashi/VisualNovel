#pragma once
#include <string>

class Settings {

	public:
		Settings(std::string Settings);
		~Settings();

		//Screen Variables
		int m_WindowWidth, m_WindowHeight, m_windowPosX, m_windowPosY;

		//TODO richtige implementierung
		std::string TextBoxColor, TextBoxSpeed, SkipSettings, TextBoxAlpha, TextBoxTextColor, SoundSettings, ScreenSettings;
};