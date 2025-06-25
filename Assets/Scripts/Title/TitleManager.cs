using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    const string SAVE_KEY = "SaveData";
    // Continueボタンのオブジェクト
    public GameObject ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        // PreyerPrefabにSAVE_KEYが存在するか確認
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            // SAVE_KEYが存在する場合、Continueボタンをアクティブにする
            ContinueButton.SetActive(true);
        }
        else
        {
            // SAVE_KEYが存在しない場合、Continueボタンを非アクティブにする
            ContinueButton.SetActive(false);
        }
    }

    public void OnStartButtonClicked()
    {
        // PlayerPrefsからSAVE_KEYを削除
        PlayerPrefs.DeleteKey(SAVE_KEY);
        // Startボタンがクリックされたら、ゲームシーンに遷移する
        SceneManager.LoadScene("GameScene");
    }
    
    public void OnContinueButtonClicked()
    {
        // Continueボタンがクリックされたら、ゲームシーンに遷移する
        SceneManager.LoadScene("GameScene");
    }
}
