using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    AsyncOperation async;
    public Slider slider;
    public Text text;//百分制顯示進度加載情況

    void Start()
    {
        //開啓協程
        StartCoroutine("loginMy");
    }

    void Update()
    {

    }
    IEnumerator loginMy()
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync("SampleScene"); //此處改成要加載的場景名
        op.allowSceneActivation = false;
        while (op.progress < 0.9f) //此處如果是 <= 0.9f 則會出現死循環所以必須小0.9
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();//ui渲染完成之後
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;

    }

    private void SetLoadingPercentage(int displayProgress)
    {
        slider.value = displayProgress;
        text.text = displayProgress.ToString() + "%";
    }
}
