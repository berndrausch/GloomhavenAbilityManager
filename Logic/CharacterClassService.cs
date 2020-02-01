using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Contracts.Data;
using GloomhavenAbilityManager.Logic.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Converters;

namespace GloomhavenAbilityManager.Logic
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
            CharacterClassDataObject dataObject = _classRepository.GetAll().FirstOrDefault(c => c.Id == id);
            return dataObject == null ? CharacterClass.Default : CharacterClassConverter.FromDataObject(dataObject);
        }

        public IEnumerable<CharacterClass> GetClasses()
        {
            return _cardRepository.GetAll().Select(c => c.ClassId).Distinct().Select(GetClass);
        }
    }
}