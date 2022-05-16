using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    [SerializeField]
    GameTimestamp timestamp;

    // to move directional light - gives illusion of day -> night
    

    public float timeScale = 1.0f;

    [Header ("Day and Night cycle")]
    public Transform sunTransform;

    // Observer pattern - time tracker
    List<ITimeTracker> listeners = new List<ITimeTracker>();

    public void Awake()
    {
        //if there is more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timestamp = new GameTimestamp(0, GameTimestamp.Season.Spring, 1, 6, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1/timeScale);
            
        }
 
    }
    // game time: 1 second == 1 minute
    public void Tick()
    {
        timestamp.UpdateClock();

        // Inform the listeners of the new time state
        foreach(ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }


        int timeInMinutes = GameTimestamp.HoursToMinutes(timestamp.hour) + timestamp.minute;
        //Debug.Log(${timeInMinutes});

        // Researched code for sun rotation
        float sunAngle = .25f * timeInMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    // LISTENERS
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }


    // retrieves current timestamp
    public GameTimestamp GetGameTimestamp()
    {
        return new GameTimestamp(timestamp);
    }
}
