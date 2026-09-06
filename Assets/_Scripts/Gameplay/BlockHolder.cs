using System.Collections.Generic;
using UnityEngine;

public class BlockHolder : MonoBehaviour
{
    [SerializeField] private List<Block> blocks = new();
    [SerializeField] private Board board;

    private void Awake()
    {
        DistanceBlocks();    
    }

    private void DistanceBlocks()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].transform.localPosition = new Vector3(-2.3f + 2.3f * i, 0, 0);
            blocks[i].Init(board);
        }

        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].Show();
        }
    }
}
