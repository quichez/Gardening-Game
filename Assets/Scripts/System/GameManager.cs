using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Idle, Paused}
public enum IdleState { Nothing, TileSelected, PlantSelected}

public class GameManager : SingletonPersistent<GameManager>
{
    public GameState gameState;
    public IdleState idleState;

    
}
