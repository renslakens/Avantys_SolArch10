using System.Text.Json;
using ExamManagement.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using ScheduleManagement.Models;

namespace ExamManagement.Repositories;

public class ReadStoreRepository {
    private readonly IMongoCollection<Schedule> _scheduleCollection;
    private readonly IMongoCollection<Student> _studentCollection;
    private readonly IMongoCollection<Class> _classCollection;
    private readonly IMongoCollection<Lesson>  _lessonCollection;
    private readonly IMongoCollection<Teacher> _teacherCollection;
    private readonly IMongoCollection<Module> _moduleCollection;

    public ReadStoreRepository()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_ADDRESS"));
        var database = client.GetDatabase("Schedule");

        _scheduleCollection = database.GetCollection<Schedule>("Schedules");
        _studentCollection = database.GetCollection<Student>("Students");
        _classCollection = database.GetCollection<Class>("Classes");
        _lessonCollection = database.GetCollection<Lesson>("Lessons");
        _teacherCollection = database.GetCollection<Teacher>("Teachers");
        _moduleCollection = database.GetCollection<Module>("Modules");
    }
    
            public async Task<bool> HandleMessageAsync(string messageType, string message)
            {
                try
                {
                    switch (messageType)
                    {
                        case "scheduleSend":
                            // Deserialize the JSON message into ExamScheduled object
                            var jsonObj = JsonSerializer.Deserialize<Schedule>(message, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                            });
                            if (jsonObj == null)
                            {
                                Console.WriteLine("Deserialized JSON object is null.");
                                return false;
                            }
    
                            await HandleScheduleCreatedAsync(jsonObj);
                            return true;
                        case "studentCreated":
                            // Deserialize the JSON message into ExamScheduled object
                            var jsonObj2 = JsonSerializer.Deserialize<Student>(message, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                            });
                            
                            if (jsonObj2 == null)
                            {
                                Console.WriteLine("Deserialized JSON object is null.");
                                return false;
                            }
                            
                            await HandleStudentCreatedAsync(jsonObj2);
                            return true;
                        case "classCreated":
                            // Deserialize the JSON message into ExamScheduled object
                            var jsonObj3 = JsonSerializer.Deserialize<Class>(message, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                            });
                            
                            if (jsonObj3 == null)
                            {
                                Console.WriteLine("Deserialized JSON object is null.");
                                return false;
                            }
                            
                            await HandleClassCreatedAsync(jsonObj3);
                            return true;
                        case "lessonCreated":
                            // Deserialize the JSON message into ExamScheduled object
                            var jsonObj4 = JsonSerializer.Deserialize<Lesson>(message, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                            });
                            
                            if (jsonObj4 == null)
                            {
                                Console.WriteLine("Deserialized JSON object is null.");
                                return false;
                            }
                            
                            await HandleLessonCreatedAsync(jsonObj4);
                            return true;
                        case "teacherCreated":
                            // Deserialize the JSON message into ExamScheduled object
                            var jsonObj5 = JsonSerializer.Deserialize<Teacher>(message, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                            });
                            
                            if (jsonObj5 == null)
                            {
                                Console.WriteLine("Deserialized JSON object is null.");
                                return false;
                            }
                            
                            await HandleTeacherCreatedAsync(jsonObj5);
                            return true;
                        case "moduleCreated":
                            // Deserialize the JSON message into ExamScheduled object
                            var jsonObj6 = JsonSerializer.Deserialize<Module>(message, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                            });
                            
                            if (jsonObj6 == null)
                            {
                                Console.WriteLine("Deserialized JSON object is null.");
                                return false;
                            }
                            
                            await HandleModuleCreatedAsync(jsonObj6);
                            return true;
                        case "examScheduled":
                            var jsonObj7 = JsonSerializer.Deserialize<Exam>(message, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                            });
                            
                            if (jsonObj7 == null)
                            {
                                Console.WriteLine("Deserialized JSON object is null.");
                                return false;
                            }
                            
                            await HandleExamScheduledAsync(jsonObj7);
                            return true;
                        case "newStudents":
                            var jsonObj8 = JsonSerializer.Deserialize<List<Student>>(message, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                            });
                            
                            if (jsonObj8 == null)
                            {
                                Console.WriteLine("Deserialized JSON object is null.");
                                return false;
                            }
                            
                            foreach (var student in jsonObj8)
                            {   
                                await HandleStudentCreatedAsync(student);
                            }
                            return true;
                        default:
                            return false;
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            
            public async Task<bool> HandleScheduleCreatedAsync(Schedule schedule)
            {
                try
                {
    
                    // // Ensure ScheduledDate is in UTC
                    // examScheduled.ScheduledDate = examScheduled.ScheduledDate.ToUniversalTime();
   
                    await _scheduleCollection.ReplaceOneAsync(x => x.Class.ScheduleCode == schedule.Class.ScheduleCode, schedule, new ReplaceOptions { IsUpsert = true });
                    
                    Console.WriteLine($"Inserted Schedule with Id: {schedule.Id}");
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

            public async Task<bool> HandleStudentCreatedAsync(Student student) {
                try {
                    await _studentCollection.ReplaceOneAsync(x => x.Id == student.Id, student, new ReplaceOptions { IsUpsert = true });
                    Console.WriteLine($"Inserted Student with Id: {student.Id}");
                    return true;
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
            public async Task<bool> HandleClassCreatedAsync(Class classObj) {
                try {
                    
                    await _classCollection.ReplaceOneAsync(x => x.Id == classObj.Id, classObj, new ReplaceOptions { IsUpsert = true });
                    Console.WriteLine($"Inserted Class with Id: {classObj.Id}");
                    return true;
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
            public async Task<bool> HandleLessonCreatedAsync(Lesson lesson) {
                try {
                    lesson.EndDateTime = lesson.EndDateTime.ToUniversalTime();
                    lesson.StartDateTime = lesson.StartDateTime.ToUniversalTime();
                    
                    await _lessonCollection.ReplaceOneAsync(x => x.Id == lesson.Id, lesson, new ReplaceOptions { IsUpsert = true });
                    Console.WriteLine($"Inserted Lesson with Id: {lesson.Id}");
                    return true;
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
            public async Task<bool> HandleTeacherCreatedAsync(Teacher teacher) {
                try {
                    await _teacherCollection.ReplaceOneAsync(x => x.Id == teacher.Id, teacher, new ReplaceOptions { IsUpsert = true });
                    Console.WriteLine($"Inserted Teacher with Id: {teacher.Id}");
                    return true;
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
            public async Task<bool> HandleModuleCreatedAsync(Module module) {
                try {
                    await _moduleCollection.ReplaceOneAsync(x => x.Id == module.Id, module, new ReplaceOptions { IsUpsert = true });
                    Console.WriteLine($"Inserted Module with Id: {module.Id}");
                    return true;
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
            public async Task<bool> HandleExamScheduledAsync(Exam exam) {
                try {
                    await _studentCollection.UpdateOneAsync(x => x.Id.ToString() == exam.StudentId, Builders<Student>.Update.Push(x => x.Exams, exam));
                    Console.WriteLine($"Updated Student with Id: {exam.StudentId} with Exam Id: {exam.Id}");
                    return true;
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
            }
}