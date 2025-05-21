using UnityEngine;
using UnityEngine.UI;

public class InteractPanel : MonoBehaviour
{
    [Header("UI 오브젝트")]
    [SerializeField]
    private Text textName;
    [SerializeField]
    private Text textDescription;

    public void InitUI(ItemEffectSO data)
    {
        textName.text = data.itemName;
        textDescription.text = data.description;
    }
}