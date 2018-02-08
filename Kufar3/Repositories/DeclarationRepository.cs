using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Repositories
{
    public class DeclarationRepository : BaseRepository
    {
        public List<Declaration> List()
        {
            return Context.Declarations.ToList();
        }

        public Declaration GetById(int? id)
        {
            return Context.Declarations.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Declaration> GetDeclarationsByUserId(int userId)
        {
            return Context.Declarations.Where(x => x.UserId == userId);
        }

        public IQueryable<Declaration> GetDeclarationsByDeclarationType(DeclarationTypes declarationType)
        {
            return Context.Declarations.Where(x => x.Type == declarationType);
        }

        public void Add(Declaration declaration)
        {
            Context.Declarations.Add(declaration);
            Context.SaveChanges();
        }

        public void Update(Declaration declaration)
        {
            var newDeclaration = GetById(declaration.Id);

            newDeclaration.Name = declaration.Name;
            newDeclaration.Description = declaration.Description;
            newDeclaration.SubCategoryId = declaration.SubCategoryId;
            newDeclaration.Type = DeclarationTypes.OnModeration;
            newDeclaration.CityId = declaration.CityId;

            Context.SaveChanges();
        }

        public void Remove(int? id)
        {
            var declaration = Context.Declarations.First(x => x.Id == id);
            Context.Declarations.Remove(declaration);
            Context.SaveChanges();
        }

        public void EditDeclarationType(int declarationId, DeclarationTypes declarationType)
        {
            var declaration = GetById(declarationId);
            declaration.Type = declarationType;
            Context.SaveChanges();
        }
    }
}