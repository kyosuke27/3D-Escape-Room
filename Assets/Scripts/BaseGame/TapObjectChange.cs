using System.Dynamic;
using UnityEngine;

// Boxに付けられているタップした際に変化する数字ボタンを切り替えるスクリプト
// タップした際にボタンの文字、イラストを切り替えたりと色々と使える
public class TapObjectChange : TapColider
{
    // タップさたときの数値は0~9までなので、Indexを参照するだけで選択されている数値が取れる
    public int Index { get; set; }
    // 数字が記載れているSpriteオブジェクトを格納する
    public GameObject[] Objects;

    override protected void OnTap()
    {
        base.OnTap();
        Objects[Index].SetActive(false);
        Index++;
        if (Index >= Objects.Length)
        {
            Index = 0; // Reset index if it exceeds the length
        }
        Objects[Index].SetActive(true);
    }
    
    public void SetIndex(int index)
    {
        // Indexを設定するメソッド
        if (index < 0 || index >= Objects.Length)
        {
            Debug.LogError("Index out of range");
            return;
        }
        // 現在のオブジェクトを非アクティブにする
        Objects[Index].SetActive(false);
        // 新しいIndexを設定
        Index = index;
        // 新しいオブジェクトをアクティブにする
        Objects[Index].SetActive(true);
    }
}
