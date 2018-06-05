#pragma once
#include "Texture.h"
class Button {

	public:

		Button(Texture _texture);
		~Button();

		int xPos;
		int yPos;
		Texture m_Texture = NULL;

		void SetPosition(int _x, int _y);
		void HandleEvent(SDL_Event _event);
		void Render();
	private:
};