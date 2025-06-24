using UnityEngine;

public class TapDateManager : GameManagerBase
{
    // 正解の数値を格納しておく配列
    public int[] CorrectValues;

    // 判定する対象のオブジェクト
    public TapObjectChange[] tapObjects;
    
    // 削除するオブジェクト
    public GameObject[] deleteObjects;

    void Update()
    {
        if (isClear) return; // クリアしている場合は何もしない
        for (int i = 0; i < CorrectValues.Length; i++)
        {
            // tapObjectsのIndexがCorrectValuesと等しい場合
            if (tapObjects[i].Index != CorrectValues[i])
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
        Debug.Log("TapDateManager: GetClear called");
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(true); // アイテムパネルをアクティブにする
        }
        // 削除するオブジェクトを非アクティブにする
        foreach (var deleteObject in deleteObjects)
        {
            deleteObject.SetActive(false); // オブジェクトを非アクティブにする
        }
    }
}
