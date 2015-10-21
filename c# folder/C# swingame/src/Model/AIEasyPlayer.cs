using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
/// <summary>
/// The AIEasyPlayer is a type of AIPlayer where it will shoot at random
/// if it has found a ship
/// </summary>
public class AIEasyPlayer : AIPlayer
{
    /// <summary>
    /// Private enumarator for AI states. currently there are two states,
    /// the AI can be searching for a ship, or if it has found a ship it will
    /// target the same ship
    /// </summary>
    private enum AIStates
    {
        Searching
    }

    private AIStates _CurrentState = AIStates.Searching;

    private Stack<Location> _Targets = new Stack<Location>();
    public AIEasyPlayer(BattleShipsGame controller) : base(controller)
    {
    }

    /// <summary>
	/// GenerateCoordinates should generate random shooting coordinates
	/// only when it has not found a ship, or has destroyed a ship and 
	/// needs new shooting coordinates
	/// </summary>
	/// <param name="row">the generated row</param>
	/// <param name="column">the generated column</param>
	protected override void GenerateCoords(ref int row, ref int column)
    {
        do
        {
            //check which state the AI is in and uppon that choose which coordinate generation
            //method will be used.
            switch (_CurrentState)
            {
                case AIStates.Searching:
                    SearchCoords(ref row, ref column);
                    break;
                case AIStates.TargetingShip:
                    TargetCoords(ref row, ref column);
                    break;
                default:
                    throw new ApplicationException("AI has gone in an imvalid state");
            }
        } while ((row < 0 || column < 0 || row >= EnemyGrid.Height || column >= EnemyGrid.Width || EnemyGrid[row, column] != TileView.Sea));
        //while inside the grid and not a sea tile do the search
    }
}

 

