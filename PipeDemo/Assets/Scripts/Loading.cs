using UnityEngine;

public class Loading : MonoBehaviour
{
    #region Attributes
    // Represents the first update boolean.
    private bool isFirstUpdate = true;
    #endregion

    #region Methods
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// Start()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // In the first update, loader callback method in ScenesManager script is called.
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            ScenesManager.Instance.LoaderCallback();
        }
    }
    #endregion
}
