#pragma once
#include <vector>
#include <string>
#include <map>
#include <SDL.h>
#include <SDL_ttf.h>

class Menu;
class MainMenu;
class Settings;
class ImageLoader;
class TextBox;
class TextLoader;
class Save;
class Panel;
class Settings;
class Button;
class OptionsMenu;
class Branch;

class Game {

public:
	~Game();

	static MainMenu* m_MainMenu;
	static OptionsMenu* m_OptionsMenu;
	static OptionsMenu* m_PauseMenu;
	static OptionsMenu* m_SaveMenu;
	static OptionsMenu* m_LoadMenu;
	static Settings* m_GameSettings;
	static ImageLoader* m_ImageLoader;
	static TextBox* m_TextBox;
	static SDL_Window* m_Window;
	static SDL_Renderer* m_Renderer;
	static SDL_Event* m_EventHandler;
	static TextLoader* m_textLoader;
	static Save* m_save;
	static Menu* m_CurrentMenu;
	static std::map<std::string, Panel*> m_PanelMap;		//TODO map für Panel Liste benutzen
	static std::map<std::string, int> m_panelNameDictionary;
	static std::vector<std::string> m_keywords;		//TODO eventuell in seperate Klasse aussondern, zusammen mit der LoadStory Funktion
	static int m_CurrentLine;
	static std::string m_CurrentPanelKey;
	static bool m_GameIsRunning;
	static bool m_IsDecisionPending;
	/**
	Initializes the Game Libraries and Variables
	*/
	static void Init(Settings* _initialSettings, SDL_Event* _eventHandler, std::string _gameName);
	/**
	Starts a new Game
	*/
	static bool NewGame(Button* _buttonCallback);
	/**
	Updates the current Process
	*/
	static bool Update(SDL_Event* _eventhandler);
	/**
	Renders all the Current Sprites
	*/
	static bool Render();
	/**
	Saves the current progress in save slot saveSlotNum
	*/
	static bool SaveFunction(Button* _button, int saveSlotNum);
	/**
	Returns the path of the save slot with the specified save slot number
	*/
	static std::string GetSaveSlotPath(int _saveSlotNum);
	/**
	Loads the Save slot saveSlotNum
	*/
	static bool LoadFunction(Button* _button, int _saveSlotNum);
	/**
	Loads a serialized game state
	*/
	static bool LoadGame(Button* _buttonCallback);
	/**
	Changes the Settings of the Game
	@param The new Settings of the Game
	*/
	static bool ChangeSettings(Settings* NewSettings);

	static bool Gallery(Button* _buttonCallback);

	static bool OpenOptions(Button* _buttonCallback);

	static bool Quit(Button* _buttonCallback);

	static bool ChangeResolution(Button* _buttonCallback);

	static bool ToggleFullscreen(Button* _buttonCallback);

	static bool OpenMainMenu(Button* _buttonCallback);

	static bool LoadCustomMethod(Button* _buttonCallback);

	static void RenderCurrentMenu();

	static void LoadStoryBoard();

	static void LoadVariables();

	static void ShowMenu(Menu* _menuInstance);

	static bool ChangeBranch(const char* _BranchKey);

	static bool ChangePanel(const char* _panelKey);

	static bool ResetGameToMainMenu();

	static bool BackToGame(Button* _buttonCallback);

	static bool ShowPauseMenu();

	static bool ShowSaveMenu(Button* _buttonCallback);

	static bool ShowLoadMenu(Button* _buttonCallback);

	inline static Game* GetInstance() { if (m_gamePointer == nullptr) { m_gamePointer = new Game(); } return m_gamePointer; };

private:
	Game();
	static Game* m_gamePointer;
};