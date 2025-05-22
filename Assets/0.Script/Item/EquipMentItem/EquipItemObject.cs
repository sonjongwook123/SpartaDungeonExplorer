using UnityEngine;

public abstract class EuipAbleSO : ScriptableObject
{
    [Header("Information")]
    public string itemName = "New Item";
    public string description = "A generic item.";
    public int itemCode;

    abstract public int Use();
}


[CreateAssetMenu(fileName = "UseableItem", menuName = "Items/Invincibility")]
public class EquipItemObject : EuipAbleSO
{
    public override int Use()
    {
        return this.itemCode;
    }
}