#pragma once
#include <vector>
#include "Texture.h"
#include <fstream>
#include <iostream>
#include <string>
class ImageLoader {

	public:
		ImageLoader();
		~ImageLoader();

		void LoadTextures();
		Texture* GetTexture(int index);
	private:
		std::vector<Texture*> m_loadedTextures;
};