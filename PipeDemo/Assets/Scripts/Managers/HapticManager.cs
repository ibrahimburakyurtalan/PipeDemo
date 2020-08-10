using UnityEngine;

/// <summary>
/// This class controls the haptic operations in the project. It is inherited from "Singleton" class.
/// </summary>
public class HapticManager : Singleton<HapticManager>
{
    #region Attributes
    // Represents native vibration classes
    public AndroidJavaClass vibrationEffectClass;
    public AndroidJavaObject vibratorService;
    // Represents default amplitude value.
    private int defaultAmplitude;
    // Represents the API level is modern or not.
    private bool isModernAPI = false;
    // Represents the limit of legacy API level.
    private const int modernAPIVersion = 26;
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
        // If the device is Android
        if (Application.platform == RuntimePlatform.Android)
        {
            // Gets android API level
            var androidAPIlevel = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
            // Assings true if android has modern API.
            isModernAPI = androidAPIlevel >= modernAPIVersion;
            // Gets unity player.
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            // Gets current activity.
            var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            // Gets vibrator service.
            vibratorService = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            // If the API level is modern
            if (isModernAPI)
            {
                // Gets vibration effect class.
                vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
                // Gets default amplitude.
                defaultAmplitude = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
            }
        }
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// CreateOneShot(long duration, int amplitude = -1)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Creates a one time vibration.
    /// </summary>
    /// <param name="milliseconds">Duration in milliseconds</param>
    /// <param name="amplitude">Strength of vibration. Between 1-255. Use -1 for default</param>
    public void CreateOneShot(long duration, int amplitude = -1)
    {
        // If amplitude is -1, amplitude will be assigned as default.
        if (amplitude == -1) { amplitude = defaultAmplitude; }

        // Modern API level vibration.
        if (isModernAPI) { ModernVibrate("createOneShot", new object[] { duration, amplitude }); }
        // Legacy API level vibration.
        else { LegacyVibrate(duration); }
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// CreatePattern(long[] timings, int[] amplitudes, int repeat)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Creates a pattern vibration.
    /// </summary>
    /// <param name="timings">Duration of each of these amplitudes in milliseconds</param>
    /// <param name="amplitudes">Amplitudes for each vibration</param>
    /// <param name="repeat">index of where to repeat, -1 for no repeat</param>
    public void CreatePattern(long[] timings, int[] amplitudes, int repeat)
    {
        // Modern API level vibration.
        if (isModernAPI) { ModernVibrate("createWaveform", new object[] { timings, amplitudes, repeat }); }
        // Legacy API level vibration.
        else { LegacyVibrate(timings, repeat); }
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// ModernVibrate(string function, params object[] args)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Internal implementation for Android function call
    /// </summary>
    /// <param name="function">Function name</param>
    /// <param name="args">Arguments</param>
    private void ModernVibrate(string function, params object[] args)
    {
        // If the device is not Android, returns.
        if (!IsAndroid()) { Handheld.Vibrate(); return; }
        // If the vibrator service is null, returns.
        if (vibratorService == null) { return; }
        // Modern vibration call.
        AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
        vibratorService.Call("vibrate", vibrationEffect);
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// LegacyVibrate(long duration)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Legacy implementation for API <= 25
    /// </summary>
    /// <param name="duration">Duration in milliseconds</param>
    private void LegacyVibrate(long duration)
    {
        // If the device is not Android, returns.
        if (!IsAndroid()) { Handheld.Vibrate(); return; }
        // If the vibrator service is null, returns.
        if (vibratorService == null) { return; }
        // Legacy vibration call.
        vibratorService.Call("vibrate", duration);
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// LegacyVibrate(long[] pattern, int repeat)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Legacy implementation for API < =25
    /// </summary>
    /// <param name="pattern">Duration of each of these amplitudes in millisecond></param>
    /// <param name="repeat">index of where to repeat, -1 for no repeat</param>
    private void LegacyVibrate(long[] pattern, int repeat)
    {
        if (!IsAndroid()) { Handheld.Vibrate(); return; }

        if (vibratorService == null) { return; }

        vibratorService.Call("vibrate", pattern, repeat);
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// IsAndroid()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Checks is the device is android or not.
    /// </summary>
    private bool IsAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return true;
        }

        return false;
    }
    #endregion
}
