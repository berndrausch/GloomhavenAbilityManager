using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public class AbilityCardRepository : IAbilityCardRepository
    {
        public Task<IEnumerable<AbilityCardInfo>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<AbilityCardInfo>>(
                new List<AbilityCardInfo>
                {
                    new AbilityCardInfo{Name = "Cragheart 1", Level = "1", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Name = "Cragheart 2", Level = "1", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Name = "Cragheart 3", Level = "2", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Name = "Cragheart 4", Level = "3", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Name = "Cragheart 5", Level = "X", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Name = "Tinkerer 1", Level = "1", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Name = "Tinkerer 2", Level = "1", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Name = "Tinkerer 3", Level = "2", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Name = "Tinkerer 4", Level = "3", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Name = "Tinkerer 5", Level = "X", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Name = "Tinkerer 6", Level = "X", Class = CharacterClass.Tinkerer},
                });
        }
    }
}