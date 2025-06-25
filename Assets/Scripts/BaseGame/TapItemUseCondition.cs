using UnityEngine;

// ある条件のもと、タップしたアイテムを使用したい場合に使う
public class TapItemUseCondition : TapColider
{
    // タップされた時に所持しているオブジェクト
    //　アイテムをタップした際に、条件となるオブジェクトをと所持しているか判定するために
    // 事前に設定しておく
    public GameObject ItemImage;
    // アイテムをタップした際に表示するオブオブジェクト
    // 使いやすいように配列に入れておく
    public GameObject[] ActiveObjects;
    // 指定したオブジェクトの活性化状態を確認して、アイテムを使用するかどうかを決定する
    public GameObject ConditionObject;
    
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
        if (!ConditionObject.activeSelf) return; // 条件オブジェクトが非アクティブな場合は何もしない
        // 自分とキーになっているオブジェクトがアクティブが確認する
        if (ItemImage.activeSelf) // activeSelfはオブジェクトがアクティブかどうかを判定する
        {
            // アイテムがアクティブな状態でタップされた場合
            if (ItemImage.GetComponent<TapItemCollider>().IsSelected)
            {
                ItemImage.SetActive(false); // アイテムを使用したら非表示にする
                // 表示するオブジェクトをアクティブにする
                foreach (var obj in ActiveObjects)
                {
                    obj.SetActive(true);
                }
                // アイテムの使用を通知する
                ClearManager.Instance.SetItems(ItemType, false);
                ClearManager.Instance.SetAction(ActionType, ActionValue); // Actionの通知
            }
        }
    }
}
