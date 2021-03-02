using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Binding : MonoBehaviour, IBindable
{
    public IBinding binding { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string bindingPath { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public int myInt { get; set; }

    public SliderInt mySlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
