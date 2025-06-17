using UnityEngine;

public class TapItemUse : TapColider
{
    // タップされた時に所持しているオブジェクト
    //　アイテムをタップした際に、条件となるオブジェクトをと所持しているか判定するために
    // 事前に設定しておく
    public GameObject ItemImage;
    // アイテムをタップした際に表示するオブオブジェクト
    public GameObject[] ActiveObjects;
    // アイテムが選択すみかどうか判定するためのオブジェクト
    public TapItemCollider SelectedObject;
    
    protected override void OnTap()
    {
        base.OnTap();
        if (ItemImage.activeSelf) // activeSelfはオブジェクトがアクティブかどうかを判定する
        {
            // アイテムがアクティブな状態でタップされた場合
            if (SelectedObject.IsSelected)
            {
                ItemImage.SetActive(false); // アイテムを使用したら非表示にする
                // 自分自身も非表示にする
                gameObject.SetActive(false);
                // 表示するオブジェクトをアクティブにする
                foreach (var obj in ActiveObjects)
                {
                    obj.SetActive(true);
                }        
            }
        }
    }
}
