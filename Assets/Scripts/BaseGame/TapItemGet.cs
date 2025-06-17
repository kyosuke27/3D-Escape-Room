using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapItemGet : TapColider
{
    public GameObject ItemImage; 
    
    protected override void OnTap()
    {
        base.OnTap();
        print("Item tapped!"); // デバッグ用のログ出力
        print("Tapped : "+CameraManager.Instance.CurrentPositionName); // 現在のカメラ位置名を出力
        print("EnablePositionName : " + EnableCameraPositionName); // 有効なカメラ位置名を出力
        // アイテムを取得したら、アイテムの画像を表示する
        ItemImage.SetActive(true);
        gameObject.SetActive(false); // タップしたオブジェクトを非表示にする
    }
}
