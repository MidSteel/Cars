using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreloadData : MonoBehaviour
{
    [SerializeField] GameObject _loadingPanel = null;
    [SerializeField] Slider _loadingBar = null;

    private float _progress = 0f;

    public  void LoadScene(int level)
    {
        _loadingPanel.gameObject.SetActive(true);
        StartCoroutine(LoadRoutine(level));
    }

    private IEnumerator LoadRoutine(int levelToLoad)
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(levelToLoad);

        while (!sceneLoad.isDone) 
        {
            _progress = sceneLoad.progress / 0.9f;
            _loadingBar.value = _progress;
            yield return null;
        }
    }
}
