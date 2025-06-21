using UnityEngine;
using UnityEngine.UI;

// アイテムパネルに表示するアイテムに対して設定する
public class ItemTapped: TapItemCollider
{
    // 詳細画面パネルに表示する画像
    public Sprite SetImage;
    public GameObject[] otherImages;
    override protected void OnTap()
    {
        base.OnTap();
        if (IsSelected)
        {
            // すでに選択されている場合はチェックを外して詳細画面を表示する
            // アイテムの詳細画面を開く
            ItemDetailPanel.SetActive(true); // アイテムの詳細画面をアクティブにする
            // 詳細パネルに対してアイテムの画像を設定
            PanelImage.sprite = SetImage; // 詳細パネルの画像を設定
            return;
        }
        else
        {
            // 選択されていない場合
            IsSelected = true; // アイテムが選択された状態にする
            gameObject.GetComponent<Image>().color = Color.gray; // 画像の色を灰色に変更
            // 他のアイテムの色を白に戻す
            // アイテムの選択状態は一つだけ選択するだけで良いので他のはチェックを外す
            foreach (var image in otherImages)
            {
                image.GetComponent<Image>().color = Color.white; // 他のアイテムの色を白に戻す
                image.GetComponent<ItemTapped>().IsSelected = false; // 他のアイテムの選択状態を解除
            }
        }
    }
}
