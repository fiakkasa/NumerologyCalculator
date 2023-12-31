﻿using System;

namespace NumerologyCalculator.Extensions;

public static class Extensions
{
    private const int _charCodeDelta = 48;

    public static int ToDeltaInt(this char c) => c - _charCodeDelta;

    public static int[] ToDeltaIntCollectionSequence(this string text)
    {
        if (text.Length == 0) return Array.Empty<int>();

        var result = new int[text.Length];

        for (var i = 0; i < text.Length; i++)
            result[i] = text[i].ToDeltaInt();

        return result;
    }

    public static string ToSumString(this int[] collection)
    {
        var result = 0L;

        for (int i = 0; i < collection.Length; i++)
            result += collection[i];

        return result.ToString();
    }
}

