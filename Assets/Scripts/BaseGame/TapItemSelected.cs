using UnityEngine;
using UnityEngine.UI;

public class TapItemSelected : TapColider
{
    public bool IsSelected = false; // アイテムが選択されているかどうか
    
    protected override void OnTap()
    {
        base.OnTap();
        // アイテムが選択されていない場合は選択する
        if (!IsSelected)
        {
            IsSelected = true;
            // 画像の背景を赤くする
            gameObject.SetActive(true); // アイテムの画像をアクティブにする
            gameObject.GetComponent<Image>().color = Color.red; // 画像の色を赤に変更
        }
        else
        {
            IsSelected = false;
            // アイテムの画像を非アクティブにする
            gameObject.GetComponent<Image>().color = Color.white; // 画像の色を赤に変更
        }
    }
    
}
