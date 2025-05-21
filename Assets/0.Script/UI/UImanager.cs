using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header("UI오브젝트")]
    public Image hpBar;
    public InteractPanel interactPanel;

    void Start()
    {
        if (GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.OnHpChanged += UpdateHealthUI;
            GameManager.Instance.Player.GetComponent<PlayerInteractive>().onRayItem += OnDisplayInteractData;
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().OnHpChanged += UpdateHealthUI;
            GameObject.FindWithTag("Player").GetComponent<PlayerInteractive>().onRayItem += OnDisplayInteractData;
        }
    }

    private void UpdateHealthUI(float hp, float maxHp)
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = hp / maxHp; // Fill Amount 업데이트
        }
    }

    private void OnDisplayInteractData(ItemEffectSO data)
    {
        if (data == null)
        {
            interactPanel.gameObject.SetActive(false);
        }
        else
        {
            interactPanel.gameObject.SetActive(true);
            interactPanel.InitUI(data);
        }
    }
}
