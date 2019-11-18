using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using System.IO;
using SwinGameSDK;

/// <summary>
/// The menu controller handles the drawing and user interactions
/// from the menus in the game. These include the main menu, game
/// menu and the settings m,enu.
/// </summary>

static class MenuController
{

	private const int MENU_TOP = 530;
	private const int MENU_LEFT = 30;
	private const int MENU_GAP = 10;
	private const int BUTTON_WIDTH = 90;
	private const int BUTTON_HEIGHT = 30;
	private const int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;
	private const int TEXT_OFFSET = 0;

	/// <summary>
	/// The menu structure for the game.
	/// </summary>
	/// <remarks>
	/// These are the text captions for the menu items.
	/// </remarks>
	private static readonly string [] [] _menuStructure = {
		new string[] {
			"PLAY",
			"HOW TO PLAY",
			"DIFFICULTY",
			"SCORES",
			"OPTION",
			"MUTE",
			"QUIT"
		},
		new string[] {
			"RETURN",
			"SURRENDER",
			"QUIT"
		},
		new string[] {
			"EASY",
			"MEDIUM",
			"HARD"
		},

		new string[] {
			"BACK"
		},

		new string[]{
			"FULLSCREEN",
			"BORDERLESS"
		},

		new string[] {
			"MUTE",
			"UNMUTE"
		},
		new string[] {
			"RESET"
		},


	};

	private const int MAIN_MENU = 0;
	private const int GAME_MENU = 1;
	private const int SETUP_MENU = 2;
	private const int BACK_MENU = 3;
	private const int OPTION_MENU = 4;
	private const int MUTE_MENU = 5;
	private const int RESET_MENU = 6;

	private const int MAIN_MENU_PLAY_BUTTON = 0;
	private const int MAIN_MENU_HOWTOPLAY_BUTTON = 1;
	private const int MAIN_MENU_SETUP_BUTTON = 2;
	private const int MAIN_MENU_TOP_SCORES_BUTTON = 3;
	private const int MAIN_MENU_OPTION_BUTTON = 4;
	private const int MAIN_MENU_MUTE_BUTTON = 5;
	private const int MAIN_MENU_QUIT_BUTTON = 6;

	private const int SETUP_MENU_EASY_BUTTON = 0;
	private const int SETUP_MENU_MEDIUM_BUTTON = 1;
	private const int SETUP_MENU_HARD_BUTTON = 2;
	private const int SETUP_MENU_EXIT_BUTTON = 3;

	private const int GAME_MENU_RETURN_BUTTON = 0;
	private const int GAME_MENU_SURRENDER_BUTTON = 1;
	private const int GAME_MENU_QUIT_BUTTON = 2;

	private const int OPTION_MENU_FULLSCREEN_BUTTON = 0;
	private const int OPTION_MENU_BORDERLESS_BUTTON = 1;

	private static readonly Color MENU_COLOR = SwinGame.RGBAColor(255, 255, 255, 255);

	private static readonly Color HIGHLIGHT_COLOR = SwinGame.RGBAColor(1, 57, 86, 255);
	/// <summary>
	/// Handles the processing of user input when the main menu is showing.
	/// </summary>
	public static void HandleMainMenuInput()
	{
		HandleMenuInput(MAIN_MENU, 0, 0);
	}

	/// <summary>
	/// Handles the processing of user input when the main menu is showing.
	/// </summary>
	public static void HandleSetupMenuInput()
	{
		bool handled = false;
		handled = HandleMenuInput(SETUP_MENU, 1, 1);

		if (!handled) {
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handles the processing of user input when the main menu is showing.
	/// </summary>
	public static void HandleOptionMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (OPTION_MENU, 1, 3);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handle input in the game menu.
	/// </summary>
	/// <remarks>
	/// Player can return to the game, surrender, or quit entirely
	/// </remarks>
	public static void HandleGameMenuInput()
	{
		HandleMenuInput(GAME_MENU, 0, 0);
	}

	/// <summary>
	/// Handles the input for back button.
	/// </summary>
	public static void HandleBackMenuInput ()
	{
		HandleMenuInput (BACK_MENU, 0, 0);
	}

	public static void HandleResetMenuInput ()
	{
		HandleMenuInput (RESET_MENU, 0, 0);
	}

	/// <summary>
	/// Handles input for the specified menu.
	/// </summary>
	/// <param name="menu">the identifier of the menu being processed</param>
	/// <param name="level">the vertical level of the menu</param>
	/// <param name="xOffset">the xoffset of the menu</param>
	/// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
	private static bool HandleMenuInput(int menu, int level, int xOffset)
	{
		if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			GameController.EndCurrentState();
			return true;
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			int i = 0;
			for (i = 0; i <= _menuStructure[menu].Length - 1; i++) {
				//IsMouseOver the i'th button of the menu
				if (IsMouseOverMenu(i, level, xOffset)) {
					PerformMenuAction(menu, i);
					return true;
				}
			}

			if (level > 0) {
				//none clicked - so end this sub menu
				GameController.EndCurrentState();
			}
		}

		return false;
	}

	/// <summary>
	/// Draws the main menu to the screen.
	/// </summary>
	public static void DrawMainMenu()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Main Menu", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons(MAIN_MENU);
	}

	/// <summary>
	/// Draws the Game menu to the screen
	/// </summary>
	public static void DrawGameMenu()
	{
		//Clears the Screen to Black
		SwinGame.DrawText ("Paused", Color.White, GameResources.GameFont ("ArialLarge"), 50, 50);

		DrawButtons(GAME_MENU);
	}

	/// <summary>
	/// Draws the Option menu to the screen.
	/// </summary>
	public static void DrawOption ()
	{
		DrawButtons (MAIN_MENU);
		DrawButtons (OPTION_MENU, 1, 3);
	}

	/// <summary>
	/// Draws the settings menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawSettings()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Settings", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons(MAIN_MENU);
		DrawButtons(SETUP_MENU, 1, 1);
	}

	/// <summary>
	/// Draws the back button.
	/// </summary>
	public static void DrawBackMenuButton ()
	{
		DrawButtons (BACK_MENU);
	}

	/// <summary>
	/// Draws the reset button.
	/// </summary>
	public static void DrawResetMenuButton ()
	{
		DrawButtons (RESET_MENU);
	}

	/// <summary>
	/// Draw the buttons associated with a top level menu.
	/// </summary>
	/// <param name="menu">the index of the menu to draw</param>
	private static void DrawButtons(int menu)
	{
		DrawButtons(menu, 0, 0);
	}

	/// <summary>
	/// Draws the menu at the indicated level.
	/// </summary>
	/// <param name="menu">the menu to draw</param>
	/// <param name="level">the level (height) of the menu</param>
	/// <param name="xOffset">the offset of the menu</param>
	/// <remarks>
	/// The menu text comes from the _menuStructure field. The level indicates the height
	/// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
	/// to allow the submenus to be positioned correctly.
	/// </remarks>
	private static void DrawButtons (int menu, int level, int xOffset)
	{
		int btnTop = 0;

		btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int i = 0;
		for (i = 0; i <= _menuStructure [menu].Length - 1; i++) {
			int btnLeft = 0;
			btnLeft = MENU_LEFT + BUTTON_SEP * (i + xOffset);
			//SwinGame.FillRectangle(Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT)
			SwinGame.DrawTextLines (_menuStructure [menu] [i], MENU_COLOR, Color.Black, GameResources.GameFont ("Menu"), FontAlignment.AlignCenter, btnLeft + TEXT_OFFSET, btnTop + TEXT_OFFSET, BUTTON_WIDTH, BUTTON_HEIGHT);

			if (SwinGame.MouseDown (MouseButton.LeftButton) & IsMouseOverMenu (i, level, xOffset)) {
				SwinGame.DrawRectangle (HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}

		for (int a = 0; a < _menuStructure [menu].Length; a++) {
			string btnText = _menuStructure [menu] [a];
			int btnLeft = MENU_LEFT + BUTTON_SEP * (a + xOffset);
			float x = btnLeft + TEXT_OFFSET;
			float y = btnTop + TEXT_OFFSET;
			int w = BUTTON_WIDTH;
			int h = BUTTON_HEIGHT;

			if (GameResources.Muted && a == MAIN_MENU_MUTE_BUTTON) {
				btnText = "UNMUTE";
			}

			if (IsMouseOverMenu (a, level, xOffset)) {
				const int numExpandFrames = 9; // 9 would gives us 0 1 2 3 4 3 2 1 0 (when we're done setting up "expExt") 
				int expExt = (int)(GameController.HighlightTimer.Ticks / 66) % numExpandFrames; // expansion extent (num of pixels outward from normal size) 
				if (expExt > numExpandFrames / 2) {
					expExt = (numExpandFrames - 1) - expExt;
				}

				SwinGame.DrawTextLines (btnText, Color.Yellow, Color.Black, GameResources.GameFont ("Menu"),
					FontAlignment.AlignCenter, x, y - expExt / 2 + expExt, w, h + expExt * 2);

				if (SwinGame.MouseDown (MouseButton.LeftButton)) {
					SwinGame.DrawRectangle (HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
				}
			} else {
				SwinGame.DrawTextLines (btnText, MENU_COLOR, Color.Black, GameResources.GameFont ("Menu"),
					FontAlignment.AlignCenter, x, y, w, h);
			}
		}
	}
	/// <summary>
	/// Determined if the mouse is over one of the button in the main menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <returns>true if the mouse is over that button</returns>
	private static bool IsMouseOverButton(int button)
	{
		return IsMouseOverMenu(button, 0, 0);
	}

	/// <summary>
	/// Checks if the mouse is over one of the buttons in a menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <param name="level">the level of the menu</param>
	/// <param name="xOffset">the xOffset of the menu</param>
	/// <returns>true if the mouse is over the button</returns>
	private static bool IsMouseOverMenu(int button, int level, int xOffset)
	{
		int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int btnLeft = MENU_LEFT + BUTTON_SEP * (button + xOffset);

		return UtilityFunctions.IsMouseInRectangle(btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
	}

	/// <summary>
	/// A button has been clicked, perform the associated action.
	/// </summary>
	/// <param name="menu">the menu that has been clicked</param>
	/// <param name="button">the index of the button that was clicked</param>
	private static void PerformMenuAction(int menu, int button)
	{
		switch (menu) {
			case MAIN_MENU:
				PerformMainMenuAction(button);
				break;
			case SETUP_MENU:
				PerformSetupMenuAction(button);
				break;
			case GAME_MENU:
				PerformGameMenuAction(button);
				break;
			case MUTE_MENU:
				GameResources.MuteButtonPressed ();
				break;
			case OPTION_MENU:
				PerformOptionMenuAction (button);
				break;
			case BACK_MENU:
				PerformBackMenuAction (button);
				break;
			case RESET_MENU:
				PerformResetMenuAction (button);
				break;

		}
	}



	/// <summary>
	/// The main menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformMainMenuAction(int button)
	{
		switch (button) {
			case MAIN_MENU_PLAY_BUTTON:
				GameController.StartGame();
				break;
			case MAIN_MENU_HOWTOPLAY_BUTTON:
				GameController.AddNewState (GameState.ViewHowToPlay);
				break;
			case MAIN_MENU_SETUP_BUTTON:
				GameController.AddNewState(GameState.AlteringSettings);
				break;
			case MAIN_MENU_TOP_SCORES_BUTTON:
				GameController.AddNewState(GameState.ViewingHighScores);
				break;
			case MAIN_MENU_MUTE_BUTTON:
				GameResources.MuteButtonPressed ();
				break;
			case MAIN_MENU_OPTION_BUTTON:
				GameController.AddNewState (GameState.AlteringOption);
				break;
			case MAIN_MENU_QUIT_BUTTON:
				GameController.EndCurrentState();
				break;
		}
	}

	/// <summary>
	/// The setup menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSetupMenuAction(int button)
	{
		switch (button) {
			case SETUP_MENU_EASY_BUTTON:
				GameController.SetDifficulty(AIOption.Easy);
				break;
			case SETUP_MENU_MEDIUM_BUTTON:
				GameController.SetDifficulty(AIOption.Medium);
				break;
			case SETUP_MENU_HARD_BUTTON:
				GameController.SetDifficulty(AIOption.Hard);
				break;
		}
		//Always end state - handles exit button as well
		GameController.EndCurrentState();
	}

	/// <summary>
	/// The option menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">Button.</param>
	private static void PerformOptionMenuAction (int button)
	{
		switch (button) {
		case OPTION_MENU_FULLSCREEN_BUTTON:
			SwinGame.ToggleFullScreen ();
			break;
		case OPTION_MENU_BORDERLESS_BUTTON:
			SwinGame.ToggleWindowBorder ();
			break;
		}
		GameController.EndCurrentState ();
	}

	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformGameMenuAction(int button)
	{
		switch (button) {
			case GAME_MENU_RETURN_BUTTON:
				GameController.EndCurrentState();
				break;
			case GAME_MENU_SURRENDER_BUTTON:
				GameController.EndCurrentState();
				//end game menu
				GameController.EndCurrentState();
				//end game
				break;
			case GAME_MENU_QUIT_BUTTON:
				GameController.AddNewState(GameState.Quitting);
				break;
		}
	}

	/// <summary>
	/// The back menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">Button.</param>
	public static void PerformBackMenuAction (int button)
	{
		GameController.EndCurrentState ();
	}

	/// <summary>
	/// The reset menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">Button.</param>
	public static void PerformResetMenuAction (int button)
	{
		string filename = null;
		filename = SwinGame.PathToResource ("highscores.txt");
		File.WriteAllText (filename, string.Empty);
		File.AppendAllText (filename, "10"+Environment.NewLine);
		File.AppendAllText (filename, "AAA0" + Environment.NewLine);
		File.AppendAllText (filename, "BBB0" + Environment.NewLine);
		File.AppendAllText (filename, "CCC0" + Environment.NewLine);
		File.AppendAllText (filename, "DDD0" + Environment.NewLine);
		File.AppendAllText (filename, "EEE0" + Environment.NewLine);
		File.AppendAllText (filename, "FFF0" + Environment.NewLine);
		File.AppendAllText (filename, "GGG0" + Environment.NewLine);
		File.AppendAllText (filename, "HHH0" + Environment.NewLine);
		File.AppendAllText (filename, "III0" + Environment.NewLine);
		File.AppendAllText (filename, "JJJ0" + Environment.NewLine);
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
