using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleChanger : MonoBehaviour
{
    public void SetScale(float newScale){
        Time.timeScale = newScale;
    }
}
