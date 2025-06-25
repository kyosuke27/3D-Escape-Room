using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームのクリア状態を保存しておくクラス

// オブジェクトにアタッチして、進行度を管理するクラス
// UserPrefabへの保存もかねる
public class ClearManager : MonoBehaviour
{
    // UserPrefabに保存するためのKey
    const string SAVE_KEY = "SaveData";
    public SaveData saveData; // 保存用のデータクラス

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
        }
        else
        {
            Destroy(gameObject);
        }
        print("_process" + _progress);
    }

    void Start()
    {
        print("ClearManager Start");
        // セーブデータが存在する場合はロードする
        LoadProgress();
    }

    // セーブデータをロードする
    public void LoadProgress()
    {
        // 以下はセーブデータをロードしてprocessを初期化する処理
        saveData = new SaveData();
        // Prefabにセーブデータが存在するか？
        if (PlayerPrefs.HasKey(SAVE_KEY) == true)
        {
            // セーブデータを取得
            string json = PlayerPrefs.GetString(SAVE_KEY);
            saveData = JsonUtility.FromJson<SaveData>(json);
            // _progressを初期化
            for (int i = 0; i < (int)ProcessType.LastProcessType; i++)
            {
                // ProcessTypeの数だけループして進行度を設定
                _progress[(ProcessType)i] = saveData.process[i];
            }
        }else
        {
            // セーブデータが存在しない場合は初期化
            for (int i = 0; i < (int)ProcessType.LastProcessType; i++)
            {
                // ProcessTypeの数だけループして進行度を設定
                _progress[(ProcessType)i] = false; // 全て未解決に設定
            }
            Save(); // 初期化後にセーブ
        }
    }

    // SaveDataを保存する
    public void Save()
    {
        print("ClearManager Save");
        // SaveDataのprocess配列に進行度を保存
        for (int i = 0; i < (int)ProcessType.LastProcessType; i++)
        {
            saveData.process[i] = _progress[(ProcessType)i];
        }
        
        // JSON形式に変換してPlayerPrefsに保存
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
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
        Save(); // 進行度を保存
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

[Serializable]
public class SaveData
{
    public bool[] process = new bool[(int)ProcessType.LastProcessType];  // アイテム取得状況を管理
}