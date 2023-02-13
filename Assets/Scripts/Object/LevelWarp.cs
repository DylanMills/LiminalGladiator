using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWarp : MonoBehaviour
{
    [SerializeField] int sceneToLoad;
    [SerializeField] Image screenCover;
    bool loading;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!loading)
                StartCoroutine(ChangeArea());
        }
    }

    IEnumerator ChangeArea()
    {
        loading = true;
        float i = 0;

        while (i < 1)
        {
            i += Time.deltaTime;
            
            screenCover.color = new Color(0, 0, 0, i);
            
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
