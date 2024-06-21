using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using ExamManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExamManagement.Services
{
    public class StudentsService
    {
        private readonly IMongoCollection<Student> _StudentsCollection;

        public StudentsService(IOptions<ExamManagementDatabaseSettings> examManagementDatabaseSettings)
        {
            var client = new MongoClient(examManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(examManagementDatabaseSettings.Value.DatabaseName);

            _StudentsCollection = database.GetCollection<Student>(examManagementDatabaseSettings.Value.Collections.StudentsCollectionName);
        }

        public async Task FetchStudentsAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://marcavans.blob.core.windows.net/solarch/fake_customer_data_export.csv?sv=2023-01-03&st=2024-06-14T10%3A31%3A07Z&se=2032-06-15T10%3A31%3A00Z&sr=b&sp=r&sig=q4Ie3kKpguMakW6sbcKl0KAWutzpMi747O4yIr8lQLI%3D");
                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsStringAsync();

                    var records = ParseCsv(data);

                    var Students = new List<Student>();
                    foreach (var record in records)
                    {
                        var filter = Builders<Student>.Filter.And(
                            Builders<Student>.Filter.Eq(p => p.CompanyName, record.CompanyName),
                            Builders<Student>.Filter.Eq(p => p.FirstName, record.FirstName),
                            Builders<Student>.Filter.Eq(p => p.LastName, record.LastName),
                            Builders<Student>.Filter.Eq(p => p.PhoneNumber, record.PhoneNumber),
                            Builders<Student>.Filter.Eq(p => p.Address, record.Address)
                        );

                        var existingStudent = await _StudentsCollection.Find(filter).FirstOrDefaultAsync();

                        if (existingStudent == null)
                        {
                            var Student = new Student
                            {
                                CompanyName = record.CompanyName,
                                FirstName = record.FirstName,
                                LastName = record.LastName,
                                PhoneNumber = record.PhoneNumber,
                                Address = record.Address
                            };
                            Students.Add(Student);
                        }
                    }

                    if (Students.Any())
                    {
                        await _StudentsCollection.InsertManyAsync(Students);
                        Console.WriteLine("Data successfully inserted into the MongoDB collection.");
                    }
                    else
                    {
                        Console.WriteLine("No new data to insert into the MongoDB collection.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private IEnumerable<ContactRecord> ParseCsv(string data)
        {
            using (var reader = new StringReader(data))
            using (var csv = new CsvReader(reader, new CsvConfiguration()))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.TrimHeaders = true;
                csv.Configuration.IgnoreHeaderWhiteSpace = true;

                var records = csv.GetRecords<ContactRecord>().ToList();
                return records;
            }
        }
    }

    public class ContactRecord
    {

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }

}