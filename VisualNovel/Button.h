#pragma once
#include "Texture.h"
class Button : public Texture {

	public:

		Button(SDL_Renderer*);
		~Button();

		int TextureIndex;
		Texture* m_textTexture;
		typedef void(*DelegateFunction)();
		bool m_Trigger;

		bool HandleEvent(SDL_Event* _event);
		void Update(DelegateFunction _function);
};