#include "Texture.h"

Texture::Texture() {

	m_Texture = NULL;
	Width = 0;
	Height = 0;
}

Texture::~Texture() {
	Free();
}

void Texture::Free() {

	if (m_Texture != NULL) {

		SDL_DestroyTexture(m_Texture);
		m_Texture = NULL;
		Width = 0;
		Height = 0;
	}
}

void Texture::Render(int x, int y, SDL_Renderer* _renderer) {

	SDL_Rect renderQuad = { x, y, Width, Height };
	SDL_RenderCopy(_renderer, m_Texture, NULL, &renderQuad);
}

bool Texture::LoadMedia(std::string path, SDL_Renderer* _renderer) {
	
	//Get rid of existing Texture
	Free();

	SDL_Texture* newTexture = NULL;
	SDL_Surface* loadedSurface = IMG_Load(path.c_str());
	SDL_SetColorKey(loadedSurface, SDL_TRUE, SDL_MapRGB(loadedSurface->format, 0, 0xFF, 0xFF));
	
	newTexture = SDL_CreateTextureFromSurface(_renderer, loadedSurface);

	Width = loadedSurface->w;
	Height = loadedSurface->h;

	SDL_FreeSurface(loadedSurface);

	m_Texture = newTexture;
	return m_Texture != NULL;
}
