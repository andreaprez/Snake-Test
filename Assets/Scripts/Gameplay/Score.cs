/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using UnityEngine;

public static class Score {

    public static Action<int> OnScoreChanged;
    public static Action<int> OnHighscoreChanged;

    private static int score;

    public static void InitializeStatic() {
        OnScoreChanged = null;
        OnHighscoreChanged = null;
        score = 0;
    }

    public static int GetScore() {
        return score;
    }

    public static void AddScore(int amount) {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    public static int GetHighscore() {
        return PlayerPrefs.GetInt("highscore", 0);
    }

    public static bool TrySetNewHighscore() {
        return TrySetNewHighscore(score);
    }

    public static bool TrySetNewHighscore(int score) {
        int highscore = GetHighscore();
        if (score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            OnHighscoreChanged?.Invoke(highscore);
            return true;
        }
        return false;
    }
}
