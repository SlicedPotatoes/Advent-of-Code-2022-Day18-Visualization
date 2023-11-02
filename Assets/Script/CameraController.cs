using UnityEngine;

// Classe pour contrôler le mouvement, la rotation et le zoom de la caméra.
public class CameraController : MonoBehaviour
{
    // Vitesse de déplacement de la caméra.
    public float moveSpeed = 5.0f;

    // Vitesse de rotation de la caméra.
    public float rotationSpeed = 2.0f;

    // Vitesse de zoom de la caméra.
    public float zoomSpeed = 5.0f;

    // Transform de la caméra principale.
    private Transform cameraTransform;

    // Indique si le bouton de la souris est enfoncé (utilisé pour faire glisser la caméra).
    private bool isDragging = false;

    // Dernière position de la souris.
    private Vector3 lastMousePosition;

    // Fonction appelée au démarrage du jeu.
    void Start()
    {
        // Obtient le transform de la caméra principale.
        cameraTransform = Camera.main.transform;
    }

    // Fonction appelée à chaque frame.
    void Update()
    {
        // Déplacement de la caméra en maintenant le bouton gauche de la souris et faisant glisser.
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
            // Calcul du mouvement de la caméra en fonction du déplacement de la souris.
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            Vector3 moveDirection = new Vector3(-mouseDelta.x, -mouseDelta.y, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(moveDirection);
        }

        lastMousePosition = Input.mousePosition;

        // Rotation de la caméra avec le bouton droit de la souris.
        if (Input.GetMouseButton(1))
        {
            // Obtient les mouvements de la souris pour la rotation.
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Applique la rotation sur l'axe Y pour la caméra principale.
            transform.Rotate(Vector3.up, mouseX);

            // Applique la rotation sur l'axe X pour le transform de la caméra.
            cameraTransform.Rotate(Vector3.left, mouseY);
        }

        // Zoom avec la molette de la souris.
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        cameraTransform.Translate(Vector3.forward * zoomInput * zoomSpeed);
    }
}