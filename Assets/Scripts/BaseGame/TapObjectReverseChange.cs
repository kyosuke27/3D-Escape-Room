using System.Dynamic;
using UnityEngine;

// オブジェクトのタップに応じてボタンなどの処理を行うための汎用的なクラス
// 想定としては、ボタン一つが他のボタンに対して影響を及ぼすようなもの
// 丸ボタンとか大元のオブジェクトに対して設定する
// ⬜︎⬜︎⬜︎⬜︎
// ⬜︎⬛︎⬜︎⬛︎
// ⬛︎⬛︎⬜︎⬛︎
// ⬛︎⬛︎⬛︎⬛︎
// ●　　●　　●　　●
// TapObjectChangeとの違いはオブジェクト自身のMaterialを変更することでタップされたことを設定する
public class TapObjectReverseChange : TapColider
{
    // 現在のMaterialインデックス
    public int Index { get; private set; }
    
    // GameObjectの配列(⬜︎の部分)
    public GameObject[] gameObjects;

    protected override void OnTap()
    {
        base.OnTap();
        
        // Indexの番号のRendererは選択済みとするように対象のブロックに通知する

        // 次のブロックを対象とするようにインデックスを更新
        if(Index>gameObjects.Length){
            Index = 0;
            // 全てを初期化する
            foreach (var gameObject in gameObjects)
            {
                // 初期化処理
            }
        }

    }
}
