#include "Texture.h"

Texture::Texture(SDL_Renderer* _renderer) {

	m_Texture = NULL;
	Width = 0;
	Height = 0;
	m_Renderer = _renderer;
	SetBlendMode(SDL_BLENDMODE_BLEND);
}

Texture::~Texture() {
	Free();
}

void Texture::SetBlendMode(SDL_BlendMode _blending)
{
	SDL_SetTextureBlendMode(m_Texture, _blending);
}

int Texture::SetAlpha(Uint8 _alpha)
{
	return SDL_SetTextureAlphaMod(m_Texture, _alpha);
}

void Texture::Free() {

	if (m_Texture != NULL) {

		SDL_DestroyTexture(m_Texture);
		m_Texture = NULL;
		Width = 0;
		Height = 0;
	}
}

void Texture::Render(int x, int y, int _Height, int _Width) {

	if (_Width < 0) {
		_Width = Width;		//TODO Screen Width
	}
	if (_Height < 0) {
		_Height = Height;	//TODO Screen Height
	}
	if (x < 0) {
		x = PosX;
	}
	if (y < 0) {
		y = PosY;
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

void Texture::SetTexture(Texture* _texture) {

	m_Texture = _texture->m_Texture;
}
