#pragma once
#include <string>
#include <vector>

class TextLoader;

class Settings {

	public:
		Settings(TextLoader* _textLoaderReference);
		~Settings();

		void LoadSettings();

		//Window Settings
		int m_WindowWidth, m_WindowHeight;
		//Textbox/Text Colors
		//SDL_Color TextBoxColor, TextBoxTextColor;		//TODO Farbumstellung muss in der Engine deaktivierbar sein. Außerdem muss die Textfarbe überprüft werden: Macht es sinn einen Textfarben Wert zu haben oder soll die Farbe über die Font geregelt werden. 
		//TextboxColor rgba Values
		unsigned char m_TextBoxRed, m_TextBoxGreen, m_TextBoxBlue, m_TextBoxAlpha;
		//The speed at which Text shows up in the Box
		short m_TextBoxSpeed;
		//Saves the Setting wheter the User wants to skip unseen Text or not
		bool m_SkipUnseenText;
		//TODO richtige implementierung
		std::string m_SoundSettings;
private:
		int m_windowPosX, m_windowPosY;			//TODO Sollte die Position raus genommen werden oder für die Engine benutzt werden?
		TextLoader* m_TextLoader;
};