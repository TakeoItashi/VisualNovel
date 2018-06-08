#include <iostream>
#include "Game.h"

int main(int argc, char* argv[]) {

	Game* newGame = new Game(new Settings("Test"));
	newGame->Init();
	newGame->NewGame();

	//Main loop
	bool quit = false;
	SDL_Event eventHandler;

	while(!quit){

		while (SDL_PollEvent(&eventHandler) != 0) {
			if (eventHandler.type == SDL_QUIT) {

				quit = true;
			}
		}

		newGame->Render();
		//TODO Remove
		////Apply the PNG image
		SDL_Rect stretchRect;
		stretchRect.x = 0;
		stretchRect.y = 0;
		stretchRect.w = 800;
		stretchRect.h = 600;
		//
		//SDL_RenderClear(newGame->m_Renderer);
		//SDL_RenderCopy(newGame->m_Renderer, newGame->m_Texture, NULL, NULL);
		SDL_RenderPresent(newGame->m_Renderer);
	}
	delete newGame;
	newGame = nullptr;
	return 0;
}