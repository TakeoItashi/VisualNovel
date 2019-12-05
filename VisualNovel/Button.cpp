#include "Button.h"

Button::Button(SDL_Renderer* _Renderer, std::function<void(Button*)> _delegate)
	: SpriteSheetTexture(_Renderer) {
	//m_callBack = _callBack;
	//m_delegateFunction = _delegate;
}

Button::~Button() {
}

void Button::HandleEvent(SDL_Event* _event) {

	if (_event->type == SDL_MOUSEMOTION || _event->type == SDL_MOUSEBUTTONUP || _event->type == SDL_MOUSEBUTTONDOWN) {

		int x, y;
		SDL_GetMouseState(&x, &y);

		bool mouseover = true;

		//Check the Mouse Position, whether it's over the Button
		if (x < PosX) {

			mouseover = false;
		}
		else if (x > PosX + Width) {

			mouseover = false;
		}
		else if (y < PosY) {

			mouseover = false;
		}
		else if (y > PosY + Height) {

			mouseover = false;
		}

		if (!mouseover) {
			m_currentSprite = ButtonSpriteState::BUTTON_SPRITE_MOUSE_OUT;
			//TODO normal sprite
		}
		else {
			int test;
			switch (_event->type) {

			case SDL_MOUSEMOTION:
				if (_event->type != SDL_MOUSEBUTTONDOWN) {
					m_currentSprite = ButtonSpriteState::BUTTON_SPRITE_MOUSE_OVER_MOTION;
					break;
				}
			case SDL_MOUSEBUTTONDOWN:
				m_currentSprite = ButtonSpriteState::BUTTON_SPRITE_MOUSE_DOWN;
				break;
			case SDL_MOUSEBUTTONUP:
				//TODO: Return the result from the delagate function
				m_delegateFunction(this);
				break;
			}
		}
	}
}
