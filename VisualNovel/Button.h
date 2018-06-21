#pragma once
#include "Texture.h"
class Button {

	public:

		Button();
		~Button();

		int xPos;
		int yPos;
		int Height;
		int Width;
		int TextureIndex;
		Texture m_Texture = NULL;

		void SetPosition(int _x, int _y);
		void HandleEvent(SDL_Event _event);
		void Render();
};