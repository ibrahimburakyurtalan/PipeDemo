using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class arranges level menu.
/// </summary>
public class LevelController : MonoBehaviour
{
    #region Attributes
    // Represents the pipes gameobject.
    [Header("Pipes gameobject")]
    [SerializeField] private Transform pipes = null;
    // Represents all sprite renderer components in pipes gameobject hiearchy.
    private List<SpriteRenderer> spriteRendererList = new List<SpriteRenderer>();
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
        // Listener is added to the check level event.
        EventManager.Instance.EventCheckLevel.AddListener(CheckLevel);
        // Total child count in pipes gamobject
        var childCountPipes = pipes.transform.childCount;
        // All sprite renderer components are added to the list.
        for (var i = 0; i < childCountPipes; i++)
        {
            Transform child = pipes.GetChild(i);
            spriteRendererList.Add(child.GetChild(0).GetComponent<SpriteRenderer>());
        }
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// CheckLevel()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Listener of the level check event. Controls z rotation of all pipes is zero or not.
    /// </summary>
    private void CheckLevel()
    {
        // Total child count in pipes gamobject
        var childCountPipes = pipes.transform.childCount;
        // Represents how many pipes are moveable.
        var dynamicPipeNumber = 0;
        // Represents correct rotation counter.
        var correctRotationCounter = 0;
        
        // Iterates for all childs.
        for (int i = 0; i < childCountPipes; i++)
        {
            // Gets the relevant child.
            Transform child = pipes.GetChild(i);
            // Gets sprite in child.
            Sprite pipeSprite = child.GetChild(0).GetComponent<SpriteRenderer>().sprite;

            // If the child is tagged as "Pipe"
            if (child.CompareTag("Pipe"))
            {
                // Increment dynamic pipe number.
                dynamicPipeNumber++;

                // If the pipe is not straight. Checks rotation and increment correct rotation counter.
                if (pipeSprite.name != "pipes_1")
                {
                    if (child.rotation == Quaternion.identity)
                    {
                        correctRotationCounter++;
                    }
                }
                // If the pipe is straight. Checks rotation and increment correct rotation counter.
                else
                {
                    if ((child.rotation == Quaternion.identity) || (child.rotation.eulerAngles.z == 180))
                    {
                        correctRotationCounter++;
                    }
                }
            }
        }

        // If correct rotation number is equal to the moveable pipe number, level ends.
        if (dynamicPipeNumber == correctRotationCounter)
        {
            StartCoroutine(ColorizePipes());
        }
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// ColorizePipes()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Colorize pipes in order to achieve water effect.
    /// </summary>
    private IEnumerator ColorizePipes()
    {
        // End level event is invoked here.
        EventManager.Instance.EventEndLevel.Invoke();

        // Iterates for all sprite renderer components in the list.
        foreach (SpriteRenderer spriteRenderer in spriteRendererList)
        {
            // Colorize as blue.
            spriteRenderer.color = Color.blue;
            yield return new WaitForSeconds(0.2f);
        }

        // Gets current level index.
        DataManager.Instance.SetPreviousLevelNumber(SceneManager.GetActiveScene().buildIndex);
        // Loads "EndOfLevel" scene.
        ScenesManager.Instance.Load(ScenesManager.Scene.EndOfLevel);
    }
    #endregion
}
