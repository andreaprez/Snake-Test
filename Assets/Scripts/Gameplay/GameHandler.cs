﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;

public class GameHandler : MonoBehaviour {

    [SerializeField] private SpriteRenderer GameplayBackground;
    
    private PlayerSnake snake;
    private LevelGrid levelGrid;

    private void Awake() {
        Score.InitializeStatic();
        Time.timeScale = 1f;
    }

    private void Start() {
        Debug.Log("GameHandler.Start");

        GameplayBackground.sprite = GameConfig.GetAssetsConfiguration().GameplayBackgroundSprite;
        
        levelGrid = new LevelGrid(GameConfig.GetGameplayConfiguration().LevelWidth, GameConfig.GetGameplayConfiguration().LevelHeight);

        snake = Instantiate(GameConfig.GetAssetsConfiguration().SnakeHeadPrefab).GetComponent<PlayerSnake>();
        if (snake != null) {
            snake.Setup(levelGrid);
            levelGrid.Setup(snake);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsGamePaused()) {
                GameHandler.ResumeGame();
            } else {
                GameHandler.PauseGame();
            }
        }
    }

    public static void SnakeDied() {
        bool isNewHighscore = Score.TrySetNewHighscore();
        GameOverWindow.ShowStatic(isNewHighscore);
        ScoreWindow.HideStatic();
    }

    public static void ResumeGame() {
        PauseWindow.HideStatic();
        Time.timeScale = 1f;
    }

    public static void PauseGame() {
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;
    }

    public static bool IsGamePaused() {
        return Time.timeScale == 0f;
    }
}
