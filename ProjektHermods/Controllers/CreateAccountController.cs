using ProjektHermods.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProjektHermods.Controllers
{
    public class CreateAccountController : Controller
    {
        
        // GET: CreateAccount
        public ActionResult Index()
        {
            
            using (ReceptTipsContext context = new ReceptTipsContext())
            {
                //Nödvändigt så att vi har en Admin om databasen är tom så skapar vi han så fort någon försöker logga in.
                int count = context.UserModels.Count();
                if (count == 0)
                {
                    UserModel newUser = new UserModel()
                    {
                        Name = "Admin",
                        Password = ""
                    };
                    context.UserModels.Add(newUser);
                    context.SaveChanges();
                }
                foreach (UserModel item in context.UserModels)
                {
                    if (Session["username"] == null)
                    {
                        return Redirect("/CreateAccount/Info");
                    }
                    if (Session["username"].ToString() == "Admin")
                    {
                
                      //Om skapa-konto-knappen har blivit använd
                      if (Request["createaccountbutton"] != null)
                         {
                            //Ta emot inputs
                            string nameinput = Request["nameinput"];
                            string passwordinput = Request["passwordinput"];
                            string password2input = Request["passwordinput2"];

                            //Meddelande till användaren om kontot skapats eller felmeddelanden
                            string messageToUser = "";

                            //Lagra eventuella fel
                            string fel = "";
                            if (nameinput == "")
                                fel += "* Du glömde använarnamn. ";
                            if (passwordinput == "" || password2input == "")
                                fel += "* Du glömde lösenord på båda delarna. ";
                            if (passwordinput != null && password2input != null && passwordinput != password2input)
                                fel += "* Lösenorden stämmer inte överrens. ";

                            //Kika om användarnamnet är upptaget också förstås
                            foreach (UserModel user in context.UserModels)
                            {
                                if (user.Name == nameinput)
                                    fel += "* Användarnamnet är upptaget. ";
                            }

                            //Om det var noll fel skapar vi kontot
                            if (fel == "")
                            {
                                UserModel newUser = new UserModel()
                                {
                                    Name = nameinput,
                                    Password = passwordinput
                                };
                                context.UserModels.Add(newUser);
                                context.SaveChanges();

                                //Lägger användarnamnet i en SEKTION
                                Session["username"] = nameinput;
                                //Slår om ISLOGGEDIN-sektionen till TRUE!
                                Session["IsLoggedIn"] = true;

                                //Skicka iväg ett meddelande
                                ViewBag.messageToUserAccountCreated = "Skapat konto!";
                            }
                            else
                            {
                                //Slänga med errormeddelandet
                                messageToUser = "Error! Följande fel: ";
                                messageToUser += fel;

                                ViewBag.messageToUser = messageToUser;
                            }
                        }
                    }
                    return View();
           }
         }
            return Redirect("/CreateAccount/Info");
        }
        public ActionResult Info()
        {
            return View();
        }
    }
}