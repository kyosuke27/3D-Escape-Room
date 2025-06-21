using UnityEngine;

public class ItemDetailManager : MonoBehaviour
{
    public GameObject ItemDetailPanel; // アイテムの詳細画面オブジェクト
    
    public void TapCloseButton()
    {
        // アイテムの詳細画面を非表示にする
        if (ItemDetailPanel != null)
        {
            ItemDetailPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("ItemDetailPanel is not assigned.");
        }
    }
}
