using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackUpGameManager : GameManagerBase
{
    // true,falseの配列の値が格納した変数
    // ブロックの数だけ用意して正解かどうか判定する
    // 0:白, 1:黒
    // 一番左は配列のIndex、隣が値
    // 3:true 7:false 11:false
    // 2:true 6:false 10:true
    // 1:true 5:false 9:true
    // 0:true 4:true 8:true
    public bool[] ClearIndexs;

    // 判定する対象のオブジェクト
    public CountUpBlock[] countUpBlocks;

    void Update()
    {
        if (isClear) return; // クリアしている場合は何もしない

        // ClearIndexのそれぞれのオブジェクトのIndexがClearIndexと等しいか確認する
        for (int i = 0; i < ClearIndexs.Length; i++)
        {
            // tapObjectsのIndexがClearIndexと等しい場合
            if (countUpBlocks[i].isCheck != ClearIndexs[i])
            {
                // クリアしていないので終了
                return;
            }
        }

        // ここから先はクリアしている場合
        Debug.Log("BlackUpGameManager: Game Cleared!");
        //  クリアフラグを立てる
        isClear = true;
        // クリアしたことを通知する
        ClearManager.Instance.SetProgress(processType); // ClearManagerに進行度を通知する
        // ブロックを非活性にする
        foreach (var tapObject in countUpBlocks)
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
        Debug.Log("BlackUpGameManager: GetClear called");
        // ここにアイテム取得の処理を追加する
        // 例えば、アイテムをインベントリに追加するなど
        foreach (var itemPanel in ItemPanel)
        {
            itemPanel.SetActive(true); // アイテムパネルをアクティブにする
        }
    }
}
