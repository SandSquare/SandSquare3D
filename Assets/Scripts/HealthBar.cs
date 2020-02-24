using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField, Tooltip("Väri, kun hit pointit ovat täysissä")]
    private Color fullColor;

    [SerializeField, Tooltip("Väri, kun hit pointit ovat minimissä")]
    private Color emptyColor;

    [SerializeField]
    [Tooltip("Läpinäkyvyys: 0 - täysin läpinäkyvä, 1 - läpinäkymätön")]
    private float alpha = 1f;

    private Image image;
    private float maxWidth;
    private Gradient gradient;

    public Player Owner
    {
        get;
        private set;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        // Ei periydy MonoBehaviour:sta, voidaan luoda new:llä
        gradient = new Gradient();

        // Gradientin värit (alku- ja loppuväri)
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        // Gradientin läpinäkyvyysarvot (alku- ja loppuarvot)
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

        // Alustetaan alkuarvot
        colorKeys[0].color = emptyColor;
        colorKeys[0].time = 0; // Tai prosenttiosuus hit pointeista
        alphaKeys[0].alpha = 1; // Läpinäkymätön
        alphaKeys[0].time = 0;

        // Loppuarvot
        colorKeys[1].color = fullColor;
        colorKeys[1].time = 1;
        alphaKeys[1].alpha = 1;
        alphaKeys[1].time = 1;

        gradient.SetKeys(colorKeys, alphaKeys);

        // Maksimileveys on kuvan koko olion alustushetkellä
        // Luetaan kuvan rectTransformilta (sizeDelta kertoo kuvan koon)
        maxWidth = image.rectTransform.sizeDelta.x;

        // Etsii UnitBase-tyyppisen olion tämän olion vanhemmista
        Owner = this.GetComponentInParent<Player>();
    }

    private void Update()
    {
        float healthPercent =
            Owner.Health.CurrentHealth / (float)Owner.Health.MaxHealth;

        // Otetaan nykyinen koko talteen, jottemme muuta health barin
        // korkeutta.
        Vector2 size = image.rectTransform.sizeDelta;
        size.x = maxWidth * healthPercent;
        image.rectTransform.sizeDelta = size;

        // Asettaa värin Gradient-olion avulla
        //Color color = gradient.Evaluate(healthPercent);
        //image.color = color;

        // Väri voidaan myös laskea itse eikä silloin tarvita
        // Gradient-oliota apuna
        Color fullPortion = healthPercent * fullColor;
        Color emptyPortion = (1 - healthPercent) * emptyColor;
        Color currentColor = fullPortion + emptyPortion;
        currentColor.a = alpha;
        image.color = currentColor;
    }
}
