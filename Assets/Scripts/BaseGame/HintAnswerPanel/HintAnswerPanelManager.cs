using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[System.Serializable]
public class HintPanelPair
{
    public ProcessType ProcessType; // ProcessTypeの列挙型
    public GameObject HintPanel; // ヒントパネルのGameObject
    public GameObject AnswerPanel; // 回答パネルのGameObject
}

public class HintAnswerPanelManager : MonoBehaviour
{
    public GameObject HintAnswerPanel; // ヒント・回答パネルのオブジェクト
    public List<HintPanelPair> HintPanelPairs; // ヒントパネルのペアリスト
    // ヒントパネルと回答パネルのペアを保持する辞書
    private Dictionary<ProcessType, GameObject[]> ProcessTypeHintAnswerPanel; // ProcessTypeごとのヒント・回答パネルの辞書

    // Dictionaryを初期化するためのAwakeメソッド
    // UnityのエディターからはDictionaryを設定できないために、一度クラスに設定する
    void Awake()
    {
        // ProcessTypeとヒントパネルの辞書を初期化
        ProcessTypeHintAnswerPanel = new Dictionary<ProcessType, GameObject[]>();
        foreach (var pair in HintPanelPairs)
        {
            if (!ProcessTypeHintAnswerPanel.ContainsKey(pair.ProcessType))
            {
                // 0番目にヒントパネル、1番目に回答パネルを設定
                GameObject[] panels = new GameObject[2];
                panels[0] = pair.HintPanel;
                panels[1] = pair.AnswerPanel;
                // 辞書にProcessTypeとヒント・回答パネルのペアを追加
                ProcessTypeHintAnswerPanel.Add(pair.ProcessType, panels);

            }
            else
            {
                Debug.LogWarning($"Duplicate ProcessType found: {pair.ProcessType}");
            }
        }
    }

    // ヒントボタンが押されたときの処理
    public void HintButton()
    {
        HintAnswerPanel.SetActive(true); // ヒント・回答パネルを表示する
        // 解決していないProcessTypeの方を返す
        ProcessType? unsolvedProcessType = ClearManager.Instance.GetUnsolvedProcessType();
        print($"Unsolved ProcessType: {unsolvedProcessType}");
        if (unsolvedProcessType != null)
        {
            // ProcessTypeに対応するヒント・回答パネルを表示する
            if (ProcessTypeHintAnswerPanel.TryGetValue(unsolvedProcessType.Value, out GameObject[] panel))
            {
                // 配列の0番目にヒントパネルが入っている
                panel[0].SetActive(true);
            }
            else
            {
                Debug.LogWarning($"No panel found for ProcessType: {unsolvedProcessType.Value}");
            }
        }
        else
        {
            return; // 全て解決済みの場合は何もしない
        }
    }

    // 回答ボタンが押されたときの処理
    public void AnswerButton()
    {
        // ヒント・回答パネルを非表示にする
        HintAnswerPanel.SetActive(false);

        // 解決していないProcessTypeの方を返す
        ProcessType? unsolvedProcessType = ClearManager.Instance.GetUnsolvedProcessType();
        if (unsolvedProcessType != null)
        {
            // ProcessTypeに対応するヒント・回答パネルを非表示にする
            if (ProcessTypeHintAnswerPanel.TryGetValue(unsolvedProcessType.Value, out GameObject[] panel))
            {
                panel[1].SetActive(true);
            }
            else
            {
                Debug.LogWarning($"No panel found for ProcessType: {unsolvedProcessType.Value}");
            }
        }
    }
}
