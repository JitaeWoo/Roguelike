using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Tile _backgroundTile;
    [SerializeField] private RuleTile _roomTile;

    [SerializeField] private Vector2Int _mapSize;

    [SerializeField] private float _minRate;
    [SerializeField] private float _maxRate;
    [SerializeField] private int _maxDepth;

    public MapNode MapGenerate()
    {
        FillBackgroundTile();
        MapNode root = new MapNode(new RectInt(-_mapSize.x / 2, -_mapSize.y / 2, _mapSize.x, _mapSize.y));
        DivideMap(root, 0);
        GenereteRoom(root, 0);
        GenereteWay(root, 0);

        return root;
    }

    private void DivideMap(MapNode node, int depth)
    {
        if (depth >= _maxDepth) return;

        int maxLength = Mathf.Max(node.NodeRect.width, node.NodeRect.height);
        int split = (int)Random.Range(maxLength * _minRate, maxLength * _maxRate);

        if(node.NodeRect.width > node.NodeRect.height)
        {
            node.LeftNode = new MapNode(new RectInt(node.NodeRect.x, node.NodeRect.y, split, node.NodeRect.height));
            node.RightNode = new MapNode(new RectInt(node.NodeRect.x + split, node.NodeRect.y, node.NodeRect.width - split, node.NodeRect.height));
        }
        else
        {
            node.LeftNode = new MapNode(new RectInt(node.NodeRect.x, node.NodeRect.y, node.NodeRect.width, split));
            node.RightNode = new MapNode(new RectInt(node.NodeRect.x, node.NodeRect.y + split, node.NodeRect.width, node.NodeRect.height - split));
        }

        DivideMap(node.LeftNode, depth + 1);
        DivideMap(node.RightNode, depth + 1);
    }

    private RectInt GenereteRoom(MapNode node, int depth)
    {
        RectInt rect;
        if(depth >= _maxDepth)
        {
            int width = Random.Range(node.NodeRect.width / 2, node.NodeRect.width - 2);
            int height = Random.Range(node.NodeRect.height / 2, node.NodeRect.height - 2);

            int x = node.NodeRect.x + Random.Range(2, node.NodeRect.width - width);
            int y = node.NodeRect.y + Random.Range(2, node.NodeRect.height - height);

            rect = new RectInt(x, y, width, height);
            FillRoomTile(rect);
        }
        else
        {
            node.LeftNode.RoomRect = GenereteRoom(node.LeftNode, depth + 1);
            node.RightNode.RoomRect = GenereteRoom(node.RightNode, depth + 1);
            rect = node.LeftNode.RoomRect;
        }

        return rect;
    }

    private void GenereteWay(MapNode node, int depth)
    {
        if (depth >= _maxDepth) return;

        Vector2 leftCenter = node.LeftNode.RoomRect.center;
        Vector2 rightCenter = node.RightNode.RoomRect.center;

        for(int i = (int)Mathf.Min(leftCenter.x, rightCenter.x) - 1; i <= (int)Mathf.Max(leftCenter.x, rightCenter.x) + 1; i++)
        {
            for(int j = (int)leftCenter.y - 1; j <= (int)leftCenter.y + 1; j++)
            {
                _tilemap.SetTile(new Vector3Int(i, j, 0), _roomTile);
            }
        }

        for (int i = (int)Mathf.Min(leftCenter.y, rightCenter.y) - 1; i <= (int)Mathf.Max(leftCenter.y, rightCenter.y) + 1; i++)
        {
            for (int j = (int)rightCenter.x - 1; j <= (int)rightCenter.x + 1; j++)
            {
                _tilemap.SetTile(new Vector3Int(j, i, 0), _roomTile);
            }
        }

        GenereteWay(node.LeftNode, depth + 1);
        GenereteWay(node.RightNode, depth + 1);
    }

    private void FillBackgroundTile()
    {
        for(int i = -10; i < _mapSize.x + 10; i++)
            for(int j = -10; j < _mapSize.y + 10; j++)
            {
                _tilemap.SetTile(new Vector3Int(i - _mapSize.x / 2, j - _mapSize.y / 2), _backgroundTile);
            }
    }

    private void FillRoomTile(RectInt rect)
    {
        for(int i = rect.x - 1; i <= rect.x + rect.width; i++)
        { 
            for(int j = rect.y - 1; j <= rect.y + rect.height; j++)
            {
                _tilemap.SetTile(new Vector3Int(i, j, 0), _roomTile);
            }
        }
    }
}
