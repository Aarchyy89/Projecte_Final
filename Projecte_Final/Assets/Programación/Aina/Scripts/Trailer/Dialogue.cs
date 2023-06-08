using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private TMP_Text _dialogue;
    [SerializeField] private List<string> _texts;
    [SerializeField] private int index;

    private void Start()
    {
        _dialogue = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        _dialogue.text = _texts[index];
    }

    public void NextTxt()
    {
        ++index;
        Debug.Log(index);
        _dialogue.text = _texts[index];
    }
}
