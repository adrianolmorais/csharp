using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace uploadAW
{
    public class FileManager : Page, IFileManager
    {
        public bool FileValidate(FileUpload FileUpload1)
        {
            return FileUpload1.HasFile && FileUpload1.PostedFile.ContentType == "text/plain";
        }

        public int Length(FileUpload FileUpload1)
        {
            return FileUpload1.PostedFile.ContentLength;
        }

        public StreamReader ler(string path)
        {
            return new StreamReader(Server.MapPath("~/planilhatatica/" + path));
        }

        public void mensagem(Label label, string mensagem)
        {
            label.Text = mensagem;
        }

        public string proximaLinha(StreamReader streamReader)
        {
            return streamReader.ReadLine();
        }

        public bool possuiRegistro(StreamReader streamReader)
        {
            return streamReader.Peek() >= 0;
        }

        public void saveAsArchive(FileUpload FileUpload1, string nomeArquivo)
        {
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/planilhatatica/" + nomeArquivo));
        }

    }
}