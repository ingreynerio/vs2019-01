using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Chinok.Data
{
    public class ArtistaDA : BaseConnection
    {
        public int GetCount()
        {
            var result = 0;
            var sql = "Select count(1) from Artist";
            /*1. Crear el objeto connection*/

            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Open();
             /*2. Creando una instancia del objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
            /*ejecutando el comando*/
                result = (int)cmd.ExecuteScalar();

            }

                return result;
        }

        public IEnumerable<Artista> Gets()
        {
            var result = new List<Artista>();
            var sql = "select ArtistId, Name from Artist";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Open();
                /*2. Creando una instancia del objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                /*ejecutando el comando*/
                var reader = cmd.ExecuteReader();
                var indice = 0;
                while (reader.Read())
                {
                    var entity = new Artista();
                    indice = reader.GetOrdinal("ArtistId");
                    entity.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    entity.Name = reader.GetString(indice);

                    result.Add(entity);
                }

            }
            return result;

        }


        public IEnumerable<Artista> GetsWithParam(string nombre)
        {
            var result = new List<Artista>();
            var sql = "select ArtistId, Name from Artist where Name LIKE @FiltroPorNombre";

            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Open();
                /*2. Creando una instancia del objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;

                cmd.Parameters.Add(new SqlParameter("@FiltroPorNombre", nombre));




                var reader = cmd.ExecuteReader();
                var indice = 0;
                while (reader.Read())
                {
                    var entity = new Artista();
                    indice = reader.GetOrdinal("ArtistId");
                    entity.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    entity.Name = reader.GetString(indice);

                    result.Add(entity);
                }

            }
            return result;

        }



        public IEnumerable<Artista> GetsWithParamSP(string nombre)
        {
            var result = new List<Artista>();
            var sql = "usp_GetArtists";

            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Open();
                /*2. Creando una instancia del objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.Add(new SqlParameter("@FiltroByName", nombre));




                var reader = cmd.ExecuteReader();
                var indice = 0;
                while (reader.Read())
                {
                    var entity = new Artista();
                    indice = reader.GetOrdinal("ArtistId");
                    entity.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    entity.Name = reader.GetString(indice);

                    result.Add(entity);
                }

            }
            return result;

        }


        public int InsertArtist(Artista Entity)
        {
            var result = 0;
            var sql = "usp_InsertArtist";

            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Open();
                /*2. Creando una instancia del objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.Add(new SqlParameter("@Name", Entity.Name));

                result = Convert.ToInt32(cmd.ExecuteScalar());


            }
            return result;

        }


    }

}
