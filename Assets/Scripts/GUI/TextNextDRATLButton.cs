using System.Collections;
using System.Collections.Generic;
using TiltBrush;
using TMPro;
using UnityEngine;

public class TextNextDRATLButton : BaseButton
{
    [SerializeField] private TextMeshPro textObject; // Objeto de texto a modificar
    [SerializeField] private Instruccion instruccionSeleccionada;

    private enum Instruccion
    {
        Next,
        Previous,
        Obra1,
        Obra2
    }

    private string[] instructionsObra1 = new string[]
    {
        "Paso 1:\nDibuja los valles con los arboles.",
        "Paso 2:\nDibuja el lago y el volcano el centro.",
        "Paso 3:\nDibuja el fondo del cielo azul.",
        "Paso 4:\nDibuja las nubes",
        "Paso 5:\nDibuja lluvia en algunas nubes.",
        "Paso 6:\n¡Estás listo con tu recreacción de la obra! ¡Camina en ella y disfrutala!"
    };

    private string[] instructionsObra2 = new string[]
    {
        "Paso 1:\nDibuja los valles y el volcán (sin el fuego y ceniza).",
        "Paso 2:\nDibuja el cielo con estrellas. ",
        "Paso 3:\nDibuja la lava, el fuego y la ceniza.",
        "Paso 4:\nDibuja los arboles.",
        "Paso 5:\n¡Estás listo con tu recreacción de la obra! ¡Camina en ella y disfrutala!"
    };
    private int currentIndex = 0; // Índice actual

    [SerializeField] private GameObject obra1;
    [SerializeField] private GameObject obra2;
    [SerializeField] private GameObject btnNext;
    [SerializeField] private GameObject btnPrev;

    private string[] currentInstructions; // Referencia al array de la obra seleccionada

    override protected void OnButtonPressed()
    {
        switch (instruccionSeleccionada)
        {
            case Instruccion.Obra1:
                SelectObra(instructionsObra1);
                break;

            case Instruccion.Obra2:
                SelectObra(instructionsObra2);
                break;

            case Instruccion.Next:
                if (currentIndex < currentInstructions.Length - 1)
                {
                    currentIndex++;
                    textObject.text = currentInstructions[currentIndex];
                    UpdateButtons();
                }
                break;

            case Instruccion.Previous:
                if (currentIndex > 0)
                {
                    currentIndex--;
                    textObject.text = currentInstructions[currentIndex];
                    UpdateButtons();
                }
                else
                {
                    // Si estamos en el primer índice, regresar a la selección de obras
                    obra1.SetActive(true);
                    obra2.SetActive(true);
                    btnNext.SetActive(false);
                    btnPrev.SetActive(false);
                }
                break;
        }
    }

    private void SelectObra(string[] instructions)
    {
        currentInstructions = instructions;
        currentIndex = 0; // Reiniciar índice

        obra1.SetActive(false);
        obra2.SetActive(false);
        btnNext.SetActive(true);
        btnPrev.SetActive(true);

        textObject.text = currentInstructions[currentIndex];

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        // Desactivar "Next" si se llegó al final del array
        btnNext.SetActive(currentIndex < currentInstructions.Length - 1);

        // "Prev" siempre debe estar visible para retroceder, pero si estamos en 0, hace la acción de volver a la selección de obras
        btnPrev.SetActive(true);
    }
    override public void GainFocus()
    {
        base.GainFocus();
        ForceDescriptionDeactivate();
    }
}
