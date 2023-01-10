using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;

namespace ClimbingApp.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;
        
        public RoleRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;
        }

        public bool Insert(Role Role)
        {
            try
            {
                dbContext.Roles.Add(Role);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool Update(Role Role)
        {
            var existingRole = GetById(Role.RoleId);

            if (existingRole == null)
                return false;

            try
            {
                existingRole.Name = Role.Name;

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            var existingRole = GetById(id);

            if (existingRole == null)
                return false;

            try
            {
                dbContext.Roles.Remove(existingRole);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Role> List()
        {
            return dbContext.Roles.ToList();
        }

        public Role GetById(int id)
        {
            return (Role)dbContext.Roles.Where(x => x.RoleId == id);
        }
    }
}
