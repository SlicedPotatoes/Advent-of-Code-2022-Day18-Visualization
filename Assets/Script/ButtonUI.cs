using UnityEngine;
using UnityEngine.UI;

// Classe pour g�rer le comportement d'un bouton dans l'interface utilisateur Unity.
public class ButtonUI : MonoBehaviour
{
    // R�f�rence au parent des blocs d'air dans la sc�ne Unity.
    public GameObject airBlockParent;

    // R�f�rence au composant texte du bouton.
    public Text buttonText;

    // Variable pour suivre l'�tat d'affichage des blocs d'air.
    private bool show = true;

    // Cha�nes de texte pour le bouton.
    private string showString = "Show Air Block";
    private string removeString = "Remove Air Block";

    // Fonction appel�e au d�marrage de la sc�ne Unity.
    void Start()
    {
        // Initialise le texte du bouton avec la cha�ne "Remove Air Block".
        buttonText.text = removeString;
    }

    // Fonction appel�e lorsqu'on clique sur le bouton.
    public void clickBtn()
    {
        // Inverse l'�tat d'affichage des blocs d'air.
        show = !show;

        // Active ou d�sactive le parent des blocs d'air en fonction de l'�tat d'affichage.
        airBlockParent.SetActive(show);

        // Change le texte du bouton en fonction de l'�tat d'affichage.
        buttonText.text = show ? removeString : showString;
    }
}
