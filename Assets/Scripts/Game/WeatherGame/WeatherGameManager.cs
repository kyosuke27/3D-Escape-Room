using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherGameManager : GameManagerBase
{
    // クリアの数字を設定しておく配列(Weatherボタンの数と同じ数)
    // WeatherボタンのIndexの正解の値を設定する
    public int[] ClearIndexNumbers;

    // 判定するボタンオブジェクト
    public TapObjectChange[] tapObjects;

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
        // 銀の鳥の羽を取得したことを通知
        ClearManager.Instance.SetItems(ItemType.SilverBirdStatue, true); //
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
        Debug.Log("WeatherGameManager: GetClear called");
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
        for (int i = 0; i < tapObjects.Length; i++)
        {
            tapObjects[i].SetIndex( ClearIndexNumbers[i]); // 正解のIndexに設定する
        }
        // オブジェクトを非アクティブにする
        foreach (var tapObject in tapObjects)
        {
            tapObject.enabled = false; // tapObjectを無効化する
            tapObject.GetComponent<Collider>().enabled = false; // colliderを無効化する
        }
    }
}
