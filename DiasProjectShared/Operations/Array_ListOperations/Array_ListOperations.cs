using System;

namespace DiasShared.Operations.ArrayListOperations
{
    public static class Array_ListOperations
    {
        /// <summary>
        /// Remove a cell from Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr">Array</param>
        /// <param name="index">Removed cell index</param>
        public static void RemoveAtArray<T>(ref T[] arr, int index)
        {
            for (int a = index; a < arr.Length - 1; a++)
            {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);
        }
    }
}
