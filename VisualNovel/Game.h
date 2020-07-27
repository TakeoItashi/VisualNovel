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

class Game {

public:
	~Game();

	static MainMenu* m_MainMenu;
	static OptionsMenu* m_OptionsMenu;
	static Settings* m_GameSettings;
	static ImageLoader* m_ImageLoader;
	static TextBox* m_TextBox;
	static SDL_Window* m_Window;
	static SDL_Renderer* m_Renderer;
	static SDL_Event* m_EventHandler;
	static TextLoader* m_textLoader;
	static Save* m_save;
	static Menu* m_CurrentMenu;
	static std::vector<Panel*> m_PanelList;		//TODO map für Panel Liste benutzen
	static std::map<std::string, int> m_panelNameDictionary;
	static std::vector<std::string> m_keywords;		//TODO eventuell in seperate Klasse aussondern, zusammen mit der LoadStory Funktion
	static int m_CurrentLine;
	static int m_CurrentPanel;
	static bool m_GameIsRunning;
	static bool m_IsDecisionPending;
	/**
	Initializes the Game Libraries and Variables
	*/
	static void Init(Settings* _initialSettings, SDL_Event* _eventHandler);
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

	static void ShowMenu(Menu* _menuInstance);

	inline static Game* GetInstance() { if (m_gamePointer == nullptr) { m_gamePointer = new Game(); } return m_gamePointer; };

private:
	Game();
	static Game* m_gamePointer;
};