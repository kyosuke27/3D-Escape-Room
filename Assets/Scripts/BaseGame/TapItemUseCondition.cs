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
    
    protected override void OnTap()
    {
        base.OnTap();
        if(!ConditionObject.activeSelf) return; // 条件オブジェクトが非アクティブな場合は何もしない
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
            }
        }
    }
}
