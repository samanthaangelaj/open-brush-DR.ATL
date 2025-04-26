using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TiltBrush;

public class TextNextDRATLButton : BaseButton
{
    [SerializeField] private TextMeshPro textObject;
    [SerializeField] private Instruccion instruccionSeleccionada;

    private enum Instruccion
    {
        Next,
        Previous,
        Obra1,
        Obra2
    }

    // Instructions for both artworks
    private string[] instructionsObra1 = new string[]
    {
        "Paso 1:\nDibuja los valles con los árboles.",
        "Paso 2:\nDibuja el lago y el volcán en el centro.",
        "Paso 3:\nDibuja el fondo del cielo azul.",
        "Paso 4:\nDibuja las nubes.",
        "Paso 5:\nDibuja lluvia en algunas nubes.",
        "Paso 6:\n¡Estás listo con tu recreación de la obra! ¡Camina en ella y disfrútala!"
    };

    private string[] instructionsObra2 = new string[]
    {
        "Paso 1:\nDibuja los valles y el volcán (sin el fuego y ceniza).",
        "Paso 2:\nDibuja el cielo con estrellas.",
        "Paso 3:\nDibuja la lava, el fuego y la ceniza.",
        "Paso 4:\nDibuja los árboles.",
        "Paso 5:\n¡Estás listo con tu recreación de la obra! ¡Camina en ella y disfrútala!"
    };

    // UI references
    [SerializeField] private GameObject obra1;
    [SerializeField] private GameObject obra2;
    [SerializeField] private GameObject btnNext;
    [SerializeField] private GameObject btnPrev;

    private Image guideImage;
    [SerializeField] private Sprite[] guideStepsObra1;
    [SerializeField] private Sprite[] guideStepsObra2;

    void Start()
    {
        GameObject canvas = GameObject.Find("UICanvas");
        if (canvas != null)
        {
            Transform guideTransform = canvas.transform.Find("GuideImage");
            if (guideTransform != null)
            {
                guideImage = guideTransform.GetComponent<Image>();
                Debug.Log("GuideImage found inside UICanvas.");
            }
            else
            {
                Debug.LogError("ERROR: GuideImage not found inside UICanvas.");
            }
        }
        else
        {
            Debug.LogError("ERROR: UICanvas not found in the scene.");
        }
    }

    override protected void OnButtonPressed()
    {
        Debug.Log("Botón presionado: " + instruccionSeleccionada);

        switch (instruccionSeleccionada)
        {
            case Instruccion.Obra1:
                SelectObra(instructionsObra1, guideStepsObra1);
                break;
            case Instruccion.Obra2:
                SelectObra(instructionsObra2, guideStepsObra2);
                break;
            case Instruccion.Next:
                GoNext();
                break;
            case Instruccion.Previous:
                GoPrevious();
                break;
        }
    }

    private void SelectObra(string[] instructions, Sprite[] guideSteps)
    {
        Debug.Log($"Seleccionando obra: {(instructions == instructionsObra1 ? "Obra 1" : "Obra 2")}");

        // Set instructions in InstructionManager
        InstructionManagerDRAtl.Instance.SetInstructions(instructions, guideSteps);

        // Update UI
        obra1.SetActive(false);
        obra2.SetActive(false);
        btnNext.SetActive(true);
        btnPrev.SetActive(true); // Now always visible, even on Step 1

        UpdateTextAndGuide();
    }

    private void GoNext()
    {
        InstructionManagerDRAtl.Instance.NextStep();
        UpdateTextAndGuide();
    }

    private void GoPrevious()
    {
        if (InstructionManagerDRAtl.Instance.CurrentIndex == 0)
        {
            // If on Step 1, return to artwork selection
            ResetToSelection();
        }
        else
        {
            InstructionManagerDRAtl.Instance.PreviousStep();
            UpdateTextAndGuide();
        }
    }

    private void UpdateTextAndGuide()
    {
        var instructions = InstructionManagerDRAtl.Instance.CurrentInstructions;
        var index = InstructionManagerDRAtl.Instance.CurrentIndex;
        var guideSteps = InstructionManagerDRAtl.Instance.CurrentGuideSteps;

        if (instructions == null || instructions.Length == 0)
        {
            Debug.LogError("ERROR: No hay instrucciones.");
            return;
        }

        textObject.text = instructions[index];
        Debug.Log($"Mostrando paso {index + 1} de {instructions.Length}");

        // Update guide image
        if (guideImage != null && guideSteps != null && index < guideSteps.Length)
        {
            guideImage.sprite = guideSteps[index];
            guideImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            guideImage.color = new Color(1, 1, 1, 0);
        }

        // Control button visibility
        btnNext.SetActive(index < instructions.Length - 1);
        btnPrev.SetActive(true); // Always visible, even on Step 1
    }

    private void ResetToSelection()
    {
        Debug.Log("Regresando a la selección de obras");

        // Reset UI to initial artwork selection
        textObject.text = "Selecciona una obra para recrear:";
        obra1.SetActive(true);
        obra2.SetActive(true);
        btnNext.SetActive(false);
        btnPrev.SetActive(false); // Hide Prev when returning to selection

        if (guideImage != null)
        {
            guideImage.color = new Color(1, 1, 1, 0);
        }
    }

    override public void GainFocus()
    {
        base.GainFocus();
        ForceDescriptionDeactivate();
    }
}