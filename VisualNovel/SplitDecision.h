#pragma once
#include <vector>
#include "ShownItem.h"
#include "BranchOption.h"
#include "Button.h"
#include "Settings.h"
#include "ImageLoader.h"

class SplitDecision : public ShownItem {
public:
	SplitDecision(SDL_Renderer* _renderer, Settings* _settings, ImageLoader* _imageLoader);
	~SplitDecision();

	std::vector<BranchOption*> m_options;
	std::vector<Button*> m_buttons;

	void CreateButtons();
	void RenderOptions();
private:
	SDL_Renderer* m_Renderer;
	Settings* m_settings;
	ImageLoader* m_imageLoader;
};