#include "Button.h"

Button::Button(SDL_Renderer* _Renderer) : Texture(_Renderer) {
}

Button::~Button() {
}

bool Button::HandleEvent(SDL_Event* _event) {

	if (_event->type == SDL_MOUSEMOTION || _event->type == SDL_MOUSEBUTTONDOWN || _event->type == SDL_MOUSEBUTTONUP) {

		int x, y;
		SDL_GetMouseState(&x, &y);

		bool mouseover = true;

		if (x < PosX) {

			mouseover = false;
		} else if (x > PosX + Width) {

			mouseover = false;
		} else if(y < PosY) {

			mouseover = false;
		} else if (y > PosY + Height) {

			mouseover = false;
		}

		if (!mouseover) {

			//TODO normal sprite. possibly change alpha
		} else {
	
			switch (_event->type) {
			
				case SDL_MOUSEMOTION:
					//TODO Mouse over sprite. possibly change alpha
					break;
				case SDL_MOUSEBUTTONDOWN:
					//TODO Mouse down sprite
					break;
				case SDL_MOUSEBUTTONUP:
					//TODO 
					return true;
					break;
			}
		}
	}
}
