using UnityEngine;

public class SettingPanelManager : MonoBehaviour
{
    public GameObject HintAnswerPanel; // 設定パネルのオブジェクト
                                       // Start is called before the first frame update
    
    public void TapShowHintAnswerPanel()
    {
        HintAnswerPanel.SetActive(true); // 設定パネルを表示する
    }
}
