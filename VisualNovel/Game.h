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
		SDL_Renderer* m_Renderer = NULL;
		SDL_Texture* m_Texture = NULL;

		SDL_Surface* m_ScreenSurface = NULL;
		SDL_Surface* m_Background = NULL;
		std::list<Panel> PanelList[];

		void Init();
		void NewGame();
		void Update();
		void Load();

		SDL_Texture* loadTexture(std::string path);

		void ChangeSettings(Settings* NewSettings);
};