#include "ImageLoader.h"

ImageLoader::ImageLoader(SDL_Renderer* _renderer)
{
	m_loadedTextures.shrink_to_fit();
	m_Renderer = _renderer;
}

ImageLoader::~ImageLoader()
{
	m_loadedTextures.shrink_to_fit();
}

void ImageLoader::LoadTextures()
{
	//Parse ImageImports.txt
	std::ifstream imagefile ("ImageImports.txt");
	std::string fileNames;

	if (imagefile.is_open()) {

		while (getline (imagefile, fileNames))
		{
			Texture* newTexture = new Texture(m_Renderer);
			newTexture->LoadMedia(fileNames);
			m_loadedTextures.push_back(newTexture);
		}
	}
}

std::vector<Texture*> ImageLoader::GetTextures(std::vector<int> indicies)
{
	std::vector<Texture*> newList;

	for (int i = 0; i < indicies.size(); i++) {

		newList.push_back(m_loadedTextures[indicies[i]]);
	}

	//TODO newList löschen?

	return newList;
}
