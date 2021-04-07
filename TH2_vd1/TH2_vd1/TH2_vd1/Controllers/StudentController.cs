using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TH2_vd1.Models;

namespace TH2_vd1.Controllers
{
    public class StudentController : ApiController
    {
        private SqlConnection _con;
        private SqlDataAdapter _adapter;
        // GET api/<controller>
        public IEnumerable<Student> Get(int id)
        {
            _con = new SqlConnection(@"Data Source=DESKTOP-QRT4B8G\SQLEXPRESS01;Initial Catalog=Nawab;Integrated Security=True");
            DataTable _dt = new DataTable();
            var query = "select*from student";

            _adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(query , _con)
            };
            _adapter.Fill(_dt);
            List<Student> students = new List<Models.Student>(_dt.Rows.Count);
            if(_dt.Rows.Count > 0)
            {
                foreach(DataRow studentReocord in _dt.Rows)
                {
                    students.Add(new ReadStudent(studentReocord));
                }
            }


            return students;
        }

        //GET api/<controller>/5
        public IEnumerable<Student> get(int id)
        {
            _con = new SqlConnection(@"Data Source=DESKTOP-QRT4B8G\SQLEXPRESS01;Initial Catalog=Nawab;Integrated Security=True");
            DataTable _dt = new DataTable();
            var query = "select*from student  where id +" + id;

            _adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(query, _con)
            };
            _adapter.Fill(_dt);
            List<Student> students = new List<Models.Student>(_dt.Rows.Count);
            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow studentReocord in _dt.Rows)
                {
                    students.Add(new ReadStudent(studentReocord));
                }
            }


            return students;
        }
                // POST api/<controller>
                public string Post([FromBody] CreateStudent value)
        {
            _con = new SqlConnection(@"Data Source=DESKTOP-QRT4B8G\SQLEXPRESS01;Initial Catalog=Nawab;Integrated Security=True");
            var query = "insert into student(f_name ,m_name,l_name,address,brthDate,score) value(@f_name ,@m_name,@l_name,@address,@brthDate,@score";
            SqlCommand insertCommad = new SqlCommand(query, _con);
            insertCommad.Parameters.AddWithValue("f_name" , value.f_name);
            insertCommad.Parameters.AddWithValue("m_name" , value.m_name);
            insertCommad.Parameters.AddWithValue("l_name", value.l_name);
            insertCommad.Parameters.AddWithValue("address", value.address);
            insertCommad.Parameters.AddWithValue("brthDate", value.brthDate);
            insertCommad.Parameters.AddWithValue("score" , value.score);

            _con.Open();
            int result = insertCommad.ExecuteNonQuery();
            if (result >0)
            {
                return "them thanh cong";
            }
            else
            {
                return  "them that bai";
            }
        }

        // PUT api/<controller>/5
        public string Put(int id, [FromBody] CreateStudent value)
        {
            _con = new SqlConnection(@"Data Source=DESKTOP-QRT4B8G\SQLEXPRESS01;Initial Catalog=Nawab;Integrated Security=True");
            var query = "update student set f_name=@f_name ,m_name= @m_name,l_name=@l_name,address=@address,brthDate=@brthDate,score=@score where id=" +id;

            SqlCommand insertCommad = new SqlCommand(query, _con);
            insertCommad.Parameters.AddWithValue("f_name", value.f_name);
            insertCommad.Parameters.AddWithValue("m_name", value.m_name);
            insertCommad.Parameters.AddWithValue("l_name", value.l_name);
            insertCommad.Parameters.AddWithValue("address", value.address);
            insertCommad.Parameters.AddWithValue("brthDate", value.brthDate);
            insertCommad.Parameters.AddWithValue("score", value.score);

            _con.Open();
            int result = insertCommad.ExecuteNonQuery();
            if (result > 0)
            {
                return "sua thanh cong";
            }
            else
            {
                return "sua that bai";
            }
        }

        // DELETE api/<controller>/5
        public string Delete(int id)
        {
            _con = new SqlConnection(@"Data Source=DESKTOP-QRT4B8G\SQLEXPRESS01;Initial Catalog=Nawab;Integrated Security=True");
            var query = "delete from student  where id=" + id;
            SqlCommand insertCommad = new SqlCommand(query, _con);


            _con.Open();
            int result = insertCommad.ExecuteNonQuery();
            if (result > 0)
            {
                return "xoa thanh cong";
            }
            else
            {
                return "xoa that bai";
            }
        }
    }
}