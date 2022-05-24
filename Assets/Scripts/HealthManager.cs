using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health;
    private int maxHealth;

    [SerializeField] private Image [ ] healthImage;
    [SerializeField] private Sprite [ ] healthSprite;

    private void Start () 
    {
        maxHealth = health;
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < maxHealth; i++) {
            if (i < health)
                healthImage[i].sprite = healthSprite[0];
            else
                healthImage[i].sprite = healthSprite[1];
        }
    }
}
