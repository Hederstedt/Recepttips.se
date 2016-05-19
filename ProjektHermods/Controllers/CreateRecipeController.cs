using ProjektHermods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektHermods.Controllers
{
    public class CreateRecipeController : Controller
    {
        // GET: CreateRecipe
        public ActionResult Index()
        {

            if (Request["buttonAdd"] != null)
            {
                string allaFel = "";
                string nameOnItem = Request["NameInput"].ToLower(); //Sätter nameOnItem till det du skrivr i formuläret
                string ingrediensType = Request["TypInput"]; ////Sätter ingredienstype till det du skrivr i formuläret
                string infoAboutItem = Request["InfoInput"]; //Sätter infoaboutitem till det du skrivr i formuläret
                string pictureLink = Request["PictureInput"]; //Bildadress, "img/nophoto.jpg" = (För standardbild)
               // string ingrediensBild = Request["ingrediensBild"]; //Bildadress, "img/nophoto.jpg" = (För standardbild)
                string allIngrediends = Request["IngrediensInput"]; //Sätter allingrediends till det du skrivr i formuläret
                bool adminOk = false;
                string tillagningstid2 = Request["Tid"];
                if (nameOnItem == "")
                {
                    allaFel += " Du glömde ett namn!";
                }
                if (infoAboutItem == "")
                {
                    allaFel += " Du glömde ReceptInfo, Hur ska vi veta hur man gör nuu?!";
                }
                if (pictureLink == "")
                {
                    ViewBag.DefaultPic = "Ändra bild annars blir det /img/nophoto.jpg!";
                    pictureLink = "/img/nophoto.jpg";
                }
                if (tillagningstid2 == "")
                {
                    allaFel += "Skriv in tillagningstiden på receptet tack!";
                }
                
                else
                {
                    bool anyFineFormat = false;
                    if (pictureLink.Contains(".jpg")||pictureLink.Contains(".png")||pictureLink.Contains(".gif"))
                    {
                        anyFineFormat = true;
                    }
                    if (anyFineFormat == false)
                    {
                        allaFel += " Bilden får bara vara i formaten: .jpg/.png/.gif!";
                    }
                }
                if (allIngrediends == "")
                {
                    allaFel += "* Du glömde Ingredienser!";
                }

                if (tillagningstid2 == "")
                {
                    allaFel += "* Du glömde tillagningstid!";
                }
                if (tillagningstid2 != null)
                {
                    foreach (char x in tillagningstid2)
                    {
                        if (!char.IsDigit(x))
                        {
                            allaFel += "* Ha bara siffror i tillagningstiden!";
                            break;
                        }
                    }
                }

                string[] input = allIngrediends.Split(','); //skapar en array som heter input, som splittar dina ingridenser när du skriver ett , i rutan
                List<string> allIngrediens = new List<string>();//skapar en lista utan dina ingrideienser
                for (int i = 0; i < input.Count(); i++)//räknar ut hur många du har skrivit
                {
                    
                  allIngrediens.Add(input[i]);//lägger till din input/ingridienser
                }
                if (allaFel == "")
                {
                    ViewBag.Success = "Ditt recept är nu inlagt!";
                    int tillagningstid = int.Parse(tillagningstid2);
                    //slutligen lägger till i databasen
                    AddRecipeToDB(nameOnItem, ingrediensType, infoAboutItem, pictureLink, allIngrediens,adminOk, tillagningstid);
                    return RedirectToAction("Förhandsvisning");
                }
                if (allaFel != "")
                {
                    ViewBag.Felmeddelande = allaFel;
                    ViewBag.nameOnItem = Request["NameInput"];
                    ViewBag.tillagningstid = Request["Tid"];
                    ViewBag.ingrediensType = Request["TypInput"]; 
                    ViewBag.infoAboutItem = Request["InfoInput"]; 
                    ViewBag.pictureLink = Request["PictureInput"]; 
                    ViewBag.allIngrediends = Request["IngrediensInput"];
                }
            }
            return View();
        }
        public void AddRecipeToDB(string nameOnItem2, string ingrediensType2, string infoAboutItem2, string pictureLink2, List<string> allIngrediends2,bool AdminOk2, int tid2)
        {
            //Skapa massa "startdata" till DB vid startup (Om DB är tom)
            using (ReceptTipsContext context = new ReceptTipsContext())
            {
                ///##############################################///
                /// Alla variabler som behövs för ett nytt Recept///
                ///##############################################///
                string nameOnItem = nameOnItem2; //Namn på grejen!
                bool adminOk = AdminOk2;
                string ingrediensType = ingrediensType2; //Dricka eller Mat
                string infoAboutItem = infoAboutItem2; //Diverse info
                string pictureLink = pictureLink2;//Bildadress, "img/nophoto.jpg" = (För standardbild)
                List<string> allIngrediends = allIngrediends2; //ListVariabel för Valda ingredienser
                int tillagningstid = tid2;//tid på saker
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
                        nyIng = new Ingrediens() { Name = newIngrediens, };// IngrediensBild };

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
                    OkRecept = adminOk,
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
        public ActionResult Förhandsvisning()
        {
            using (ReceptTipsContext context =new ReceptTipsContext())
            {
                foreach (Recept item in context.Recepts)
                {
                    if (!item.OkRecept)
                    {
                        ViewBag.Förnhandsvisning = item.Name;
                            
                    }
                }
            }
            return View();
        }
    }
    
}