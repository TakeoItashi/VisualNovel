#pragma once
#include "Panel.h"
#include "Settings.h"
#include <vector>
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
		std::vector<Panel> m_PanelList[];

		void Init();
		void NewGame();
		void Update();
		void Render();
		void Load();
		void ChangeSettings(Settings* NewSettings);

		SDL_Texture* loadTexture(std::string path);
};