using UnityEngine;

public static class AvaiableShape
{
    private static int[][,] shapes = new int[][,]
    {
        new int[,]
        {
            {0, 0, 1},
            {0, 1, 1},
            {1, 1, 1}
        },
        new int[,]
        {
            {1, 0, 0},
            {1, 1, 0},
            {1, 1, 1}
        },
        new int[,]
        {
            {0, 0, 0},
            {1, 1, 1},
            {0, 0, 0}
        },
        new int[,]
        {
            {0, 1, 0},
            {0, 1, 0},
            {0, 1, 0}
        },
        new int[,]
        {
            {0, 1, 1},
            {0, 1, 1},
            {0, 0, 0}
        },
        new int[,]
        {
            {1, 1, 1},
            {1, 1, 1},
            {1, 1, 1}
        },
        new int[,]
        {
            {0, 0, 0},
            {1, 1, 0},
            {0, 0, 0}
        },
        new int[,]
        {
            {0, 0, 1},
            {0, 1, 0},
            {0, 0, 0}
        },
        new int[,]
        {
            {1, 0, 0},
            {0, 1, 0},
            {0, 0, 0}
        },
        new int[,]
        {
            {1, 0, 0},
            {0, 1, 0},
            {0, 0, 1}
        },
        new int[,]
        {
            {0, 0, 1},
            {0, 1, 0},
            {1, 0, 0}
        },
        new int[,]
        {
            {0, 0, 1},
            {1, 1, 1},
            {1, 0, 0}
        },
        new int[,]
        {
            {1, 0, 0},
            {1, 1, 1},
            {0, 0, 1}
        },
        new int[,]
        {
            {0, 1, 0},
            {0, 1, 0},
            {0, 1, 1}
        },
        new int[,]
        {
            {0, 1, 0},
            {0, 1, 0},
            {1, 1, 0}
        },
        new int[,]
        {
            {0, 1, 0},
            {0, 1, 0},
            {1, 1, 1}
        },
        new int[,]
        {
            {1, 1, 1},
            {0, 1, 0},
            {0, 1, 0}
        },
        new int[,]
        {
            {1, 1, 1},
            {0, 1, 0},
            {0, 0, 0}
        },
        new int[,]
        {
            {0, 0, 0},
            {0, 1, 0},
            {1, 1, 1}
        },
        new int[,]
        {
            {0, 0, 0},
            {0, 1, 0},
            {0, 0, 0}
        },
        new int[,]
        {
            {0, 1, 0},
            {0, 1, 0},
            {0, 0, 0}
        },
        new int[,]
        {
            {0, 0, 0},
            {0, 1, 1},
            {0, 0, 0}
        },
    };

    public static int[,] GetShape(int index)
    {
        return shapes[index];
    }

    public static int GetShapeCount()
    {
        return shapes.Length;
    }
}
