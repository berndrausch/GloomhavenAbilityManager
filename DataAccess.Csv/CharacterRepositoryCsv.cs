using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Exceptions;
using GloomhavenAbilityManager.DataAccess.Contracts.interfaces;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CharacterRepositoryCsv : ICharacterRepository
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICsvConfiguration _configuration;
        private IAbilityCardRepository _cardRepository;
        public CharacterRepositoryCsv(IFileSystem fileSystem, ICsvConfiguration configuration, IAbilityCardRepository cardRepository)
        {
            _fileSystem = fileSystem;
            _configuration = configuration;
            _cardRepository = cardRepository;
        }

        public IEnumerable<CharacterDataObject> GetAll()
        {
            List<CharacterDataObject> characters = ReadCharacters();
            List<CharacterAbilityCardRelation> relations = ReadRelations();
            List<AbilityCardDataObject> cards = _cardRepository.GetAll().ToList();

            foreach (CharacterDataObject character in characters)
            {
                character.PoolCards = relations.Where(rel => rel.CharacterId == character.Id).Select(rel => cards.FirstOrDefault(card => card.Id == rel.AbilityCardId));
                character.SelectedCards = relations.Where(rel => rel.IsSelected && rel.CharacterId == character.Id).Select(rel => cards.FirstOrDefault(card => card.Id == rel.AbilityCardId));
            }

            return characters;
        }

        private List<CharacterDataObject> ReadCharacters()
        {
            string fileName = Path.Combine(_configuration.DataDir, _configuration.CharactersFileName);
          
            try
            {
                var csvFileReader = new CsvFileReader<CharacterDataObject>(_fileSystem, fileName);
                return csvFileReader.GetAll();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read characters from {fileName}", ex);
            }
        }

        private List<CharacterAbilityCardRelation> ReadRelations()
        {
            string fileName = Path.Combine(_configuration.DataDir, _configuration.CharacterCardsFileName);
         
            try
            {
                var csvFileReader = new CsvFileReader<CharacterAbilityCardRelation>(_fileSystem, fileName);
                return csvFileReader.GetAll();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read character cards from {fileName}", ex);
            }
        }

        public void SaveAll(IEnumerable<CharacterDataObject> characters)
        {
            string fileName = Path.Combine(_configuration.DataDir, _configuration.CharactersFileName);
            
            try
            {
                var csvFileWriter = new CsvFileWriter<CharacterDataObject>(_fileSystem, fileName);
                csvFileWriter.SaveAll(characters);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to write character cards from {fileName}", ex);
            }

            List<CharacterAbilityCardRelation> relations = new List<CharacterAbilityCardRelation>();
            foreach (CharacterDataObject character in characters)
            {
                foreach (int cardId in character.PoolCards.Select(card => card.Id))
                {
                    var rel = new CharacterAbilityCardRelation()
                    {
                        CharacterId = character.Id,
                        AbilityCardId = cardId,
                        IsSelected = character.SelectedCards.Any(card => card.Id == cardId)
                    };
                    relations.Add(rel);
                }
                character.PoolCards = null;
                character.SelectedCards = null;
            }

            fileName = Path.Combine(_configuration.DataDir, _configuration.CharacterCardsFileName);

            try
            {
                var csvFileWriter = new CsvFileWriter<CharacterAbilityCardRelation>(_fileSystem, fileName);
                csvFileWriter.SaveAll(relations);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to write character cards to {fileName}", ex);
            }
        }
    }
}
