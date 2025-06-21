using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブジェクトにアタッチして、進行度を管理するクラス
public class ClearManager : MonoBehaviour
{
    // 進行度を表す辞書
    // 中身に進行度のキーと、終わっているかのbool値が入っている
    // 未終了：false 
    // 終了：true
    private Dictionary<ProcessType, bool> _progress = new();
    public static ClearManager Instance { get; private set; }

    // 辞書型の変数を初期化する
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // ProcessTypeから値を取得してきて設定する
            foreach (ProcessType type in System.Enum.GetValues(typeof(ProcessType)))
            {
                _progress[type] = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        print("_process" + _progress);
    }

    // 進行度を更新
    public void SetProgress(ProcessType type)
    {
        // 進行度を設定する
        if (_progress.ContainsKey(type))
        {
            // 完了済みにする
            _progress[type] = true;
        }
        else //念の為キーが存在しない場合のエラーハンドリング
        {
            Debug.LogError($"ProcessType {type} does not exist in the progress dictionary.");
        }
    }

    // 解決していないProcessTypeを一つ返す
    public ProcessType? GetUnsolvedProcessType()
    {
        foreach (var kvp in _progress)
        {
            if (!kvp.Value) // 未解決のProcessTypeを見つけたら返す
            {
                return kvp.Key;
            }
        }
        // 全て解決済みの場合はnullを返す
        return null; 
    }
    
}
