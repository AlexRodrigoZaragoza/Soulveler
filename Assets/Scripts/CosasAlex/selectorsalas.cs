using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class selectorsalas : MonoBehaviour
{
    public List<GameObject> Boss1Cards = new List<GameObject>();
    List<int> totalcards = new List<int>();
    string level="one";
    public List<int> pickedcards = new List<int>();
    Vector3 pos;
    bool rep_heal,rep_shop;
    GameObject cartaActual;
    public static List<int> Hard_rooms = new List<int>();

    public static List<GameObject> posicionesocupadas = new List<GameObject>();
    int noRepetirDificil=0;
    int norepetirTimer;

    List<int> SalasRandom = new List<int>();
    void Awake()
    {
        // A TESTEAR
        GM.salas_ordenadas.Clear();
        posicionesocupadas.Clear();
        Hard_rooms.Clear();
        pickedcards.Clear();
        SalasRandom.Clear();
        GM.salas_recorridas = 0;

        for (int i = 0; i <= 9; i++)
        {
            SalasRandom.Add(i);
        }
        
        if (level == "one")//Dependiendo del nivel que cambie el ratio de cartas
        {
            totalcards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,12 };

            for (int i = 0; i < 7; i++)
            {
                int rand = Random.Range(0, totalcards.Count);
                pickedcards.Add(totalcards[rand]);
                totalcards.RemoveAt(rand);

                if (i == 0)//CAMBIAR PARA AJUSTAR A CANVAS
                {
                    pos = new Vector3(-7.5f,0,0);
                }
                if (i == 1)
                {
                    pos = new Vector3(-7.5f+ 2.14f, 0, 0);
                }
                if (i == 2)
                {
                    pos = new Vector3(-7.5f + (2* 2.14f), 0, 0);
                }
                if (i == 3)
                {
                    pos = new Vector3(-7.5f + (3 * 2.14f), 0, 0);
                }
                if (i == 4)
                {
                    pos = new Vector3(-7.5f + (4 * 2.14f), 0, 0);
                }
                if (i == 5)
                {
                    pos = new Vector3(-7.5f + (5 * 2.14f), 0, 0);
                }
                if (i == 6)
                {
                    pos = new Vector3(-7.5f + (6 * 2.14f), 0, 0);
                }
                if ((pickedcards[i]) == 9 && rep_heal == false || (pickedcards[i]) == 10 && rep_heal == false)//heal
                {
                    cartaActual = Instantiate(Boss1Cards[2], pos, Quaternion.identity);
                    rep_heal = true;
                }
                else if((pickedcards[i]) == 11&&rep_shop==false || (pickedcards[i]) == 12 && rep_shop == false)
                {
                    cartaActual = Instantiate(Boss1Cards[3], pos, Quaternion.identity);//toca carta tienda
                    rep_shop = true;
                }
                else
                {
                    float dificultad = Random.Range(0,10);
                    if (dificultad <=3)
                    {
                        cartaActual = Instantiate(Boss1Cards[0], pos, Quaternion.identity);

                    }
                    else if(dificultad >3&& dificultad <= 6)
                    {
                        noRepetirDificil++;
                        if (noRepetirDificil <=3) 
                        { 
                            cartaActual = Instantiate(Boss1Cards[4], pos, Quaternion.identity); 
                        }
                        else
                        {
                            cartaActual = Instantiate(Boss1Cards[0], pos, Quaternion.identity);
                        }
                        

                    }
                    else if (dificultad > 6)
                    {
                        norepetirTimer++;
                        if (norepetirTimer <= 1)
                        {
                            cartaActual = Instantiate(Boss1Cards[5], pos, Quaternion.identity);
                        }
                        else
                        {
                            cartaActual = Instantiate(Boss1Cards[0], pos, Quaternion.identity);
                        }
                        
                    }
                }
               
                posicionesocupadas.Add(cartaActual);

            }

            

        }
        
    }
 
  
    public void change()
    {
        List<int> Positions = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            Positions.Add(i);//Creamos las posiciones
        }
        


        for (int i = 0; i <posicionesocupadas.Count;  i++)
        {

            if (posicionesocupadas[i].tag == "duel")
            {
                //ASI ESTARÍA BIEN HECHO PERO PARA PRUEBA SOLO 3 SALAS
                int randomValor = Random.Range(0, SalasRandom.Count);
                int valorElegido = SalasRandom[randomValor];
                SalasRandom.RemoveAt(randomValor);
                
                //int prototipo = Random.Range(0, 3);
                GM.salas_ordenadas.Add("Sala "+(valorElegido));//aqui una vez tengamos todas las salas hacer un if random de todas las salas
            }
            if (posicionesocupadas[i].tag == "heal")
            {
                GM.salas_ordenadas.Add("SalaCuracion");
            }
            if (posicionesocupadas[i].tag == "shop")
            {
                GM.salas_ordenadas.Add("Tienda");
            }
            if (posicionesocupadas[i].tag == "boss")
            {
                if (Select.RUN == 2)
                {
                    GM.salas_ordenadas.Add("FinalBoss");
                }
                else
                {
                    if (Random.Range(0, 2)==0)
                    {
                        GM.salas_ordenadas.Add("WitchBoss");//Bruja
                    }
                    else
                    {
                        GM.salas_ordenadas.Add("Boss");//Payaso
                    }
                }
                

            }
            if (posicionesocupadas[i].tag == "Timer")
            {
                GM.salas_ordenadas.Add("TimerDuel");

            }
            if (posicionesocupadas[i].tag == "hard_duel")
            {
                //ASI ESTARÍA BIEN HECHO PERO PARA PRUEBA SOLO 3 SALAS
                int randomValor = Random.Range(0, SalasRandom.Count);
                int valorElegido = SalasRandom[randomValor];
                SalasRandom.RemoveAt(randomValor);

                //int prototipo = Random.Range(0, 3);
                GM.salas_ordenadas.Add("Sala " + (valorElegido));//aqui una vez tengamos todas las salas hacer un if random de todas las salas


                //int randPattern = Random.Range(0, Positions.Count);
                //int positionReset = Positions[randPattern];
                //Positions.Remove(positionReset);
                ////int prototipo = Random.Range(0, 3);
                //if (positionReset == 0) { GM.salas_ordenadas.Add("WitchBoss"); }
                //if (positionReset == 1) { GM.salas_ordenadas.Add("Lobo"); }
                //if (positionReset == 2) { GM.salas_ordenadas.Add("Boss"); }
                ////GM.salas_ordenadas.Add("Duelo00" + (prototipo));//aqui una vez tengamos todas las salas hacer un if random de todas las salas
                Hard_rooms.Add(i);
            }

        }
        GM.salas_recorridas=1;
        SceneLoader.Instance.LoadScene(GM.salas_ordenadas[0]);
    }
   

}
