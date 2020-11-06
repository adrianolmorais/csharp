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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string[] caminhoArquivos = Directory.GetFiles(Server.MapPath("~/planilhatatica/"));
                var lista = new List<string>();

                foreach (string caminhoArquivo in caminhoArquivos)
                {
                    string nomeArquivo = Path.GetFileName(caminhoArquivo);
                    lista.Add(nomeArquivo);
                }

                //    gvArquivos.DataSource = lista;
                //    gvArquivos.DataBind();
            }
        }

        private bool UploadArquivo(string nomeArquivo, long tamanhoArquivo)
        {

            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/planilhatatica/" + nomeArquivo));
            lblmsg.Text = "Arquivo enviado com sucesso. " + "Tamanho do Arquivo = " + tamanhoArquivo.ToString() + "bytes.";

            return true;
        }

        private void Ler(string nomeArquivo)
        {
            List<string> lista = new List<string>();
            

            using (StreamReader sr = new StreamReader(Server.MapPath("~/planilhatatica/" + nomeArquivo)))
            {
                List<string> linhasAquivo = new List<string>();
                List<string[]> listRegistrosArquivo = new List<string[]>();
                string[] camposPlanilaTatica;
                HashSet<string> anosMesesVigenciaArquivo = new HashSet<string>();

                while (sr.Peek() >= 0) {
                    //string linha = sr.ReadLine();
                    linhasAquivo.Add(sr.ReadLine());
                }

                foreach (var linhaArquivo in linhasAquivo)
                {

                    camposPlanilaTatica = linhaArquivo.Split(';');

                    listRegistrosArquivo.Add(camposPlanilaTatica);

                    
                }

                anosMesesVigenciaArquivo = getAnoMesVigenciaArquivo(listRegistrosArquivo);

                deleteAnoMesVigenciaArquivo(anosMesesVigenciaArquivo);
                insertPlanilhaEligibilidade(listRegistrosArquivo);
                


               




                //if (cnpj.Length == 14 && funcional.Length == 9)
                //{
                //    var ano = anoMes.Substring(0, 4);
                //    var mes = anoMes.Substring(4, 2);


                //List<string> datasVigencia = GetPlanilhaTaticaAnoMes(cnpj);
                //List<string> Cnpj = GetPlanilhaTaticaAnoMes(cnpj);

                //foreach (var dataVigencia in datasVigencia)
                //{
                //    var ano = dataVigencia.Substring(1, 2);
                //    var mes = dataVigencia.Substring(0, 4);

                //    var anoMesAtual = ano + mes;

                //    if (anoMes.Equals(anoMesAtual))
                //    {
                //        deletebyAnoMes();
                //        inserir no banco
                //    }
                //}



                //}


                //txtData.Text = array[0];
                //txtIdade.Text = array[1];
                //txtValor.Text = array[2];
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


            }

        }

        private void deleteAnoMesVigenciaArquivo(HashSet<string> anosMesesVigenciaArquivo)
        {
            foreach ( string anoMesVigenciaArquivo in anosMesesVigenciaArquivo) {
                Console.WriteLine(anoMesVigenciaArquivo);
                //DELETE FROM dbo.TBAW013_CADA_RSPL_VISI WHERE DAT_RFRC_VIGE_CADA LIKE 'anoMesVigenciaArquivo%'
                //DELETE FROM dbo.TBAW013_CADA_RSPL_VISI WHERE DAT_RFRC_VIGE_CADA LIKE '2020-10-%'
                //DELETE FROM dbo.TBAW013_CADA_RSPL_VISI WHERE DAT_RFRC_VIGE_CADA IN (SELECT DAT_RFRC_VIGE_CADA FROM dbo.TBAW013_CADA_RSPL_VISI WHERE DAT_RFRC_VIGE_CADA LIKE '2020-10-%')

            }
        }

        private HashSet<string> getAnoMesVigenciaArquivo(List<string[]> listaArray)
        {
            HashSet<string> anoMesVigencia = new HashSet<string>();
            foreach (string[] array in listaArray) {
                var anoMes = array[2];
                var ano = anoMes.Substring(0, 4);
                var mes = anoMes.Substring(4, 2);

                anoMesVigencia.Add(ano + "-" + mes + "-");
            }
            return anoMesVigencia;
        }

        private List<string> GetPlanilhaTaticaAnoMes(string cnpj)
        {
            List<string> datasVigencia = new List<string>();
            datasVigencia.Add("2020-10-01");
            datasVigencia.Add("2020-10-01");
            datasVigencia.Add("2020-10-01");
            datasVigencia.Add("2020-10-01");
            datasVigencia.Add("2020-10-01");
            datasVigencia.Add("2020-10-01");
            datasVigencia.Add("2020-10-01");
            datasVigencia.Add("2020-10-02");
            datasVigencia.Add("2021-10-01");
            datasVigencia.Add("2020-11-01");
            return datasVigencia;
        }

        protected void btnEnviarArquivo_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && FileUpload1.PostedFile.ContentType == "text/plain")
            {
                //string nomeArquivo = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string nomeArquivo = "planilha_tatica";
                long tamanhoArquivo = FileUpload1.PostedFile.ContentLength;

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
                lblmsg.Text = "Por Favor, selecione um arquivo a enviar.";
            }
        }
    }
}