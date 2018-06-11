#pragma once
#include "Panel.h"
#include "Settings.h"
#include <vector>
#include "include\SDL.h"
#include "include\SDL_image.h"
#include "ImageLoader.h"
#include "TextBox.h"
#include "TextLoader.h"

class Game {

	public:
		Game(Settings* _initialSettings);
		~Game();

		Settings* m_GameSettings = nullptr;
		ImageLoader* m_ImageLoader = nullptr;
		TextBox* m_TextBox = nullptr;
		SDL_Window* m_Window = nullptr;
		SDL_Renderer* m_Renderer = nullptr;
		SDL_Texture* m_Texture = nullptr;
		SDL_Surface* m_ScreenSurface = nullptr;
		SDL_Surface* m_Background = nullptr;
		std::vector<Panel*> m_PanelList;
		TextLoader* m_textLoader = nullptr;
		std::vector<std::string> m_keywords;
		int m_CurrentLine;
		int m_CurrentPanel;

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