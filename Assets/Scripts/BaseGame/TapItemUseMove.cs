using UnityEngine;

//　アイテムを使ってオブジェクトに対して影響を及ぼし、移動も行う
public class TapItemUseMove : TapColider
{
    // タップされた時に所持しているオブジェクト
    //　アイテムをタップした際に、条件となるオブジェクトをと所持しているか判定するために
    // 事前に設定しておく
    public GameObject ItemImage;
    // アイテムをタップした際に表示するオブオブジェクト
    // 使いやすいように配列に入れておく
    public GameObject[] ActiveObjects;
    // アイテムが選択すみかどうか判定するためのオブジェクト
    // public TapItemCollider SelectedObject;
    
    public string MovePositionName; // 移動先の位置の名前
    
    protected override void OnTap()
    {
        base.OnTap();
        // 自分とキーになっているオブジェクトがアクティブが確認する
        if (ItemImage.activeSelf) // activeSelfはオブジェクトがアクティブかどうかを判定する
        {
            print("ItemImage.GetComponent<TapItemCollider>().IsSelected: " + ItemImage.GetComponent<TapItemCollider>().IsSelected);
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
                // Camraの移動
                CameraManager.Instance.ChangeCameraPosition(MovePositionName); // カメラを指定した位置に移動させる
            }
        }
    }
}
