using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    // カメラオブジェクトを管理するシングルトン
    public static CameraManager Instance { get; private set; }
    // 現在のカメラ位置名(現在どこにいるかをわかるようにするため)
    public string CurrentPositionName { get; private set; }
    
    //　矢印キーの管理するオブジェクト
    public GameObject ButtonLeft;
    public GameObject ButtonRight;
    public GameObject ButtonBack;
    public GameObject ButtonUp;

    // <summary>
    // カメラの位置を格納を格納するためのクラス
    // 内部的に使用するプロパティをまとめめるためにクラスを用意する
    // </summary>
    private class CameraPosition
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public MoveNames MoveNames { get; set; }
    }

    // <summary>
    // ボタン移動先クラス
    // 矢印ボタンを押したときの移動先を格納するためのクラス
    // </summary>
    private class MoveNames
    {
        // 左ボタンを押したときの移動先
        public string Left { get; set; }
        // 右ボタンを押したときの移動先
        public string Right { get; set; }
        // 下ボタンを押したときの移動先
        public string Back { get; set; }
        // 上ボタンを押したときの移動先
        public string Up {get;set;}
    }

    // 移動先の座標を格納したリスト
    // この辞書にカメラの位置、回転、移動先情報を格納する
    private Dictionary<string, CameraPosition> cameraPositionInfos = new Dictionary<string, CameraPosition>
    {
        // Bed Room Camera Position
        {
            "BedRoomMain",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-9.15f, 6.0f, -1.95f),
                Rotation = Quaternion.Euler(18.0f, 415.0f, 0),
                MoveNames = new MoveNames { Left = "BedRoomImageAndTable", Right = "BedRoomDoor"}
            }
        },
        {
            "BedRoomImageAndTable",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-7.0f, 6.0f, -3.0f),
                Rotation = Quaternion.Euler(18.0f, 300.0f, 0),
                MoveNames = new MoveNames { Left = "BedRoomDoorOpen", Right = "BedRoomMain"}
            }
        },
        {
            "BedRoomDoor",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.0f, 6.0f, 0.4f),
                Rotation = Quaternion.Euler(9.2f, 180.0f, 0),
                MoveNames = new MoveNames { Left = "BedRoomMain", Right = "BedRoomImageAndTable"}
            }
        },
        {
            "BedRoomDoorOpen",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.0f, 6.0f, 0.4f),
                Rotation = Quaternion.Euler(9.2f, 180.0f, 0),
                MoveNames = new MoveNames { Left = "BedRoomMain", Right = "BedRoomImageAndTable",Up="Corridor"}
            }
        },
        // ここから先は廊下
        {
            "Corridor",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.0f, 6.0f, -2.9f),
                Rotation = Quaternion.Euler(21.0f, 180.0f, 0),
                MoveNames = new MoveNames {Back="BedRoomDoorOpen",Right="CorridorBack",Left="CorridorFront"}
            }
        },
        {
            "CorridorFront",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.0f, 5.85f, -4.15f),
                Rotation = Quaternion.Euler(9.2f, 91.5f, 0),
                MoveNames = new MoveNames {Back="Corridor",Up="CorridorDoorOpen"}
            }
        },
        {
            "CorridorBack",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.0f, 5.85f, -4.15f),
                Rotation = Quaternion.Euler(9.2f, 267f, 0),
                MoveNames = new MoveNames {Back="Corridor"}
            }
        },
        {
            "CorridorDoor",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-5.45f, 5.85f, -4.15f),
                Rotation = Quaternion.Euler(9.2f, 91.5f, 0),
                MoveNames = new MoveNames {Back="CorridorFront"}
            }
        },
        {
            "CorridorDoorOpen",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-5.45f, 5.85f, -4.15f),
                Rotation = Quaternion.Euler(9.2f, 91.5f, 0),
                MoveNames = new MoveNames {Back="CorridorFront",Up="LastRoom"}
            }
        },
        {
            "LastRoom",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-0.8f, 5.85f, -4.15f),
                Rotation = Quaternion.Euler(9.2f, 91.5f, 0),
                MoveNames = new MoveNames {Back="CorridorDoorOpen",Right="LastRoomTable"}
            }
        },
        {
            "LastRoomTable",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1.3f, 5.7f, -6.5f),
                Rotation = Quaternion.Euler(9f, 133.0f, 0),
                MoveNames = new MoveNames {Left="LastRoom"}
            }
        },
        {
            "LastRoomTV",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1.3f, 5.7f, -6.5f),
                Rotation = Quaternion.Euler(9f, 218.0f, 0),
                MoveNames = new MoveNames {Left="LastRoomTable"}
            }
        },
        {
            "LastRoomDoor",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1.3f, 5.7f, -6.5f),
                Rotation = Quaternion.Euler(9f, 218.0f, 0),
                MoveNames = new MoveNames {Left="LastRoomTable"}
            }
        },

    };

    void Start()
    {
        Instance = this;
        // カメラの初期位置を設定
        ChangeCameraPosition("BedRoomMain");
        // ボタンの初期設定をする
        ButtonBack.GetComponent<Button>().onClick.AddListener(() =>
        {
            // Backボタンが押されたときの処理
            ChangeCameraPosition(cameraPositionInfos[CurrentPositionName].MoveNames.Back);
        });
        ButtonLeft.GetComponent<Button>().onClick.AddListener(() =>
        {
            // Leftボタンが押されたときの処理
            ChangeCameraPosition(cameraPositionInfos[CurrentPositionName].MoveNames.Left);
        });
        ButtonRight.GetComponent<Button>().onClick.AddListener(() =>
        {
            // Rightボタンが押されたときの処理
            ChangeCameraPosition(cameraPositionInfos[CurrentPositionName].MoveNames.Right);
        });
        ButtonUp.GetComponent<Button>().onClick.AddListener(() =>
        {
            // Rightボタンが押されたときの処理
            ChangeCameraPosition(cameraPositionInfos[CurrentPositionName].MoveNames.Up);
        });

    }

    // カメラの切り替えを実際に行うメソッド
    public void ChangeCameraPosition(string positionName)
    {
        if (positionName == null) return;
        // 移動先名を代入する
        CurrentPositionName = positionName;
        // 移動先の座標はDictionaryから取得する
        GetComponent<Camera>().transform.position = cameraPositionInfos[positionName].Position;
        GetComponent<Camera>().transform.rotation = Quaternion.Euler(cameraPositionInfos[positionName].Rotation.eulerAngles);
        UpdateButtonActive();
    }

    // ボタンの表示/非表示を更新するメソッド
    // ボタンの中身がnullであった場合にはボタンは非表示にする
    private void UpdateButtonActive()
    {
        // ボタンの表示を更新する
        ButtonLeft.SetActive(cameraPositionInfos[CurrentPositionName].MoveNames.Left != null);
        ButtonRight.SetActive(cameraPositionInfos[CurrentPositionName].MoveNames.Right != null);
        ButtonBack.SetActive(cameraPositionInfos[CurrentPositionName].MoveNames.Back != null);
        ButtonUp.SetActive(cameraPositionInfos[CurrentPositionName].MoveNames.Up!= null);
    }
}
