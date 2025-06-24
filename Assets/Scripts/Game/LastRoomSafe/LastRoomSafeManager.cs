using Unity.VisualScripting;
using UnityEngine;

public class LastRoomSafeManager : GameManagerBase
{
    // アクティブなオブジェクトか確認するオブジェクト
    public GameObject[] CheckActiveObject;
    
    void Update()
    {
        if (isClear) return; // クリア済みなら何もしない
        // アクティブなオブジェクトが一つでもアクティブになっているか確認する
        foreach (var obj in CheckActiveObject)
        {
            if (!obj.activeSelf)
            {
                return;
            }
        }
        // 全てのオブジェクトがアクティブになっている場合
        isClear = true; // クリアフラグを立てる
        // クリアしたことを通知する
        ClearManager.Instance.SetProgress(processType); // ClearManagerに進行度を通知する
        GetClear(); // アイテム取得などの処理を呼び出す
    }
    
    protected override void GetClear()
    {
        Debug.Log("LastRoomSafeManager: GetClear called");
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(true); // アイテムパネルをアクティブにする
        }
    }
}
