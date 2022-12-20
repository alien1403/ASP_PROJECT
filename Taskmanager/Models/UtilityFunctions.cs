using Microsoft.AspNetCore.Identity;
using Taskmanager.Data;

namespace Taskmanager.Models
{
    public class UtilityFunctions
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public UtilityFunctions() { }
        public UtilityFunctions(UserManager<ApplicationUser> uM, ApplicationDbContext ctx)
        {
            userManager = uM;
            db = ctx;
        }
        
        public static bool IsAdmin(Team t, string id)
        {

            UtilityFunctions uf = new();
            var db = uf.db;
            var userManager = uf.userManager;

            if(db.Teams.Find(t.Id).IdAdmin == id)
            {
                return true;
            }

            return false;
        }
        public static bool IsAdmin(Projects t, string id)
        {

            UtilityFunctions uf = new();
            var db = uf.db;
            var userManager = uf.userManager;

            if (db.Projects.Find(t.Id).IdAdmin == id)
            {
                return true;
            }

            return false;
        }


    }
}
