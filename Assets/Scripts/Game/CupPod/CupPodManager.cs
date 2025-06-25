using UnityEngine;

public class CupPodManager : GameManagerBase
{
    // 答えの配列を格納した配列
    // 0: 黒いマグカップ
    // 1: 白いマグカップ
    // 2: 赤いポッド
    // 3: 白いタイル
    // 配列のIndex:正解のIndex
    // 0:1 1:3 2:3 3:2
    // 4:3 5:3 6:3 7:3
    // 8:3 9:3 10:3 11:3 
    // 12:0 13:3 14:3 15:3
    public int[] ClearIndexs;

    // 判定する対象のオブジェクト
    public TapObjectChange[] tapObjects;
    
    // 

    void Update()
    {
        if (isClear) return; // クリアしている場合は何もしない
        // ClearIndexのそれぞれのオブジェクトのIndexがClearIndexと等しいか確認する
        for (int i = 0; i < ClearIndexs.Length; i++)
        {
            // tapObjectsのIndexがClearIndexと等しくない場合
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
        // クリアした際に、鍵が手に入るので通知する
        ClearManager.Instance.SetItems(ItemType.BedLoomDrawerKey, true); // CupPodの白いマグカップを取得したことを通知
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
        Debug.Log("CupPodManager: GetClear called");
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(true); // アイテムパネルをアクティブにする
        }
    }

    // ゲームクリアの処理
    // ClearManagerから呼ばれることが前提   
    public void GameClear()
    {
        isClear = true; // クリアフラグを立てる
        // Tapするパネルを正解のパネルにする
        for(int i = 0; i < tapObjects.Length; i++)
        {
            // tapObjectsのIndexをClearIndexに設定する
            tapObjects[i].SetIndex(ClearIndexs[i]);
        }

        // アクティブなオブジェクトを非アクティブにする
        foreach (var obj in tapObjects)
        {
            obj.enabled = false; // オブジェクトを無効化する
            obj.GetComponent<Collider>().enabled = false; // colliderを無効化する
        }
    }
}
