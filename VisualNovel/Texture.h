#pragma once
#include <string>
#include "include\SDL.h"
#include "include\SDL_image.h"

class Texture {

	public:
		Texture(SDL_Renderer* _renderer);
		~Texture();

		//TODO minimale Zugriffsberechtigung
		int Width;
		int Height;
		int PosX;
		int PosY;

		void SetBlendMode(SDL_BlendMode _blending);
		/**
		Sets the Alpha value for this Texture
		@param _alpha the alpha value this Texture should use. 0 means transparent and 255 means solid
		*/
		void SetAlpha(Uint8 _alpha);
		void Free();
		void Render(int x, int y, int Height, int Width);
		bool LoadMedia(std::string _path);
		void CreateFromSurface(SDL_Surface* _surface);
	private:
		SDL_Renderer* m_Renderer;
		SDL_Texture* m_Texture;
};