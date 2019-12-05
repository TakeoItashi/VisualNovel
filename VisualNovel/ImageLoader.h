#pragma once
#include <vector>
#include <string>
#include <fstream>
#include <sstream>

class SDL_Renderer;
class Texture;
class SpriteSheetTexture;

class ImageLoader {

	public:
		ImageLoader(SDL_Renderer* _renderer);
		~ImageLoader();
		//Loads the Textures defined in the ImageImport file into the m_loaded Textures List
		//@return The number of Images that have been loaded
		int LoadTextures();
		////TODO Image Index System überarbeiten: Panel Indicies oder Globale Indicies -> Bilder pro Panel laden, oder global laden
		//Returns a List of Texture Pointers specified by thier indicies
		//@param _indicies: A List of the Indicies of the desired Textures
		//@return A List of Pointers to the Textures
		std::vector<Texture*> GetTextures(std::vector<int> _indicies);
		//Returns a single Texture specified by an index
		//@param _index: the Index of the desired Texture
		//@return The pointer to the desired Texture
		Texture* GetTexture(int _index);
		SpriteSheetTexture* GetSprite(int _index);
	private:
		//A List of Pointers to all the loaded Textures
		std::vector<Texture*> m_loadedTextures;
		std::vector<SpriteSheetTexture*> m_loadedSprites;

		SDL_Renderer* m_Renderer;
};