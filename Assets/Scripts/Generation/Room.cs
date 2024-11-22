using System;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Room(int depth, Room parentRoom)
    {
        roomEntities = new List<EC_Entity>();
        this.depth = depth;
        this.parentRoom = parentRoom;
    }

    public List<EC_Entity> roomEntities;
    public Room parentRoom;
    
    // Variables
    public int depth;

    public bool Clear { get { return CheckClear(); } }
    /// <summary>
    /// Returns true if no hostile entities exist in room, returns false if any hostile entities exist in room
    /// </summary>
    bool CheckClear()
    {
        // Check for hostiles
        for (int i = 0; i < roomEntities.Count; i++)
        {
            if (roomEntities[i].GetComponent<EC_Damage>())
                return false;
        }

        return true;
    }

    /// <summary>
    /// Handles room enter logic
    /// </summary>
    public void EnterRoom()
    {
        for (int i = 0; i < roomEntities.Count; i++)
        {
            roomEntities[i].IsEnabled(true, DungeonManager.instance.transform);
        }
    }

    /// <summary>
    /// Handles room exit logic
    /// </summary>
    public void ExitRoom()
    {
        for (int i = 0; i < roomEntities.Count; i++)
        {
            roomEntities[i].IsEnabled(false);
        }
    }

    /// <summary>
    /// Lock or unlock all doors in room
    /// </summary>
    public void SetAllDoors(bool _locked)
    {
        // Get all doors in room
        List<EC_Door> _doors = new List<EC_Door>();
        for (int i = 0; i < roomEntities.Count;i++)
        {
            if (roomEntities[i].GetComponent<EC_Door>())
                _doors.Add(roomEntities[i].GetComponent<EC_Door>());
        }
        // Set all doors
        for (int i = 0; i < _doors.Count;i++)
            _doors[i].SetLocked(_locked);
    }
}
