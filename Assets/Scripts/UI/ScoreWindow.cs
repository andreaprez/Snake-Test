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

public class ScoreWindow : MonoBehaviour {

    private static ScoreWindow instance;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;

    private void Awake() {
        instance = this;
    }

    private void UpdateScore(int score) {
        scoreText.text = score.ToString();
    }
    
    private void UpdateHighscore(int highscore) {
        highscoreText.text = "HIGHSCORE\n" + highscore.ToString();
    }
    
    public static void UpdateScoreStatic(int score)
    {
        instance.UpdateScore(score);
    }
    
    public static void UpdateHighscoreStatic(int highscore)
    {
        instance.UpdateHighscore(highscore);
    }

    public static void HideStatic() {
        instance.gameObject.SetActive(false);
    }
}
