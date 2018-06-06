#pragma once
#include <string>
#include "include\SDL.h"
#include "include\SDL_image.h"

class Texture {

	public:
		Texture(SDL_Renderer* _renderer);
		~Texture();

		//TODO minimale Zugriffsberechtigung
		
		int Width = 0;
		int Height = 0;
		int PosX = 0;
		int PosY = 0;
		/**
		Sets the Blend Mode for this Texture
		@param _blending: brief The blend operation used when combining source and destination pixel components
		*/
		void SetBlendMode(SDL_BlendMode _blending);
		/**
		Sets the Alpha value for this Texture
		@param _alpha: the alpha value this Texture should use. 0 means transparent and 255 means solid
		*/
		void SetAlpha(Uint8 _alpha);
		/**
		Deallocates this Texture
		*/
		void Free();
		/**
		Render the Texture at the specified Position
		*/
		void Render(int x, int y, int Height, int Width);
		bool LoadMedia(std::string _path);
		void CreateFromSurface(SDL_Surface* _surface);
	private:
		SDL_Renderer* m_Renderer;
		SDL_Texture* m_Texture;
};