using UnityEngine;

public class PlayerSnake : Snake {

    private PlayerInput playerInput;
    
    protected override void Init() {
        SetGridPosition(new Vector2Int(GameConfig.GetGameplayConfiguration().LevelWidth / 2, GameConfig.GetGameplayConfiguration().LevelHeight / 2));
        SetGridMoveDirection(Direction.Right);
        gridMoveTimerMax = GameConfig.GetGameplayConfiguration().SnakeMovementTime;

        base.Init();
    }
    
    public void Setup(LevelGrid levelGrid, PlayerInput playerInput) {
        base.Setup(levelGrid);
        this.playerInput = playerInput;
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

    protected override void Die() {
        base.Die();
        SoundManager.PlaySound(SoundManager.Sound.SnakeDie);
        GameHandler.SnakeDied();
    }
    
    private void HandleInput() {
        Vector2 moveInputVector = playerInput.Snake.Move.ReadValue<Vector2>();
        
        if (moveInputVector.y > 0) {
            if (gridMoveDirection != Direction.Down) {
                SetGridMoveDirection(Direction.Up);
            }
        } 
        else if (moveInputVector.y < 0) {
            if (gridMoveDirection != Direction.Up) {
                SetGridMoveDirection(Direction.Down);
            }
        }
        if (moveInputVector.x < 0) {
            if (gridMoveDirection != Direction.Right) {
                SetGridMoveDirection(Direction.Left);
            }
        }
        else if (moveInputVector.x > 0) {
            if (gridMoveDirection != Direction.Left) {
                SetGridMoveDirection(Direction.Right);
            }
        }
    }
}
