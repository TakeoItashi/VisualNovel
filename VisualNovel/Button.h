#pragma once
#include "Texture.h"
#include <functional>

class Button : public Texture {

	public:
		//Standard Constructor
		//Creates a new Instance of a Button
		//@param _Renderer: The Renderer, that renders this Button
		//@param _callBack: The Adress of a function that is called when the Button is pressed.
		Button(SDL_Renderer* _Renderer, std::function<void(Button*)> _callBack);
		//Standard Deconstructor
		~Button();
		//The index of the Texture in the Texture define file
		int TextureIndex;
		//The Texture of the Button
		Texture* m_textTexture;
		//Handles the Events for the Button
		//@param A pointer to the event that is passed to the Button
		//@return true for a Button press
		void HandleEvent(SDL_Event* _event);
		std::function<void(Button*)> m_delegateFunction;
	private:
};
