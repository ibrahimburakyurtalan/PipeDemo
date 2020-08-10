using UnityEngine;

/// <summary>
/// Manager scripts will inherit this generic singleton pattern class.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Represents static instance of the class.
    public static T Instance;

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// Awake()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        // If the instance is not null, destroy this object.
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        
        // Instance assignment.
        Instance = this as T;

        // Do not destroy this object when changing scenes.
        DontDestroyOnLoad(this);
    }
}