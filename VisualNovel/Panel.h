#pragma once
#include <string>
#include <vector>
#include <map>

class TextBox;
class ImageLoader;
class Texture;
class Condition;
class SpritePosition;
class Button;
class ShownItem;
class SDL_Renderer;
class Branch;

class Panel {

	public:
		Panel(TextBox* _textBoxReference, ImageLoader* _imageLoaderReference);
		~Panel();

		//TODO Background Image entfernen und m_SpriteList[0] verwenden
		int m_BackgroundImageIndex;
		Texture* m_BackgroundImage = nullptr;
		TextBox* m_TextBox = nullptr;
		ImageLoader* m_ImageLoader;							//TODO statische Image Loader Referenz
		Condition* m_PanelCondition;
		std::string m_PanelName;
		std::map<std::string, Branch*> m_Branches;
		std::vector<SpritePosition> m_SpriteIndexList;		//TODO Liste wieder auf ints zurueck setzen
		std::vector<Texture*> m_SpriteList;
		std::string m_currentBranchKey;
		std::string m_EntryBranchKey;
		/**
		Displays the Line corresponding to the index that is handed over
		@param _lineIndex: The Index of the Line in this Panel
		*/
		void ShowLine(int _lineIndex);
		void ShowSplit(int _lineIndex);
		void LoadImages();
		void RenderCurrentSplit(int _lineIndex);
		Branch* GetCurrentBranch();
};