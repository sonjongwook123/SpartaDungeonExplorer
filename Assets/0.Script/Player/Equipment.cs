using System;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Transform curEquip;
    public Transform equipParent;

    public event Action<int> OnEquipChanged;

    void Start()
    {
        curEquip = null;
        OnEquipChanged += EquipEffect;
    }

    public void Equip(Transform obj)
    {
        curEquip = obj;
        curEquip.SetParent(equipParent);
        curEquip.transform.position = equipParent.transform.position;
        curEquip.GetComponent<Rigidbody>().isKinematic = true;
        curEquip.GetComponent<BoxCollider>().enabled = false;

        OnEquipChanged?.Invoke(obj.GetComponent<EquipItem>().data.Use());
    }

    public void UnEquip()
    {
        curEquip.GetComponent<Rigidbody>().isKinematic = false;
        curEquip.GetComponent<BoxCollider>().enabled = true;
        curEquip.parent = null;
        curEquip.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        curEquip.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
        curEquip = null;

        OnEquipChanged?.Invoke(-1);
    }

    public bool IsEquipped()
    {
        if (curEquip == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void EquipEffect(int num)
    {
        switch (num)
        {
            case -1:
                {
                    GetComponent<Player>().TakeInvincibility(false);
                    break;
                }
            case 0:
                {
                    GetComponent<Player>().TakeInvincibility(true);
                    break;
                }
            default:
                {
                    GetComponent<Player>().TakeInvincibility(false);
                    break;
                }
        }
    }
}
