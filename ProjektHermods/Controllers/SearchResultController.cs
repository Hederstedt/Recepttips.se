using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Sql;
using System.Web.Mvc;
using ProjektHermods.Models;

namespace ProjektHermods.Controllers
    {

    [Serializable]
    public class SearchResultController : Controller
        {

        public ActionResult Index()
            {
            //ReceptTipsContext context = new ReceptTipsContext();

            List<string> keys = new List<string>(Request.QueryString.AllKeys);
            //List<string> list = new List<string>();
            IList<Ingrediens> Excluded2 = new List<Ingrediens>();
            //Om det finns något sökresultat
            if(keys.Count>1)
                {
               
                //Lagrar alla ingredienser MED kommatecken
                var query = Request.QueryString["ingrediens"];
                //Splittar alla ingredienser där det är kommatecken
                string[] words = query.Split(',');
               
                //Skapa en DB-koppling
                using(ReceptTipsContext context = new ReceptTipsContext())
                    {
                    //NU ska dessa ingredienser söka genom ALLA recept som matchar
                   
                    //Ny lista att lagra sökresultaten
                    List<SearchResultList> searchResultList = new List<SearchResultList>();
                    List<SearchResultList> secondsResultList = new List<SearchResultList>();
                    //Denna foreach kollar recept efter recept
                    foreach(var item in context.Recepts)
                        {
                        //Hämtar ALLA ingredienser för det aktuella receptet
                        IList<Ingrediens> aktuelltRecept = item.Ingredients;
                        IList<Ingrediens> Excluded = item.Ingredients;
                  
                        int antalMatchningar = 0;
                        int antalIckeMatchningar = 0;
                        //Kollar nu splitt-orden i den aktuella listan
                        for(int i = 0; i<words.Count(); i++)
                            {
                            bool didItExist = false;
                            foreach(Ingrediens item2 in aktuelltRecept)
                                {
                                //Om splittord är lika med någon av det aktuella receptets ingredienser
                                if(words[i]==item2.Name)
                                    didItExist=true;
                              
                                }

                            if(didItExist==true) { 
                                antalMatchningar++;
                                }
                            else if(words.Count()>item.Ingredients.Count()||words.Count()<item.Ingredients.Count())
                                {
                                antalIckeMatchningar+=words.Count()-item.Ingredients.Count(); 
                             
                                }
                            else { 
                                antalIckeMatchningar++;
                                   
                            }
                            }

                        //Nu har "antalmatchingar/antalIckeMatchningar" fått en summa av det aktuella receptet: "item.id"
                     
                        //Mustafa För att filtera in det i två olika sök resultat, en med matchningar och en med nästan matchningar. Första kollar att det är exakt samma
                        //ingredients. Antal Matchningar måste vara lika mycket som wordcount också och den dubbelkollar med item.ingredients count är samma som wordscount
                        if (antalIckeMatchningar==0&&antalMatchningar==words.Count()&&item.Ingredients.Count==words.Count())
                            {
                            searchResultList.Add(new SearchResultList() { Id=item.Id, Name=item.Name, match=antalMatchningar, noMatch=antalIckeMatchningar });
                            }
                        //Andra result filtrering. Kollar om antalIcke matchningar är 1 eller lägre men inte lägre än 0 
                        //då många recept har -5 Ickematchningar. Till sust kollar vi att det är fler matchningar än icke matchningar
                        ///<summary>Kollar om det är fler matchningar än ickematchningar</summary>
                        else if (antalIckeMatchningar <= 1 && antalMatchningar >= antalIckeMatchningar && antalIckeMatchningar >= 0)
                            {

                       
                            secondsResultList.Add(new SearchResultList() { Id = item.Id, Name = item.Name, match = antalMatchningar, noMatch = antalIckeMatchningar, Excluded=null});
                            Excluded=null;
                            
                            }

                        }
                    //ViewBag.Excluded=Excluded2;   //Viewbag Excluded visar vilka ingredienser som behövs. MEN bortkommenterat just nu då det inte fungerar. 
                    ViewBag.SearchWords=words; //Innehåller sökorden
                    ViewBag.SecondChoice=secondsResultList; //Andra resultatet
                    Session["SearchResult"]=searchResultList;
                    ViewBag.Result=searchResultList;//Första resultat
                    }

                }
            if(keys.Count > 1)
                {
                 if(ViewBag.SecondChoice.Count == 0 )
                {
                 ViewBag.SecondChoice=null;

                }
            if(ViewBag.Result.Count == 0)
                { ViewBag.Result=null;

                }
                }
            if(keys.Count <1 )
                {
                    ViewBag.Excluded=null;   //Viewbag Excluded visar vilka ingredienser som behövs. MEN bortkommenterat just nu då det inte fungerar. 
                    ViewBag.SearchWords=null; //Innehåller sökorden
                ViewBag.SecondChoice=null; ; //Andra resultatet
                    Session["SearchResult"]=null;
                    ViewBag.Result=null;//Första resultat
                }
            return View();
            }
        }
    }

