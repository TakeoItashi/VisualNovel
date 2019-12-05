#pragma once
#include "ButtonSpriteState.h"
#include "Texture.h"
#include <functional>
#include <SDL.h>

class SpriteSheetTexture : public Texture {
public:
	SpriteSheetTexture(SDL_Renderer* _Renderer);
	~SpriteSheetTexture();

	bool LoadMedia(std::string _path) override;
	void Render(int x = -1, int y = -1, int _height = -1, int _width = -1, ButtonSpriteState _State = ButtonSpriteState::BUTTON_SPRITE_MOUSE_OUT);
	void SetSprite(SpriteSheetTexture* _sprite);
protected:
	ButtonSpriteState m_currentSprite;
private:
	SDL_Rect m_SpriteClips[3];
};