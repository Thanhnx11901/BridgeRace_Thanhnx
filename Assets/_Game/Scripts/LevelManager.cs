using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerCtl playerCtl;
    public DataColor dataColor;
    public List<EColor> colors = new List<EColor>();

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        colors = dataColor.GetRandomEnumColors(4);
    }
    private void Start()
    {
        playerCtl.SetColor(colors[0]);
    }
}
