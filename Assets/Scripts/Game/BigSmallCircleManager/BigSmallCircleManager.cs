using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSmallCircleManager : GameManagerBase
{
    // 正解の値が格納されている配列
    public int[] ClearIndexs;

    // 判定する対象のオブジェクト
    public TapObjectChange[] tapObjects;

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
        Debug.Log("BigSmallCircleManager: GetClear called");
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(true); // アイテムパネルをアクティブにする
        }
    }
}
