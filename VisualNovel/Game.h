#pragma once
#include "Panel.h"
#include "Settings.h"
#include "list"
#include "include\SDL.h"
#include "include\SDL_image.h"

class Game {

	public:
		Game(Settings* _initialSettings);
		~Game();

		Settings* GameSettings;
		SDL_Window* m_Window = NULL;
		SDL_Surface* m_ScreenSurface = NULL;
		SDL_Surface* m_Background = NULL;
		std::list<Panel> PanelList[];

		void Init();
		void NewGame();
		void Load();
		void ChangeSettings(Settings* NewSettings);
};