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
    public Transform equipText;
    public Transform equipEffectText;

    void Start()
    {
        if (GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.OnHpChanged += UpdateHealthUI;
            GameManager.Instance.Player.GetComponent<PlayerInteractive>().OnRayItem += OnDisplayInteractData;
            GameManager.Instance.Player.OnDoubleJump += OnDisplayDoubleJump;
            GameManager.Instance.Player.GetComponent<Player>().OnStaminaChanged += UpdateStaminaUI;
            GameManager.Instance.Player.GetComponent<PlayerInteractive>().OnRayInteract += OnDisplayInteractText;
            GameManager.Instance.Player.GetComponent<PlayerInteractive>().OnRayEauipable += OnDisplayEquipText;
            GameManager.Instance.Player.GetComponent<Equipment>().OnEquipChanged += OnDisplayEquipEffect;
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().OnHpChanged += UpdateHealthUI;
            GameObject.FindWithTag("Player").GetComponent<PlayerInteractive>().OnRayItem += OnDisplayInteractData;
            GameObject.FindWithTag("Player").GetComponent<Player>().OnDoubleJump += OnDisplayDoubleJump;
            GameObject.FindWithTag("Player").GetComponent<Player>().OnStaminaChanged += UpdateStaminaUI;
            GameObject.FindWithTag("Player").GetComponent<PlayerInteractive>().OnRayInteract += OnDisplayInteractText;
            GameObject.FindWithTag("Player").GetComponent<PlayerInteractive>().OnRayEauipable += OnDisplayEquipText;
            GameObject.FindWithTag("Player").GetComponent<Equipment>().OnEquipChanged += OnDisplayEquipEffect;
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

    private void OnDisplayEquipText(bool value)
    {
        equipText.gameObject.SetActive(value);
    }

    void OnDisplayEquipEffect(int num)
    {
        switch (num)
        {
            case -1:
                {
                    equipEffectText.gameObject.SetActive(false);
                    break;
                }
            case 0:
                {
                    equipEffectText.gameObject.SetActive(true);
                    equipEffectText.GetComponent<Text>().text = "무적 효과 적용중";
                    break;
                }
            default:
                {
                    equipEffectText.gameObject.SetActive(false);
                    break;
                }
        }
    }
}
