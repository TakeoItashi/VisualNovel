#include "SpriteSheetTexture.h"

SpriteSheetTexture::SpriteSheetTexture(SDL_Renderer* _Renderer) : Texture(_Renderer)
{
	m_currentSprite = ButtonSpriteState::BUTTON_SPRITE_MOUSE_OUT;
}

SpriteSheetTexture::~SpriteSheetTexture()
{
}

bool SpriteSheetTexture::LoadMedia(std::string _path)
{
	//Get rid of existing Texture
	Free();

	SDL_Texture* newTexture = NULL;
	SDL_Surface* loadedSurface = IMG_Load(_path.c_str());
	SDL_SetColorKey(loadedSurface, SDL_TRUE, SDL_MapRGB(loadedSurface->format, 0, 0xFF, 0xFF));

	newTexture = SDL_CreateTextureFromSurface(m_Renderer, loadedSurface);
	m_SpriteClips[0].x = 0;
	m_SpriteClips[0].y = 0;
	m_SpriteClips[0].w = 512;
	m_SpriteClips[0].h = 512;

	m_SpriteClips[1].x = 512;
	m_SpriteClips[1].y = 0;
	m_SpriteClips[1].w = 512;
	m_SpriteClips[1].h = 512;

	m_SpriteClips[2].x = 0;
	m_SpriteClips[2].y = 512;
	m_SpriteClips[2].w = 512;
	m_SpriteClips[2].h = 512;

	Width = loadedSurface->w;
	Height = loadedSurface->h;

	SDL_FreeSurface(loadedSurface);

	m_Texture = newTexture;
	return m_Texture != NULL;
}

void SpriteSheetTexture::Render(int x, int y, int _Height, int _Width, ButtonSpriteState _State)
{
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

	SDL_RenderCopy(m_Renderer, m_Texture, &m_SpriteClips[m_currentSprite], &renderQuad);

	//SDL_RenderCopy(m_Renderer, m_Texture, NULL, &renderQuad);
}

void SpriteSheetTexture::SetSprite(SpriteSheetTexture* _sprite)
{
	m_Texture = _sprite->m_Texture;
	m_SpriteClips[0] = _sprite->m_SpriteClips[0];
	m_SpriteClips[1] = _sprite->m_SpriteClips[1];
	m_SpriteClips[2] = _sprite->m_SpriteClips[2];
}
