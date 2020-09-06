//TODO: Weg finden ifdef von .exe oder .dll abhängig machen
//#if 0
#include <SDL.h>
#include <iostream>
#include "Game.h"
#include "TextLoader.h"
#include "Settings.h"

int main(int argc, char* argv[]) {

	SDL_Event* currentEvent = new SDL_Event();	//TODO in game verschieben
	Game::GetInstance()->Init(new Settings(Game::GetInstance()->m_textLoader), currentEvent);

	//Main loop
	bool quit = new bool;
	quit = false;
	while (!quit) {
		int pending = SDL_PollEvent(currentEvent);
		if ((pending != 0)) {

			if (currentEvent->type == SDL_QUIT) {

				quit = true;
			}
			Game::GetInstance()->Update(currentEvent);
		}
	}
	SDL_Quit();
	return 0;
}
//#endif