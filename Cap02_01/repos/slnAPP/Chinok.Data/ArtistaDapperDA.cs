using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Dapper;
using System.Linq;

namespace Chinok.Data
{
    public class ArtistaDapperDA : BaseConnection
    {
        public int GetCount()
        {
            var result = 0;
            var sql = "Select count(1) from Artist";
            /*1. Crear el objeto connection*/

            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                //Uso de dapper : Single es parte de linq
                result = cn.Query<int>(sql).Single();


                //cn.Open();
                ///*2. Creando una instancia del objeto command*/
                //IDbCommand cmd = new SqlCommand(sql);
                //cmd.Connection = cn;
                ///*ejecutando el comando*/
                //result = (int)cmd.ExecuteScalar();

            }

            return result;
        }

        public IEnumerable<Artista> Gets()
        {
            var result = new List<Artista>();
            var sql = "select ArtistId, Name from Artist";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<Artista>(sql).ToList();

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
                result = cn.Query<Artista>(sql,
                    new { FiltroPorNombre = nombre }
                    ).ToList();
            }
            return result;
            //    cn.Open();
            //    /*2. Creando una instancia del objeto command*/
            //    IDbCommand cmd = new SqlCommand(sql);
            //    cmd.Connection = cn;

            //    cmd.Parameters.Add(new SqlParameter("@FiltroPorNombre", nombre));

            //    var reader = cmd.ExecuteReader();
            //    var indice = 0;
            //    while (reader.Read())
            //    {
            //        var entity = new Artista();
            //        indice = reader.GetOrdinal("ArtistId");
            //        entity.ArtistId = reader.GetInt32(indice);

            //        indice = reader.GetOrdinal("Name");
            //        entity.Name = reader.GetString(indice);

            //        result.Add(entity);
            //    }

            //}


        }



        public IEnumerable<Artista> GetsWithParamSP(string nombre)
        {
            var result = new List<Artista>();
            var sql = "usp_GetArtists";

            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<Artista>(sql,
                   new { FiltroPorNombre = nombre },
                   commandType:CommandType.StoredProcedure
                   ).ToList();


                //    cn.Open();
                //    /*2. Creando una instancia del objeto command*/
                //    IDbCommand cmd = new SqlCommand(sql);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Connection = cn;

                //    cmd.Parameters.Add(new SqlParameter("@FiltroByName", nombre));

                //    var reader = cmd.ExecuteReader();
                //    var indice = 0;
                //    while (reader.Read())
                //    {
                //        var entity = new Artista();
                //        indice = reader.GetOrdinal("ArtistId");
                //        entity.ArtistId = reader.GetInt32(indice);

                //        indice = reader.GetOrdinal("Name");
                //        entity.Name = reader.GetString(indice);

                //        result.Add(entity);
                //    }

            }
            return result;

        }


        public int InsertArtist(Artista entity)
        {
            var result = 0;
            var sql = "usp_InsertArtist";

            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<int>(sql,
                     new
                     {
                         Nombre = entity.Name
                     },
                     commandType: CommandType.StoredProcedure).Single();
            }
            return result;

        }

        //Transacción local
        public int InsertArtistTX(Artista entity)
        {
            var result = 0;
            
            //Iniciando la transacción local
            // Si es transacción local debemos tener commit y rollback
            // y usar el try catch 
            //Con el objeto transactionscope se inicia la transaccion

            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {

                try
                {
                    var sql = "usp_InsertArtist";
                    /* crea el objeto conexion*/
                    result = cn.Query(sql,
                   new { Nombre = entity.Name },
                   commandType: CommandType.StoredProcedure).Single();

                //HASTA AQUÍ ME QUEDO FALTA RESOLVER Y FALTAN HACER LOS TEST.
                }
                catch (Exception)
                {

                    transaction.Rollback();
                }

            }
            return result;

        }


        //Transacciones distribuidas
        //No necesitamos abrir conexion

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
