#pragma once
#include <string>
#include "include\SDL.h"
#include "include\SDL_image.h"

class Texture {

	public:
		Texture();
		~Texture();

		int Width;
		int Height;
		int PosX;
		int PosY;

		void Free();
		void Render(int x, int y, int Height, int Width, SDL_Renderer* _renderer);
		bool LoadMedia(std::string, SDL_Renderer* _renderer);
	private:
		SDL_Texture* m_Texture;
};