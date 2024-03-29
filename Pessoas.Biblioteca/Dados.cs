﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pessoas.Biblioteca {
    public class Dados : IDados {

        public IEnumerable<Pessoa> BuscarTodasPessoas() {
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
                int id = int.Parse(dadosDaPessoa[0]);
                string nome = dadosDaPessoa[1];
                string sobreNome = dadosDaPessoa[2];
                DateTime dataDeAniversario = Convert.ToDateTime(dadosDaPessoa[3]);

                //preencher a classe pessoa com esses dados
                Pessoa pessoa = new Pessoa(id, nome, sobreNome, dataDeAniversario);

                //adicionar em uma lista essa pessoa
                listaPessoasEncontradas.Add(pessoa);
            }

            //retornar a lista
            return listaPessoasEncontradas;
        }

        public void Deletar(int id) {
            var todasPessoas = BuscarTodasPessoas();
            List<Pessoa> listaPessoasAtt = new List<Pessoa>();
            foreach (var pessoa in todasPessoas) {
                if (id != pessoa.Id) {
                    listaPessoasAtt.Add(pessoa);
                } else {
                }
            }
            ApagaRecebeECria(listaPessoasAtt);
        }

        public void Editar(Pessoa p) {
            var todasPessoas = BuscarTodasPessoas();
            List<Pessoa> listaPessoasAtt = new List<Pessoa>();
            foreach (var pessoa in todasPessoas) {
                if (p.Id == pessoa.Id) {
                    listaPessoasAtt.Add(p);
                } else {
                    listaPessoasAtt.Add(pessoa);
                }
            }
            ApagaRecebeECria(listaPessoasAtt);
        }

        public IEnumerable<Pessoa> BuscarTodasPessoas(string nome) {
            return (from x in BuscarTodasPessoas()
                    where x.nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                    orderby x.nome
                    select x);
        }

        public IEnumerable<Pessoa> BuscarTodasPessoas(DateTime data) {
            return (from x in BuscarTodasPessoas()
                    where x.birth.Day == data.Day && x.birth.Month == data.Month
                    orderby x.birth
                    select x);
        }

        public void ApagaRecebeECria(List<Pessoa> listaPessoasAtt) {
            string nomeDoArquivo = RecebeArquivo();
            File.Delete(RecebeArquivo());
            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo)) {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            foreach (var pessoa in listaPessoasAtt) {
                Salvar(pessoa);
            }
        }

        public void Salvar(Pessoa pessoa) {
            // if (PessoaExistente(pessoa)) {

            // } else {
            CriarPessoa(pessoa);
            //}
        }

        public bool PessoaExistente(Pessoa pessoa) {
            var id = pessoa.Id;

            var pessoasEncontradas = BuscarPessoaPorId(id);

            if (pessoasEncontradas != null) {
                return true;
            } else {
                return false;
            }
        }

        public Pessoa BuscarPessoaPorId(int id) {

            return (from x in BuscarTodasPessoas()
                    where x.Id == id
                    select x).FirstOrDefault();
        }

        public string RecebeArquivo() {
            var pasta = Environment.SpecialFolder.Desktop;

            string pastaNoDesktop = Environment.GetFolderPath(pasta);
            string nomeDoArquivo = @"\repositorioPessoas.txt";

            return pastaNoDesktop + nomeDoArquivo;
        }

        public void CriarPessoa(Pessoa pessoa) {
            string arquivo = RecebeArquivo();

            string formatacao = $"{pessoa.Id},{pessoa.nome},{pessoa.sobreNome},{pessoa.birth};";

            File.AppendAllText(arquivo, formatacao);
        }
    }
}
