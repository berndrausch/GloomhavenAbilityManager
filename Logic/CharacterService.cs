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
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IAbilityCardService _cardService;

        public CharacterService(ICharacterRepository characterRepository, IAbilityCardService cardService)
        {
            _characterRepository = characterRepository;
            _cardService = cardService;
        }

        public Character GetCharacter(int id)
        {
            IEnumerable<Character> allCharacters = GetCharacters();

            try
            {
                return allCharacters.First(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new LogicException($"Unable to find characters with id {id} in all {allCharacters.Count()} characters", ex);
            }
        }

        public void UpdateCharacter(Character character)
        {
            var allCharacters = _characterRepository.GetAll().ToList();
            allCharacters.RemoveAll(c => c.Id == character.Id);
            allCharacters.Add(CharacterConverter.ToDataObject(character));
            _characterRepository.SaveAll(allCharacters);
        }

        public void AddCharacter(Character character)
        {
            var allCharacters = _characterRepository.GetAll().ToList();
            character.Id = allCharacters.Select(c => c.Id).Max() + 1;
            character.Level = 1;
            character.PoolCards = _cardService.GetAvailableCards(character).ToList();
            allCharacters.Add(CharacterConverter.ToDataObject(character));
            _characterRepository.SaveAll(allCharacters);
        }

        public IEnumerable<Character> GetCharacters()
        {
            IEnumerable<CharacterDataObject> allCharacterDataObjects;

            try
            {
                allCharacterDataObjects = _characterRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new LogicException($"Unable to get character data from repository", ex);
            }

            return allCharacterDataObjects.Select(CharacterConverter.FromDataObject);
        }
    }
}