using UnityEngine;
using System.Collections;

/// <summary>
/// This class rotates pipes.
/// </summary>
public class Rotater : MonoBehaviour
{
    #region Attributes
    // Represents movement enable.
    private bool movementEnable = true;
    #endregion

    #region Methods
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// Start()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        // Listeners are added to the start and end level events
        EventManager.Instance.EventStartLevel.AddListener(StartLevel);
        EventManager.Instance.EventEndLevel.AddListener(EndLevel);
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// OnMouseDown()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while over the Collider.
    /// TODO: It may impact performance, consider using another method like RayCasting.
    /// </summary>
    private void OnMouseDown()
    {
        // If the gameobject is tagged as pipe, it means it is moveable.
        var rotationEnable = transform.parent.CompareTag("Pipe");
        // If rotation enable is false, returns.
        if (!rotationEnable) { return; }
        // If movement enable is false, returns.
        if (!movementEnable) { return; }
        // Rotates.
        StartCoroutine(LerpRotation(0.1f));
        // Vibrates.
        HapticManager.Instance.CreateOneShot(50);
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// LerpRotation(float time)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Lerps rotation value and implements to the gameobject.
    /// </summary>
    /// <param name="milliseconds">Duration in milliseconds</param>
    /// <param name="amplitude">Strength of vibration. Between 1-255. Use -1 for default</param>
    private IEnumerator LerpRotation(float time)
    {
        // Represents the elapsed time.
        var elapsedTime = 0f;
        // Represents the initial rotation value.
        var rotation = transform.parent.rotation;
        // Represents the requested rotation value.
        var endRotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z - 90);
        // Lerps rotation from initial to requested.
        while (elapsedTime < time)
        {
            transform.parent.rotation = Quaternion.Lerp(rotation, endRotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Makes sure.
        transform.parent.rotation = endRotation;
        // Check level event is invoked.
        EventManager.Instance.EventCheckLevel.Invoke();
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// StartLevel()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Enables movement.
    /// </summary>
    private void StartLevel()
    {
        movementEnable = true;
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// EndLevel()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Disables movement.
    /// </summary>
    private void EndLevel()
    {
        movementEnable = false;
    }
    #endregion
}
