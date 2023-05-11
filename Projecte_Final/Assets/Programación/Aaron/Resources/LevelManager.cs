using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TMP_Text text_stone;
    public TMP_Text text_wood;

    public int max_wood;
    public int max_stone;
    public float current_wood;
    public int current_stone;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Resources_Controller.instance.IncreaseMadera();
    }



}
