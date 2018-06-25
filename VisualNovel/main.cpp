#include <iostream>
#include "Game.h"

int main(int argc, char* argv[]) {

	Game* newGame = new Game(new Settings("Test"));
	newGame->Init();
	newGame->NewGame();

	//Main loop
	bool quit = false;
	SDL_Event eventHandler;
	//newGame->Render();
	//SDL_RenderPresent(newGame->m_Renderer);
	while(!quit){

		while (SDL_PollEvent(&eventHandler) != 0) {
			if (eventHandler.type == SDL_QUIT) {

				quit = true;
			}
			if (eventHandler.type == SDL_MOUSEBUTTONUP || eventHandler.type == SDL_KEYDOWN) {

				newGame->Update();
				newGame->Render();
				SDL_RenderPresent(newGame->m_Renderer);
			}
		}
	}
	delete newGame;
	newGame = nullptr;
	return 0;
}