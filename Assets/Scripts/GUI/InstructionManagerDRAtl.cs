using UnityEngine;
using System.Collections.Generic;
using System.Linq;


    public class InstructionManagerDRAtl : MonoBehaviour
    {

        static public InstructionManagerDRAtl Instance;
        //public static InstructionManagerDRAtl Instance { get; private set; }

        // Estas propiedades exponen el estado actual de las instrucciones
        public string[] CurrentInstructions { get; private set; }
        public Sprite[] CurrentGuideSteps { get; private set; }
        public int CurrentIndex { get; private set; }

        // Se llama al iniciar el GameObject donde está este script
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // Si NO necesitas que persista entre escenas, puedes omitir esto:
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Asigna las instrucciones y el índice inicial.
        /// </summary>
        public void SetInstructions(string[] instructions, Sprite[] guideSteps)
        {
            if (instructions == null || instructions.Length == 0)
            {
                Debug.LogError("ERROR: Instrucciones nulas o vacías.");
                return;
            }

            CurrentInstructions = instructions;
            CurrentGuideSteps = guideSteps;
            CurrentIndex = 0;
        }

        /// <summary>
        /// Avanza al siguiente paso, si existe.
        /// </summary>
        public void NextStep()
        {
            if (CurrentInstructions == null || CurrentInstructions.Length == 0)
                return;

            if (CurrentIndex < CurrentInstructions.Length - 1)
                CurrentIndex++;
        }

        /// <summary>
        /// Regresa al paso anterior, si existe.
        /// </summary>
        public void PreviousStep()
        {
            if (CurrentInstructions == null || CurrentInstructions.Length == 0)
                return;

            if (CurrentIndex > 0)
                CurrentIndex--;
        }
    }
