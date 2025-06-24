using UnityEngine;

public class TapItemUseNotDeleteObject : TapColider
{
    // タップされた時に所持しているオブジェクト
    //　アイテムをタップした際に、条件となるオブジェクトをと所持しているか判定するために
    // 事前に設定しておく
    public GameObject ItemImage;
    // アイテムをタップした際に表示するオブオブジェクト
    // 使いやすいように配列に入れておく
    public GameObject[] ActiveObjects;
    
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
                // 表示するオブジェクトをアクティブにする
                foreach (var obj in ActiveObjects)
                {
                    obj.SetActive(true);
                }        
            }
        }
    }
}
