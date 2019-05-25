using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    public static float Map(float value, float input_start, float input_end, float output_start, float output_end)
    {
        float output = output_start + ((output_end - output_start) / (input_end - input_start)) * (value - input_start);
        return output;//(value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
