using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridTests {
    [UnityTest]
    public IEnumerator GridValidatePositionUp() {
        var levelGrid = new LevelGrid(20, 20);

        var position = new Vector2Int(0, 20);
        position = levelGrid.ValidateGridPosition(position);
        
        Assert.True(position.y == 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator GridValidatePositionDown() {
        var levelGrid = new LevelGrid(20, 20);

        var position = new Vector2Int(0, -1);
        position = levelGrid.ValidateGridPosition(position);
        
        Assert.True(position.y == 19);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator GridValidatePositionRight() {
        var levelGrid = new LevelGrid(20, 20);

        var position = new Vector2Int(20, 0);
        position = levelGrid.ValidateGridPosition(position);
        
        Assert.True(position.x == 0);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator GridValidatePositionLeft() {
        var levelGrid = new LevelGrid(20, 20);

        var position = new Vector2Int(-1, 0);
        position = levelGrid.ValidateGridPosition(position);
        
        Assert.True(position.x == 19);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator GridValidatePositionNone() {
        var levelGrid = new LevelGrid(20, 20);

        var position = new Vector2Int(10, 10);
        position = levelGrid.ValidateGridPosition(position);
        
        Assert.True(position.x == 10 && position.y == 10);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator GridEatFoodFromFoodCell() {
        var levelGrid = new LevelGrid(20, 20);
        Snake snake = InitSnake(levelGrid);
        levelGrid.Setup(snake);
        
        levelGrid.SpawnFood();
        var foodPos = levelGrid.GetFoodGridPosition();
        var ateFood = levelGrid.TrySnakeEatFood(foodPos);
        
        Assert.True(ateFood);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator GridEatFoodFromEmptyCell() {
        var levelGrid = new LevelGrid(20, 20);
        Snake snake = InitSnake(levelGrid);
        levelGrid.Setup(snake);
        
        levelGrid.SpawnFood();
        var foodPos = levelGrid.GetFoodGridPosition();

        var tryEatPos = new Vector2Int();
        do {
            tryEatPos = new Vector2Int(Random.Range(0, 20), Random.Range(0, 20));
        } while (tryEatPos == foodPos);
        var ateFood = levelGrid.TrySnakeEatFood(tryEatPos);
        
        Assert.False(ateFood); 
        yield return null;
    }

    [UnityTest]
    public IEnumerator SnakeCheckCollisionTrue() {
        var levelGrid = new LevelGrid(20, 20);
        Snake snake = InitSnake(levelGrid);
        
        snake.SetGridPosition(new Vector2Int(10, 10));

        Assert.True(snake.CheckSnakeCollision(new Vector2Int(10, 10)));
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator SnakeCheckCollisionFalse() {
        var levelGrid = new LevelGrid(20, 20);
        Snake snake = InitSnake(levelGrid);
        
        snake.SetGridPosition(new Vector2Int(10, 10));

        Assert.False(snake.CheckSnakeCollision(new Vector2Int(2, 12)));
        yield return null;
    }

    private Snake InitSnake(LevelGrid levelGrid) {
        var gameObject = new GameObject();
        var snake = gameObject.AddComponent<Snake>();
        snake.Setup(levelGrid);
        return snake;
    }
}
