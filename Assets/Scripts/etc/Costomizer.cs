using UnityEngine;
using UnityEngine.UI;

public class Customizer : MonoBehaviour
{
    public void ChangeHairTexture(Player player, UIManager uiManager, int itemNum)
    {
        // Player 객체에서 P_Hair Renderer를 찾기
        SpriteRenderer playerHairRenderer = player.transform.Find("PlayerRender/UnitRoot/Root/BodySet/P_Body/HeadSet/P_Head/P_Hair").GetComponent<SpriteRenderer>();

        if (playerHairRenderer == null)
        {
            Debug.LogError("P_Hair Renderer not found!");
            return;
        }

        string hairImagePath = $"ShopUI/Hair_{itemNum}";  
        Image shopHairImage = uiManager.transform.Find(hairImagePath).GetComponent<Image>();

        if (shopHairImage == null)
        {
            Debug.LogError($"Hair image at {hairImagePath} not found!");
            return;
        }

        // UIImage에서 Sprite를 가져와서 Texture를 추출
        Sprite newSprite = shopHairImage.sprite;
        if (newSprite != null)
        {
            playerHairRenderer.material.mainTexture = newSprite.texture;  // P_Hair의 렌더러에 새로운 텍스처 할당
        }
        else
        {
            Debug.LogError("No sprite found in the UIImage!");
        }
    }
}
