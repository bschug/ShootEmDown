using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class XpBar : MonoBehaviour
{
    public PlayerStats playerShip;
    private PlayerLevelup _playerLevelup;
    private Image _healthbarImage;

    public void Start()
    {
        _playerLevelup = playerShip.GetComponent<PlayerLevelup>();
        _healthbarImage = GetComponent<Image>();
    }

    public void Update()
    {
        _healthbarImage.fillAmount = (float)playerShip.currentXp / _playerLevelup.XpForNextLevel;
    }
}
