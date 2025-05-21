using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("플레이어 데이터")]
    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxHp = 100;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float maxStamina = 100;

    [Header("컨트롤러")]
    public PlayerController controller;

    public event Action<float, float> OnHpChanged;
    public event Action<float, float> OnStaminaChanged;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        hp = maxHp;
        stamina = maxStamina;
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0) return;

        hp -= amount;
        hp = Mathf.Max(hp, 0); // HP는 0 미만으로 내려가지 않음

        // HP가 변경되었으므로 옵저버에게 알림
        OnHpChanged?.Invoke(hp, maxHp);

        if (hp <= 0)
        {
            // 플레이어 죽음
        }
    }

    // HP를 회복하는 메서드
    public void RestoreHealth(float amount)
    {
        if (amount < 0) return;

        hp += amount;
        hp = Mathf.Min(hp, maxHp); // HP는 MaxHp를 넘지 않음

        // HP가 변경되었으므로 옵저버에게 알림
        OnHpChanged?.Invoke(hp, maxHp);
    }
}