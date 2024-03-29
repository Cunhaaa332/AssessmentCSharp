﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pessoas.Biblioteca {
    public interface IDados {
        IEnumerable<Pessoa> BuscarTodasPessoas();

        void Deletar(int id);

        void Editar(Pessoa p);

        IEnumerable<Pessoa> BuscarTodasPessoas(string nome);

        IEnumerable<Pessoa> BuscarTodasPessoas(DateTime data);

        void ApagaRecebeECria(List<Pessoa> listaPessoasAtt);

        void Salvar(Pessoa pessoa);

        bool PessoaExistente(Pessoa pessoa);

        Pessoa BuscarPessoaPorId(int id);

        string RecebeArquivo();

        void CriarPessoa(Pessoa pessoa);
    }
}
