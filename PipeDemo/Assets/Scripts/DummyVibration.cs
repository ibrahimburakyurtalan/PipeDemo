using UnityEngine;

/// <summary>
/// This class is used to create dummy vibrations.
/// </summary>
public class DummyVibration : MonoBehaviour
{
    #region Methods
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// Start()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        // Represents timings.
        long[] timings = { 50, 100, 50, 100, 50, 100, 50, 100, 50 };
        // Represents amplitudes.
        int[] amplitudes = { 50, 0, 50, 0, 50, 0, 50, 0, 50 };
        // Vibrates.
        HapticManager.Instance.CreatePattern(timings, amplitudes, -1);
    }
    #endregion
}
