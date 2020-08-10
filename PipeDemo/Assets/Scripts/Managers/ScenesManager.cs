using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controls the scene operations in the project. It is inherited from "Singleton" class.
/// </summary>
public class ScenesManager : Singleton<ScenesManager>
{
    #region Attributes
    // Represents scene name enumerations.
    public enum Scene
    {
        MainMenu, LevelMenu, EndOfLevel, Loading,
        Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10,
        Finish,
    }
    // Represents loader callback.
    private Action onLoaderCallBack;
    #endregion

    #region Methods
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// Load(Scene scene)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Loads scene.
    /// </summary>
    /// <param name="scene">Target scene enumeration.</param>
    public void Load(Scene scene)
    {
        // Set the loader callback action to load the target scene.
        onLoaderCallBack = () =>
        {
            StartCoroutine(LoadSceneAsync(scene));
        };

        // Load the loading scene.
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// LoadSceneAsync(Scene scene)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Internal implementation for loading scenes asynchronously.
    /// </summary>
    /// <param name="scene">Target scene enumeration.</param>
    private IEnumerator LoadSceneAsync(Scene scene)
    {
        // Yields null.
        yield return null;
        // AsyncOperation object.
        AsyncOperation asyncOperation;

        // If the target scene can not be loaded, "Finish" scene will be loaded.
        if (!Application.CanStreamedLevelBeLoaded((int)scene))
        {
            asyncOperation = SceneManager.LoadSceneAsync(Scene.Finish.ToString());
        }
        // If not requested scene will be loaded.
        else
        {
            asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        }

        // When the operation is done, yields.
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// LoaderCallback()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Loader callback.
    /// </summary>
    public void LoaderCallback()
    {
        if (onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }
    #endregion
}
