using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

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

        public int InsertArtistTX(Artista Entity)
        {
            var result = 0;
            var sql = "usp_InsertArtist";
            
            //Iniciando la transacción local
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Open();
                //Iniciando la transacción local
                var transaccion = cn.BeginTransaction();

                //Bloque de transacción de excepciones 
                try
                {
                   
                    /*2. Creando una instancia del objeto command*/
                    IDbCommand cmd = new SqlCommand(sql);
                    //le asigno el bloque de transacción
                    cmd.Transaction = transaccion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cn;

                    cmd.Parameters.Add(new SqlParameter("@Name", Entity.Name));

                    result = Convert.ToInt32(cmd.ExecuteScalar());

                  //  throw new Exception("Error de transacción");

                    //Con el método commit se confirma la transacción
                    transaccion.Commit(); //siempre y cuando estemos de locales.
               

                }
                catch (Exception ex)
                {

                    transaccion.Rollback();
                }

               

              

            }
            return result;

        }


        public int InsertArtistTXDist(Artista Entity)
        {
            var result = 0;

            using (var tx = new TransactionScope())
            {

                try
                {
                    var sql = "usp_InsertArtist";

                    using (IDbConnection cn = new SqlConnection(GetConnection()))
                    {
                        cn.Open();
                        IDbCommand cmd = new SqlCommand(sql);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = cn;

                        cmd.Parameters.Add(new SqlParameter("@Name", Entity.Name));

                        //ejecutando el comando bloquear tabla
                        result = Convert.ToInt32(cmd.ExecuteScalar());

                    }

                    tx.Complete();

                  

                }
                catch (Exception ex)
                {

                }
                return result;

            }

        }







            public int UpdateArtist(Artista Entity)
            {
                var result = 0;
                var sql = "usp_UpdatetArtist";

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

            public int DeleteArtist(Artista Entity)
            {
                var result = 0;
                var sql = "usp_DeleteArtist";

                using (IDbConnection cn = new SqlConnection(GetConnection()))
                {
                    cn.Open();
                    /*2. Creando una instancia del objeto command*/
                    IDbCommand cmd = new SqlCommand(sql);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cn;

                    cmd.Parameters.Add(new SqlParameter("@ArtistId", Entity.ArtistId));

                    result = Convert.ToInt32(cmd.ExecuteScalar());


                }
                return result;


            }

        }
}
