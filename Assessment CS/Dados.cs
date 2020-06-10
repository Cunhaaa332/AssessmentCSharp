﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assessment_CS {
    class Dados {

        public static IEnumerable<Pessoa> BuscarTodasPessoas() {
            string nomeDoArquivo = RecebeArquivo();

            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo)) {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(nomeDoArquivo);

            //identificar cada pessoa
            string[] pessoas = resultado.Split(';');

            List<Pessoa> listaPessoasEncontradas = new List<Pessoa>();

            for (int i = 0; i < pessoas.Length - 1; i++) {
                string[] dadosDaPessoa = pessoas[i].Split(',');

                //identificar cada dado da pessoa
                string nome = dadosDaPessoa[0];
                string sobreNome = dadosDaPessoa[1];
                DateTime dataDeAniversario = Convert.ToDateTime(dadosDaPessoa[2]);

                //preencher a classe pessoa com esses dados
                Pessoa pessoa = new Pessoa(nome, sobreNome, dataDeAniversario);

                //adicionar em uma lista essa pessoa
                listaPessoasEncontradas.Add(pessoa);
            }

            //retornar a lista
            return listaPessoasEncontradas;
        }

        public static IEnumerable<Pessoa> BuscarTodasPessoas(string nome) {
            return (from x in BuscarTodasPessoas()
                    where x.nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                    orderby x.nome
                    select x);
        }

        public static void Salvar(Pessoa pessoa) {
            if (PessoaExistente(pessoa)) {
             
            } else {
                CriarPessoa(pessoa);
            }
        }

        private static bool PessoaExistente(Pessoa pessoa) {
            var id = pessoa.Id;

            var pessoasEncontradas = BuscarPessoaPorId(id);

            if (pessoasEncontradas != null) {
                return true;
            } else {
                return false;
            }
        }

        public static Pessoa BuscarPessoaPorId(int id) {
       
            return (from x in BuscarTodasPessoas()
                    where x.Id == id
                    select x).FirstOrDefault();
        }

        private static string RecebeArquivo() {
            var pasta = Environment.SpecialFolder.Desktop;

            string pastaNoDesktop = Environment.GetFolderPath(pasta);
            string nomeDoArquivo = @"\repositorioPessoas.txt";

            return pastaNoDesktop + nomeDoArquivo;
        }

        public static void CriarPessoa(Pessoa pessoa) {
            string arquivo = RecebeArquivo();

            string formatacao = $"{pessoa.nome},{pessoa.sobreNome},{pessoa.birth};";

            File.AppendAllText(arquivo, formatacao);
        }
    }
}