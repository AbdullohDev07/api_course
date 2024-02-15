using Microsoft.AspNetCore.Mvc;
using Dapper;
using WebApplication1.Roots;
using Npgsql;


using System;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        public string connectionString = "Server=localhost;Port=16172;Database=console_task;username=postgres;Password=axihub;";

        [HttpGet]
        public List<Student> GetListStudents()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select * from students;";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                List<Student> list = new List<Student>();

                while (result.Read())
                {
                    list.Add(new Student
                    {
                        id = (int)result[0],
                        name = (string)result[1],
                        courses = GetStudentCourses((int)result[0])
                    });
                }
                return list;
            }
        }
        private List<Course> GetStudentCourses(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select c.id, c.name from students s join student_course sc on sc.student_id = s.id join courses c on sc.course_id = c.id where s.id = {id};";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                List<Course> list = new List<Course>();

                while (result.Read())
                {
                    list.Add(new Course
                    {
                        id = (int)result[0],
                        name = (string)result[1]
                    });
                }
                return list;
            }
        }

        [HttpGet]
        public List<Course> GetListCourses()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select * from courses;";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                List<Course> list = new List<Course>();

                while (result.Read())
                {
                    list.Add(new Course
                    {
                        id = (int)result[0],
                        name = (string)result[1],
                        students = GetCourseStudents((int)result[0])
                    });
                }
                return list;
            }
        }
        private List<Student> GetCourseStudents(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select s.id, s.name from students s join student_course sc on sc.student_id = s.id join courses c on sc.course_id = c.id where c.id = {id};";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                List<Student> list = new List<Student>();

                while (result.Read())
                {
                    list.Add(new Student
                    {
                        id = (int)result[0],
                        name = (string)result[1]
                    });
                }
                return list;
            }
        }
    }
}
