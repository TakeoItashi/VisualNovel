#pragma once
#include "Texture.h"
class Button : public Texture {

	public:

		Button(SDL_Renderer*);
		~Button();

		int TextureIndex;
		Texture* m_textTexture;

		bool HandleEvent(SDL_Event* _event);
};