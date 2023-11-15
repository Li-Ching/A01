using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class apitest : MonoBehaviour
{
    private string furnitureName;
    private string type;
    private string color;
    private string style;
    private string brand1;
    private string picture;
    private string sceneName;
    public Text FurnitureName; // 在Unity中指派相應的UI元素
    public Text FurnitureType; // 在Unity中指派相應的UI元素
    public Text FurnitureColor; // 在Unity中指派相應的UI元素
    public Text FurnitureStyle; // 在Unity中指派相應的UI元素
    public Text FurnitureBrand1; // 在Unity中指派相應的UI元素
    public Image FurnitureImage; // 這是用來顯示圖片的 RawImage UI 元素
    public Button button; // 在Unity中指派相應的UI元素

    void Start()
    {
        // 呼叫個別家具 API
        StartCoroutine(SetupSceneData());

        button.onClick.AddListener(OnButtonClick);
    }

    #region IEnumerator SetupSceneData() 設定家具詳細資料
    IEnumerator SetupSceneData()
    {
        string sceneDataJson = PlayerPrefs.GetString("SceneData"); // 取得參數
        SceneData sceneData = JsonUtility.FromJson<SceneData>(sceneDataJson); // 解析參數

        //將 API 設置為個別家具 API
        string apiUri = (sceneData.furnitureId <= 0) ? "http://140.137.41.136:1380/A01/api/Furnitures/1" : "http://140.137.41.136:1380/A01/api/Furnitures/" + sceneData.furnitureId;

        UnityWebRequest uwr = UnityWebRequest.Get(apiUri); // GET 方法
        yield return uwr.SendWebRequest(); // 獲得回傳值

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            // 解析回傳資訊
            var furnitureData = JsonUtility.FromJson<Furniture>(uwr.downloadHandler.text);
            furnitureName = furnitureData.furnitureName;
            type = furnitureData.type;
            color = furnitureData.color;
            style = furnitureData.style;
            brand1 = furnitureData.brand1;
            picture = furnitureData.picture;
            sceneName = furnitureData.sceneName;

            // 替換UIText元素的文字
            FurnitureName.text = furnitureName;
            FurnitureType.text = type;
            FurnitureColor.text = color;
            FurnitureStyle.text = style;
            FurnitureBrand1.text = brand1;

            // 取得按鈕上的Image元素，將其設定為家具圖片
            string url = "http://140.137.41.136:1380/A01Web/Images/" + picture; // 
            if (FurnitureImage != null)
            {
                StartCoroutine(GetTexture(url));
            }
            else
            {
                Debug.LogError("FurnitureImage is not assigned!");
            }
        }
    }
    #endregion

    #region IEnumerator GetTexture(string url) 圖片放至 UI 上
    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url); // GET 圖片方法
        yield return www.SendWebRequest(); // 獲得回傳值

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + www.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);

            // 將 Texture2D 轉換為 Sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // 顯示圖片在 Image UI 元素中
            FurnitureImage.sprite = sprite;
        }
    }
    #endregion

    void OnButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }
    void Update()
    {

    }
}

[System.Serializable]
public class Furniture
{
    public int furnitureId;
    public string furnitureName;
    public string type;
    public string color;
    public string style;
    public string brand1;
    public string phoneNumber;
    public string address;
    public string logo;
    public string location;
    public string picture;
    public string sceneName;
}
