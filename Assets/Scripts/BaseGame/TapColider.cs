using UnityEngine;
using UnityEngine.EventSystems;

public class TapColider : MonoBehaviour
{
    // 有効なカメラ位置を格納するための値
    public string EnableCameraPositionName;
    void Start()
    {
        // 自分のオブジェクトにEventTriggerコンポーネントを追加
        var CurrentTrigger = gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();
        // イベントの中身を作成
        var EntryClick = new EventTrigger.Entry();
        EntryClick.eventID = EventTriggerType.PointerClick;
        EntryClick.callback.AddListener((eventData) => {
            OnTap();
        });
        // イベントを登録
        CurrentTrigger.triggers.Add(EntryClick);

    }
    // Update is called once per frame
    void Update()
    {
        // CameraManagerはシングルトンのstaticクラスなのでどこからでもアクセス可能
        if (EnableCameraPositionName == CameraManager.Instance.CurrentPositionName)
        {
            // カメラの位置が有効な位置の場合は、コライダーを有効化する
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            // カメラの位置が有効な位置でない場合は、コライダーを無効化する
            GetComponent<Collider>().enabled = false;
        }
    }

    // colliderタップした際の処理を記述する
    //　これは継承先にでの実装を想定している
    protected virtual void OnTap()
    {
        // 何もしない
        // 継承先で実装することを想定している
    }
}
