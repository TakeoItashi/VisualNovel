#include "Button.h"

Button::Button(SDL_Renderer* _Renderer)
	: SpriteSheetTexture(_Renderer) {
	//m_callBack = _callBack;
	//m_delegateFunction = _delegate;
}

Button::~Button() {
}

bool Button::HandleEvent(SDL_Event* _event) {

	if (_event->type == SDL_MOUSEMOTION || _event->type == SDL_MOUSEBUTTONUP || _event->type == SDL_MOUSEBUTTONDOWN) {

		int x, y;
		SDL_GetMouseState(&x, &y);

		bool mouseover = true;

		//Check the Mouse Position, whether it's over the Button
		if (x < PosX) {

			mouseover = false;
		} else if (x > PosX + Width) {

			mouseover = false;
		} else if (y < PosY) {

			mouseover = false;
		} else if (y > PosY + Height) {

			mouseover = false;
		}

		if (!mouseover) {
			m_currentSprite = ButtonSpriteState::BUTTON_SPRITE_MOUSE_OUT;
			return false;
			//TODO normal sprite
		} else {
			switch (_event->type) {

				case SDL_MOUSEMOTION:
					m_currentSprite = ButtonSpriteState::BUTTON_SPRITE_MOUSE_OVER_MOTION;
					return false;
				case SDL_MOUSEBUTTONDOWN:
					m_currentSprite = ButtonSpriteState::BUTTON_SPRITE_MOUSE_DOWN;
					return false;
				case SDL_MOUSEBUTTONUP:
					//TODO: Return the result from the delagate function
					return m_delegateFunction(this);
			}
		}
	}
	return false;
}
