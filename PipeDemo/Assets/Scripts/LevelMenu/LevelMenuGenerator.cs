using TMPro;
using UnityEngine;

/// <summary>
/// This class arranges the level menu.
/// </summary>
public class LevelMenuGenerator : MonoBehaviour
{
    #region Attributes
    // Represents total level number.
    [Header("Total level number")]
    [Tooltip("Enter how many levels are requested")]
    [Range(5, 10)]
    [SerializeField] private int levelNumber = 5;
    // Represents frame and background sprites.
    [Header("Level game object")]
    [Tooltip("Enter levelUI prefab")]
    [SerializeField] private GameObject levelUI = null;
    // Represents column number.
    private const int totalColumnNumber = 5;
    // Represents gap between level boxes.
    private const int gapBetweenLevels = 220;
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
        // Calculates how many rows are needed.
        var totalRowNumber = (levelNumber - 1) / totalColumnNumber;
        // Checks if row number is even or not.
        var isRowNumberEven = ((totalRowNumber + 1) % 2) == 0;

        // Iterates for all levels.
        for (var i = 0; i < levelNumber; i++)
        {
            // Instantiates "levelUI" prefab.
            var level = Instantiate(levelUI, transform);
            // Renames it.
            level.name = (i + 1).ToString();
            // Fills the text.
            level.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();

            // Calculates current row and column indexes.
            var currentRowNumber = i / totalColumnNumber;
            var currentColumnNumber = i % totalColumnNumber;

            // Calculates the x position.
            var positionX = (currentColumnNumber - 2) * 220;
            // Calculates the y position.
            int positionY;
            if (isRowNumberEven)
            {
                positionY = (currentRowNumber - ((levelNumber - 1) / totalColumnNumber / 2 - 1)) * -gapBetweenLevels + (gapBetweenLevels + gapBetweenLevels / 2);
            }
            else
            {
                positionY = (currentRowNumber - (levelNumber / totalColumnNumber / 2)) * -gapBetweenLevels;
            }

            // Repositions the instantiated "levelUI" prefab.
            var position = new Vector2(positionX, positionY);
            level.GetComponent<RectTransform>().localPosition = position;
        }
    }
    #endregion
}
