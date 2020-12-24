using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;  // the crossfade transition animator

    public float transitionTime = 2f;  // the transition animation length


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        // load the next scene with current scene index + 1 (setting in File->Build Setting)
        // using coroutine to delay the loading for having time playing the transition animation
        StartCoroutine(LoadLevelWithTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /// <summary>
    /// Coroutine function to load the level (scene) with transition animation 
    /// </summary>
    /// <param name="sceneIndexToLoad"></param>
    /// <returns></returns>
    IEnumerator LoadLevelWithTransition(int sceneIndexToLoad)
    {
        // trigger the exit transition animation
        animator.SetTrigger("IsExit");

        // wait for 2 seconds before loading the next level (2 seconds to play the animation)
        yield return new WaitForSeconds(transitionTime);

        // load the next 
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
