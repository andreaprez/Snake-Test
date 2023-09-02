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

public class MainMenuWindow : MonoBehaviour
{

    [SerializeField] private RectTransform howToPlaySub;
    [SerializeField] private RectTransform mainSub;
    [SerializeField] private Button_UI playBtn;
    [SerializeField] private Button_UI quitBtn;
    [SerializeField] private Button_UI howToPlayBtn;
    [SerializeField] private Button_UI backBtn;

    private enum Sub {
        Main,
        HowToPlay,
    }

    private void Awake() {
        howToPlaySub.anchoredPosition = Vector2.zero;
        mainSub.anchoredPosition = Vector2.zero;

        playBtn.ClickFunc = () => Loader.Load(Loader.Scene.GameScene);
        playBtn.AddButtonSounds();

        quitBtn.ClickFunc = () => Application.Quit();
        quitBtn.AddButtonSounds();

        howToPlayBtn.ClickFunc = () => ShowSub(Sub.HowToPlay);
        howToPlayBtn.AddButtonSounds();

        backBtn.ClickFunc = () => ShowSub(Sub.Main);
        backBtn.AddButtonSounds();

        ShowSub(Sub.Main);
    }

    private void ShowSub(Sub sub) {
        mainSub.gameObject.SetActive(false);
        howToPlaySub.gameObject.SetActive(false);

        switch (sub) {
        case Sub.Main:
            mainSub.gameObject.SetActive(true);
            break;
        case Sub.HowToPlay:
            howToPlaySub.gameObject.SetActive(true);
            break;
        }
    }

}
