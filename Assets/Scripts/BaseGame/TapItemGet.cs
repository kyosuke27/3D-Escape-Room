using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapItemGet : TapColider
{
    public GameObject ItemImage; 
    
    protected override void OnTap()
    {
        base.OnTap();
        // アイテムを取得したら、アイテムの画像を表示する
        ItemImage.SetActive(true);
        gameObject.SetActive(false); // タップしたオブジェクトを非表示にする
    }
}
