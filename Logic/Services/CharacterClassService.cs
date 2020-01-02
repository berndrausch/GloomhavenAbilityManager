using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Contracts;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.Logic.Services
{
    public class CharacterClassService : ICharacterClassService
    {
        private readonly ICharacterClassRepository _classRepository;
        private readonly IAbilityCardRepository _cardRepository;

        public CharacterClassService(ICharacterClassRepository classRepository, IAbilityCardRepository cardRepository)
        {
            _classRepository = classRepository;
            _cardRepository = cardRepository;
        }

        public CharacterClass GetClass(int id)
        {
            return _classRepository.GetAll().FirstOrDefault(c => c.Id == id) ?? CharacterClass.Default;
        }

        public IEnumerable<CharacterClass> GetClasses()
        {
            return _cardRepository.GetAll().Select(c => c.ClassId).Distinct().Select(GetClass);
        }
    }
}