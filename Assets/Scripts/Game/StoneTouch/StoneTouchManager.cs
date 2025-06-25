using UnityEngine;

public class StoneTouchManager : GameManagerBase
{
    // クリアしているかどうか判定フラグ
    private bool _isCleared = false;

    // 色のチェックをするボールの値を全て入れておく配列
    // 例: [0, 1, 2] -> 0番目のボールは白色、1番目のボールは青色、2番目のボールは赤色
    // スマートではないが、簡単に実装できるのでとりあえずこれで実装する
    public int[] CheckBallIndexs;
    // 上記のチェックする配列のボールのIndex(色を表す)を格納する配列)

    // チェックするボールのオブジェクト
    public TapObjectChangeMaterial[] CheckBallObjects;

    void Update()
    {
        if (_isCleared) return; // クリアしている場合は何もしない

        for (int i = 0; i < CheckBallIndexs.Length; i++)
        {
            // チェックするボールのIndexがCheckBallIndexsと等しいか確認する
            if (CheckBallObjects[i].Index != CheckBallIndexs[i])
            {
                // 等しくないの場合は即終了
                return;
            }
        }
        // ここから先はクリアしている場合
        _isCleared = true; // クリアフラグを立てる
        ClearManager.Instance.SetProgress(processType); // ClearManagerに進行度を通知する
        ClearManager.Instance.SetItems(ItemType.BedRoomSafeKey, true); // ベッドルームの金庫を開けるアイテムを取得したことを通知
        foreach (var tapObject in CheckBallObjects)
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
        Debug.Log("StoneTouchManager: GetClear called");
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
        _isCleared = true; // クリアフラグを立てる
        // Tapするパネルを正解のパネルにする
        for (int i = 0; i < CheckBallObjects.Length; i++)
        {
            CheckBallObjects[i].SetMaterialIndex(CheckBallIndexs[i]);  // 正解のIndexに設定
        }
        // オブジェクトを非アクティブにする
        foreach (var tapObject in CheckBallObjects)
        {
            tapObject.enabled = false; // tapObjectを無効化する
            tapObject.GetComponent<Collider>().enabled = false; // colliderを無効化する 
        }
    }
}
