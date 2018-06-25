#pragma once
#include <vector>
#include "Texture.h"
#include <fstream>
#include <sstream>
#include <string>
#include "SpritePosition.h"

class ImageLoader {

	public:
		ImageLoader(SDL_Renderer* _renderer);
		~ImageLoader();
		
		SDL_Renderer* m_Renderer;
		/**
		Loads the Textures defined in the ImageImport file into the m_loaded Textures List
		@return The number of Images that have been loaded
		*/
		int LoadTextures();

		//TODO Image Index System überarbeiten: Panel Indicies oder Globale Indicies -> Bilder pro Panel laden, oder global laden
		/**
		Gibt eine Liste von Texturen wieder, die anhand ihres Index spezifiziert werden
		@param _indicies: A List of the Indicies of the desired Textures
		@return A List of Pointers to the Textures
		*/
		std::vector<Texture*> GetTextures(std::vector<int> _indicies);
		Texture* GetTextures(int _indicie);
	private:
		std::vector<Texture*> m_loadedTextures;
};