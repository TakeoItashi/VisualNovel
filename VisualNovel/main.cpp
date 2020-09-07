//TODO: Weg finden ifdef von .exe oder .dll abhängig machen
//#if 0
#include <SDL.h>
#include <iostream>
#include "Game.h"
#include "TextLoader.h"
#include "Settings.h"

int main(int argc, char* argv[]) {

	char sep = '/';

#ifdef _WIN32
	sep = '\\';
#endif
	std::string gamePath = argv[0];
	std::string gameName;
	size_t i = gamePath.rfind(sep, gamePath.length());
	if (i != std::string::npos) {
		gameName = (gamePath.substr(i + 1, gamePath.length() - i));
	}

	SDL_Event* currentEvent = new SDL_Event();	//TODO in game verschieben
	Game::GetInstance()->Init(new Settings(Game::GetInstance()->m_textLoader), currentEvent, gameName);

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