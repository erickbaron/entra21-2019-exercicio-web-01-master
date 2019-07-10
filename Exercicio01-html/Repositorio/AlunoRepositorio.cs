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
    public class AlunoRepositorio
    {
        Conexao connection = new Conexao();

        public int Inserir(Aluno aluno)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"INSERT INTO alunos (nome, cpf, nota1, nota2, nota3)
                VALUES(@NOME, @CPF, @NOTA1, @NOTA2, @NOTA3)";
            command.Parameters.AddWithValue("@NOME", aluno.Nome);
            command.Parameters.AddWithValue("@CPF", aluno.Cpf);
            command.Parameters.AddWithValue("@NOTA1", aluno.Nota1);
            command.Parameters.AddWithValue("@NOTA2", aluno.Nota2);
            command.Parameters.AddWithValue("@NOTA3", aluno.Nota3);

            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public List<Aluno> ObterTodos(string busca)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"SELECT * FROM alunos WHERE nome LIKE @BUSCA";
            busca = $"%{busca}%";
            command.Parameters.AddWithValue("@BUSCA", busca);
            List<Aluno> alunos = new List<Aluno>();
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow linha = table.Rows[i];
                Aluno aluno = new Aluno();

                aluno.Id = Convert.ToInt32(linha["id"]);
                aluno.Nome = linha["nome"].ToString();
                aluno.Cpf = linha["cpf"].ToString();
                aluno.Nota1 = Convert.ToDecimal(linha["nota1"]);
                aluno.Nota2 = Convert.ToDecimal(linha["nota2"]);
                aluno.Nota3 = Convert.ToDecimal(linha["nota3"]);

                alunos.Add(aluno);
            }
            return alunos;
        }

        public bool Apagar(int id)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"DELETE FROM alunos WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Aluno ObterPeloId(int id)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = @"SELECT * FROM alunos WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            if (table.Rows.Count == 1)
            {
                DataRow linha = table.Rows[0];
                Aluno aluno = new Aluno();

                aluno.Id = Convert.ToInt32(linha["id"]);
                aluno.Nome = linha["nome"].ToString();
                aluno.Cpf = linha["cpf"].ToString();
                aluno.Nota1 = Convert.ToDecimal(linha["nota1"]);
                aluno.Nota2 = Convert.ToDecimal(linha["nota2"]);
                aluno.Nota3 = Convert.ToDecimal(linha["nota3"]);

                return aluno;
            }
            return null;
        }

        public bool Atualizar(Aluno aluno)
        {
            SqlCommand command = connection.Conectar();
            command.CommandText = "UPDATE alunos SET nome = @NOME, cpf = @CPF, nota1 = @NOTA1, nota2 = @NOTA2, nota3 = @NOTA3 WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", aluno.Nome);
            command.Parameters.AddWithValue("@CPF", aluno.Cpf);
            command.Parameters.AddWithValue("@NOTA1", aluno.Nota1);
            command.Parameters.AddWithValue("@NOTA2", aluno.Nota2);
            command.Parameters.AddWithValue("@NOTA3", aluno.Nota3);
            command.Parameters.AddWithValue("@ID", aluno.Id);

            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;


        }
    }
    
}
