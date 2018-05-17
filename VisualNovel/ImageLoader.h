#pragma once
#include <vector>
#include "Texture.h"
#include <fstream>
#include <sstream>
#include <string>

class ImageLoader {

	public:
		ImageLoader(SDL_Renderer* _renderer);
		~ImageLoader();

		void LoadTextures();
		SDL_Renderer* m_Renderer;
		std::vector<Texture*> GetTextures(std::vector<int> index);
	private:
		std::vector<Texture*> m_loadedTextures;
};