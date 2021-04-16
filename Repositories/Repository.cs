using Dapper;
using heythem_Demo.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace heythem_Demo.Controllers
{
    public class Repository
    {
        public Repository()
        {
        }
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=DemoDB;" +
                "Trusted_Connection=True;MultipleActiveResultSets=true");

            return conn;
        }
        public int IsMember(Login log)
        {
            int id = -1;
            var connection = GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand("SELECT Id FROM [dbo].[User] where Username = @un and Password = @p", connection);
                command.Parameters.AddWithValue("@un", log.Username);
                command.Parameters.AddWithValue("@p", log.Password);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception e)
            {
                return -1;
            }
            return id;
        }

        public int addBien(Bien bien, int v)
        {
            int id = -1;
            var connection = GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("insert into  [dbo].[Bien] (Adresse , Owner ,  PostalCode , Type)  Values( @ad , @ow , @cp , @t)  SELECT SCOPE_IDENTITY()", connection);
            command.Parameters.AddWithValue("@ad", bien.Adresse);
            command.Parameters.AddWithValue("@ow", v);
            command.Parameters.AddWithValue("@cp", bien.PostalCode);
            command.Parameters.AddWithValue("@t", bien.Type);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = int.Parse(reader.GetDecimal(0).ToString());
                    }
                }
            }
            connection.Close();

            return id;
        }

        public List<Bien> getBien(Bien bien)
        {
            List<Bien> res = new List<Bien>();

            var connection = GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("Select b.Id , b.Adresse , u.Phone FROM  Bien b , [User] u  where b.PostalCode = @cp and b.Type = @type and u.id = b.Owner ", connection);
            command.Parameters.AddWithValue("@cp", int.Parse(bien.PostalCode));
            command.Parameters.AddWithValue("@type", bien.Type);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Bien obj = new Bien();

                        obj.Type = bien.Type;
                        obj.id = reader.GetInt32(0);
                        obj.Adresse = reader.GetString(1);
                        obj.OwnerTel = reader.GetString(2);
                        obj.PostalCode = bien.PostalCode;
                        res.Add(obj);


                    }
                }
            }
            connection.Close();

            return res;
        }

        internal bool Insert(Registration registrationModel)
        {
            using (var connection = GetConnection())
            {

                if (registrationModel.Password != registrationModel.Password2)
                    return false;

                try
                {
                    var Added = connection.Execute(
                    "AddMember",
                    new
                    {
                        Username = registrationModel.Username,
                        Password = registrationModel.Password,
                        Phone = registrationModel.Phone
                    },
                    commandType: CommandType.StoredProcedure
                    );
                    if (Added >0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    // si on tente une sql injection  ou que le username existe deja ça lance une exception 
                    return false;
                }

            }
        }
    }
}