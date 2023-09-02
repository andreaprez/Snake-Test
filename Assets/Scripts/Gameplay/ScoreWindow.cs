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

    private void Start() {
        Score.OnHighscoreChanged += Score_OnHighscoreChanged;
        UpdateHighscore();
    }

    private void Score_OnHighscoreChanged(object sender, System.EventArgs e) {
        UpdateHighscore();
    }

    private void Update() {
        scoreText.text = Score.GetScore().ToString();
    }

    private void UpdateHighscore() {
        int highscore = Score.GetHighscore();
        highscoreText.text = "HIGHSCORE\n" + highscore.ToString();
    }

    public static void HideStatic() {
        instance.gameObject.SetActive(false);
    }
}
