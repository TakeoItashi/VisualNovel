#pragma once
#include "include\SDL.h"
#include "include\SDL_image.h"
#include "ImageLoader.h"
#include "Condition.h"
#include "MainMenu.h"
#include "Panel.h"
#include "Save.h"
#include "Settings.h"
#include "TextBox.h"
#include "TextLoader.h"
#include <vector>

class Game {

public:
	~Game();

	static MainMenu* m_MainMenu;
	static Settings* m_GameSettings;
	static ImageLoader* m_ImageLoader;
	static TextBox* m_TextBox;
	static SDL_Window* m_Window;
	static SDL_Renderer* m_Renderer;
	static SDL_Event* m_EventHandler;
	static TextLoader* m_textLoader;
	static Save* m_save;
	static std::vector<Panel*> m_PanelList;
	static std::vector<std::string> m_keywords;		//TODO eventuell in seperate Klasse aussondern, zusammen mit der LoadStory Funktion
	static int m_CurrentLine;
	static int m_CurrentPanel;

	/**
	Initializes the Game Libraries and Variables
	*/
	static void Init(Settings* _initialSettings, SDL_Event* _eventHandler);
	/**
	Starts a new Game
	*/
	static void NewGame(Button* _buttonCallback);
	/**
	Updates the current Process
	*/
	static void Update(SDL_Event* _eventhandler, bool* _quitCondition);
	/**
	Renders all the Current Sprites
	*/
	static void Render();
	/**
	Loads a serialized game state
	*/
	static void LoadGame(Button* _buttonCallback);
	/**
	Changes the Settings of the Game
	@param The new Settings of the Game
	*/
	static void ChangeSettings(Settings* NewSettings);

	static void Gallery(Button* _buttonCallback);

	static void OpenOptions(Button* _buttonCallback);

	static void Quit(Button* _buttonCallback);

	static void LoadStoryBoard();

	static void LoadCustomMethod(Button* _buttonCallback);

	inline static Game* GetInstance() { if ( m_gamePointer == nullptr) { m_gamePointer = new Game(); } return m_gamePointer; };

private:
	Game();
	static Game* m_gamePointer;
};