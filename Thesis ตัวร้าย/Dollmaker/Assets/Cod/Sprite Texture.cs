//Attach this script to a Sprite GameObject. Make sure it has a SpriteRenderer component (should have by default)
//Next, attach a second Sprite in the Inspector window of your first Sprite GameObject

using UnityEngine;

public class SpriteTexture : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public Sprite m_Sprite;

    void Start()
    {
        //Fetch the SpriteRenderer of the Sprite
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Output the current Texture of the Sprite (this returns the source Sprite if the Texture isn't packed)
        Debug.Log("Texture 1 : " + m_SpriteRenderer.sprite.texture);
    }

    void Update()
    {
        //Press Space key to change the Sprite to the Sprite you attach in the Inspector
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Change the Sprite
            m_SpriteRenderer.sprite = m_Sprite;
            //Output the Texture of the new Sprite (this returns the source Sprite if the Texture isn't packed)
            Debug.Log("Texture 2 : " + m_SpriteRenderer.sprite.texture);
        }
    }
}