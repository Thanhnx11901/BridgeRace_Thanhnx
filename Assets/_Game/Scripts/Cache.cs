using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cache
{
    private static Dictionary<Collider, CharacterCtl> characters = new Dictionary<Collider, CharacterCtl>();

    public static CharacterCtl GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<CharacterCtl>());
        }

        return characters[collider];
    }

    private static Dictionary<Collider, Brick> bricks = new Dictionary<Collider, Brick>();

    public static Brick GetBrick(Collider collider)
    {
        if (!bricks.ContainsKey(collider))
        {
            bricks.Add(collider, collider.GetComponent<Brick>());
        }

        return bricks[collider];
    }

    private static Dictionary<Collider, Stair> stairs = new Dictionary<Collider, Stair>();

    public static Stair GetStair(Collider collider)
    {
        if (!stairs.ContainsKey(collider))
        {
            stairs.Add(collider, collider.GetComponent<Stair>());
        }

        return stairs[collider];
    }

    private static Dictionary<Collider, Door> doors = new Dictionary<Collider, Door>();

    public static Door GetDoor(Collider collider)
    {
        if (!doors.ContainsKey(collider))
        {
            doors.Add(collider, collider.GetComponent<Door>());
        }

        return doors[collider];
    }

    private static Dictionary<Collider, DoorFinish> doorFinishs = new Dictionary<Collider, DoorFinish>();

    public static DoorFinish GetDoorFinishs(Collider collider)
    {
        if (!doorFinishs.ContainsKey(collider))
        {
            doorFinishs.Add(collider, collider.GetComponent<DoorFinish>());
        }

        return doorFinishs[collider];
    }

    private static Dictionary<Collider, Finish> finish = new Dictionary<Collider, Finish>();

    public static Finish GetFinish(Collider collider)
    {
        if (!finish.ContainsKey(collider))
        {
            finish.Add(collider, collider.GetComponent<Finish>());
        }

        return finish[collider];
    }
}
