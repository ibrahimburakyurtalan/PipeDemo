/// <summary>
/// This class controls the data in the project. It is inherited from "Singleton" class.
/// </summary>
public class DataManager : Singleton<DataManager>
{
    #region Attributes
    // Represents the camera event.
    private int previousLevelNumber = 1;
    #endregion

    #region Methods
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// GetPreviousLevelNumber()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This method returns previous level number.
    /// </summary>
    public int GetPreviousLevelNumber()
    {
        return previousLevelNumber;
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// SetPreviousLevelNumber(int value)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This method sets previous level number.
    /// </summary>
    /// <param name="value">Integer value.</param>
    public void SetPreviousLevelNumber(int value)
    {
        previousLevelNumber = value;
    }
    #endregion
}
