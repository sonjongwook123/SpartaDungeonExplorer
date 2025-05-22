using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header("UI오브젝트")]
    public Image hpBar;
    public Image staminaBar;
    public InteractPanel interactPanel;
    public Text textDoubleJump;
    public Transform interactText;

    void Start()
    {
        if (GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.OnHpChanged += UpdateHealthUI;
            GameManager.Instance.Player.GetComponent<PlayerInteractive>().OnRayItem += OnDisplayInteractData;
            GameManager.Instance.Player.OnDoubleJump += OnDisplayDoubleJump;
            GameManager.Instance.Player.GetComponent<Player>().OnStaminaChanged += UpdateStaminaUI;
            GameManager.Instance.Player.GetComponent<PlayerInteractive>().OnRayInteract += OnDisplayInteractText;
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().OnHpChanged += UpdateHealthUI;
            GameObject.FindWithTag("Player").GetComponent<PlayerInteractive>().OnRayItem += OnDisplayInteractData;
            GameObject.FindWithTag("Player").GetComponent<Player>().OnDoubleJump += OnDisplayDoubleJump;
            GameObject.FindWithTag("Player").GetComponent<Player>().OnStaminaChanged += UpdateStaminaUI;
            GameObject.FindWithTag("Player").GetComponent<PlayerInteractive>().OnRayInteract += OnDisplayInteractText;
        }
    }

    private void UpdateHealthUI(float hp, float maxHp)
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = hp / maxHp; // Fill Amount 업데이트
        }
    }

    private void UpdateStaminaUI(float stamina, float maxStamina)
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = stamina / maxStamina; // Fill Amount 업데이트
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

    void OnDisplayInteractText(bool value)
    {
        interactText.gameObject.SetActive(value);
    }

}
