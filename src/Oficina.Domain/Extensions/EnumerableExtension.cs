namespace System;

public static class EnumerableExtension
{
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection)
            action(item);
    }

    public static async ValueTask ForEachAsync<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection)
            action(item);

        await ValueTask.CompletedTask;
    }
}