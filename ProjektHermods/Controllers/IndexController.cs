using ProjektHermods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;


namespace ProjektHermods.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {

            //######################################################################//
            List<string> allIngrediends; //Lagra ingredienser                       //
            //######################################################################//
            //  Metoden: "AddRecipeToDB()" - FEM Parametrar:                        //
            //  (1)Namn, (2)Typ, (3)Info, (4)Bild, (5)Ingrediens (6)Tillagningstid  //
            //######################################################################//
            #region Lägga in Data i DB(Om den är tom)
            int thingsInDB;
            using (ReceptTipsContext context = new ReceptTipsContext())
            {
                thingsInDB = context.Recepts.Count();
            }
            if (thingsInDB == 0)
            {
                allIngrediends = new List<string>() { "Tomat", "Ost" };
                AddRecipeToDB("Margherta", "Mat", "How to do...", "/img/margareta.png", allIngrediends, 30,true);

                allIngrediends = new List<string>() { "Tomat", "Ost", "Skinka" };
                AddRecipeToDB("Vesuvio", "Mat", "How to do...", "/img/vesuvio.jpg", allIngrediends, 30, true);

                allIngrediends = new List<string>() { "Tomat", "Ost", "Skinka", "Ananas" };
                AddRecipeToDB("Hawaii", "Mat", "How to do...", "/img/hawaii.png", allIngrediends, 30, true);

                allIngrediends = new List<string>() { "Tomat", "Ost", "Kebabkött", "Gurka", "Isbergssallad", "Pepperoni", "Kebabsås" };
                AddRecipeToDB("Kebabpizza", "Mat", "How to do...", "/img/kebabpizza.jpg", allIngrediends, 30, true);

                allIngrediends = new List<string>() { "Vaniljvodka", "Sourz Sour Apple", "Limejuice", "Fruktsoda" };
                AddRecipeToDB("P2", "Dricka", "How to do...", "/img/p2.jpg", allIngrediends, 5, true);

                allIngrediends = new List<string>() { "Vodka", "Blå curacao", "Sprite" };
                AddRecipeToDB("Blue Lagoon", "Dricka", "How to do...", "/img/cocktail_blue_lagoon-1.png", allIngrediends, 5, true);

                allIngrediends = new List<string>() { "Whiskey", "Martini rosso", "Angostura bitter" };
                AddRecipeToDB("Manhattan", "Dricka", "How to do...", "/img/7-manhattan_400.jpg", allIngrediends, 5, true);

                allIngrediends = new List<string>() { "Rom", "Malibu", "Mjölk", "Ananasjuice" };
                AddRecipeToDB("Piña Colada", "Dricka", "How to do...", "/img/39801_large.jpg", allIngrediends, 5, true);

                allIngrediends = new List<string>() { "Vodka", "Redbull", "Is" };
                AddRecipeToDB("RedbullVodka", "Dricka", "4 cl Vodka och 1 redbull, blanda med is så blir den super!", "/img/di_10323_3.jpg", allIngrediends, 5, true);

                allIngrediends = new List<string>() { "Ägg", "Mjölk", "Mjöl", "Smör" };
                AddRecipeToDB("Pannkaka", "Mat", "Blanda 6 dl mjölk med 3 dl mjöl, Rör om, lägg i 3 ägg och låt den vila 10 minuter innan stekning", "/img/pannkaka.jpg", allIngrediends, 15, true);
            }
            #endregion

            return View();
        }

        public ActionResult Kontakt()
        {
            if (Request["buttonAdd"] != null)
            {


               
                string allaFel = "";
                string nameVariabel = Request["inputText"];
                string nameEmail = Request["inputEmail"];
                string nameSelectTopic = Request["selectTopic"];
                string nameTextArea = Request["inputTextArea"];

                if (nameEmail == "")
                {
                    allaFel += "Du glömde skriva in Email!";
                }
                else
                {
                    bool ok = Regex.IsMatch(nameEmail,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    if (!ok)
                    {
                        allaFel += "Skriv in rätt format på Email IDIOT!!!! skriva in Email!";
                    }
                }

                if (nameVariabel == "")
                {
                    allaFel += "Du glömde skriva in ditt namn!";
                }
                if (nameSelectTopic == "")
                {
                    allaFel += "Du valde inget alternativ!";
                }
                if (nameTextArea == "")
                {
                    allaFel += "Skriv in ett meddelande, så vi vet vad du behöver hjälp med";
                }
                if (allaFel != "")
                {
                    ViewBag.Felmeddelande = allaFel;

                    ViewBag.nameVariabel = Request["inputText"];
                    ViewBag.nameEmail = Request["inputEmail"];
                    ViewBag.nameSelectTopic = Request["selectTopic"];
                    ViewBag.nameTextArea = Request["inputTextArea"];
                }

                if(allaFel == "")
                {
                    return Redirect("FormulärSkickat");
                }

            }
       
            return View();
        }
        public ActionResult FormulärSkickat()
        {
            return View();
        }

        //om det uppstår ett fel på vår sida så vissas denna sidan istället
        public ActionResult Error()

        {
            return View();
        }

        public void AddRecipeToDB(string nameOnItem2, string ingrediensType2, string infoAboutItem2, string pictureLink2, List<string> allIngrediends2, int tid2, bool Ok)
        {
            //Skapa massa "startdata" till DB vid startup (Om DB är tom)
            using (ReceptTipsContext context = new ReceptTipsContext())
            {
                ///##############################################///
                /// Alla variabler som behövs för ett nytt Recept///
                ///##############################################///
                string nameOnItem = nameOnItem2; //Namn på grejen!
                string ingrediensType = ingrediensType2; //Dricka eller Mat
                string infoAboutItem = infoAboutItem2; //Diverse info
                string pictureLink = pictureLink2;//Bildadress, "img/nophoto.jpg" = (För standardbild)
                List<string> allIngrediends = allIngrediends2; //ListVariabel för Valda ingredienser
                int tillagningstid = tid2;//tid på saker
                bool okej = Ok;
                #region Lägga till vald Ingrediens i en Lista (Till Fullständiga receptet sen)
                //Skapa en TOM Lista för nytt Recept
                IList<Ingrediens> nuvarandeReceptLista = new List<Ingrediens>();
                for (int i = 0; i < allIngrediends.Count(); i++)
                {
                    string newIngrediens = allIngrediends[i];
                    // allIngrediends.RemoveAt(0);

                    //Skapa variabel av ny ingrediens
                    Ingrediens nyIng;
                    //Hämta vald ingrediens till en lista
                    var listWithChoosenIng = context.Ingrediens.Where(ing => ing.Name == newIngrediens);
                    //Om den finns sen innan tar vi den datan, _annars_ skapar vi en ny
                    if (listWithChoosenIng.Count() > 0)
                        nyIng = listWithChoosenIng.First();
                    else
                        nyIng = new Ingrediens() { Name = newIngrediens };

                    //Lägga till i nuvarande listan:
                    nuvarandeReceptLista.Add(nyIng);
                }
                #endregion
                #region Val av typ (Till Receptet)
                //Skapa variabel av typen MAT eller DRICKA
                ChoosenType choosenType;
                //Hämta vald ingrediens till en lista
                var listWithChoosenType = context.ChoosenTypes.Where(ing => ing.Typ == ingrediensType);
                //Om den finns sen innan tar vi den datan, _annars_ skapar vi en ny
                if (listWithChoosenType.Count() > 0)
                    choosenType = listWithChoosenType.First();
                else
                    choosenType = new ChoosenType() { Typ = ingrediensType };
                #endregion
                #region Skapa Receptet och lägga till aktuella ingredienser i DB
                //Lägga till KOMPLETT recept
                Recept nyttRecept = new Recept()
                {
                    Name = nameOnItem,
                    Tid = tillagningstid,
                    ChoosenTypes = choosenType,
                    Info = infoAboutItem,
                    Picture = pictureLink,
                    OkRecept = Ok,
                    Ingredients = nuvarandeReceptLista
                };
                //Nolla nuvarande listan
                nuvarandeReceptLista = new List<Ingrediens>();

                //Lägga till i lista för DB
                context.Recepts.Add(nyttRecept);

                //Spara ändringar till DB
                context.SaveChanges();
                #endregion
            }
        }
    }
}