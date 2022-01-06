using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;

namespace Password
{
    public class Serializer
    {
        public string Path { get; set; } = Directory.GetCurrentDirectory();

        // Konfigurerer hvilken delimiter, der skal bruges for at adskille værdierne ad i hver række
        // i csv filen
        private readonly CsvConfiguration config = new (CultureInfo.InvariantCulture) 
        { 
            Delimiter = ";", 
            HasHeaderRecord = false 
        };

        public Serializer(string fileName)
        {
            Path += fileName;
        }

        public List<User> ReadUsers()
        {
            if (!File.Exists(Path)) { File.Create(Path); }

            // laver en reader stream som automatisk lukkes igen
            using var reader = new StreamReader(Path);

            // specificerer at det skal være et csv stream
            using var csv = new CsvReader(reader, config);

            // getrecords læser hver linje i csv filen og laver en liste med user objekter
            return csv.GetRecords<User>().ToList();
        }

        public bool Exists(string name)
        {
            var users = ReadUsers();

            // linq expression der tjekker om navnet findes i filen
            return users.Any(userFromUsers => userFromUsers.Name == name);
        }

        public List<User> FindUser(string name)
        {
            var users = ReadUsers();

            // linq der finder brugere med samme navn
            return users.Where(user => name == user.Name).ToList();
        }

        public void WriteUser(User user)
        {
            using var reader = new StreamWriter(Path, true);
            using var csv = new CsvWriter(reader, config);

            // skriver brugeren til sidst i filen
            csv.WriteRecord(user);

            // laver en ny linje. Kræves med dette bibliotek
            csv.NextRecord();
        }
    }
}
