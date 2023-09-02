using System;
using UnityEngine;

public class Food : MonoBehaviour {
    public enum FoodType {
        Apple
    }
    
    [SerializeField] private FoodType type;
    private int scoreAmount;

    private void Awake() {
        switch (type) {
            case FoodType.Apple:
                scoreAmount = GameConfig.GetGameplayConfiguration().FoodAppleScoreAmount;
                break;
        }
    }

    public int GetScoreAmount() { return scoreAmount; }
}
