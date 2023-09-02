/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;

public class LevelGrid {

    private Vector2Int foodGridPosition;
    private Food foodGameObject;
    private int width;
    private int height;
    private PlayerSnake snake;

    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public void Setup(PlayerSnake snake) {
        this.snake = snake;

        SpawnFood();
    }

    public void SpawnFood() {
        do {
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        if (PoolManager.instance) {
            foodGameObject = PoolManager.instance.GetPoolObject(ObjectPoolType.Food).GetComponent<Food>();
            foodGameObject.gameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
            foodGameObject.gameObject.SetActive(true);
        }
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == foodGridPosition) {
            Score.AddScore(foodGameObject.GetScoreAmount());
            if (PoolManager.instance) {
                PoolManager.instance.ResetPoolObject(foodGameObject.gameObject, ObjectPoolType.Food);
            }
            SpawnFood();
            return true;
        }
        return false;
    }

    public Vector2Int GetFoodGridPosition() {
        return foodGridPosition;
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        if (gridPosition.x < 0) {
            gridPosition.x = width - 1;
        }
        if (gridPosition.x > width - 1) {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0) {
            gridPosition.y = height - 1;
        }
        if (gridPosition.y > height - 1) {
            gridPosition.y = 0;
        }
        return gridPosition;
    }

}
