using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class autobuttontest : MonoBehaviour
{
    public GameObject buttonPrefab; // 在 Unity 中指派按鈕的預製體，Furniture
    public Transform buttonParent; // 在 Unity 中指派按鈕的父物件，Content
    public InputField searchInputField; // InputField

    public List<Furniture> furnitureList = new List<Furniture>(); // 宣告一個家具list，顯示在畫面的Scroll View中

    void Start()
    {
        // 監看 InputField 的 onValueChanged 事件
        if (searchInputField != null)
        {
            searchInputField.onValueChanged.AddListener(OnSearchInputValueChanged);
        }
        else
        {
            Debug.LogError("searchInputField is null. Make sure it is assigned in the Unity editor.");
        }

        // 呼叫家具 API
        StartCoroutine(getRequest("http://140.137.41.136:1380/A01/api/Furnitures"));
    }

    #region void OnSearchInputValueChanged(string searchValue) 當搜尋欄位的值變化時呼叫的方法
    void OnSearchInputValueChanged(string searchValue)
    {
        if (string.IsNullOrEmpty(searchValue)) // 如果輸入欄位全部刪除之後為空
        {
            StartCoroutine(getRequest("http://140.137.41.136:1380/A01/api/Furnitures"));
        }
        
        // 呼叫搜尋 API
        StartCoroutine(SearchFurnitureAPI(searchValue));
    }
    #endregion

    #region IEnumerator SearchFurnitureAPI(string searchValue) 搜尋 API
    IEnumerator SearchFurnitureAPI(string searchValue)
    {
        string apiUrl = "http://140.137.41.136:1380/A01/api/Furnitures/Search?content=" + searchValue; // 搜尋 API 的 url
        UnityWebRequest uwr = UnityWebRequest.Get(apiUrl); // GET 方法
        yield return uwr.SendWebRequest(); // 獲得回傳值

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            // 清空家具清單
            furnitureList.Clear();

            // 解析並添加新的家具資料
            Furniture[] jsonData = JsonHelper.getJsonArray<Furniture>(uwr.downloadHandler.text);
            furnitureList.AddRange(jsonData);

            // 生成按鈕
            GenerateButtons();
        }
    }
    #endregion

    #region IEnumerator getRequest(string uri) 家具 API
    IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri); // GET 方法
        yield return uwr.SendWebRequest(); // 獲得回傳值

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            // 解析並添加新的家具資料
            Furniture[] jsonData = JsonHelper.getJsonArray<Furniture>(uwr.downloadHandler.text);
            if (jsonData != null)
            {
                furnitureList.AddRange(jsonData);
            }
            else
            {
                Debug.LogWarning("Received null or invalid JSON data from the API.");
            }

            // 生成按鈕
            GenerateButtons();
        }
    }
    #endregion

    #region void GenerateButtons() 生成按鈕
    void GenerateButtons()
    {
        // 清空 Content 元素的子物件，以便重新生成按鈕
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        float yOffset = 0f; // 初始 Y 軸偏移量
        float buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height; // 取得按鈕的高度
        float totalHeight = (buttonHeight + 10) * furnitureList.Count; // 按鈕的總高度

        // 設置 Content 元素的高度
        RectTransform contentTransform = buttonParent.GetComponent<RectTransform>();
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, totalHeight);

        foreach (Furniture furniture in furnitureList)
        {
            // 動態生成按鈕
            GameObject button = Instantiate(buttonPrefab, buttonParent);

            // 設置按鈕的位置
            RectTransform buttonTransform = button.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = new Vector2(0, yOffset);

            // 增加 Y 軸偏移量，以便下一個按鈕不會和前一個重疊
            yOffset -= (buttonHeight + 10);

            // 取得按鈕上的Text元素，將其設定為家具名稱
            // 取得按鈕上的Image元素，將其設定為家具圖片
            Text buttonText = button.transform.Find("FurnitureName")?.GetComponent<Text>();
            Text brandText = button.transform.Find("FurnitureBrand")?.GetComponent<Text>();
            Image buttonImage = button.transform.Find("FurnitureImage")?.GetComponent<Image>();

            if (buttonText != null && brandText != null && buttonImage != null)
            {
                buttonText.text = furniture.furnitureName;
                brandText.text = furniture.brand1;

                string imageUrl = "http://140.137.41.136:1380/A01Web/Images/" + furniture.picture;
                StartCoroutine(GetTexture(imageUrl, buttonImage));

                button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(furniture));
            }
            else
            {
                Debug.LogWarning("One or more UI elements not found in the button prefab.");
            }
        }
    }
    #endregion

    #region IEnumerator GetTexture(string url, Image image) 圖片放至 UI 上
    IEnumerator GetTexture(string url, Image image)
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

            // 檢查 image 是否為 null 或者已經被刪除
            if (image != null)
            {
                // 將 Texture2D 轉換為 Sprite
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                // 顯示圖片在 Image UI 元素中
                image.sprite = sprite;
            }
            else
            {
                Debug.LogWarning("Image is null or destroyed. Texture not set.");
            }
        }
    }
    #endregion

    #region void OnButtonClick(Furniture furniture) 每個按鈕增加點選的功能，要帶參數
    void OnButtonClick(Furniture furniture)
    {
        Debug.Log("Button Clicked for Furniture: " + furniture.furnitureName);

        // 創建包含數據的資料
        SceneData sceneData = new SceneData
        {
            furnitureId = furniture.furnitureId,
        };

        // 將數據轉為JSON格式
        string jsonData = JsonUtility.ToJson(sceneData);

        // 使用SceneManager.LoadScene傳遞數據
        UnityEngine.SceneManagement.SceneManager.LoadScene("testHome");
        PlayerPrefs.SetString("SceneData", jsonData);
    }
    #endregion

    void Update()
    {
        
    }
}

// 要跳轉頁面傳遞的參數，可新增模型名稱等等
[System.Serializable]
public class SceneData
{
    public int furnitureId;
}

// 如果回傳參數是 list 的話，在 unity 需要用別的格式
// 將回傳內容改為 Unity 可讀取的 Array 格式
// 可參考 https://stackoverflow.com/questions/58908464/how-to-fix-argumentexception-json-must-represent-an-object-type
public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}