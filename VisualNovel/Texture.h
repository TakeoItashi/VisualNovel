#pragma once
#include <string>
#include "include\SDL.h"
#include "include\SDL_image.h"

class Texture {

	public:
		Texture(SDL_Renderer*);
		~Texture();

		//TODO minimale Zugriffsberechtigung
		int Width;
		int Height;
		int PosX;
		int PosY;

		void setBlendMode(SDL_BlendMode _blending);
		/**
		Sets the Alpha value for this Texture
		@param _alpha the alpha value this Texture should use. 0 means transparent and 255 means solid
		*/
		void setAlpha(Uint8 _alpha);
		void Free();
		void Render(int x, int y, int Height, int Width);
		bool LoadMedia(std::string);
		void CreateFromSurface(SDL_Surface* _surface);
	private:
		SDL_Renderer * m_Renderer;
		SDL_Texture* m_Texture;
};