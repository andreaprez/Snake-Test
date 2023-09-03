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

public class GameHandler : MonoBehaviour {

    [SerializeField] private SpriteRenderer GameplayBackground;
    
    private PlayerInput playerInput;
    private PlayerSnake snake;
    private LevelGrid levelGrid;

    private void Awake() {
        Score.InitializeStatic();
        ScoreWindow.UpdateScoreStatic(Score.GetScore());
        ScoreWindow.UpdateHighscoreStatic(Score.GetHighscore());
        Score.OnScoreChanged += ScoreWindow.UpdateScoreStatic;
        Score.OnHighscoreChanged += ScoreWindow.UpdateHighscoreStatic;
        
        playerInput = new PlayerInput();
        playerInput.Enable();
        
        Time.timeScale = 1f;
    }

    private void Start() {
        Debug.Log("GameHandler.Start");

        GameplayBackground.sprite = GameConfig.GetAssetsConfiguration().GameplayBackgroundSprite;

        levelGrid = new LevelGrid(GameConfig.GetGameplayConfiguration().LevelWidth, GameConfig.GetGameplayConfiguration().LevelHeight);
        
        snake = Instantiate(GameConfig.GetAssetsConfiguration().SnakeHeadPrefab).GetComponent<PlayerSnake>();
        if (snake != null) {
            snake.Setup(levelGrid, playerInput);
            levelGrid.Setup(snake);
        }

        PauseWindow.ResumeGame += ResumeGame;
    }

    private void Update() {
        if (playerInput.Snake.Pause.triggered) {
            if (IsGamePaused()) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public static void SnakeDied() {
        bool isNewHighscore = Score.TrySetNewHighscore();
        GameOverWindow.ShowStatic(isNewHighscore, Score.GetScore(), Score.GetHighscore());
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

    private void OnDisable() {
        playerInput.Disable();
    }

    private void OnDestroy() {
        PauseWindow.ResumeGame = null;
    }
}
