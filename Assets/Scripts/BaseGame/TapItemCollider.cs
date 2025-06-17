using UnityEngine;
using UnityEngine.UI;

// Canvas内で表示されたアイテムオブジェクトのタップイベントの基底クラス
public class TapItemCollider : MonoBehaviour
{
    public bool IsSelected = false; // タップされているかどうか
    // タップされた際に表示するアイテムの詳細画面オブジェクト
    public GameObject ItemDetailPanel;
    // 詳細画面に表示するアイテムの画像
    public Image ItemImage;
    void Start()
    {
        // 自分のオブジェクトにたいしてEventTiggerを追加する
        var CurrentTrigger = gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();
        // イベントの中身を作成
        var EntryClick = new UnityEngine.EventSystems.EventTrigger.Entry();
        EntryClick.eventID = UnityEngine.EventSystems.EventTriggerType.PointerClick;
        EntryClick.callback.AddListener((eventData) => {
            if(gameObject.activeSelf) // オブジェクトがアクティブな場合のみ処理を実行
            {
                OnTap(); // タップされた時の処理を呼び出す
            }
        });
        // イベントを登録
        CurrentTrigger.triggers.Add(EntryClick);
    }

    protected virtual void OnTap()
    {
        // タップされた時の処理をここに記述する
        // これは継承先で実装することを想定している
    }
}
