using System;
using System.Collections.Generic;
using UnityEngine;

//ゲームのクリア状態を保存しておくクラス
// オブジェクトにアタッチして、進行度を管理するクラス
// UserPrefabへの保存もかねる
public class ClearManager : MonoBehaviour
{
    // UserPrefabに保存するためのKey
    const string SAVE_KEY = "SaveData";

    // GameMangerを格納した配列ギミックの完了の初期状態を設定するためにしようする
    // Processtypeの順番に設定する
    public GameObject[] GameManagers;

    // itemsのパネルを格納した配列
    // ItemsTypeの順番に設定する
    public GameObject[] ItemPanels;

    // 保存用のデータクラス
    public SaveData saveData;

    // 進行度を表す辞書
    // 中身に進行度のキーと、終わっているかのbool値が入っている
    // 未終了：false 
    // 終了：true
    private Dictionary<ProcessType, bool> _progress = new();

    // アイテムの取得状況を表すデータクラス
    // ItemTypeと取得しているか判定するためのbool値が入っている
    // 持っていない：　false
    // 持っている:true
    private Dictionary<ItemType, bool> _items = new();
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
        Load();

        // 各GameManagerの初期化
        foreach (var p in _progress)
        {
            // ProcessTypeの数だけループして進行度を設定
            if (p.Value != false) // 未解決の場合
            {
                ClearGimic(p.Key); // ゲームクリアの処理を呼び出す    
            }
        }
        // Itemを取得の初期化
        foreach (var i in _items)
        {
            // ProcessTypeの数だけループして進行度を設定
            if (i.Value != false) // 未解決の場合
            {
                // アイテムを取得したことを通知する
                GetItem(i.Key);
            }
        }
    }
    
    // アイテムを持っているのでパネルを表示する
    private void GetItem(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.BedRoomDoorKey:
                ItemPanels[(int)ItemType.BedRoomDoorKey].SetActive(true);
                break;
            case ItemType.CorridorDoorKey:
                ItemPanels[(int)ItemType.CorridorDoorKey].SetActive(true);
                break;
            case ItemType.BedRoomSafeKey:
                ItemPanels[(int)ItemType.BedRoomSafeKey].SetActive(true);
                break;
            case ItemType.Driver:
                ItemPanels[(int)ItemType.Driver].SetActive(true);
                break;
            case ItemType.RemoController:
                ItemPanels[(int)ItemType.RemoController].SetActive(true);
                break;
            case ItemType.SilverBirdStatue:
                ItemPanels[(int)ItemType.SilverBirdStatue].SetActive(true);
                break;
            case ItemType.GoldBirdStatue:
                ItemPanels[(int)ItemType.GoldBirdStatue].SetActive(true);
                break;
            case ItemType.WineBottle:
                ItemPanels[(int)ItemType.WineBottle].SetActive(true);
                break;
            case ItemType.WhiteMugCup:
                ItemPanels[(int)ItemType.WhiteMugCup].SetActive(true);
                break;
            case ItemType.BedLoomDrawerKey:
                ItemPanels[(int)ItemType.BedLoomDrawerKey].SetActive(true);
                break;
            case ItemType.DateMemo:
                ItemPanels[(int)ItemType.DateMemo].SetActive(true);
                break;
            case ItemType.Ball:
                ItemPanels[(int)ItemType.Ball].SetActive(true);
                break;
            case ItemType.MarkMemo:
                ItemPanels[(int)ItemType.MarkMemo].SetActive(true);
                break;
            case ItemType.LastRoomOutKey:
                ItemPanels[(int)ItemType.LastRoomOutKey].SetActive(true);
                break;
            default:
                Debug.LogWarning($"Item {itemType} does not have a corresponding panel.");
                break;
        }
    }

    private void ClearGimic(ProcessType p)
    {
        // ゲームがクリアしたとみなして処理を続ける
        // それぞれのTypeごとに対応するクラスを探す
        // クラスにはGameClear()メソッドがあることを前提とする
        switch (p)
        {
            // EtoGameの処理
            case ProcessType.EtoGame:
                EtoGameManager etoGameManager = GameManagers[(int)ProcessType.EtoGame].GetComponent<EtoGameManager>();
                etoGameManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.ArrowGame:
                // ArrowGameの処理をここに追加
                ArrowGameManager arrowGameManager = GameManagers[(int)ProcessType.ArrowGame].GetComponent<ArrowGameManager>();
                // arrowGameManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.StoneTouch:
                // StoneTouchの処理をここに追加
                StoneTouchManager stoneTouchManager = GameManagers[(int)ProcessType.StoneTouch].GetComponent<StoneTouchManager>();
                // stoneTouchManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.CupGame:
                // CupGameの処理をここに追加
                CupGameManager cupGameManager = GameManagers[(int)ProcessType.CupGame].GetComponent<CupGameManager>();
                // cupGameManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.WeatherGame:
                // WeatherGameの処理をここに追加
                WeatherGameManager weatherGameManager = GameManagers[(int)ProcessType.WeatherGame].GetComponent<WeatherGameManager>();
                // weatherGameManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.MoveBar:
                // MoveBarの処理をここに追加
                MoveBarManager moveBarManager = GameManagers[(int)ProcessType.MoveBar].GetComponent<MoveBarManager>();
                // moveBarManager.GameClear(); // ゲームクリア              
                break;
            case ProcessType.BlackUpGame:
                // BlackUpGameの処理をここに追加
                BlackUpGameManager blackUpGameManager = GameManagers[(int)ProcessType.BlackUpGame].GetComponent<BlackUpGameManager>();
                // blackUpGameManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.BirdStatue:
                // BirdStatueの処理をここに追加

                LastRoomSafeManager birdStatueManager = GameManagers[(int)ProcessType.BirdStatue].GetComponent<LastRoomSafeManager>();
                // birdStatueManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.CupPodPosition:
                // CupPodPositionの処理をここに追加
                CupPodManager cupPodPositionManager = GameManagers[(int)ProcessType.CupPodPosition].GetComponent<CupPodManager>();
                // cupPodPositionManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.TapDate:
                // TapDateの処理をここに追加
                TapDateManager tapDateManager = GameManagers[(int)ProcessType.TapDate].GetComponent<TapDateManager>();
                // tapDateManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.BigSmallCircle:
                // BigSmallCircleの処理をここに追加
                BigSmallCircleManager bigSmallCircleManager = GameManagers[(int)ProcessType.BigSmallCircle].GetComponent<BigSmallCircleManager>();
                // bigSmallCircleManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            case ProcessType.MarkGame:
                // MarkGameの処理をここに追加
                MarkButtonManager markGameManager = GameManagers[(int)ProcessType.MarkGame].GetComponent<MarkButtonManager>();
                // markGameManager.GameClear(); // ゲームクリアの処理を呼び出す
                break;
            // 他のProcessTypeに対する処理もここに追加する
            default:
                Debug.LogWarning($"ProcessType {p} does not have a specific clear action defined.");
                break;
        }

    }

    // セーブデータをロードする
    public void Load()
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
            // Itemsを初期化する
            for (int i = 0; i < (int)ItemType.LastItemKey; i++)
            {
                // ProcessTypeの数だけループして進行度を設定
                _items[(ItemType)i] = saveData.items[i];
            }
        }
        else
        {
            // セーブデータが存在しない場合は初期化
            for (int i = 0; i < (int)ProcessType.LastProcessType; i++)
            {
                // ProcessTypeの数だけループして進行度を設定
                _progress[(ProcessType)i] = false; // 全て未解決に設定
            }
            for (int i = 0; i < (int)ItemType.LastItemKey; i++)
            {
                // ProcessTypeの数だけループして進行度を設定
                _items[(ItemType)i] = false;
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
        // SaveDateのitemsを更新する
        for (int i = 0; i < (int)ItemType.LastItemKey; i++)
        {
            saveData.items[i] = _items[(ItemType)i];
        }
        // JSON形式に変換してPlayerPrefsに保存
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    // SaveDataを保存する
    // Progressデータのみ保存する
    public void SaveProgress()
    {
        print("ClearManager Save Progress");
        // SaveDataのprocess配列に進行度を保存
        for (int i = 0; i < (int)ProcessType.LastProcessType; i++)
        {
            saveData.process[i] = _progress[(ProcessType)i];
        }

        // JSON形式に変換してPlayerPrefsに保存
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }
    // SaveDataを保存する
    // Itemsデータのみ保存する
    public void SaveItems()
    {
        print("ClearManager Save Items");
        // SaveDataのprocess配列に進行度を保存
        for (int i = 0; i < (int)ItemType.LastItemKey; i++)
        {
            saveData.items[i] = _items[(ItemType)i];
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
        SaveProgress(); // 進行度を保存
    }

    // アイテムの取得状況を更新する
    public void SetItems(ItemType type)
    {
        // 進行度を設定する
        if (_items.ContainsKey(type))
        {
            // 完了済みにする
            _items[type] = true;
        }
        else //念の為キーが存在しない場合のエラーハンドリング
        {
            Debug.LogError($"ProcessType {type} does not exist in the progress dictionary.");
        }
        SaveItems(); // 進行度を保存
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

    // 取得しているアイテムを取得する
    // 先頭から確認して最初にtrueのものを取得する
    public ItemType[] GetItems()
    {
        List<ItemType> items = new List<ItemType>();
        foreach (var kvp in _items)
        {
            // trueのもの
            if (kvp.Value) // 取得しているアイテムをリストに追加
            {
                items.Add(kvp.Key);
            }
        }
        return items.ToArray(); // リストを配列に変換して返す
    }
}

[Serializable]
public class SaveData
{
    public bool[] process = new bool[(int)ProcessType.LastProcessType];  // アイテム取得状況を管理
    public bool[] items = new bool[(int)ItemType.LastItemKey];  // アイテム取得状況を管理
}
