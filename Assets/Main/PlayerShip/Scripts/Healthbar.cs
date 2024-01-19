
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Healthbar : MonoBehaviour
{
    public PlayerHealth playerShip;
    private Image _healthbarImage;

    public void Start()
    {
        _healthbarImage = GetComponent<Image>();
    }

    public void Update()
    {
        _healthbarImage.fillAmount = playerShip.CurrentHealth / playerShip.maxHealth;
    }
}
