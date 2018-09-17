#include "Button.h"

Button::Button(SDL_Renderer* _Renderer /*, std::function<void*(void*)> _delegate*/) : Texture(_Renderer) {
	//m_callBack = _callBack;
	//m_delegateFunction = _delegate;
}

Button::~Button() {
}

void* Button::HandleEvent(SDL_Event* _event) {

	if (_event->type == SDL_MOUSEMOTION || _event->type == SDL_MOUSEBUTTONDOWN || _event->type == SDL_MOUSEBUTTONUP) {

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

			//TODO normal sprite
		} else {
	
			switch (_event->type) {
			
				case SDL_MOUSEMOTION:
					//TODO Mouse over sprite
					break;
				case SDL_MOUSEBUTTONDOWN:
					//TODO Mouse down sprite
					break;
				//if the button is pressed, call the delegate function;
				case SDL_MOUSEBUTTONUP:
					//TODO 
					//Return the result from the delagate function
					return m_delegateFunction(nullptr);
					break;
			}
		}
	}
}

void Button::Update(DelegateFunction _function) {
}
