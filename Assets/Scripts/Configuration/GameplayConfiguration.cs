using UnityEngine;

[CreateAssetMenu(menuName = "Game Config/Gameplay Configuration")]
public class GameplayConfiguration : ScriptableObject {

    [Header("Level")]
    public int LevelWidth = 20;
    public int LevelHeight = 20;

    [Header("Snake")]
    public float SnakeMovementTime = .2f;
    
    [Header("PoolManager")]
    public int AudioPoolAmount = 10;
    public int FoodPoolAmount = 5;
    
}
