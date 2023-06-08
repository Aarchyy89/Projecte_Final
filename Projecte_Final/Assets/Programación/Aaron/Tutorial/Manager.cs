using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject alcalde;

    public static Manager instance;

    [Header("---Dialog panels---")]
    [SerializeField] private GameObject _1;
    [SerializeField] private GameObject _2;
    [SerializeField] private GameObject _3;
    [SerializeField] private GameObject _4;
    [SerializeField] private GameObject _5;
    [SerializeField] private GameObject _6;
    [SerializeField] private GameObject _7;
    [SerializeField] private GameObject _8;

    [Header("---pointer arrows---")]
    [SerializeField] private Image _A1;
    [SerializeField] private Image _A2;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void D_1()
    {
        _1.SetActive(false);
        _2.SetActive(true);

        _A1.gameObject.SetActive(true);
    }

    public void D_2()
    {
        _2.SetActive(false);
        _3.SetActive(true);

        _A2.gameObject.SetActive(true);
        _A1.gameObject.SetActive(false);
    }

    public void D_3()
    {
        _A2.gameObject.SetActive(false);
        anim.SetTrigger("Bye");
    }

    public void D_4()
    {
        Destroy(alcalde);
        _3.SetActive(false);
        _4.SetActive(true);
    }

    public void D_5()
    {
        _4.SetActive(false);
        _5.SetActive(true);
    }

    public void D_6()
    {
        _5.SetActive(false);
        _6.SetActive(true);
    }

    public void D_7()
    {
        _6.SetActive(false);
        _7.SetActive(true);
    }

}
