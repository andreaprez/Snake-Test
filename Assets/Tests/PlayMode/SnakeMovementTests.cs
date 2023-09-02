using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SnakeMovementTests {
    [UnityTest]
    public IEnumerator SnakeMovementUp() {
        Snake snake = InitSnake();
        
        var initialPos = snake.transform.position;
        MoveSnake(snake, Snake.Direction.Up);
        var newPos = snake.transform.position;
        
        Assert.True(newPos.y > initialPos.y);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator SnakeMovementDown() {
        Snake snake = InitSnake();
        
        var initialPos = snake.transform.position;
        MoveSnake(snake, Snake.Direction.Down);
        var newPos = snake.transform.position;
        
        Assert.True(newPos.y < initialPos.y);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SnakeMovementRight() {
        Snake snake = InitSnake();
        
        var initialPos = snake.transform.position;
        MoveSnake(snake, Snake.Direction.Right);
        var newPos = snake.transform.position;
        
        Assert.True(newPos.x > initialPos.x);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator SnakeMovementLeft() {
        Snake snake = InitSnake();
        
        var initialPos = snake.transform.position;
        MoveSnake(snake, Snake.Direction.Left);
        var newPos = snake.transform.position;
        
        Assert.True(newPos.x < initialPos.x);
        yield return null;
    }

    
    private Snake InitSnake() {
        var gameObject = new GameObject();
        var snake = gameObject.AddComponent<Snake>();
        snake.Setup(new LevelGrid(20, 20));
        snake.SetGridPosition(new Vector2Int(10, 10));
        snake.transform.position = new Vector3(snake.GetGridPosition().x, snake.GetGridPosition().y);
        return snake;
    }

    private void MoveSnake(Snake snake, Snake.Direction direction) {
        snake.SetGridMoveDirection(direction);
        var directionVector = snake.UpdateGridPosition();
        snake.Move(directionVector);
    }
}
