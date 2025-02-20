using UnityEngine;
using UnityEngine.UI;

public class Customizer : MonoBehaviour
{
    public void ChangeHairTexture(Player player, UIManager uiManager, int itemNum)
    {
        // Player ��ü���� P_Hair Renderer�� ã��
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

        // UIImage���� Sprite�� �����ͼ� Texture�� ����
        Sprite newSprite = shopHairImage.sprite;
        if (newSprite != null)
        {
            playerHairRenderer.material.mainTexture = newSprite.texture;  // P_Hair�� �������� ���ο� �ؽ�ó �Ҵ�
        }
        else
        {
            Debug.LogError("No sprite found in the UIImage!");
        }
    }
}
