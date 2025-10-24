using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody jogador;
    
    [Header("Bounce Settings")]
    public float bounceForce = 10f; // Força do salto no chão saltitante
    public float bounceInterval = 0.5f; // Tempo entre saltos
    
    private bool isOnBouncySurface = false;
    private float lastBounceTime = 0f;
    private UIManager uiManager;
   
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    
    void Update()
    {
        // Movimento com as setas
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            jogador.AddForce(-2, 0, 0);
        }    
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            jogador.AddForce(2, 0, 0);
        }    
        if (Keyboard.current.upArrowKey.isPressed)
        {
            jogador.AddForce(0, 0, 2);
        }    
        if (Keyboard.current.downArrowKey.isPressed)
        {
            jogador.AddForce(0, 0, -2);
        }
        
        // Sistema de salto contínuo no chão saltitante
        if (isOnBouncySurface && Time.time > lastBounceTime + bounceInterval)
        {
            jogador.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            lastBounceTime = Time.time;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        DetectFloorType(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        DetectFloorType(collision);
    }
    
    private void OnCollisionExit(Collision collision)
    {
        // Para de saltar quando sair do chão saltitante
        PhysicsMaterial physicsMat = collision.collider.material;
        if (physicsMat != null)
        {
            string matName = physicsMat.name.ToLower();
            if (matName.Contains("saltitante") || physicsMat.bounciness > 0.5f)
            {
                isOnBouncySurface = false;
            }
        }
    }

    private void DetectFloorType(Collision collision)
    {
        PhysicsMaterial physicsMat = collision.collider.material;
        if (physicsMat != null)
        {
            string matName = physicsMat.name.ToLower();
            
            if (matName.Contains("gelo") || physicsMat.dynamicFriction < 0.1f)
            {
                if (uiManager != null) uiManager.UpdateMaterialText("Gelo");
                isOnBouncySurface = false;
            }
            else if (matName.Contains("alcatifa") || physicsMat.dynamicFriction > 0.8f)
            {
                if (uiManager != null) uiManager.UpdateMaterialText("Alcatifa");
                isOnBouncySurface = false;
            }
            else if (matName.Contains("saltitante") || physicsMat.bounciness > 0.5f)
            {
                if (uiManager != null) uiManager.UpdateMaterialText("Saltitante");
                isOnBouncySurface = true; // Ativa os saltos automáticos!
            }
        }
    }
}