#include "SpritePosition.h"
#include "Texture.h"
#include "SpriteSheetTexture.h"
#include "ImageLoader.h"

ImageLoader::ImageLoader(SDL_Renderer* _renderer)
{
	m_loadedTextures.shrink_to_fit();
	m_loadedSprites.shrink_to_fit();
	m_Renderer = _renderer;
}

ImageLoader::~ImageLoader()
{
	m_loadedTextures.shrink_to_fit();
	m_loadedSprites.shrink_to_fit();
}

int ImageLoader::LoadTextures()
{
	//Parse ImageImports.txt
	std::ifstream imagefile("ImageImports.txt");
	std::ifstream spriteFile ("SpriteImports.txt");
	std::string fileNames;
	int imageCount = 0;
	if (imagefile.is_open()) {

		while (getline (imagefile, fileNames))
		{
			if (fileNames[0] == '#' && fileNames[1] == '#') {
				continue;
			}
			Texture* newTexture = new Texture(m_Renderer);
			newTexture->LoadMedia(fileNames);
			m_loadedTextures.push_back(newTexture);
			imageCount++;
		}
		while (getline(spriteFile, fileNames)) {
			if (fileNames[0] == '#' && fileNames[1] == '#') {
				continue;
			}
			SpriteSheetTexture* newSpriteSheet = new SpriteSheetTexture(m_Renderer);
			newSpriteSheet->LoadMedia(fileNames);
			m_loadedSprites.push_back(newSpriteSheet);
			imageCount++;
		}
	}
	return imageCount;
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

Texture * ImageLoader::GetTexture(int _indicie) {

	return m_loadedTextures[_indicie];
}

SpriteSheetTexture* ImageLoader::GetSprite(int _indicie) {

	return m_loadedSprites[_indicie];
}