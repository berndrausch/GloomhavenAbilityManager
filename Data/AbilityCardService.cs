using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public class AbilityCardService : IAbilityCardService
    {
        public Task<IEnumerable<AbilityCardInfo>> GetCardsAsync()
        {
            return Task.FromResult<IEnumerable<AbilityCardInfo>>(
                new List<AbilityCardInfo>
                {
                    new AbilityCardInfo{Name = "Dummy 1", Level = "1"},
                    new AbilityCardInfo{Name = "Dummy 2", Level = "1"},
                    new AbilityCardInfo{Name = "Dummy 3", Level = "2"},
                    new AbilityCardInfo{Name = "Dummy 4", Level = "3"},
                    new AbilityCardInfo{Name = "Dummy 5", Level = "X"},
                });
        }
    }
}