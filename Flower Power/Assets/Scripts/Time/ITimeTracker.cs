using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeTracker
{
    // Start is called before the first frame update
    void ClockUpdate(GameTimestamp timestamp);
}
