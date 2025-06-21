using UnityEngine;

public class UIButtonManager : MonoBehaviour
{
    // 汎用的な閉じるボタンののコード
    // 引数に指定したパネルを非表示にするメソッド
    public void CloseButton(GameObject Panel)
    {
        Panel.SetActive(false); // パネルを非表示にする
    }

    // 汎用的な開くボタンのコード
    // 引数に指定したパネルを表示するメソッド
    public void OpenButton(GameObject Panel)
    {
            Panel.SetActive(true); // パネルを表示する
    }
}
