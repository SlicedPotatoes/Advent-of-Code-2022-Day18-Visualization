using UnityEngine;
using UnityEngine.UI;

// Classe pour gérer le comportement d'un bouton dans l'interface utilisateur Unity.
public class ButtonUI : MonoBehaviour
{
    // Référence au parent des blocs d'air dans la scène Unity.
    public GameObject airBlockParent;

    // Référence au composant texte du bouton.
    public Text buttonText;

    // Variable pour suivre l'état d'affichage des blocs d'air.
    private bool show = true;

    // Chaînes de texte pour le bouton.
    private string showString = "Show Air Block";
    private string removeString = "Remove Air Block";

    // Fonction appelée au démarrage de la scène Unity.
    void Start()
    {
        // Initialise le texte du bouton avec la chaîne "Remove Air Block".
        buttonText.text = removeString;
    }

    // Fonction appelée lorsqu'on clique sur le bouton.
    public void clickBtn()
    {
        // Inverse l'état d'affichage des blocs d'air.
        show = !show;

        // Active ou désactive le parent des blocs d'air en fonction de l'état d'affichage.
        airBlockParent.SetActive(show);

        // Change le texte du bouton en fonction de l'état d'affichage.
        buttonText.text = show ? removeString : showString;
    }
}
