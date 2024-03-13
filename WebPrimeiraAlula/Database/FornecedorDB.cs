/*
using System.Data.SqlClient;
using WebPrimeiraAlula.Database;
using WebPrimeiraAlula.Models;

namespace WebPrimeiraAula.Database
{
    internal class FornecedorDB
    {

        private SqlConnection connection;
        public FornecedorDB()
        {
            connection = ConexaoSqlServer.Conectar();
        }


        internal void Inserir(FornecedorModel fornecedorModelo)
        {

            try
            {
                string queryString = "INSERT INTO Fornecedores VALUES (@CNPJ, @Nome, @Telefone, @Logradouro, @Numero, @Complemento, @Cidade, @Estado, @DataInclusao, null, @Ativo)";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@CNPJ", fornecedorModelo.CNPJ);
                command.Parameters.AddWithValue("@Nome", fornecedorModelo.Nome);
                command.Parameters.AddWithValue("@Telefone", fornecedorModelo.Telefone);
                command.Parameters.AddWithValue("@Logradouro", fornecedorModelo.Logradouro);
                command.Parameters.AddWithValue("@Numero", fornecedorModelo.Numero);
                command.Parameters.AddWithValue("@Complemento", fornecedorModelo.Complemento);
                command.Parameters.AddWithValue("@Cidade", fornecedorModelo.Cidade);
                command.Parameters.AddWithValue("@Estado", fornecedorModelo.Estado);
                command.Parameters.AddWithValue("@DataInclusao", DateTime.Now);
                command.Parameters.AddWithValue("@Ativo", fornecedorModelo.Ativo);

                connection.Open();
                command.ExecuteNonQuery();
               

            }
            catch (Exception)
            {
                *//*throw;*//*

            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        internal FornecedorModel ObterDadosFornecedores(string cnpjDigitado)
        {
            FornecedorModel modelo = new FornecedorModel();

            try
            {
                string queryString = "SELECT * FROM Fornecedores WHERE CNPJ = @cnpjDigitado";
                SqlCommand command = new SqlCommand(queryString, connection);

                _ = command.Parameters.AddWithValue("@cnpjDigitado", cnpjDigitado);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    modelo.CNPJ = reader["CNPJ"].ToString();
                    modelo.Nome = reader["Nome"].ToString();
                    modelo.Telefone = reader["Telefone"].ToString();
                    modelo.Logradouro = reader["Logradouro"].ToString();
                    modelo.Numero = reader["Numero"].ToString();
                    modelo.Complemento = reader["Complemento"].ToString();
                    modelo.Cidade = reader["Cidade"].ToString();
                    modelo.Estado = reader["Estado"].ToString();
                    modelo.DataInclusao = Convert.ToDateTime(reader["DataInclusao"].ToString());
                    modelo.DataAlteracao = Convert.ToDateTime(reader["DataAlteracao"].ToString());
                    modelo.Ativo = Convert.ToBoolean(reader["Ativo"].ToString());

                }
                else
                {
                  

                }
                reader.Close();
              

            }
            catch (Exception)
            {
                *//*throw;*//*
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return modelo;

        }

        internal void Alterar(FornecedorModel fornecedorModelo)
        {
            try
            {


                string queryString = "UPDATE Fornecedores SET CNPJ = @Cnpj, Ativo = @Ativo, Nome = @Nome, Telefone = @Telefone, Logradouro = @Logradouro, Numero = @Numero, Complemento = @Complemento, Cidade = @Cidade, Estado = @Estado,DataAlteracao = @DataAlteracao WHERE CNPJ = @cnpj";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Cnpj", fornecedorModelo.CNPJ);
                command.Parameters.AddWithValue("@Nome", fornecedorModelo.Nome);
                command.Parameters.AddWithValue("@Telefone", fornecedorModelo.Telefone);
                command.Parameters.AddWithValue("@Logradouro", fornecedorModelo.Logradouro);
                command.Parameters.AddWithValue("@Numero", fornecedorModelo.Numero);
                command.Parameters.AddWithValue("@Complemento", fornecedorModelo.Complemento);
                command.Parameters.AddWithValue("@Cidade", fornecedorModelo.Cidade);
                command.Parameters.AddWithValue("@Estado", fornecedorModelo.Estado);
                command.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                command.Parameters.AddWithValue("@Ativo", fornecedorModelo.Ativo);

               
                //command.Parameters.AddWithValue("@Ativo", cbbAtivo.Text.ToUpper() == "SIM" ? true : false);  //Ternário
                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                *//*throw;*//*
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        internal List<FornecedorModel> ObterTodosFornecedores()
        {
            List<FornecedorModel> lFornecedorModel = new List<FornecedorModel>();

            try
            {
                FornecedorModel fornecedorModel;

                string queryString = "SELECT * FROM Fornecedores";
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    fornecedorModel = new FornecedorModel();

                    if (!reader["CNPJ"].Equals(DBNull.Value)) fornecedorModel.CNPJ = (String)reader["CNPJ"];
                    if (!reader["Nome"].Equals(DBNull.Value)) fornecedorModel.Nome = (String)reader["Nome"];
                    if (!reader["Telefone"].Equals(DBNull.Value)) fornecedorModel.Telefone = (String)reader["Telefone"];
                    if (!reader["Logradouro"].Equals(DBNull.Value)) fornecedorModel.Logradouro = (String)reader["Logradouro"];
                    if (!reader["Numero"].Equals(DBNull.Value)) fornecedorModel.Numero = (String)reader["Numero"];
                    if (!reader["Complemento"].Equals(DBNull.Value)) fornecedorModel.Complemento = (String)reader["Complemento"];
                    if (!reader["Cidade"].Equals(DBNull.Value)) fornecedorModel.Cidade = (String)reader["Cidade"];
                    if (!reader["Estado"].Equals(DBNull.Value)) fornecedorModel.Estado = (String)reader["Estado"];
                    if (!reader["DataInclusao"].Equals(DBNull.Value)) fornecedorModel.DataInclusao = (DateTime)reader["DataInclusao"];
                    if (!reader["DataAlteracao"].Equals(DBNull.Value)) fornecedorModel.DataAlteracao = (DateTime)reader["DataAlteracao"];
                    if (!reader["Ativo"].Equals(DBNull.Value)) fornecedorModel.Ativo = (bool)reader["Ativo"];

                    lFornecedorModel.Add(fornecedorModel);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            return lFornecedorModel;
        }

    }
}
*/