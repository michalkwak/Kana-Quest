using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelectionManager : MonoBehaviour
{
    public static bool isHiraganaSelected;

    public void SelectHiraganaCategory()
    {
        PlayerPrefs.SetInt("IsHiraganaSelected", 1);
    }

    public void SelectKatakanaCategory()
    {
        PlayerPrefs.SetInt("IsHiraganaSelected", 0);
    }
}

