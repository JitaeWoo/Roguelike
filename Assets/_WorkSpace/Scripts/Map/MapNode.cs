using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    public MapNode LeftNode;
    public MapNode RightNode;
    public RectInt NodeRect;
    public RectInt RoomRect;

    public MapNode(RectInt rect)
    {
        NodeRect = rect;
    }
}
