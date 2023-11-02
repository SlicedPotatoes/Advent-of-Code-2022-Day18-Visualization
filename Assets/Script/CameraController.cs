using UnityEngine;

// Classe pour contr�ler le mouvement, la rotation et le zoom de la cam�ra.
public class CameraController : MonoBehaviour
{
    // Vitesse de d�placement de la cam�ra.
    public float moveSpeed = 5.0f;

    // Vitesse de rotation de la cam�ra.
    public float rotationSpeed = 2.0f;

    // Vitesse de zoom de la cam�ra.
    public float zoomSpeed = 5.0f;

    // Transform de la cam�ra principale.
    private Transform cameraTransform;

    // Indique si le bouton de la souris est enfonc� (utilis� pour faire glisser la cam�ra).
    private bool isDragging = false;

    // Derni�re position de la souris.
    private Vector3 lastMousePosition;

    // Fonction appel�e au d�marrage du jeu.
    void Start()
    {
        // Obtient le transform de la cam�ra principale.
        cameraTransform = Camera.main.transform;
    }

    // Fonction appel�e � chaque frame.
    void Update()
    {
        // D�placement de la cam�ra en maintenant le bouton gauche de la souris et faisant glisser.
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // Calcul du mouvement de la cam�ra en fonction du d�placement de la souris.
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            Vector3 moveDirection = new Vector3(-mouseDelta.x, -mouseDelta.y, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(moveDirection);
        }

        lastMousePosition = Input.mousePosition;

        // Rotation de la cam�ra avec le bouton droit de la souris.
        if (Input.GetMouseButton(1))
        {
            // Obtient les mouvements de la souris pour la rotation.
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Applique la rotation sur l'axe Y pour la cam�ra principale.
            transform.Rotate(Vector3.up, mouseX);

            // Applique la rotation sur l'axe X pour le transform de la cam�ra.
            cameraTransform.Rotate(Vector3.left, mouseY);
        }

        // Zoom avec la molette de la souris.
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        cameraTransform.Translate(Vector3.forward * zoomInput * zoomSpeed);
    }
}