using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text materialText;  // Referência ao texto do Canvas

    private void Start()
    {
        // Limpa o texto no início
        if (materialText != null)
        {
            materialText.text = "";
        }
    }

    // Função que atualiza o texto conforme o tipo de chão
    public void UpdateMaterialText(string materialName)
    {
        if (materialText == null) return;

        // Atualiza o texto
        materialText.text = materialName;

        // Muda a cor do texto conforme o tipo de chão
    
    }
}