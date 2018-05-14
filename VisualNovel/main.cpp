#include <iostream>
#include "Game.h"

int main(int argc, char* argv[]) {

	Game* newGame = new Game(new Settings("Test"));
	newGame->Init();
	newGame->NewGame();
	while (true) {

	}
	return 0;
}