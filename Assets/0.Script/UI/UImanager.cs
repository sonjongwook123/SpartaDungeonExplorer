using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header("UI오브젝트")]
    public Image hpBar;
    public InteractPanel interactPanel;
    public Text textDoubleJump;

    void Start()
    {
        if (GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.OnHpChanged += UpdateHealthUI;
            GameManager.Instance.Player.GetComponent<PlayerInteractive>().OnRayItem += OnDisplayInteractData;
            GameManager.Instance.Player.OnDoubleJump += OnDisplayDoubleJump;
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().OnHpChanged += UpdateHealthUI;
            GameObject.FindWithTag("Player").GetComponent<PlayerInteractive>().OnRayItem += OnDisplayInteractData;
            GameObject.FindWithTag("Player").GetComponent<Player>().OnDoubleJump += OnDisplayDoubleJump;
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
    private void OnDisplayDoubleJump(bool value)
    {
        textDoubleJump.gameObject.SetActive(value);
    }

}
