using UnityEngine;

/// <summary>
/// This class initializes levels.
/// </summary>
public class LevelInitializer : MonoBehaviour
{
    #region Attributes
    // Represents pipes gameobject.
    [Header("Pipes gameobject")]
    [SerializeField] private Transform pipes = null;
    // Represents dummy pipes gameobject.7
    [Header("Total level number")]
    [SerializeField] private Transform dummyPipes = null;
    #endregion

    #region Methods
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// Start()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        // Total child count in pipes gamobject.
        int childCountPipes = pipes.transform.childCount;
        // Total child count in dummy pipes gamobject.
        int childCountDummyPipes = dummyPipes.transform.childCount;

        // Iterates for all childs in pipes gameobject.
        for (int i = 0; i < childCountPipes; i++)
        {
            // Gets child gameobject.
            Transform child = pipes.GetChild(i);

            // If the child is tagged as "Pipe", random rotation value will be implemented.
            if (child.CompareTag("Pipe"))
            {
                int randomRotationSelector = Random.Range(1, 4);
                child.transform.eulerAngles = new Vector3(0, 0, randomRotationSelector * 90);
            }
        }

        // Iterates for all childs in dummy pipes gameobject.
        for (int i = 0; i < childCountDummyPipes; i++)
        {
            // Get child gameobject.
            Transform child = pipes.GetChild(i);

            // If the child is tagged as "Pipe", random rotation value will be implemented.
            if (child.CompareTag("Pipe"))
            {
                int randomRotationSelector = Random.Range(1, 4);
                child.transform.eulerAngles = new Vector3(0, 0, randomRotationSelector * 90);
            }
        }

        // The first and last child will be colored as green
        pipes.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.green;
        pipes.GetChild(childCountPipes - 1).GetComponentInChildren<SpriteRenderer>().color = Color.green;

        // Start level event will be invoked.
        EventManager.Instance.EventStartLevel.Invoke();
    }
    #endregion
}
