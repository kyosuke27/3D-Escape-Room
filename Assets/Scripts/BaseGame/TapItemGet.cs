using UnityEngine;

// タップされた際にアイテムを拾うときに使うクラス
public class TapItemGet : TapColider
{
    // タップされた際にアイテムパネルに表示する画像
    public GameObject ItemImage;

    //アイテムを拾った際にActionTypeを設定する
    public ActionType ActionType;
    // Actionが起きた後の値を設定する
    // Actionごとに個別に設定する
    public int ActionValue;

    protected override void OnTap()
    {
        base.OnTap();
        // アイテムを取得したら、アイテムの画像を表示する
        ItemImage.SetActive(true);
        // 拾われた自分自身は非表示にする
        gameObject.SetActive(false); // タップしたオブジェクトを非表示にする
        ClearManager.Instance.SetItems(ItemType.Driver, true); // アイテムの取得を通知する
        ClearManager.Instance.SetAction(ActionType, ActionValue); // Actionの通知
    }
}
