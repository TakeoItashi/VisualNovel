#include "ImageLoader.h"

ImageLoader::ImageLoader()
{
	m_loadedTextures.shrink_to_fit();
}

ImageLoader::~ImageLoader()
{
	m_loadedTextures.shrink_to_fit();
}

void ImageLoader::LoadTextures()
{
	//Parse ImageImports.txt
	std::ifstream imagefile ("ImageImports.txt");
	std::vector<std::string> fileNames;

	if (imagefile.is_open()) {

		while (getline (imagefile, fileNames.back()))
		{

		}
	}

	//Add Images to loadedTextures List
}

Texture* ImageLoader::GetTexture(int index)
{
	return m_loadedTextures[index];
}
