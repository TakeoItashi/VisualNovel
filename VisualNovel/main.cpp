#include <iostream>
#include "Game.h"

int main(int argc, char* argv[]) {

	SDL_Event* eventHandler = new SDL_Event();
	Game* newGame = new Game(new Settings("Test"), eventHandler);
	newGame->Init();
	newGame->NewGame();

	//Main loop
	bool quit = false;
	while(!quit){

		newGame->Update();
		
	}
	delete newGame;
	newGame = nullptr;
	return 0;
}