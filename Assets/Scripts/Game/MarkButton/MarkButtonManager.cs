using UnityEngine;

public class MarkButtonManager : GameManagerBase
{
    // 正解の値が格納されている配列
    public int[] ClearIndexs;

    // 判定する対象のオブジェクト
    public TapObjectChange[] tapObjects;
    
    // 非表示にするアイテムパネル
    public GameObject[] DeleteItemPanel;

    // Update is called once per frame
    void Update()
    {
        if (isClear) return; // クリア済みなら何もしない
        for (int i = 0; i < ClearIndexs.Length; i++)
        {
            // tapObjectsのIndexがClearIndexsと等しい場合
            if (tapObjects[i].Index != ClearIndexs[i])
            {
                // クリアしていないので終了
                return;
            }
        }
        // ここから先はクリアしている場合
        isClear = true; // クリアフラグを立てる
        // クリアしたことを通知する
        ClearManager.Instance.SetProgress(processType); // ClearManagerに進行度を通知する
        // 最後の鍵を取得
        ClearManager.Instance.SetItems(ItemType.LastRoomOutKey, true); // MarkButtonの最後の鍵を取得したことを通知
        // 記号メモを非表示
        ClearManager.Instance.SetItems(ItemType.MarkMemo, false); // MarkButtonの記号メモを非表示にする
        // ブロックを非活性にする
        foreach (var tapObject in tapObjects)
        {
            // tapObjectを無効化する
            tapObject.enabled = false;
            // colliderを無効化する
            tapObject.GetComponent<Collider>().enabled = false;
        }

        GetClear(); // アイテム取得などの処理を呼び出す
    }

    protected override void GetClear()
    {
        Debug.Log("MarkButtonManager: GetClear called");
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(true); // アイテムパネルをアクティブにする
        }
        // 非表示にするオブジェクトを非アクティブにする
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(false); // オブジェクトを非アクティブにする
        }
    }

    // ゲームクリアの処理
    // ClearManagerから呼ばれることが前提
    public void GameClear()
    {
        isClear = true; // クリアフラグを立てる
        // Tapするパネルを正解のパネルにする
        for (int i = 0; i < tapObjects.Length; i++)
        {
            // tapObjectsのIndexをClearIndexsに設定する
            tapObjects[i].SetIndex(ClearIndexs[i]);
        }
        // オブジェクトを非アクティブにする
        foreach (var tapObject in tapObjects)
        {
            tapObject.enabled = false; // tapObjectを無効化する
            tapObject.GetComponent<Collider>().enabled = false; // colliderを無効化する
        }
    }
}
