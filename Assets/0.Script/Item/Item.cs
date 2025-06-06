using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("전략 아이템 스크립트")]
    [Tooltip("이 오브젝트가 나타낼 아이템 데이터 ScriptableObject를 할당.")]
    public ItemEffectSO itemData;


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            this.itemData.ApplyEffect(other.transform, this.transform);
        }
    }

}