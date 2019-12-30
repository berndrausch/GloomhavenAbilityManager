﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{

    public class CharacterClassService : ICharacterClassService
    {
        private IAbilityCardRepository _cardRepository;

        private CharacterClass[] _allClasses = new[] 
        {
            new CharacterClass {Id = 0, Name = "Alle" },
            new CharacterClass {Id = 1, Name = "Barbar" },
            new CharacterClass {Id = 2, Name = "Felsenherz" },
            new CharacterClass {Id = 3, Name = "Tüftler" },
        };

        public CharacterClassService(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public CharacterClass GetClass(int id)
        {
            return _allClasses.FirstOrDefault(c => c.Id == id) ?? _allClasses[0];
        }

        public async Task<IEnumerable<CharacterClass>> GetClassesAsync()
        {
            IEnumerable<AbilityCardInfo> allCards = await _cardRepository.GetAllAsync();
            return allCards.Select( c => c.ClassId).Distinct().Select(GetClass);      
        }
    }
}