using UnityEngine;

public class PlayerSnake : Snake {
    protected override void Init() {
        SetGridPosition(new Vector2Int(GameConfig.GetGameplayConfiguration().LevelWidth / 2, GameConfig.GetGameplayConfiguration().LevelHeight / 2));
        SetGridMoveDirection(Direction.Right);
        gridMoveTimerMax = GameConfig.GetGameplayConfiguration().SnakeMovementTime;
        
        base.Init();
    }
    
    protected override void Update() {
        switch (state) {
            case State.Alive:
                HandleInput();
                break;
            case State.Dead:
                break;
        }
        
        base.Update();
    }

    protected override void Die()
    {
        base.Die();
        SoundManager.PlaySound(SoundManager.Sound.SnakeDie);
        GameHandler.SnakeDied();
    }
    
    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (gridMoveDirection != Direction.Down) {
                SetGridMoveDirection(Direction.Up);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (gridMoveDirection != Direction.Up) {
                SetGridMoveDirection(Direction.Down);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (gridMoveDirection != Direction.Right) {
                SetGridMoveDirection(Direction.Left);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (gridMoveDirection != Direction.Left) {
                SetGridMoveDirection(Direction.Right);
            }
        }
    }
}
