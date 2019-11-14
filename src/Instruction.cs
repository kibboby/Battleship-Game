using System;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using SwinGameSDK;

public class Instruction
{
	private const int LEFT = 20;
	private const int TOP = 50;

	public static void ExitInstruction ()
	{
		if (SwinGame.MouseClicked (MouseButton.LeftButton) ||SwinGame.KeyTyped (KeyCode.vk_ESCAPE)
		   || SwinGame.KeyTyped (KeyCode.vk_RETURN)) 
		{
			
			GameController.EndCurrentState ();
		}
	}

	public static void WriteInstruction ()
	{
		SwinGame.DrawText ("HOW TO PLAY BATTLESHIP GAME",Color.Aqua,GameResources.GameFont("Courier"),LEFT,20);
		SwinGame.DrawText ("Player will take turn to fire shots on opponent's battleship board", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 50);
		SwinGame.DrawText ("Upon player's turn, click on a tile on the opponent's board to hit the opponent's ship", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 90);
		SwinGame.DrawText ("If the player manage to hit a tile which contains a ship, the tile will turn into red coloured background", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 120);
		SwinGame.DrawText ("If the tile is empty, the tile will turn into blue colored background", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 150);
		SwinGame.DrawText ("The player will get another chance to hit the opponent's board if ", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 190);
		SwinGame.DrawText ("player manage to hit the tile that contains a ship",Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 230);
		SwinGame.DrawText ("Otherwise, the other player will have their turn to hit the opponent's board.", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 260);
		SwinGame.DrawText ("The game consist 5 ships with different sizes.", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 300);
		SwinGame.DrawText ("Therefore, the player who hit all the 5 five ships first wins", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 340);
		SwinGame.DrawText ("Each round of the game is only limited to 2 minutes and 30 seconds", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 380);
		SwinGame.DrawText ("Player need to finish the game in the timegiven,or else the player will lose in the game", Color.Aqua, GameResources.GameFont ("Courier"), LEFT, 430);
	}

	public static void ViewInstruction ()
	{
		GameController.AddNewState (GameState.ViewHowToPlay);
		UtilityFunctions.DrawBackground ();
		WriteInstruction ();
	}
}
