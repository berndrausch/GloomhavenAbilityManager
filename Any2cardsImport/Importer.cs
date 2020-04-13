using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Csv;

namespace GloomhavenAbilityManager.Any2cardsImport
{
    public class Importer
    {
        public void Import(IEnumerable<FileInfo> cardImageFiles)
        {
            string additionalClassInfoFile = @"Any2cardsImport\additionalclassinfo.csv";
            string additionalCardInfoFile = @"Any2cardsImport\additionalcardinfo.csv";
            
            var additionalClassInfoReader = new CsvFileReader<AdditionalClassInfo>(new FileSystem(), additionalClassInfoFile);
            var additionalClassInfos = additionalClassInfoReader.GetAll();

            var additionalCardInfoReader = new CsvFileReader<AdditionalCardInfo>(new FileSystem(), additionalCardInfoFile);
            var additionalCardInfos = additionalCardInfoReader.GetAll();

            var cards = new List<AbilityCardDataObject>();
            var classes = new List<CharacterClassDataObject>();

            foreach(FileInfo file in cardImageFiles)
            {
                string className = file.Directory.Name;
                
                CharacterClassDataObject classInfo = FindClassByName(classes, className);
                if (classInfo == null)
                {
                    classInfo = new CharacterClassDataObject
                    {
                        Id = GetNextClassId(classes),
                        Name = className
                    };

                    classes.Add(classInfo);
                }

                AbilityCardDataObject cardInfo = new AbilityCardDataObject
                {
                    Id = GetNextCardId(cards),
                    Name = Path.GetFileNameWithoutExtension(file.Name),
                    ClassId = classInfo.Id,
                };

                cards.Add(cardInfo);
            }
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
