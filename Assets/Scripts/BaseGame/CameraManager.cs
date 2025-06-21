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
        // ベッド横の引き出し
        // カップとティーポッド
        {
            "BedRoomDrawerCup",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-7.6f, 6.0f, -0.89f),
                Rotation = Quaternion.Euler(58.4f, 90.0f, 0),
                MoveNames = new MoveNames { Back="BedRoomMain"}
            }
        },
        // 引き出しの棚
        {
            "BedRoomDrawer",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-7.3f, 4.9f, -1.4f),
                Rotation = Quaternion.Euler(14f, -325.0f, 0),
                MoveNames = new MoveNames { Back="BedRoomMain"}
            }
        },

        // 金庫
        {
            "BedRoomSafe",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-7.75f, 4.45f, -0.885f),
                Rotation = Quaternion.Euler(17f, -267.0f, 0.015f),
                MoveNames = new MoveNames { Back="BedRoomMain"}
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
        // ベッドルームのテーブル
        {
            "BedRoomTable",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-9.6f, 6.3f, -1.38f),
                Rotation = Quaternion.Euler(52.41f, -44.0f, 0),
                MoveNames = new MoveNames { Back="BedRoomImageAndTable"}
            }
        },
        // イメージボタン
        {
            "BedRoomTableTapImageButton",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.3f, 5.9f, -0.88f),
                Rotation = Quaternion.Euler(85.43f, -42.0f, 0.123f),
                MoveNames = new MoveNames { Back="BedRoomTable"}
            }
        },
        // 時計
        {
            "BedRoomTableTapWatch",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10f, 5.5f, -0.5f),
                Rotation = Quaternion.Euler(34f, -46.0f, 0),
                MoveNames = new MoveNames { Back="BedRoomTable"}
            }
        },
        // 部屋の絵
        {
            "BedRoomTableWallPicture",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-7.6f, 6.55f, -1.23f),
                Rotation = Quaternion.Euler(0.035f, -90.9f, 0),
                MoveNames = new MoveNames { Back="BedRoomImageAndTable"}
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
        // 廊下の引き出し
        // 日付のボタンを揃える
        {
            "CorridorDrawerDate",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.0f, 6.3f, -5.0f),
                Rotation = Quaternion.Euler(90.0f, -180.0f, 0),
                MoveNames = new MoveNames {Back="Corridor"}
            }
        },
        // バスケットボール
        {
            "CorridorDrawerBall",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.0f, 5.0f, -4.1f),
                Rotation = Quaternion.Euler(21.0f, -180.0f, 0),
                MoveNames = new MoveNames {Back="Corridor"}
            }
        },
        // 廊下のドア方向
        {
            "CorridorFront",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-10.0f, 5.85f, -4.15f),
                Rotation = Quaternion.Euler(9.2f, 91.5f, 0),
                MoveNames = new MoveNames {Back="Corridor",Up="CorridorDoorOpen"}
            }
        },
        // 廊下の椅子にある矢印ボタン
        {
            "CorridorFrontArrow",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-5.27f, 5.2f, -4.5f),
                Rotation = Quaternion.Euler(30f, 180f, 0),
                MoveNames = new MoveNames {Back="CorridorFront"}
            }
        },
        // 廊下の画像
        {
            "CorridorFrontImage",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-6.5f, 5.9f, -5.87f),
                Rotation = Quaternion.Euler(4f, 2f, 0f),
                MoveNames = new MoveNames {Back="CorridorFront"}
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
            "CorridorBackDrawerCupButton",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-11.85f, 5f, -4.15f),
                Rotation = Quaternion.Euler(18.6f, -452f, 0),
                MoveNames = new MoveNames {Back="CorridorBack"}
            }
        },
        {
            "CorridorBackDrawerBox",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-12.8f, 6.15f, -4.21f),
                Rotation = Quaternion.Euler(69f, -450f, 0),
                MoveNames = new MoveNames {Back="CorridorBack"}
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
        // 最後の部屋
        {
            "LastRoom",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-0.8f, 5.85f, -4.15f),
                Rotation = Quaternion.Euler(9.2f, 91.5f, 0),
                MoveNames = new MoveNames {Back="CorridorDoorOpen",Right="LastRoomTable"}
            }
        },
        // 最後の部屋のテーブル
        {
            "LastRoomTableCupPod",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(5.1f, 5.9f, -3.66f),
                Rotation = Quaternion.Euler(76.25f, 91.5f, 0),
                MoveNames = new MoveNames {Back="LastRoom"}
            }
        },
        // 壁にかけてあるワインラック
        {
            "LastRoomWineLackWall",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(3.37f, 6.65f, -4.15f),
                Rotation = Quaternion.Euler(9.2f, 92f, 0),
                MoveNames = new MoveNames {Back="LastRoom"}
            }
        },
        // テーブルとソファーが置いてある場所
        {
            "LastRoomTable",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(0.15f, 6.34f, -8.3f),
                Rotation = Quaternion.Euler(20f, 117.0f, 0),
                MoveNames = new MoveNames {Left="LastRoom",Right="LastRoomDoor"}
            }
        },
        // 長いテーブルの上に置かれているい色カウントアップギミック
        {
            "LastRoomTableCountUp",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1.6f, 5.35f, -9.34f),
                Rotation = Quaternion.Euler(73f, 90.0f, 0),
                MoveNames = new MoveNames {Back="LastRoomTable"}
            }
        },
        // 長いテーブルの大小が変化するボタン
        {
            "LastRoomTableBigSmall",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1.7f, 5.35f, -9.88f),
                Rotation = Quaternion.Euler(78.6f, 90.0f, 0),
                MoveNames = new MoveNames {Back="LastRoomTable"}
            }
        },
        // ソファーの天気ボタン
        {
            "LastRoomSofaWeather",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(4.58f, 5.95f, -10.01f),
                Rotation = Quaternion.Euler(77.35f, 90.0f, 0),
                MoveNames = new MoveNames {Back="LastRoomTable"}
            }
        },
        // 最後の部屋のテレビ
        {
            "LastRoomTV",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1.03f, 5.7f, -7.32f),
                Rotation = Quaternion.Euler(10f, 219.0f, 0),
                MoveNames = new MoveNames {Left="LastRoomDoor",Right="LastRoom"}
            }
        },
        // 最後部屋のテレビ台スライド式のボタン
        {
            "LastRoomTVMoveBar",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-0.07f, 5.7f, -9.05f),
                Rotation = Quaternion.Euler(55f, 269.0f, 0),
                MoveNames = new MoveNames {Back="LastRoomTV"}
            }
        },
        // 最後部屋のテレビ台4*4
        {
            "LastRoomTVBall",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(-0.085f, 5.71f, -10.3f),
                Rotation = Quaternion.Euler(46f, 270.0f, 0),
                MoveNames = new MoveNames {Back="LastRoomTV"}
            }
        },
        // 最後部屋のテレビの記号ボタン
        {
            "LastRoomTVMark",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1f, 4.5f, -9.12f),
                Rotation = Quaternion.Euler(3.14f, 268.0f, 0),
                MoveNames = new MoveNames {Back="LastRoomTV"}
            }
        },
        // TVスクリーン
        {
            "LastRoomTVScreen",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1.6f, 6f, -9.7f),
                Rotation = Quaternion.Euler(10f, 261.0f, 0),
                MoveNames = new MoveNames {Back="LastRoomTV"}
            }
        },

        {
            "LastRoomDoor",
            new CameraPosition // カメラ位置
            {
                Position = new Vector3(1.6f, 6.0f, -12.5f),
                Rotation = Quaternion.Euler(9f, 237.0f, 0),
                MoveNames = new MoveNames {Left="LastRoomTable",Right="LastRoomTV"}
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
