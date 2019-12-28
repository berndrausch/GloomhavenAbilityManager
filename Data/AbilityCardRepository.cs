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
                    new AbilityCardInfo{Id = 0, Name = "Cragheart 1", Level = "1", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Id = 1, Name = "Cragheart 2", Level = "1", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Id = 2, Name = "Cragheart 3", Level = "2", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Id = 3, Name = "Cragheart 4", Level = "3", Class = CharacterClass.Cragheart},
                    new AbilityCardInfo{Id = 4, Name = "Cragheart 5", Level = "X", Class = CharacterClass.Cragheart, ImagePath=@"\images\04.png"},
                    new AbilityCardInfo{Id = 5, Name = "Tinkerer 1", Level = "1", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Id = 6, Name = "Tinkerer 2", Level = "1", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Id = 7, Name = "Tinkerer 3", Level = "2", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Id = 8, Name = "Tinkerer 4", Level = "3", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Id = 9, Name = "Tinkerer 5", Level = "X", Class = CharacterClass.Tinkerer},
                    new AbilityCardInfo{Id = 10, Name = "Tinkerer 6", Level = "X", Class = CharacterClass.Tinkerer},
                });
        }
    }
}