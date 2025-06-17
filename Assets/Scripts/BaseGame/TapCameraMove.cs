using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapCameraMove : TapColider
{
    public string MovePositionName;

    protected override void OnTap()
    {
        base.OnTap();
        // CameraManagerはシングルトンのstaticクラスなのでどこからでもアクセス可能
        CameraManager.Instance.ChangeCameraPosition(MovePositionName);
    }
}
