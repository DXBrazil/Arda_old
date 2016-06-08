using Arda.Common.Interfaces.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arda.Common.ViewModels.Kanban;
using Arda.Kanban.Models;
using Arda.Common.Models.Kanban;

namespace Arda.Kanban.Repositories
{
    public class UserRepository : IUserRepository
    {
        private KanbanContext _context;

        public UserRepository(KanbanContext context)
        {
            _context = context;
        }


        public bool AddNewUser(UserViewModel user)
        {
            try
            {
                _context.Users.Add(new User()
                {
                    UniqueName = user.UniqueName,
                    Name = user.Name
                });
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUserByID(string userID)
        {
            try
            {
                var user = _context.Users.First(u=> u.UniqueName==userID);
                //TODO: Remove Appointments and WBs

                //Removing User:
                _context.Users.Remove(user);

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
