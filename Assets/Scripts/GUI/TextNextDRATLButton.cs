using System.Collections;
using System.Collections.Generic;
using TiltBrush;
using TMPro;
using UnityEngine;

public class TextNextDRATLButton : BaseButton
{
    [SerializeField] private TextMeshPro textObject; // Objeto de texto a modificar
    [SerializeField] private string[] instructions; // Array con todas las instrucciones
    private int currentIndex = 0; // Índice actual

    override protected void OnButtonPressed()
    {
        if (instructions.Length == 0 || textObject == null) return;

        // Avanzar en la lista de instrucciones
        currentIndex++;
        if (currentIndex >= instructions.Length)
        {
            currentIndex = 0; // Reiniciar al inicio si llega al final
        }

        // Cambiar el texto del objeto
        textObject.text = instructions[currentIndex];
    }
}
