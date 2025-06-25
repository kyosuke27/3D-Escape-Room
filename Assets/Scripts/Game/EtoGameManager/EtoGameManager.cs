using UnityEngine;

public class EtoGameManager : MonoBehaviour
{
    // クリアしているか判定する
    private bool isClear = false;
    public int[] ClearIndexs;

    public TapObjectChange[] tapObjects;

    // ClearManagerの進行度を管理するためのProcessType
    public ProcessType processType;

    // ゲームクリア時に表示するオブジェクト
    public GameObject ItemPanel;


    void Update()
    {
        // クリアしている場合には終了
        if (isClear) return;
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
        isClear = true;
        // クリアしたことを通知する
        ClearManager.Instance.SetProgress(processType);
        ClearManager.Instance.SetItems(ItemType.BedRoomDoorKey); // ベッドルームのドアを開けるアイテムを取得したことを通知
        // ブロックを非活性にする
        foreach (var tapObject in tapObjects)
        {
            // tapObjectを無効化する
            tapObject.enabled = false;
            // colliderを無効化する
            tapObject.GetComponent<Collider>().enabled = false;
        }
        GetClear();
    }

    // 正解した際のアイテム取得とか諸々をひとまとめに
    public void GetClear()
    {
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        ItemPanel.SetActive(true); // アイテムパネルをアクティブにする
    }

    // ゲームクリアの処理
    // ClearManagerから呼ばれることが前提
    public void GameClear()
    {
        // クリフラグを立てる
        isClear = true;
        // Tapするパネルを正解のパネルにする
        for (int i = 0; i < tapObjects.Length; i++)
        {
            tapObjects[i].SetIndex(ClearIndexs[i]);
        }
        
        foreach (var tapObject in tapObjects)
        {
            // tapObjectを無効化する
            tapObject.enabled = false;
            // colliderを無効化する
            tapObject.GetComponent<Collider>().enabled = false;
        }
    }
}
