using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Contracts.Data;
using GloomhavenAbilityManager.Logic.Contracts.Exceptions;
using GloomhavenAbilityManager.Logic.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Converters;

namespace GloomhavenAbilityManager.Logic
{
    public class CharacterClassService : ICharacterClassService
    {
        private readonly ICharacterClassRepository _classRepository;

        public CharacterClassService(ICharacterClassRepository classRepository, IAbilityCardRepository cardRepository)
        {
            _classRepository = classRepository;
        }

        public CharacterClass GetClass(int id)
        {
            IEnumerable<CharacterClass> allClasses = GetClasses();

            try
            {
                return allClasses.First(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new LogicException($"Unable to find class with id {id} in all {allClasses.Count()} classes", ex);
            }
        }

        public IEnumerable<CharacterClass> GetClasses()
        {
            IEnumerable<CharacterClassDataObject> allClassesObjects;

            try
            {
                allClassesObjects = _classRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new LogicException($"Unable to get class data from repository", ex);
            }

            return allClassesObjects.Select(CharacterClassConverter.FromDataObject);
        }
    }
}