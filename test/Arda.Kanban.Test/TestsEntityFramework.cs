using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Arda.Kanban.Models;
using Arda.Common.Models.Kanban;
using System.Data.SqlClient;

namespace Arda.Kanban.Tests
{
    public class TestsEntityFramework : IDisposable
    {
        KanbanContext _context;
        SqlConnection _connection;

        public TestsEntityFramework()
        {
            string connectionString = "Server=tcp:arda.database.windows.net,1433;Data Source=arda.database.windows.net;Initial Catalog=ArdaTest;Persist Security Info=False;User ID=ardaadmin;Password=arda@1029384756;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);

            for(int i=0; i<3; i++)
            {
                try { connection.Open();
                    break;
                }
                catch(SqlException)
                {
                }
            }

            var options = new DbContextOptionsBuilder<Arda.Kanban.Models.KanbanContext>().UseSqlServer(connection).Options;

            _connection = connection;
            _context = new KanbanContext(options);
        }

        [Fact]
        public void Users_List()
        {
            var users = (from u in _context.Users
                         select u).ToList();
        }

        [Fact]
        public void Users_Add()
        {
            User user = new User()
            {
                UniqueName = "Guest Random " + Guid.NewGuid(),
                Name = "Guest"
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        [Fact]
        public void WBTech_Add()
        {
            var tech = _context.Technologies.First();

            Guid wbbacklogGUID = Guid.NewGuid();
            var wbbacklog = new WorkloadBacklog()
            {
                WBID = wbbacklogGUID,
                WBIsWorkload = true,
                WBStatus = Status.To_Do,
                WBStartDate = DateTime.Now,
                WBEndDate = DateTime.Now,
                WBTitle = "Teste",
                WBDescription = null,
                WBExpertise = Expertise.Coding,
                WBComplexity = Complexity.Low,
                WBCreatedBy = "guest",
                WBCreatedDate = DateTime.Now,
                WBActivity = null
            };

            _context.WorkloadBacklogs.Add(wbbacklog);

            WorkloadBacklogTechnology wbTech = new WorkloadBacklogTechnology()
            {
                WBUTechnologyID = Guid.NewGuid(),
                WorkloadBacklog = wbbacklog,
                Technology = tech
            };

            _context.WorkloadBacklogTechnologies.Add(wbTech);
            
            _context.SaveChanges();
        }

        public static void Main(string[] args)
        {
            string connectionString = "Server=tcp:arda.database.windows.net,1433;Data Source=arda.database.windows.net;Initial Catalog=ArdaTest;Persist Security Info=False;User ID=ardaadmin;Password=arda@1029384756;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;";
            var options = new DbContextOptionsBuilder<Arda.Kanban.Models.KanbanContext>().UseSqlServer(connectionString).Options;

            var context = new Arda.Kanban.Models.KanbanContext(options);

            var tech = context.Technologies.First();

            Guid wbbacklogGUID = Guid.NewGuid();
            var wbbacklog = new WorkloadBacklog()
            {
                WBID = wbbacklogGUID,
                WBIsWorkload = true,
                WBStatus = Status.To_Do,
                WBStartDate = DateTime.Now,
                WBEndDate = DateTime.Now,
                WBTitle = "Teste",
                WBDescription = null,
                WBExpertise = Expertise.Coding,
                WBComplexity = Complexity.Low,
                WBCreatedBy = "guest",
                WBCreatedDate = DateTime.Now,
                WBActivity = null
            };

            context.WorkloadBacklogs.Add(wbbacklog);

            WorkloadBacklogTechnology wbTech = new WorkloadBacklogTechnology()
            {
                WBUTechnologyID = Guid.NewGuid(),
                WorkloadBacklog = wbbacklog,
                Technology = tech
            };

            context.WorkloadBacklogTechnologies.Add(wbTech);

            //User user = new Arda.Common.Models.Kanban.User()
            //{
            //    UniqueName = "Guest 001",
            //    Name = "Guest"
            //};

            //context.Users.Add(user);

            context.SaveChanges();
        }

        public void Dispose()
        {
            if(_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
