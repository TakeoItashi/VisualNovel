#pragma once
#include <vector>
#include <string>
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
	static std::vector<Panel*> m_PanelList;
	static std::vector<std::string> m_keywords;		//TODO eventuell in seperate Klasse aussondern, zusammen mit der LoadStory Funktion
	static int m_CurrentLine;
	static int m_CurrentPanel;
	static bool m_GameIsRunning;
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

	static void ChangeResolution(Button* _buttonCallback);

	static void ToggleFullscreen(Button* _buttonCallback);

	static void OpenMainMenu(Button* _buttonCallback);

	static void LoadCustomMethod(Button* _buttonCallback);

	static void RenderCurrentMenu();

	static void LoadStoryBoard();

	inline static Game* GetInstance() { if (m_gamePointer == nullptr) { m_gamePointer = new Game(); } return m_gamePointer; };

private:
	Game();
	static Game* m_gamePointer;
};