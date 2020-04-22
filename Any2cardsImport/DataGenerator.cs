using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Csv;

namespace GloomhavenAbilityManager.Any2cardsImport
{
    public class DataGenerator
    {
        public void Generate(IEnumerable<FileInfo> cardImageFiles, bool useSpoilerFiles)
        {
            string assemblyDirectory = new FileInfo(Assembly.GetAssembly(this.GetType()).Location).Directory.FullName;

            string additionalClassInfoFile = Path.Combine(assemblyDirectory, "additionalclassinfo.csv");
            string additionalCardInfoFile = Path.Combine(assemblyDirectory, "additionalcardinfo.csv");
            
            var additionalClassInfos = ImportAdditionalClassInfos(additionalClassInfoFile);
            var additionalCardInfos = ImportAdditionalCardInfos(additionalCardInfoFile);

            if (useSpoilerFiles)
            {
                string additionalClassInfoSpoilerFile = Path.Combine(assemblyDirectory, "additionalclassinfo.spoiler.csv");
                string additionalCardInfoSpoilerFile = Path.Combine(assemblyDirectory, "additionalcardinfo.spoiler.csv");

                additionalClassInfos.AddRange(ImportAdditionalClassInfos(additionalClassInfoSpoilerFile));
                additionalCardInfos.AddRange(ImportAdditionalCardInfos(additionalCardInfoSpoilerFile));
            }

            var classes = additionalClassInfos.ConvertAll(ConvertToDataObject);
            var cards = GenerateCardDataObjects(cardImageFiles, additionalClassInfos, additionalCardInfos);

            ExportCharacterClassDataObjects(Path.Combine(assemblyDirectory, "classes.csv"),classes);
            ExportAbilityCardDataObjects(Path.Combine(assemblyDirectory, "cards.csv"),cards);
        }

        private List<AbilityCardDataObject> GenerateCardDataObjects(IEnumerable<FileInfo> cardImageFiles,
            List<AdditionalClassInfo> additionalClassInfos, List<AdditionalCardInfo> additionalCardInfos)
        {
            var cards = new List<AbilityCardDataObject>();
            foreach (FileInfo file in cardImageFiles)
            {
                var card = ConvertToDataObject(file, additionalClassInfos, additionalCardInfos);
                if (card != null)
                {
                    cards.Add(card);
                }
            }

            return cards;
        }

        private AbilityCardDataObject ConvertToDataObject(FileInfo file, List<AdditionalClassInfo> additionalClassInfos, List<AdditionalCardInfo> additionalCardInfos)
        {
            string classAbbreviation = file.Directory.Name;

            int classId = FindClassId(additionalClassInfos, classAbbreviation);

            if (classId < 0)
            {
                return null;
            }

            AdditionalCardInfo additionalCardInfo = FindCard(additionalCardInfos, file.Name);

            if (additionalCardInfo == null)
            {
                return null;
            }


            return new AbilityCardDataObject
            {
                Id = additionalCardInfo.Id,
                Name = GetCardName(additionalCardInfo),
                ClassId = classId,
                Level = additionalCardInfo.Level,
                ImagePath = Path.Combine("character-ability-cards", classAbbreviation, file.Name)
            };
        }

        private List<AdditionalClassInfo> ImportAdditionalClassInfos(string file)
        {
            var additionalClassInfoReader = new CsvFileReader<AdditionalClassInfo>(new FileSystem(), file);
            return additionalClassInfoReader.GetAll();
        }

        private List<AdditionalCardInfo> ImportAdditionalCardInfos(string file)
        {
            var additionalCardInfoReader = new CsvFileReader<AdditionalCardInfo>(new FileSystem(), file);
            return additionalCardInfoReader.GetAll(); 
        }

        private void ExportCharacterClassDataObjects(string fileName, List<CharacterClassDataObject> classes)
        {
            var csvFileWriter = new CsvFileWriter<CharacterClassDataObject>(new FileSystem(), fileName);
            csvFileWriter.SaveAll(classes.OrderBy(c => c.Id));
        }

        private void ExportAbilityCardDataObjects(string fileName, List<AbilityCardDataObject> cards)
        {
            var csvFileWriter = new CsvFileWriter<AbilityCardDataObject>(new FileSystem(), fileName);
            csvFileWriter.SaveAll(cards.OrderBy(c => c.Id));
        }

        private string GetCardName(AdditionalCardInfo cardInfo)
        {
            var fileName = Path.GetFileNameWithoutExtension(cardInfo.FileName);
            var splittedFileName = fileName.Split("-");
            var splittedFileNameUpper = splittedFileName.Select(ToUpperFirstChar);

            string generatedName = string.Join(" ", splittedFileNameUpper);

            if (string.IsNullOrWhiteSpace(cardInfo.Name))
            {
                return generatedName;
            }

            return $"{generatedName} ({cardInfo.Name})";
        }

        private string ToUpperFirstChar(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        private int FindClassId(IEnumerable<AdditionalClassInfo> additionalClassInfos, string abbreviation)
        {
            AdditionalClassInfo additionalClassInfo = additionalClassInfos.FirstOrDefault(aci =>
                string.Compare(aci.Abbreviation, abbreviation, StringComparison.OrdinalIgnoreCase) == 0);

            if (additionalClassInfo != null)
            {
                return additionalClassInfo.Id;
            }

            return -1;
        }

        private AdditionalCardInfo FindCard(IEnumerable<AdditionalCardInfo> additionalCardInfos, string fileName)
        {
            return additionalCardInfos.FirstOrDefault(aci =>
                string.Compare(aci.FileName, fileName, StringComparison.OrdinalIgnoreCase) == 0);
        }

        private CharacterClassDataObject ConvertToDataObject(AdditionalClassInfo info)
        {
            return new CharacterClassDataObject()
            {
                Id = info.Id,
                Name = info.Name,
                NumberOfCards = info.NumberOfCards
            };
        }

        private CharacterClassDataObject FindClassByName(IEnumerable<CharacterClassDataObject> classes, string name)
        {
            return classes.FirstOrDefault( c => c.Name.Equals(name));
        }

        private int GetNextClassId(IEnumerable<CharacterClassDataObject> classes)
        {
            return classes.Any() ? classes.Max(c => c.Id) + 1 : 1;
        }

        private int GetNextCardId(IEnumerable<AbilityCardDataObject> cards)
        {
            return cards.Any() ? cards.Max(c => c.Id) + 1 : 1;
        }
    }
}
