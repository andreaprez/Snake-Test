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

public static class SoundManager {

    public enum Sound {
        SnakeMove,
        SnakeDie,
        SnakeEat,
        ButtonClick,
        ButtonOver,
    }
    
    public static void PlaySound(Sound sound) {
        var audioObj = PoolManager.instance.GetPoolObject(ObjectPoolType.Audio);

        if (audioObj != null) {
            AudioSource audioSource = audioObj.GetComponent<AudioSource>();
            if (audioSource != null) {
                audioObj.SetActive(true);
                audioSource.PlayOneShot(GetAudioClip(sound));
            }
        }
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach (AudioClip soundAudioClip in GameConfig.GetAssetsConfiguration().AudioClips) {
            if (soundAudioClip.name.Contains(sound.ToString())) {
                return soundAudioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    public static void AddButtonSounds(this Button_UI buttonUI) {
        buttonUI.MouseOverOnceFunc += () => SoundManager.PlaySound(Sound.ButtonOver);
        buttonUI.ClickFunc += () => SoundManager.PlaySound(Sound.ButtonClick);
    }

}
