/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    public enum Direction {
        Left,
        Right,
        Up,
        Down
    }

    protected enum State { 
        Alive,
        Dead
    }

    protected State state;
    protected Direction gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    protected float gridMoveTimerMax;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }

    private void Awake() {
        Init();
    }

    protected virtual void Update() {
        switch (state) {
        case State.Alive:
            HandleGridMovement();
            break;
        case State.Dead:
            break;
        }
    }

    protected virtual void Init() {
        gridMoveTimer = gridMoveTimerMax;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        snakeBodySize = 0;
        
        state = State.Alive;
    }

    private void HandleGridMovement() {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax) {
            gridMoveTimer -= gridMoveTimerMax;
            
            SoundManager.PlaySound(SoundManager.Sound.SnakeMove);

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0) {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }

            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector = UpdateGridPosition();

            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood) {
                // Snake ate food, grow body
                snakeBodySize++;
                CreateSnakeBodyPart();
                SoundManager.PlaySound(SoundManager.Sound.SnakeEat);
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1) {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyParts();

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList) {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (CheckSnakeCollision(snakeBodyPartGridPosition)) {
                    // Game Over!
                    //CMDebug.TextPopup("DEAD!", transform.position);
                    Die();
                }
            }
            
            Move(gridMoveDirectionVector);
        }
    }

    public Vector2Int UpdateGridPosition() {
        Vector2Int gridMoveDirectionVector;
        
        switch (gridMoveDirection) {
            default:
            case Direction.Right:   gridMoveDirectionVector = new Vector2Int(+1, 0); break;
            case Direction.Left:    gridMoveDirectionVector = new Vector2Int(-1, 0); break;
            case Direction.Up:      gridMoveDirectionVector = new Vector2Int(0, +1); break;
            case Direction.Down:    gridMoveDirectionVector = new Vector2Int(0, -1); break;
        }

        gridPosition += gridMoveDirectionVector;
        gridPosition = levelGrid.ValidateGridPosition(gridPosition);

        return gridMoveDirectionVector;
    }

    public void Move(Vector2Int gridMoveDirectionVector) {
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);
    }

    public bool CheckSnakeCollision(Vector2Int position) {
        return gridPosition == position;
    }
    
    private void CreateSnakeBodyPart() {
        snakeBodyPartList.Add(new SnakeBodyPart(GameConfig.GetAssetsConfiguration().SnakeBodyPrefab));
    }

    private void UpdateSnakeBodyParts() {
        for (int i = 0; i < snakeBodyPartList.Count; i++) {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleFromVector(Vector2Int dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition() {
        return gridPosition;
    }

    public void SetGridPosition(Vector2Int pos) {
        gridPosition = pos;
    }
    
    public void SetGridMoveDirection(Direction direction) {
        gridMoveDirection = direction;
    }

    // Return the full list of positions occupied by the snake: Head + Body
    public List<Vector2Int> GetFullSnakeGridPositionList() {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList) {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }

    protected virtual void Die() {
        state = State.Dead;
    }

    /*
     * Handles a Single Snake Body Part
     * */
    private class SnakeBodyPart {

        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart(GameObject prefab) {
            SpriteRenderer instance = Instantiate(prefab).GetComponent<SpriteRenderer>();
            instance.sortingOrder = 1;
            transform = instance.gameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition) {
            this.snakeMovePosition = snakeMovePosition;

            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection()) {
            default:
            case Direction.Up: // Currently going Up
                switch (snakeMovePosition.GetPreviousDirection()) {
                default: 
                    angle = 0; 
                    break;
                case Direction.Left: // Previously was going Left
                    angle = 0 + 45; 
                    transform.position += new Vector3(.2f, .2f);
                    break;
                case Direction.Right: // Previously was going Right
                    angle = 0 - 45; 
                    transform.position += new Vector3(-.2f, .2f);
                    break;
                }
                break;
            case Direction.Down: // Currently going Down
                switch (snakeMovePosition.GetPreviousDirection()) {
                default: 
                    angle = 180; 
                    break;
                case Direction.Left: // Previously was going Left
                    angle = 180 - 45;
                    transform.position += new Vector3(.2f, -.2f);
                    break;
                case Direction.Right: // Previously was going Right
                    angle = 180 + 45; 
                    transform.position += new Vector3(-.2f, -.2f);
                    break;
                }
                break;
            case Direction.Left: // Currently going to the Left
                switch (snakeMovePosition.GetPreviousDirection()) {
                default: 
                    angle = +90; 
                    break;
                case Direction.Down: // Previously was going Down
                    angle = 180 - 45; 
                    transform.position += new Vector3(-.2f, .2f);
                    break;
                case Direction.Up: // Previously was going Up
                    angle = 45; 
                    transform.position += new Vector3(-.2f, -.2f);
                    break;
                }
                break;
            case Direction.Right: // Currently going to the Right
                switch (snakeMovePosition.GetPreviousDirection()) {
                default: 
                    angle = -90; 
                    break;
                case Direction.Down: // Previously was going Down
                    angle = 180 + 45; 
                    transform.position += new Vector3(.2f, .2f);
                    break;
                case Direction.Up: // Previously was going Up
                    angle = -45; 
                    transform.position += new Vector3(.2f, -.2f);
                    break;
                }
                break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition() {
            return snakeMovePosition.GetGridPosition();
        }
    }

    /*
     * Handles one Move Position from the Snake
     * */
    private class SnakeMovePosition {

        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction) {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition() {
            return gridPosition;
        }

        public Direction GetDirection() {
            return direction;
        }

        public Direction GetPreviousDirection() {
            if (previousSnakeMovePosition == null) {
                return Direction.Right;
            } else {
                return previousSnakeMovePosition.direction;
            }
        }

    }

}
