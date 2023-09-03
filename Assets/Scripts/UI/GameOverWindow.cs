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
using UnityEngine.UI;
using CodeMonkey.Utils;

public class GameOverWindow : MonoBehaviour {

    private static GameOverWindow instance;

    [SerializeField] private Button_UI retryBtn;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Text newHighscoreText;

    private void Awake() {
        instance = this;

        retryBtn.ClickFunc = () => Loader.Load(Loader.Scene.GameScene);

        Hide();
    }

    private void Show(bool isNewHighscore, int score, int highscore) {
        gameObject.SetActive(true);

        newHighscoreText.gameObject.SetActive(isNewHighscore);

        scoreText.text = score.ToString();
        highscoreText.text = "HIGHSCORE " + highscore.ToString();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public static void ShowStatic(bool isNewHighscore, int score, int highscore) {
        instance.Show(isNewHighscore, score, highscore);
    }
}
