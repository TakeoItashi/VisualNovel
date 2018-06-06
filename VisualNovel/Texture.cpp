#include "Texture.h"

Texture::Texture(SDL_Renderer* _renderer) {

	m_Texture = NULL;
	Width = 0;
	Height = 0;
	m_Renderer = _renderer;
}

Texture::~Texture() {
	Free();
}

void Texture::SetBlendMode(SDL_BlendMode _blending)
{
	SDL_SetTextureBlendMode(m_Texture, _blending);
}

void Texture::SetAlpha(Uint8 _alpha)
{
	SDL_SetTextureAlphaMod(m_Texture, _alpha);
}

void Texture::Free() {

	if (m_Texture != NULL) {

		SDL_DestroyTexture(m_Texture);
		m_Texture = NULL;
		Width = 0;
		Height = 0;
	}
}

void Texture::Render(int x, int y, int _Height = -1, int _Width = -1) {

	if (_Width == -1) {
		_Width = Width;
	}
	if (_Height == -1) {
		_Height = Height;
	}

	SDL_Rect renderQuad = { x, y, _Width, _Height };
	
	SDL_RenderCopy(m_Renderer, m_Texture, NULL, &renderQuad);
}

bool Texture::LoadMedia(std::string _path) {
	
	//Get rid of existing Texture
	Free();

	SDL_Texture* newTexture = NULL;
	SDL_Surface* loadedSurface = IMG_Load(_path.c_str());
	SDL_SetColorKey(loadedSurface, SDL_TRUE, SDL_MapRGB(loadedSurface->format, 0, 0xFF, 0xFF));
	
	newTexture = SDL_CreateTextureFromSurface(m_Renderer, loadedSurface);

	Width = loadedSurface->w;
	Height = loadedSurface->h;

	SDL_FreeSurface(loadedSurface);

	m_Texture = newTexture;
	return m_Texture != NULL;
}

void Texture::CreateFromSurface(SDL_Surface* _surface)
{
	if (m_Texture != NULL) {

		Free();
	}

	m_Texture = SDL_CreateTextureFromSurface(m_Renderer, _surface);

	Width = _surface->w;
	Height = _surface->h;
}