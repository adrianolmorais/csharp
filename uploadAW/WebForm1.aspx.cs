using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace uploadAW
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public IFileManager fileManager = new FileManager();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string[] caminhoArquivos = Directory.GetFiles(Server.MapPath("~/planilhatatica/"));
                var lista = new List<string>();
                DateTime dateArchive;
                if (caminhoArquivos.Any())
                {
                    foreach (string caminhoArquivo in caminhoArquivos)
                    {
                        string nomeArquivo = Path.GetFileName(caminhoArquivo);
                        lista.Add(nomeArquivo);
                    }

                    if (lista[0] != null)
                    {
                        dateArchive = File.GetLastWriteTime(Server.MapPath("~/planilhatatica/planilha_tatica"));
                    }
                    else
                    {
                        dateArchive = File.GetCreationTime(Server.MapPath("~/planilhatatica/planilha_tatica"));
                    }

                    lblmsg.Text = "Última data de processamento da planilha: " + dateArchive;
                }
            }
        }

        private bool UploadArquivo(string nomeArquivo, long tamanhoArquivo)
        {
            fileManager.saveAsArchive(FileUpload1, nomeArquivo);
            //lblmsg.Text = "Último arquivo processado com sucesso: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt") + " tamanho: " + tamanhoArquivo.ToString() + " bytes.";
            fileManager.mensagem(lblmsg, "Último arquivo processado com sucesso: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt") + " tamanho: " + tamanhoArquivo.ToString() + " bytes.");
            return true;
        }

        public void Ler(string nomeArquivo)
        {
            List<string> lista = new List<string>();

            using (StreamReader streamReader = fileManager.ler(nomeArquivo))
            {
                List<string> linhasAquivo = new List<string>();
                List<string[]> listRegistrosArquivo = new List<string[]>();
                string[] camposPlanilaTatica;
                HashSet<string> anosMesesVigenciaArquivo = new HashSet<string>();

                while (fileManager.possuiRegistro(streamReader))
                {
                    linhasAquivo.Add(fileManager.proximaLinha(streamReader));
                }

                foreach (var linhaArquivo in linhasAquivo)
                {

                    camposPlanilaTatica = linhaArquivo.Split(';');

                    listRegistrosArquivo.Add(camposPlanilaTatica);


                }

                anosMesesVigenciaArquivo = getAnoMesVigenciaArquivo(listRegistrosArquivo);

                deleteAnoMesVigenciaArquivo(anosMesesVigenciaArquivo);
                insertPlanilhaEligibilidade(listRegistrosArquivo);
            }

        }

        private void insertPlanilhaEligibilidade(List<string[]> listRegistrosArquivo)
        {
            foreach (var linhaArquivo in listRegistrosArquivo)
            {
                var cnpj = linhaArquivo[0];
                var funcional = linhaArquivo[1];
                var anoMes = linhaArquivo[2];
                var prioridadeHunter = linhaArquivo[3];
                var ano = anoMes.Substring(0, 4);
                var mes = anoMes.Substring(4, 2);

                if (cnpj.Length == 14)
                {
                    if (funcional.Length == 9)
                    {
                        anoMes.ToString();
                        Console.WriteLine(ano + "-" + mes + "-" + "01");
                    }
                    else
                    {
                        fileManager.mensagem(lblmsg, "A funcional deve ter 9 characters, verifique o arquivo.");
                    }
                }
                else
                {
                    fileManager.mensagem(lblmsg, "O CNPJ deve possui 14 characters, verifique o arquivo.");
                }
            }

        }

        private void deleteAnoMesVigenciaArquivo(HashSet<string> anosMesesVigenciaArquivo)
        {
            foreach (string anoMesVigenciaArquivo in anosMesesVigenciaArquivo)
            {
                Console.WriteLine(anoMesVigenciaArquivo);
            }
        }

        public HashSet<string> getAnoMesVigenciaArquivo(List<string[]> listaArray)
        {
            HashSet<string> anoMesVigencia = new HashSet<string>();
            foreach (string[] array in listaArray)
            {
                var anoMes = array[2];
                var ano = anoMes.Substring(0, 4);
                var mes = anoMes.Substring(4, 2);

                anoMesVigencia.Add(ano + "-" + mes + "-");
            }
            return anoMesVigencia;
        }

        public void btnEnviarArquivo_Click(object sender, EventArgs e)
        {
            if (fileManager.FileValidate(FileUpload1))
            {
                string nomeArquivo = "planilha_tatica";
                long tamanhoArquivo = fileManager.Length(FileUpload1);

                try
                {
                    if (UploadArquivo(nomeArquivo, tamanhoArquivo))
                    {
                        Ler(nomeArquivo);
                    }
                    else
                    {
                        lblmsg.Text = "Não foi possível realizar a importação do arquivo";
                    }
                }

                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                lblmsg.Text = "Por Favor, selecione um arquivo no formato .txt para enviar.";
            }
        }
    }
}