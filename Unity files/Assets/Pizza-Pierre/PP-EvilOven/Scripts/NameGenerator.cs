using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour{

    private enum firstName {Aleksandr, Dimitrij, Andrej, Artjom, Alexej, Michail, Igor, Wladimir, Sergej, Anastasija, Olga, Natascha, Nadja, Iwan, Pjotr, Wladislaw, Mischa, Nikolaj, Vasiliy, Oleg, EnumLength};
    private enum lastName {Tomatjonow, Bryokkolow, Ananadjow, Frjuchtow };
    private string fullName;
	// Use this for initialization
	void Start () {

    }

    public string GenerateName(string whichFruit)
    {
        fullName = ((firstName)Random.Range(0, (int)firstName.EnumLength)).ToString() + " ";
        switch (whichFruit)
        {
            case "Tomate": fullName += (lastName.Tomatjonow).ToString();
                break;
            case "Brokkoli": fullName += (lastName.Bryokkolow).ToString();
                break;
            case "Ananas": fullName += (lastName.Ananadjow).ToString();
                break;
            default: fullName += (lastName.Frjuchtow).ToString();
                break;
        }
        return fullName;
    }
}
