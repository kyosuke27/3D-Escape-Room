using UnityEngine;

public class TapItemUse : TapColider
{
    // タップされた時に所持しているオブジェクト
    //　アイテムをタップした際に、条件となるオブジェクトをと所持しているか判定するために
    // 事前に設定しておく
    public GameObject ItemImage;
    // アイテムをタップした際に表示するオブオブジェクト
    // 使いやすいように配列に入れておく
    public GameObject[] ActiveObjects;

    // 使用したアイテムを識別するためにItemTypsを設定する
    public ItemType ItemType;
    
    // ActionTypeを設定する
    public ActionType ActionType;
    // Actionが起きた後の値を設定する
    // Actionごとに個別に設定する
    public int ActionValue;
   
    protected override void OnTap()
    {
        base.OnTap();
        // 自分とキーになっているオブジェクトがアクティブが確認する
        if (ItemImage.activeSelf) // activeSelfはオブジェクトがアクティブかどうかを判定する
        {
            // アイテムがアクティブな状態でタップされた場合
            if (ItemImage.GetComponent<TapItemCollider>().IsSelected)
            {
                ItemImage.SetActive(false); // アイテムを使用したら非表示にする
                // 自分自身も非表示にする
                gameObject.SetActive(false);
                // 表示するオブジェクトをアクティブにする
                foreach (var obj in ActiveObjects)
                {
                    obj.SetActive(true);
                }
                // アイテムの使用を通知する
                ClearManager.Instance.SetItems(ItemType, false);
                ClearManager.Instance.SetAction(ActionType, ActionValue);
            }
        }
    }
}
