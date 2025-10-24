using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;  // O cubo jogador
    public Vector3 offset = new Vector3(0f, 5f, -10f);  // Distância da câmera ao cubo
    public float smoothSpeed = 0.125f;  // Suavidade do movimento

    [Header("Look At Settings")]
    public bool lookAtTarget = true;  // Se a câmera deve olhar para o cubo

    private void LateUpdate()
    {
        // Verifica se há um alvo definido
        if (target == null) return;

        // Calcula a posição desejada (posição do cubo + offset)
        Vector3 desiredPosition = target.position + offset;

        // Suaviza o movimento da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Faz a câmera olhar para o cubo
        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}