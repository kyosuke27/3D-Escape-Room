using UnityEngine;

// タップされた際にアイテムを拾うときに使うクラス
public class TapItemGet : TapColider
{
    // タップされた際にアイテムパネルに表示する画像
    public GameObject ItemImage;

    protected override void OnTap()
    {
        base.OnTap();
        // アイテムを取得したら、アイテムの画像を表示する
        ItemImage.SetActive(true);
        // 拾われた自分自身は非表示にする
        gameObject.SetActive(false); // タップしたオブジェクトを非表示にする
    }
}
