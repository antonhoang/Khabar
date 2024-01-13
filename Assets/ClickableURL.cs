using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableURL : MonoBehaviour, IPointerClickHandler
{
    public enum TextCase
    {
        aboutUs,
        supportUs
    }

    public TextCase textCase;
    public TMP_Text textComponent;
    private string myText;

    private void Start()
    {
        // Оновити текст і зробити URL клікабельним
        if (textCase == TextCase.aboutUs) {
            myText = "Ласкаво просимо до нашої гри! Ми - команда ентузіастів, що працює над цим проектом, і ми раді вас вітати в нашій ігровій спільноті.\n\nДолучайтесь до нашої громади, діліться своїми враженнями та ідеями, і разом ми робимо цей світ більш захопливим. Дякуємо, що обрали нашу гру та подорожуєте разом з нами!\n\nШановні! У мене є колізія:\n";
            myText += "Підпис здесь ";
            myText += "<link=https://www.behance.net/alinamokliak>Behance</link>\n";
            myText += "і підпис здесь ";
            myText += "<link=https://github.com/antonhoang?tab=repositories>GitHub</link>\n";
        }

        if (textCase == TextCase.supportUs)
        {
            myText += "Ваша підтримка надає життєвий вплив грі, я зроблю все можливе, щоб зробити її кращою. Допоможіть нам, кинувши донат через <link=https://www.buymeacoffee.com/5xcnzc9ln0/>Buy Me a Coffee</link> тут. Дякуємо!";
        }

        textComponent.text = myText;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            int index = TMP_TextUtilities.FindIntersectingLink(textComponent, eventData.position, Camera.main);
            if(index >-1)
            {
                Application.OpenURL(textComponent.textInfo.linkInfo[index].GetLinkID());
            }
        }
    }
}