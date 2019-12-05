#include "Button.h"

Button::Button(SDL_Renderer* _Renderer , std::function<void(Button*)> _delegate)
	: Texture(_Renderer) {
	//m_callBack = _callBack;
	//m_delegateFunction = _delegate;
}

Button::~Button() {
}

void Button::HandleEvent(SDL_Event* _event) {

	if (_event->type == SDL_MOUSEMOTION || _event->type == SDL_MOUSEBUTTONUP) {

		int x, y;
		SDL_GetMouseState(&x, &y);

		bool mouseover = true;

		//Check the Mouse Position, whether it's over the Button
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
			SetAlpha(255);
			//TODO normal sprite
		} else {
			int test;
			switch (_event->type) {
			
				case SDL_MOUSEMOTION:
					test = SetAlpha(0);
					//TODO Mouse over sprite
					break;
				case SDL_MOUSEBUTTONDOWN:
					SetAlpha(128);
					//TODO Mouse down sprite
					break;
				//if the button is pressed, call the delegate function;
				case SDL_MOUSEBUTTONUP:
					//TODO 
					SetAlpha(255);
					//Return the result from the delagate function
					m_delegateFunction(this);
					break;
			}
		}
	}
}
