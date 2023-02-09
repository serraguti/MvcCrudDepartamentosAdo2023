using MvcCrudDepartamentosAdo.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcCrudDepartamentosAdo.Repositories
{
    public class RepositoryDepartamentos
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryDepartamentos()
        {
            string connectionString =
                @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Departamento> GetDepartamentos()
        {
            string sql = "select * from dept";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<Departamento> departamentos = new List<Departamento>();
            while (this.reader.Read())
            {
                Departamento dept = new Departamento();
                dept.IdDepartamento = int.Parse(this.reader["DEPT_NO"].ToString());
                dept.Nombre = this.reader["DNOMBRE"].ToString();
                dept.Localidad = this.reader["LOC"].ToString();
                departamentos.Add(dept);
            }
            this.reader.Close();
            this.cn.Close();
            return departamentos;
        }

        public Departamento FindDepartamento(int id)
        {
            string sql = "select * from dept where dept_no=@iddepartamento";
            SqlParameter pamid = new SqlParameter("@iddepartamento", id);
            this.com.Parameters.Add(pamid);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.reader.Read();
            Departamento departamento = new Departamento();
            departamento.IdDepartamento = int.Parse(this.reader["DEPT_NO"].ToString());
            departamento.Nombre = this.reader["DNOMBRE"].ToString();
            departamento.Localidad = this.reader["LOC"].ToString();
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return departamento;
        }

        public void InsertDepartamento(int id, string nombre, string localidad)
        {
            string sql = "insert into dept values (@ID, @NOMBRE, @LOCALIDAD)";
            SqlParameter pamid = new SqlParameter("@ID", id);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamloc = new SqlParameter("@LOCALIDAD", localidad);
            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamloc);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void UpdateDepartamento
            (int id, string nombre, string localidad)
        {
            string sql = "update dept set dnombre=@NOMBRE, loc=@LOC "
                + " where dept_no=@ID";
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamid = new SqlParameter("@ID", id);
            SqlParameter pamloc = new SqlParameter("@LOC", localidad);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamloc);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void DeleteDepartamento(int id)
        {
            string sql = "delete from dept where dept_no=@id";
            SqlParameter pamid = new SqlParameter("@id", id);
            this.com.Parameters.Add(pamid);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
