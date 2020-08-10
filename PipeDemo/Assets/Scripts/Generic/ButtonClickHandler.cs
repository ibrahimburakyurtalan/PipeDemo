using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class involves button click operations and it will be used in all game objects that has button component.
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonClickHandler : MonoBehaviour
{
    #region Methods
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// Start()
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => OnClickedButton(gameObject.name));
    }

    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// OnClickedButton(string gameObjectName)
    ///-------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This method is invoked by pressing any button in the project.
    /// </summary>
    /// <param name="gameObjectName">Name of the game object which button belongs.</param>
    private void OnClickedButton(string gameObjectName)
    {
        switch (gameObjectName)
        {
            // If the name of game object is "TapToPlay", "LevelMenu" scene will be loaded.
            case "TapToPlay":
                ScenesManager.Instance.Load(ScenesManager.Scene.LevelMenu);
                break;

            // If the name of game object is "Back", "MainMenu" scene will be loaded.
            case "Back":
                ScenesManager.Instance.Load(ScenesManager.Scene.MainMenu);
                break;

            // If the name of game object is "Next", next level scene will be loaded.
            case "Next":
                int currentLevelNumber = DataManager.Instance.GetPreviousLevelNumber();
                ScenesManager.Instance.Load((ScenesManager.Scene)(currentLevelNumber + 1));
                break;

            // If rest, corresponding level scene will be loaded.
            default:
                int levelNumber = int.Parse(gameObjectName);
                ScenesManager.Instance.Load((ScenesManager.Scene)(levelNumber + 3));
                break;
        }
    }
    #endregion
}
