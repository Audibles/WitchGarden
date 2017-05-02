using UnityEngine;

public class SpriteOutline : MonoBehaviour {
    public Color color = Color.white;
    public bool activate;

    [Range(0, 16)]
    public int outlineSize = 1;

    private SpriteRenderer spriteRenderer;

    void OnEnable() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        activate = false;
    }

    void OnDisable() {
        UpdateOutline(false);
    }

    public void setActivate(bool x) {
        if (x)
        {
            activate = true;
        }
        else {
            activate = false;
        }
    }

    void Update() {
        if (activate)
        {
            UpdateOutline(true);
        }
        else if (!activate) {
            OnDisable();
        }
    }

    void UpdateOutline(bool outline) {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
