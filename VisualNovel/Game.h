#pragma once
#include "Panel.h"
#include "Settings.h"
#include <vector>
#include "include\SDL.h"
#include "include\SDL_image.h"
#include "ImageLoader.h"
#include "TextBox.h"
class Game {

	public:
		Game(Settings* _initialSettings);
		~Game();

		Settings* m_GameSettings;
		ImageLoader* m_ImageLoader;
		TextBox* m_TextBox;
		SDL_Window* m_Window = NULL;
		SDL_Renderer* m_Renderer = NULL;
		SDL_Texture* m_Texture = NULL;
		SDL_Surface* m_ScreenSurface = NULL;
		SDL_Surface* m_Background = NULL;
		std::vector<Panel*> m_PanelList;

		/**
		Initializes the Game Libraries and Variables
		*/
		void Init();
		/**
		Starts a new Game
		*/
		void NewGame();
		/**
		Updates the current Process
		*/
		void Update();
		/**
		Renders all the Current Sprites
		*/
		void Render();
		/**
		Loads a serialized game state
		*/
		void Load();
		/**
		Changes the Settings of the Game
		@param The new Settings of the Game
		*/
		void ChangeSettings(Settings* NewSettings);
};