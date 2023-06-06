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

    [Header("---pointer arrows---")]
    [SerializeField] private Image _A1;
    [SerializeField] private Image _A2;
    [SerializeField] private Image _A3;



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

        _A1.gameObject.SetActive(false);
    }

    public void D_3()
    {
        anim.SetTrigger("Bye");
        Destroy(alcalde, 2f);
    }

    public void D_4()
    {
        _3.SetActive(false);
        _4.SetActive(true);
    }

}
