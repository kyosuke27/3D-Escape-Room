using UnityEngine;

// 一度ドアを開けた後、そのドアを通過できるかどうか判定するために作成した
// Openのオブジェクトが活性化しているときに通過できる
public class TapCameraMoveConditionLastDoor : TapColider
{

    // アクティブかどうか確認するオブジェクト
    public GameObject CheckObject;

    // クリアパネルを表示するためのオブジェクト
    public GameObject ActivePanel;

    protected override void OnTap()
    {
        base.OnTap();
        if(!CheckObject.activeSelf)
        {
            // CheckObjectが非アクティブな状態でタップされた場合は何もしない
            return;
        }
        // クリアパネルの表示
        ActivePanel.SetActive(true); // パネルを表示にする
    }
}
