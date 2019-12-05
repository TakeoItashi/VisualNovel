#pragma once
#include <string>
#include "include\SDL.h"
#include "include\SDL_image.h"

class SDL_Renderer;
class SDL_Surface;
class SDL_Texture;

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
		@param x: The Position on the X Axis where the Texture is supposed to Render
			   y: The Position on the Y Axis where the Texture is supposed to Render
			   _height: The Height of the Texture. If no Value is set the Textures own Height will be used
			   _hidth: The Width of the Texture. If no Value is set the Textures own Width will be used
		*/
		void Render(int x = -1, int y=-1, int _height= -1, int _width = -1);
		/**
		Allocates a Texture from a given filepath to the m_Texture Variable
		@param _path: The Path of the image file
		*/
		bool LoadMedia(std::string _path);
		/**
		Allocates a Texture from a given Surface to the m_Texture Variable
		@param _path: The Surface that will be turned into a Texture
		*/
		void CreateFromSurface(SDL_Surface* _surface);

		void SetTexture(Texture* _texture);
	protected:
		SDL_Renderer* m_Renderer;
		SDL_Texture* m_Texture;
};