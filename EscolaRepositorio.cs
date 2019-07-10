using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class EscolaRepositorio
    {
        Conexao connection = new Conexao();

        public int Inserir(Escola escola)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"INSERT INTO escolas(nome) VALUES (@NOME)";
            command.Parameters.AddWithValue("@NOME", escola.Nome);

            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public List<Escola> ObterTodos(string busca)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"SELECT * FROM escolas WHERE nome LIKE @BUSCA";
            busca = $"%{busca}%";
            command.Parameters.AddWithValue("@BUSCA", busca);

            List<Escola> escolas = new List<Escola>();
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow linha = table.Rows[i];
                Escola escola = new Escola();

                escola.Id = Convert.ToInt32(linha["id"]);
                escola.Nome = linha["nome"].ToString();

                escolas.Add(escola);
            }
            return escolas;
        }

        public bool Apagar(int id)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"DELETE FROM escolas WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Escola ObterPeloId(int id)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"SELECT * FROM escolas WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if(table.Rows.Count == 1)
            {
                DataRow linha = table.Rows[0];
                Escola escola = new Escola();

                escola.Id = Convert.ToInt32(linha["id"]);
                escola.Nome = linha["nome"].ToString();

                return escola;
            }
            return null;
        }

        public bool Atualizar(Escola escola)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = "UPDATE escolas SET nome = @NOME WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", escola.Nome);
            command.Parameters.AddWithValue("@ID", escola.Id);

            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
            

        }
    }

}
