using UnityEngine;

public class ArrowGameManager : GameManagerBase
{
    // クリアしてやじ矢印のインデックス
    // >:0,<:1
    public int[] ClearIndexs;
    
    // 判定する対象の矢印オブジェクト
    public TapObjectChange[] tapObjects;

    // Update is called once per frame
    void Update()
    {
        if (isClear) return; // クリアしている場合は何もしない
        // ClearIndexのそれぞれのオブジェクトのIndexがClearIndexと等しいか確認する
        for (int i = 0; i < ClearIndexs.Length; i++)
        {
            // tapObjectsのIndexがClearIndexと等しい場合
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
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(true); // アイテムパネルをアクティブにする
        }
    }
}
