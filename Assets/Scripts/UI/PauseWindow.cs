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
using CodeMonkey.Utils;

public class PauseWindow : MonoBehaviour {

    private static PauseWindow instance;

    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private Button_UI resumeBtn;
    [SerializeField] private Button_UI mainMenuBtn;

    private void Awake() {
        instance = this;

        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;

        resumeBtn.ClickFunc = () => GameHandler.ResumeGame();
        resumeBtn.AddButtonSounds();

        mainMenuBtn.ClickFunc = () => Loader.Load(Loader.Scene.MainMenu);
        mainMenuBtn.AddButtonSounds();

        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public static void ShowStatic() {
        instance.Show();
    }

    public static void HideStatic() {
        instance.Hide();
    }
}
