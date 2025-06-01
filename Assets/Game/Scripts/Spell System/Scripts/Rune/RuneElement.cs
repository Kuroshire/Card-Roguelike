using System;

public enum RuneElement {
    FIRE, WATER, AIR, EARTH
}

public class RuneElementExtension
{
    public static RuneElement GetOneRandomElement()
    {
        Array values = Enum.GetValues(typeof(RuneElement));
        Random random = new();
        RuneElement randomElement = (RuneElement)values.GetValue(random.Next(values.Length));

        return randomElement;
    }
}