using System;
using System.Collections.Generic;
using System.Linq;

public static class DataExtensions
{
    // comment
    private static readonly Random Random = new Random();

    public static void Shuffle<T>(this Queue<T> queue)
    {
        var values = queue.ToArray();
        queue.Clear();
        foreach (T value in values.OrderBy(x => Random.Next()))
            queue.Enqueue(value);
    }
}