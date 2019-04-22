#include <iostream>
#include "Game.h"
#include <SDL.h>
#include "TextLoader.h"
#include "Settings.h"

int main(int argc, char* argv[]) {

	SDL_Event* eventHandler = new SDL_Event();	//TODO in game verschieben
	Game::GetInstance()->Init(new Settings(Game::GetInstance()->m_textLoader), eventHandler);

	//Main loop
	bool* quit = new bool;
	*quit = false;
	while (!*quit) {

		Game::GetInstance()->Update(eventHandler, quit);

	}
	SDL_Quit();
	return 0;
}