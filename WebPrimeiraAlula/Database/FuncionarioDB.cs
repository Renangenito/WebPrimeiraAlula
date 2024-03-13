/*
using System.Data.SqlClient;
using WebPrimeiraAlula.Models;

namespace WebPrimeiraAlula.Database
{
    internal class FuncionarioDB
    {
        private SqlConnection connection;
        public FuncionarioDB()
        {
            connection = ConexaoSqlServer.Conectar();
        }
        internal void Inserir(FuncionarioModel funcionarioModelo)
        {
            try
            {
                string queryString = "INSERT INTO Funcionarios VALUES (@CPF, @RG, @Nome, @DataNascimento, @Telefone, @Celular, @Logradouro, @Numero, @Complemento, @Cidade, @Estado, @DataInclusao, null, @Ativo)";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@CPF", funcionarioModelo.Cpf);
                command.Parameters.AddWithValue("@RG", funcionarioModelo.Rg);
                command.Parameters.AddWithValue("@Nome", funcionarioModelo.Nome);
                command.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(funcionarioModelo.DataNascimento));
                command.Parameters.AddWithValue("@Telefone", funcionarioModelo.Telefone);
                command.Parameters.AddWithValue("@Celular", funcionarioModelo.Celular);
                command.Parameters.AddWithValue("@Logradouro", funcionarioModelo.Logradouro);
                command.Parameters.AddWithValue("@Numero", funcionarioModelo.Numero);
                command.Parameters.AddWithValue("@Complemento", funcionarioModelo.Complemento);
                command.Parameters.AddWithValue("@Cidade", funcionarioModelo.Cidade);
                command.Parameters.AddWithValue("@Estado", funcionarioModelo.Estado);
                command.Parameters.AddWithValue("@DataInclusao", DateTime.Now);
                command.Parameters.AddWithValue("@Ativo", funcionarioModelo.Ativo);

                connection.Open();
                command.ExecuteNonQuery();
                *//*connection.Close();
                connection.Dispose();*//*
            }

            catch (Exception)
            {

                *//*                throw;
                *//*
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        internal FuncionarioModel ObterDadosFuncionarios(string cpfDigitado)
        {

            FuncionarioModel modelo = new FuncionarioModel();

            try
            {
                string queryString = "SELECT * FROM Funcionarios WHERE CPF = @cpfDigitado";


                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@cpfDigitado", cpfDigitado);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    modelo.Cpf = reader["CPF"].ToString();
                    modelo.Rg = reader["RG"].ToString();
                    modelo.Nome = reader["Nome"].ToString();
                    modelo.DataNascimento = Convert.ToDateTime(reader["DataNascimento"].ToString());
                    modelo.Telefone = reader["Telefone"].ToString();
                    modelo.Celular = reader["Celular"].ToString();
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

        internal void Alterar(FuncionarioModel funcionarioModelo)
        {

            try
            {
                string queryString = "UPDATE Funcionarios SET Ativo = @Ativo, RG = @Rg, Nome = @Nome, DataNascimento = @DataNascimento, Telefone = @Telefone, Celular = @Celular, Logradouro = @Logradouro, Numero = @Numero, Complemento = @Complemento, Cidade = @Cidade, Estado = @Estado, DataAlteracao = @DataAlteracao WHERE CPF = @Cpf";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Cpf", funcionarioModelo.Cpf);
                command.Parameters.AddWithValue("@Rg", funcionarioModelo.Rg);
                command.Parameters.AddWithValue("@Nome", funcionarioModelo.Nome);
                command.Parameters.AddWithValue("@DataNascimento", funcionarioModelo.DataNascimento);
                command.Parameters.AddWithValue("@Telefone", funcionarioModelo.Telefone);
                command.Parameters.AddWithValue("@Celular", funcionarioModelo.Celular);
                command.Parameters.AddWithValue("@Logradouro", funcionarioModelo.Logradouro);
                command.Parameters.AddWithValue("@Numero", funcionarioModelo.Numero);
                command.Parameters.AddWithValue("@Complemento", funcionarioModelo.Complemento);
                command.Parameters.AddWithValue("@Cidade", funcionarioModelo.Cidade);
                command.Parameters.AddWithValue("@Estado", funcionarioModelo.Estado);
                command.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                command.Parameters.AddWithValue("@Ativo", funcionarioModelo.Ativo);



                //command.Parameters.AddWithValue("@Ativo", cbbAtivo.Text.ToUpper() == "SIM" ? true : false);  //Ternário
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception x)
            {

                *//*                throw;
                *//*
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
           

        }

        internal List<FuncionarioModel> ObterTodosFuncionarios()
        {
            List<FuncionarioModel> lFuncionarioModel = new List<FuncionarioModel>();

            try
            {
                FuncionarioModel funcionarioModel;

                string queryString = "SELECT * FROM Funcionarios";
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    funcionarioModel = new FuncionarioModel();

                    if (!reader["CPF"].Equals(DBNull.Value)) funcionarioModel.Cpf = (String)reader["CPF"];
                    if (!reader["RG"].Equals(DBNull.Value)) funcionarioModel.Rg = (String)reader["RG"];
                    if (!reader["Nome"].Equals(DBNull.Value)) funcionarioModel.Nome = (String)reader["Nome"];
                    if (!reader["DataNascimento"].Equals(DBNull.Value)) funcionarioModel.DataNascimento = (DateTime)reader["DataNascimento"];
                    if (!reader["Telefone"].Equals(DBNull.Value)) funcionarioModel.Telefone = (String)reader["Telefone"];
                    if (!reader["Celular"].Equals(DBNull.Value)) funcionarioModel.Celular = (String)reader["Celular"];
                    if (!reader["Logradouro"].Equals(DBNull.Value)) funcionarioModel.Logradouro = (String)reader["Logradouro"];
                    if (!reader["Numero"].Equals(DBNull.Value)) funcionarioModel.Numero = (String)reader["Numero"];
                    if (!reader["Complemento"].Equals(DBNull.Value)) funcionarioModel.Complemento = (String)reader["Complemento"];
                    if (!reader["Cidade"].Equals(DBNull.Value)) funcionarioModel.Cidade = (String)reader["Cidade"];
                    if (!reader["Estado"].Equals(DBNull.Value)) funcionarioModel.Estado = (String)reader["Estado"];
                    if (!reader["DataInclusao"].Equals(DBNull.Value)) funcionarioModel.DataInclusao = (DateTime)reader["DataInclusao"];
                    if (!reader["DataAlteracao"].Equals(DBNull.Value)) funcionarioModel.DataAlteracao = (DateTime)reader["DataAlteracao"];
                    if (!reader["Ativo"].Equals(DBNull.Value)) funcionarioModel.Ativo = (bool)reader["Ativo"];

                    lFuncionarioModel.Add(funcionarioModel);
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

            return lFuncionarioModel;
        }
    }
}
*/