namespace kesera2.FBXOptionsManager
{
    internal static class FBXOptionOptimizerUtility
    {
        internal static bool[] toggleArrayChecks(bool[] array, bool condition)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = condition;
            }

            return array;
        }
    }

}