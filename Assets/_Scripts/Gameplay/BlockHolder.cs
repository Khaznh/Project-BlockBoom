using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockHolder : MonoBehaviour
{
    [SerializeField] private List<Block> blocks = new();
    [SerializeField] private Board board;

    [Header("Init data")]
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int blockMaxCount = 3;

    private void Awake()
    {
        HandleBlockLogic();
    }

    private void Update()
    {
        if (blocks != null && blocks.Count == 0)
        {
            HandleBlockLogic();
        }
    }

    public void DespawnBlock(Block block)
    {
        blocks.Remove(block);
        Destroy(block.gameObject);
    }

    private void HandleBlockLogic()
    {
        SpawnBlocks();
        DistanceBlocks();
    }

    private void DistanceBlocks()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].transform.localPosition = new Vector3(-2.3f + 2.3f * i, 0, 0);
            blocks[i].Init(board, this);
        }

        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].Show();
            blocks[i].GetComponent<Block>().SetSizeCell(BlockStatus.NotHold);
        }
    }

    private void SpawnBlocks()
    {
        for (int i = 0; i < blockMaxCount; i++)
        {
            GameObject blockIns = Instantiate(blockPrefab, transform);
            blocks.Add(blockIns.GetComponent<Block>());

            int randomIndexColor = Enum.GetValues(typeof(CellColor)).Length;
            blocks[i].blockColor = (CellColor)UnityEngine.Random.Range(0, randomIndexColor);
        }
    }
}
