using UnityEngine;

// 一度ドアを開けた後、そのドアを通過できるかどうか判定するために作成した
// Openのオブジェクトが活性化しているときに通過できる
public class TapCameraMoveCondition : TapColider
{

    public GameObject CheckObject;
    public string MovePositionName;

    protected override void OnTap()
    {
        base.OnTap();
        if(!CheckObject.activeSelf)
        {
            // CheckObjectが非アクティブな状態でタップされた場合は何もしない
            return;
        }
        // CameraManagerはシングルトンのstaticクラスなのでどこからでもアクセス可能
        CameraManager.Instance.ChangeCameraPosition(MovePositionName);
    }
}
