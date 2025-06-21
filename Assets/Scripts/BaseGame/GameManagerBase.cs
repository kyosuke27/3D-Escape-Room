using UnityEngine;

public class GameManagerBase : MonoBehaviour
{
    // クリアしているか判定する
    protected bool isClear = false;
    // ClearManagerの進行度を管理するためのProcessType
    public ProcessType processType;
    // ゲームクリア時に表示するオブジェクト
    public GameObject[] ItemPanel;
    // ゲームクリア時に表示するオブジェクトとか諸々の処理
    protected virtual void GetClear()
    {}
}
