#include "Game.h"
#include "SplitDecision.h"
#include <SDL_ttf.h>

SplitDecision::SplitDecision(SDL_Renderer* _renderer, Settings* _settings, ImageLoader* _imageLoader) {
	m_Renderer = _renderer;
	m_settings = _settings;
	m_imageLoader = _imageLoader;
}

SplitDecision::~SplitDecision() {

}

void SplitDecision::CreateButtons() {

	int aviableButtonSpace = (600 / (m_options.size() + 1));

	//SDL_Surface* buttonsSurface = SDL_CreateRGBSurface(0, 800, 600, 32, 0, 0, 0, 0);
	Texture* m_textTexture = new Texture(m_Renderer);
	Texture* m_boxBackground = new Texture(m_Renderer);
	//SDL_FillRect(buttonsSurface, NULL, SDL_MapRGB(buttonsSurface->format, m_settings->m_TextBoxRed, m_settings->m_TextBoxGreen, m_settings->m_TextBoxBlue));
	//m_boxBackground->CreateFromSurface(buttonsSurface);
	//SDL_FreeSurface(buttonsSurface);
	//TODO checke Settings auf Alpha für TextBox
	m_boxBackground->SetBlendMode(SDL_BLENDMODE_BLEND);
	////TODO SetAlpha Änderungen spiegeln sich nicht in der Textbox wieder.
	//m_boxBackground->SetAlpha((m_settings->m_TextBoxAlpha * 0.5));

	for (int i = 0; i < m_options.size(); ++i) {
		Button* newButton = new Button(m_Renderer);

		m_textTexture = new Texture(m_Renderer);
		newButton->SetSprite(m_imageLoader->GetSprite(m_options[i]->SpriteIndex));
		SDL_Surface* textSurface = TTF_RenderText_Blended(m_settings->m_Font, m_options[i]->m_shownText.c_str(), SDL_Color{ 0, 0, 0 });
		m_textTexture->CreateFromSurface(textSurface);
		newButton->m_textTexture = m_textTexture;
		TTF_SizeText(m_settings->m_Font, m_options[i]->m_shownText.c_str(), &newButton->Width, &newButton->Height);	//TODO Breite des Textes an Button oder vice versa anpassen
		newButton->PosX = (800 / 2) - (newButton->Width / 2);	//Middle of the screen = (ScreenWidth/2)-buttonWidth
		newButton->PosY = (aviableButtonSpace * (i + 1));
		m_textTexture->PosX = newButton->PosX;
		m_textTexture->PosY = newButton->PosY;
		SDL_FreeSurface(textSurface);

		switch (m_options[i]->m_type) {
			case ContinueType::BranchContinue:
				newButton->m_delegateFunction = std::bind(&Game::GetInstance()->ChangeBranch, m_options[i]->m_continueKey.c_str());
				break;
			case ContinueType::PanelContinue:
				newButton->m_delegateFunction = std::bind(&Game::GetInstance()->ChangePanel, m_options[i]->m_continueKey.c_str());
				break;
		}
		
		m_buttons.push_back(newButton);
	}
}

void SplitDecision::RenderOptions() {

	for (int i = 0; i < m_buttons.size(); i++) {

		m_buttons[i]->Render();
		m_buttons[i]->m_textTexture->Render();
	}
	SDL_RenderPresent(m_Renderer);
}
