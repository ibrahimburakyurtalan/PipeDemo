using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class controls the events in the project. It is inherited from "Singleton" class.
/// </summary>
public class EventManager : Singleton<EventManager>
{
    #region Attributes
    // Represents the check level event.
    [HideInInspector] public UnityEvent EventCheckLevel = new UnityEvent();
    // Represents the start level event.
    [HideInInspector] public UnityEvent EventStartLevel = new UnityEvent();
    // Represents the end level event.
    [HideInInspector] public UnityEvent EventEndLevel = new UnityEvent();
    #endregion
}
