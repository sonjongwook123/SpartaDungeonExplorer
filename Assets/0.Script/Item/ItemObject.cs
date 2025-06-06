using Unity.VisualScripting;
using UnityEngine;

public interface ItemEffect
{
    void ApplyEffect(Transform target, Transform mine);
}

public abstract class ItemEffectSO : ScriptableObject, ItemEffect
{
    [Header("Basic Item Info")]
    public string itemName = "New Item";
    public string description = "A generic item.";
    public abstract void ApplyEffect(Transform target, Transform mine);
}

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Item Effects/HP restore")]
public class HpItem : ItemEffectSO
{
    [SerializeField] private float restoreAmount = 10;

    public override void ApplyEffect(Transform target, Transform mine)
    {
        target.GetComponent<Player>().RestoreHealth(restoreAmount);
        Destroy(mine.gameObject);
    }
}

[CreateAssetMenu(fileName = "SpecialItem", menuName = "Item Effects/HP minus")]
public class HpMinusItem : ItemEffectSO
{
    [SerializeField] private float restoreAmount = 10;

    public override void ApplyEffect(Transform target, Transform mine)
    {
        target.GetComponent<IDamageable>().TakeDamage(restoreAmount);
        if (target.GetComponent<Rigidbody>() != null)
        {
            target.GetComponent<Rigidbody>().AddForce(-target.transform.forward * 100, ForceMode.Impulse);
            target.GetComponent<Rigidbody>().AddForce(Vector3.up * 6, ForceMode.Impulse);
        }
    }
}

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Item Effects/Stamina restore")]
public class StaminaItem : ItemEffectSO
{
    [SerializeField] private float restoreAmount = 10;

    public override void ApplyEffect(Transform target, Transform mine)
    {
        target.GetComponent<Player>().RestoreStamina(restoreAmount);
        Destroy(mine.gameObject);
    }
}

[CreateAssetMenu(fileName = "JustItem", menuName = "Items/No Effect")]
public class JustItem : ItemEffectSO
{
    public override void ApplyEffect(Transform target, Transform mine)
    {
    }
}

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Item Effects/Double Jump")]
public class JumpItem : ItemEffectSO
{
    [SerializeField] private int restoreAmount = 10;

    public override void ApplyEffect(Transform target, Transform mine)
    {
        target.GetComponent<Player>().TakeDoubleJump();
        Destroy(mine.gameObject);  //target 더블 점프
    }
}

