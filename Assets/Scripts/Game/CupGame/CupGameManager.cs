using UnityEngine;

public class CupGameManager : GameManagerBase
{
    // クリアの数字を設定しておく配列(Cupボタンの数と同じ数)
    public int[] ClearIndexNumbers;

    // 判定するボタンオブジェクト
    public TapObjectChange[] tapObjects;

    // Update is called once per frame
    void Update()
    {
        if (isClear) return; // クリアしている場合は何もしない
        for (int i = 0; i < ClearIndexNumbers.Length; i++)
        {
            // tapObjectsのIndexがClearIndexNumbersと等しい場合
            if (tapObjects[i].Index != ClearIndexNumbers[i])
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
        Debug.Log("CupGameManager: GetClear called");
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(true); // アイテムパネルをアクティブにする
        }
    }
}
