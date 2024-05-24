using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataColor", menuName = "ScriptableObjects/DataColor", order = 1)]
public class DataColor : ScriptableObject
{
    public List<Color> colors;

    public Color GetColor(EColor color)
    {
        return colors[((int)color)];
    }

    public List<EColor> GetRandomEnumColors(int numberOfColors)
    {
        // Lấy tất cả các giá trị của enum, loại bỏ giá trị None
        List<EColor> colorValues = new List<EColor>((EColor[])Enum.GetValues(typeof(EColor)));
        colorValues.Remove(EColor.None);

        List<EColor> selectedColors = new List<EColor>();
        System.Random random = new System.Random();

        while (selectedColors.Count < numberOfColors && colorValues.Count > 0)
        {
            int randomIndex = random.Next(0, colorValues.Count);

            // Chọn màu ngẫu nhiên và thêm vào danh sách
            selectedColors.Add(colorValues[randomIndex]);
            // Loại bỏ màu đã chọn để không chọn lại
            colorValues.RemoveAt(randomIndex);
        }

        return selectedColors;
    }



}
