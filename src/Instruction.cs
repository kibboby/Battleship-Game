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
		SwinGame.DrawText ("HOW TO PLAY BATTLESHIP GAME",Color.Black,GameResources.GameFont("Arial"),LEFT,20);
		SwinGame.DrawText ("-----------------------------------------------------------------------------------------------------------",Color.White,GameResources.GameFont("Arial"),0,40);
		SwinGame.DrawText (">>Player will take turn to fire shots on opponent's battleship board", Color.Black, GameResources.GameFont ("Game"), LEFT, 90);
		SwinGame.DrawText (">>Upon player's turn, click on a tile on the opponent's board to hit the opponent's ship", Color.Black, GameResources.GameFont ("Game"), LEFT, 120);
		SwinGame.DrawText (">>If the player manage to hit a tile which contains a ship,", Color.Black, GameResources.GameFont ("Game"), LEFT, 150);
		SwinGame.DrawText ("  the tile will turn into red coloured background",Color.Black, GameResources.GameFont ("Game"), LEFT, 170);
		SwinGame.DrawText (">>If the tile is empty, the tile will turn into blue colored background", Color.Black, GameResources.GameFont ("Game"), LEFT, 200);
		SwinGame.DrawText (">>The player will get another chance to hit the opponent's board if ", Color.Black, GameResources.GameFont ("Game"), LEFT, 230);
		SwinGame.DrawText ("  player manage to hit the tile that contains a ship",Color.Black, GameResources.GameFont ("Game"), LEFT, 250);
		SwinGame.DrawText (">>Otherwise, the other player will have their turn to hit the opponent's board.", Color.Black, GameResources.GameFont ("Game"), LEFT, 280);
		SwinGame.DrawText (">>The game consist 5 ships with different sizes.", Color.Black, GameResources.GameFont ("Game"), LEFT, 310);
		SwinGame.DrawText (">>Therefore, the player who hit all the 5 five ships first wins", Color.Black, GameResources.GameFont ("Game"), LEFT, 340);
		SwinGame.DrawText (">>Each round of the game is only limited to 2 minutes and 30 seconds", Color.Black, GameResources.GameFont ("Game"), LEFT, 370);
		SwinGame.DrawText (">>Player need to finish the game in the timegiven,or else the player will lose in the game", Color.Black, GameResources.GameFont ("Game"), LEFT, 400);
		SwinGame.DrawText ("-----------------------------------------------------------------------------------------------------------", Color.White, GameResources.GameFont ("Arial"), 0, 410);
		SwinGame.DrawText ("Press 'Esc' or 'Space' or 'Left Mouse Key' to exit the page", Color.Black,GameResources.GameFont("Game"),LEFT+80, 450);
	}

	public static void ViewInstruction ()
	{
		GameController.AddNewState (GameState.ViewHowToPlay);
		UtilityFunctions.DrawBackground ();
		WriteInstruction ();
	}
}
