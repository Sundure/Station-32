
public struct CustomMathf
{
    /// <summary>
    /// Get Precent Betwen First And Second Value.
    /// </summary>
    /// <param name="firstValue"></param>
    /// <param name="secondValue"></param>
    /// <returns></returns>
    public static float GetPrecent(float firstValue, float secondValue) => (firstValue / secondValue) * 100;

    /// <summary>
    /// Get Precent Betwen First And Second Value.
    /// </summary>
    /// <param name="firstValue"></param>
    /// <param name="secondValue"></param>
    /// <returns></returns>
    public static float GetPrecent(float firstValue, float secondValue, float minSecondValue)
    {
        if (secondValue <= minSecondValue) secondValue = minSecondValue;

        return (firstValue / secondValue) * 100;
    }

    /// <summary>
    /// Get Precent Betwen First And Second Value.
    /// If First Value More That Second Returns Precent Betwen Second And First Value
    /// </summary>
    /// <param name="firstValue"></param>
    /// <param name="secondValue"></param>
    /// <returns></returns>
    public static float GetAdaptivePrecent(float firstValue, float secondValue)
    {
        if (firstValue > secondValue)
            return (secondValue / firstValue) * 100;

        return (firstValue / secondValue) * 100;
    }

    /// <summary>
    /// Get Precent Betwen First And Second Value.
    /// If First Value More That Second Returns Precent Betwen Second And First Value
    /// </summary>
    /// <param name="firstValue"></param>
    /// <param name="secondValue"></param>
    /// <returns></returns>
    public static float GetAdaptivePrecent(float firstValue, float secondValue, float minSecondValue)
    {
        if (secondValue <= minSecondValue) secondValue = minSecondValue;

        if (firstValue > secondValue)
            return (secondValue / firstValue) * 100;

        return (firstValue / secondValue) * 100;
    }
}
